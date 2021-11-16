﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IntranetDBEntities : DbContext
    {
        public IntranetDBEntities()
            : base("name=IntranetDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EMISOR> EMISOR { get; set; }
        public virtual DbSet<TIPOEMISOR> TIPOEMISOR { get; set; }
        public virtual DbSet<VentasKiuAdminFile> VentasKiuAdminFile { get; set; }
        public virtual DbSet<META_VENTA> META_VENTA { get; set; }
        public virtual DbSet<TMPVentasKiuAdmin> TMPVentasKiuAdmin { get; set; }
        public virtual DbSet<VentasKiuAdmin> VentasKiuAdmin { get; set; }
        public virtual DbSet<RUTA> RUTA { get; set; }
        public virtual DbSet<RUTA_MERCADO> RUTA_MERCADO { get; set; }
        public virtual DbSet<OCUPACION_ARCHIVO> OCUPACION_ARCHIVO { get; set; }
        public virtual DbSet<OCUPACION> OCUPACION { get; set; }
        public virtual DbSet<VoladosKiuAdminfile> VoladosKiuAdminfile { get; set; }
        public virtual DbSet<OCUPACION_RUTA> OCUPACION_RUTA { get; set; }
        public virtual DbSet<OCUPACION_SEGMENTO> OCUPACION_SEGMENTO { get; set; }
        public virtual DbSet<AERONAVES> AERONAVES { get; set; }
        public virtual DbSet<VOLADOS> VOLADOS { get; set; }
        public virtual DbSet<OCUPACION_META> OCUPACION_META { get; set; }
        public virtual DbSet<VOLADOS_META> VOLADOS_META { get; set; }
        public virtual DbSet<VOLADOS_RUTA> VOLADOS_RUTA { get; set; }
        public virtual DbSet<VOLADOS_SEGMENTO> VOLADOS_SEGMENTO { get; set; }
    
        public virtual ObjectResult<ValidarDuplicadosVentas_Result> ValidarDuplicadosVentas(string archivo)
        {
            var archivoParameter = archivo != null ?
                new ObjectParameter("archivo", archivo) :
                new ObjectParameter("archivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ValidarDuplicadosVentas_Result>("ValidarDuplicadosVentas", archivoParameter);
        }
    
        public virtual ObjectResult<VentaCanal_Result> VentaCanal(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaCanal_Result>("VentaCanal", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<VentaDiaria_Result> VentaDiaria(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaDiaria_Result>("VentaDiaria", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<VentaTotalDocumento_Result> VentaTotalDocumento(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaTotalDocumento_Result>("VentaTotalDocumento", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<VentaCanalDiario_Result> VentaCanalDiario(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaCanalDiario_Result>("VentaCanalDiario", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<VentaCanalDetalle_Result> VentaCanalDetalle(Nullable<int> anio, Nullable<int> mes, string canal)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            var canalParameter = canal != null ?
                new ObjectParameter("canal", canal) :
                new ObjectParameter("canal", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaCanalDetalle_Result>("VentaCanalDetalle", anioParameter, mesParameter, canalParameter);
        }
    
        public virtual ObjectResult<VentaCanalDiarioDocumento_Result> VentaCanalDiarioDocumento(Nullable<int> anio, Nullable<int> mes, string tipoDoc1, string tipoDoc2, string tipoDoc3, string tipoDoc4)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            var tipoDoc1Parameter = tipoDoc1 != null ?
                new ObjectParameter("tipoDoc1", tipoDoc1) :
                new ObjectParameter("tipoDoc1", typeof(string));
    
            var tipoDoc2Parameter = tipoDoc2 != null ?
                new ObjectParameter("tipoDoc2", tipoDoc2) :
                new ObjectParameter("tipoDoc2", typeof(string));
    
            var tipoDoc3Parameter = tipoDoc3 != null ?
                new ObjectParameter("tipoDoc3", tipoDoc3) :
                new ObjectParameter("tipoDoc3", typeof(string));
    
            var tipoDoc4Parameter = tipoDoc4 != null ?
                new ObjectParameter("tipoDoc4", tipoDoc4) :
                new ObjectParameter("tipoDoc4", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaCanalDiarioDocumento_Result>("VentaCanalDiarioDocumento", anioParameter, mesParameter, tipoDoc1Parameter, tipoDoc2Parameter, tipoDoc3Parameter, tipoDoc4Parameter);
        }
    
        public virtual ObjectResult<VentaTotalDocumentoRFND_Result> VentaTotalDocumentoRFND(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaTotalDocumentoRFND_Result>("VentaTotalDocumentoRFND", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<VentaTotalDocumentoMercado_Result> VentaTotalDocumentoMercado(Nullable<int> anio, Nullable<int> mes, Nullable<int> idRuta)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            var idRutaParameter = idRuta.HasValue ?
                new ObjectParameter("idRuta", idRuta) :
                new ObjectParameter("idRuta", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaTotalDocumentoMercado_Result>("VentaTotalDocumentoMercado", anioParameter, mesParameter, idRutaParameter);
        }
    
        public virtual ObjectResult<OcupacionMensualPenultima_Result> OcupacionMensualPenultima(string origen, string destino)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var destinoParameter = destino != null ?
                new ObjectParameter("Destino", destino) :
                new ObjectParameter("Destino", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OcupacionMensualPenultima_Result>("OcupacionMensualPenultima", origenParameter, destinoParameter);
        }
    
        public virtual ObjectResult<OcupacionMensualUltima_Result> OcupacionMensualUltima(string origen, string destino)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var destinoParameter = destino != null ?
                new ObjectParameter("Destino", destino) :
                new ObjectParameter("Destino", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OcupacionMensualUltima_Result>("OcupacionMensualUltima", origenParameter, destinoParameter);
        }
    
        public virtual ObjectResult<VentaTotalDocumento1_Result> VentaTotalDocumento1(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaTotalDocumento1_Result>("VentaTotalDocumento1", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<ResumenOcupacion_Result> ResumenOcupacion(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ResumenOcupacion_Result>("ResumenOcupacion", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<ResumenVolados_Result> ResumenVolados(Nullable<int> anio, Nullable<int> mes)
        {
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var mesParameter = mes.HasValue ?
                new ObjectParameter("mes", mes) :
                new ObjectParameter("mes", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ResumenVolados_Result>("ResumenVolados", anioParameter, mesParameter);
        }
    
        public virtual ObjectResult<OcupacionDiariaPenultima_Result> OcupacionDiariaPenultima(string origen, string destino)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var destinoParameter = destino != null ?
                new ObjectParameter("Destino", destino) :
                new ObjectParameter("Destino", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OcupacionDiariaPenultima_Result>("OcupacionDiariaPenultima", origenParameter, destinoParameter);
        }
    
        public virtual ObjectResult<OcupacionDiariaUltima_Result> OcupacionDiariaUltima(string origen, string destino)
        {
            var origenParameter = origen != null ?
                new ObjectParameter("Origen", origen) :
                new ObjectParameter("Origen", typeof(string));
    
            var destinoParameter = destino != null ?
                new ObjectParameter("Destino", destino) :
                new ObjectParameter("Destino", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OcupacionDiariaUltima_Result>("OcupacionDiariaUltima", origenParameter, destinoParameter);
        }
    }
}