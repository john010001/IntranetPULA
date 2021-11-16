using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Intranet.Models.Metadata
{
    public class RutaMetadata
    {
        [Display(Name = "Ruta")]
        
        public int IdRuta { get; set; }

        [Display(Name = "Codigo de Ruta")]
        [Required]
        public string CodigoRuta { get; set; }
        [Display(Name = "Descripción Ruta 2")]
        [MaxLength(5)]
        [Required]
        public string DescripcionRuta { get; set; }
    }
}