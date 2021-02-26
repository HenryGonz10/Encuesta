using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace encuestas.Models.Dinamico
{
    public class ModeloCompuesto
    {
        public List<entrevista> data { get; set; }
    }

    public class entrevista 
    { 
        public int id { get; set; }
        [Required]
        [Display(Name ="Título")]
        [StringLength(45)]
        public string titulo { get; set; }
        [Display(Name ="Descripción")]
        [StringLength(500)]
        public string desc { get; set; }
        [Display(Name = "Fecha")]
        public DateTime? fecha { get; set; }
        public List<detalle> detalles { get; set; }
    }

    public class detalle
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public bool requerido { get; set; }
        public int tipo { get; set; }
    }
}