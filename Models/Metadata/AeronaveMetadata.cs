using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Metadata
{
    public class AeronaveMetadata
    {


        [Display(Name = "Matricula")]
        [MaxLength(8)]
        [Required]
        public string Matricula { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(4)]
        [Required]
        public string Tipo { get; set; }

        [Display(Name = "Cabina C")]
        [Required]
        public Nullable<int> CabinaC { get; set; }

        [Display(Name = "Cabina Y")]
        [Required]
        public Nullable<int> CabinaY { get; set; }

        public Nullable<int> Total { get; set; }
    }
}