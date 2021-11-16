using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Intranet.Models.Metadata
{
    public class RutaMercadoMetadata
    {
        public int IdRutaMercado { get; set; }
        [Display(Name = "Ruta")]
        [Required]
        public int IdRuta { get; set; }
        [Display(Name = "Tramo ruta mercado")]
        [Required]
        public string TramoRutaMercado { get; set; }
    }
}