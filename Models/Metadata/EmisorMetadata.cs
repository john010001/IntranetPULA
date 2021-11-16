using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Intranet.Models.Metadata
{
    public class EmisorMetadata
    {
        public int IdEmisor { get; set; }
        
        [Display(Name = "Descripción")]
        [MaxLength(100)]
        [Required]
        public string Descripcion { get; set; }

        [Display(Name = "Canal")]        
        [Required]
        public int IdTipoEmisor { get; set; }
        public Nullable<bool> Mostrar { get; set; }

        
    }
}