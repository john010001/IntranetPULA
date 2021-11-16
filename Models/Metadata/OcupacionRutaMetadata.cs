using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Metadata
{
    public class OcupacionRutaMetadata
    {
        [Display(Name = "Ocupacion Ruta")]
        public int IdOcupacionRuta { get; set; }
        [Display(Name = "Descripción")]
        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }
        [Display(Name = "Descripción Larga")]
        [MaxLength(100)]
        [Required]
        public string DescripcionLarga { get; set; }
    }
}