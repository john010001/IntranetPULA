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
    
    public partial class AERONAVES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AERONAVES()
        {
            this.VOLADOS = new HashSet<VOLADOS>();
        }
    
        public string Matricula { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> CabinaC { get; set; }
        public Nullable<int> CabinaY { get; set; }
        public Nullable<int> Total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOLADOS> VOLADOS { get; set; }
    }
}