using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Data;

namespace Intranet.ModelsApp
{
    public class ResumenCanalDocumento
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public ResumenCanalDocumento(int year, int month)
        {
            List<VentaCanalDiarioDocumento_Result>   ventaCanalDiarioDocumentos = db.VentaCanalDiarioDocumento(year, month,"TKTT","","","").
                OrderBy(o=>o.Directa).OrderBy(o => o.Canal).ToList();

            List<DateTime> DiasFecha = new List<DateTime>();

            int _diasMes = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Day;

            for (int i = 0; i < _diasMes; i++)
            {               
                DiasFecha.Add(new DateTime(year, month, i + 1));
                
            }
            this.Fecha = DiasFecha;

            #region TKTT
            var CanalDetalle = ventaCanalDiarioDocumentos.OrderBy(o => o.Directa).OrderBy(o => o.Canal)
            .Select(s => new VentaDetalle
            {
                Id = s.IdTipoEmisor.Value,
                Canal = s.Canal,
                Fecha = s.Fecha.Value,
                //Revenue_YQ = Math.Round(s.Revenue + s.YQ.Value ,2),
                //** Revenue ya contiene YQ **
                Revenue_YQ = Math.Round(s.Revenue , 2),
                Cupones = s.Cupones.Value,
                Transacciones = s.Transacciones.Value
            }).ToList();

            this.Venta_TKT = CanalDetalle;
            #endregion

            #region EMDA, EXCH                        
            ventaCanalDiarioDocumentos = db.VentaCanalDiarioDocumento(year, month, "EMDA", "EXCH", "", "").
                    OrderBy(o => o.Directa).OrderBy(o => o.Canal).ToList();
                CanalDetalle = ventaCanalDiarioDocumentos.OrderBy(o => o.Directa).OrderBy(o => o.Canal)
                .Select(s => new VentaDetalle
                {
                    Id = s.IdTipoEmisor.Value,
                    Canal = s.Canal,
                    Fecha = s.Fecha.Value,
                    //Revenue_YQ = Math.Round(s.Revenue + s.YQ.Value, 2),
                    Revenue_YQ = Math.Round(s.Revenue , 2),
                    Cupones = s.Cupones.Value,
                    Transacciones=s.Transacciones.Value
                }).ToList();

                this.Venta_EMDA_EXCH = CanalDetalle;
            #endregion 

            #region RFND                        
            ventaCanalDiarioDocumentos = db.VentaCanalDiarioDocumento(year, month, "RFND", "", "", "").
                    OrderBy(o => o.Directa).OrderBy(o => o.Canal).ToList();
            CanalDetalle = ventaCanalDiarioDocumentos.OrderBy(o => o.Directa).OrderBy(o => o.Canal)
            .Select(s => new VentaDetalle
            {
                Id = s.IdTipoEmisor.Value,
                Canal = s.Canal,
                Fecha = s.Fecha.Value,
                //Revenue_YQ = Math.Round(s.Revenue + s.YQ.Value, 2),
                Revenue_YQ = Math.Round(s.Revenue , 2),
                Cupones = s.Cupones.Value,
                Transacciones = s.Transacciones.Value
            }).ToList();

            this.Venta_RFND = CanalDetalle;
            #endregion 

        }
        public List<DateTime> Fecha { get; set; }
        public List<VentaDetalle> Venta_TKT { get; set; }       
        public List<VentaDetalle> Venta_EMDA_EXCH { get; set; }
        public List<VentaDetalle> Venta_RFND { get; set; }
        
        public class VentaDetalle
        {
            public int Id { get; set; }

            public string Canal { get; set; }
            public DateTime Fecha { get; set; }
            
            public DateTime Cantidad { get; set; }
            public Decimal Revenue_YQ { get; set; }
            public double Cupones { get; set; }
            public double Transacciones { get; set; }



        }


    }
}