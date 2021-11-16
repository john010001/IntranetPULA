using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Models.Metadata
{
    public class TipoEmisorMetadata
    {
        [Display(Name = "Canal")]
        [MaxLength(45)]
        [Required]
        public string Descripcion { get; set; }
        [Display(Name = "Descripción KIU")]
        [MaxLength(45)]
        [Required]
        public string DescripcionEquivalente { get; set; }
        [Display(Name = "Venta directa")]
        [Required]
        public Nullable<bool> Directa { get; set; }
    }
}