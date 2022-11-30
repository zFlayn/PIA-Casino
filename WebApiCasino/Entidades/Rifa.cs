﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCasino.Entidades
{
    public class Rifa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreationDate { get; set; }

        //premios y carta
        public virtual ICollection<Premio> Premios { get; set; }
        public virtual ICollection<RifaParticipante> RifaParticipantes { get; set; }
    }
}
