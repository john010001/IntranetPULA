using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Models.Metadata
{
    public class OcupacionSegmentoMetadata
    {
        public int IdOcupacionSegmentos { get; set; }
        [Display(Name = "Ruta")]
        public int IdOcupacionRuta { get; set; }
        [Required]
        [MaxLength(3)]
        public string Origen { get; set; }
        [Required]
        [MaxLength(3)]
        public string Destino { get; set; }
        [Required]
        public Nullable<int> Tramo { get; set; }
    }
}