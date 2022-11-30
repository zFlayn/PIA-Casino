﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiCasino.DTOs;

namespace WebApiCasino.Entidades
{
    public class Carta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CartaId { get; set; }
        public string Nombre { get; set; }
    }
}
