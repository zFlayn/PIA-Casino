using WebApiCasino.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.Validaciones;
using WebApiCasino.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCasino.Entidades
{
    public class Premio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ValidarLugar]
        [Range(+1.0,+5.0,ErrorMessage ="Rango error")]
        public int Lugar { get; set; }

        [MinLength(3, ErrorMessage = "Length error")]
        public string Recompensa { get; set; }

        public virtual ICollection<Rifa> Rifas { get; set; }

    }

    //añadir con los DTOS basandote en la tarea
}
