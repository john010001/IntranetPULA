using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Models.Metadata
{
    public class VoladosMetaMetadata
    {
        public int IdVoladoMeta { get; set; }

        [Display(Name = "Volados Ruta")]
        [Required]
        public int IdVoladoRuta { get; set; }
        [Required]
        public int Año { get; set; }
        [Required]
        public int Mes { get; set; }
        [Display(Name = "Meta")]
        [Required]
        public decimal Meta { get; set; }
    }
}