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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Usuario_Entities : DbContext
    {
        public Usuario_Entities()
            : base("name=Usuario_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MENU> MENU { get; set; }
        public virtual DbSet<MENU_ROL> MENU_ROL { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<USUARIO_ROL> USUARIO_ROL { get; set; }
    }
}
