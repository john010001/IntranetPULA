using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Metadata
{
    public class MenuMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required]
        public string Descripcion { get; set; }

        [Display(Name = "Nivel")]
        [Required]
        public string Nivel { get; set; }

        [Display(Name = "URL Destino")]
        [Required]
        public string Url { get; set; }

        [Display(Name = "Principal")]
        [Required]
        public Nullable<int> Principal { get; set; }

        [Display(Name = "URL Imagen")]
        public string Imagen { get; set; }

        //public override bool Equals(object obj)
        //{
        //    return ((MenuMetadata)obj).Id_menu == Id_menu;
        //    // or: 
        //    // var o = (Person)obj;
        //    // return o.Id == Id && o.Name == Name;
        //}
        //public override int GetHashCode()
        //{
        //    return Id_menu.GetHashCode();
        //}

    }
}