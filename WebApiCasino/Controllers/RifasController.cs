using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApiCasino.DTOs;
using WebApiCasino.Entidades;

namespace WebApiCasino.Controllers
{
    [ApiController]
    [Route("/rifas")]
    public class RifasController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ILogger<RifasController> logger;
        public RifasController(ApplicationDbContext dbContext, IMapper mapper,ILogger<RifasController>logger)
        {
            //Inyeccion de dependecias
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public ApplicationDbContext dbContext { get; }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<IActionResult> Post([FromBody] RifaDTO objRifa)
        {
            var veifircarNombreRifa = await dbContext.Rifas.AnyAsync(x => x.Nombre == objRifa.Nombre);
            if (veifircarNombreRifa)
            {
                return BadRequest("Ya existe");
            }
            var rifaAux = mapper.Map<Rifa>(objRifa);
            rifaAux.CreationDate = DateTime.Now;
            dbContext.Add(rifaAux);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<List<Rifa>>> Get()
        {
            logger.LogInformation("Obteneniendo las Rifas");
            var rifas = dbContext.Rifas.ToList();
            return rifas;
        }

        [HttpGet("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<Rifa>> Get(int id)
        {
            var aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == id);
            if (aux == null)
            {
                logger.LogWarning($"El autor de Id{id}no ha sido encontrado");
                return NotFound();
            }
            return aux;
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Delete(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult<GetRifaDTO>> Patch(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Update(new Rifa()
            {
                Id = id,
                Nombre = objRifa.Nombre
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<IActionResult> Put(int id, [FromBody] RifaDTO objRifa)
        {
            var existe = await dbContext.Rifas.AnyAsync(a => a.Id == id);
            if (!existe)
            {
                return NotFound("El recurso no fue encontrado");
            }
            dbContext.Rifas.Update(new Rifa()
            {
                Id = id,
                Nombre = objRifa.Nombre
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Rifa>> Search([FromBody] BuscarRifaDTO buscarRifaDTO)
        {
            var aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            if (buscarRifaDTO.Id != 0)
            {
                aux = await dbContext.Rifas.FirstOrDefaultAsync(db => db.Id == buscarRifaDTO.Id);
            } else
            {
                aux = dbContext.Rifas.FirstOrDefault(db => db.Nombre == buscarRifaDTO.Nombre);
            }
            return aux;
        }

    }
}
