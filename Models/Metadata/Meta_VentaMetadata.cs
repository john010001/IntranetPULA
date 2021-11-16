using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Metadata
{
    public class Meta_VentaMetadata
    {
        public int IdMetaVenta { get; set; }
        public int Mes { get; set; }

        [Display(Name = "Año")]
        [Required]
        public int Anio { get; set; }

        [Display(Name = "Canal")]
        [Required]
        public int idTipoEmisor { get; set; }
        public decimal Monto { get; set; }

        

    }
}