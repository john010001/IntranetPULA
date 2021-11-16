using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Models.Metadata
{
    public class OcupacionMetaMetadata
    {
        public int IdOcupacionMeta { get; set; }

        [Display(Name = "Ocupacion Ruta")]
        [Required]
        public int IdOcupacionRuta { get; set; }
        [Required]
        public int Año { get; set; }
        [Required]
        public int Mes { get; set; }
        [Display(Name = "Meta")]
        [Required]
        public decimal Meta { get; set; }
    }
}