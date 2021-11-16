using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Metadata
{
    public class Usuario_RolMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Correo")]
        [Required]
        public string Correo { get; set; }

        [Display(Name = "Rol")]
        [Required]
        public int IdRol { get; set; }
        [Display(Name ="Estado")]
        [Required]
        public Boolean Estado { get; set; }




    }
}