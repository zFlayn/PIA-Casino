using WebApiCasino.Entidades;

namespace WebApiCasino.DTOs
{
    public class GetRifaDTO
    {
        //public int Id { get; set; }
        public string Nombre { get; set; }
        //premios y cartas

        public List<PremioDTO> Premios { get; set; }
        public List<CartaDTO> Carta { get; set; }
        public List <ParticipantesDTO> Participantes { get; set; }
        //public virtual ICollection<Premio> Premios { get; set; }
        //public virtual ICollection<Carta> Cartas { get; set; }
        //public virtual ICollection<Participante> Participantes { get; set; }
    }
}
