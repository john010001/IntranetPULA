//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Intranet.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class VOLADOS_META
    {
        public int IdVoladoMeta { get; set; }
        public int IdVoladoRuta { get; set; }
        public int Año { get; set; }
        public int Mes { get; set; }
        public decimal Meta { get; set; }
    
        public virtual VOLADOS_RUTA VOLADOS_RUTA { get; set; }
    }
}
