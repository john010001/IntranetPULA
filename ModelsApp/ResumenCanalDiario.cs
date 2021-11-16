using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Data;

namespace Intranet.ModelsApp
{
    public class ResumenCanalDiario
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public ResumenCanalDiario(int year, int month)
        {
            List<VentaCanalDiario_Result>   ventaCanalDiario_Result = db.VentaCanalDiario(year, month).
                OrderBy(o=>o.Directa).ToList();


            
            List<DateTime> DiasFecha = new List<DateTime>();

            int _diasMes = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Day;

            for (int i = 0; i < _diasMes; i++)
            {               
                DiasFecha.Add(new DateTime(year, month, i + 1));
                
            }
            this.Fecha = DiasFecha;

            VentaDetalle TipoVenta = new VentaDetalle();
            List<VentaDetalle> ListaTipoVenta = new List<VentaDetalle>();


            var CanalGeneral = ventaCanalDiario_Result.GroupBy(g => new { g.Directa, g.Fecha })
            .Select(s => new VentaDetalle
            {
                Id= s.Key.Directa,
                Canal= (s.Key.Directa == 1) ? "Venta Directa" : "Venta Indirecta",
                Fecha =s.Key.Fecha.Value,
                //Revenue_YQ = Math.Round(s.Sum(r => r.Revenue) + s.Sum(r => r.YQ).Value + s.Sum(r => r.YR).Value, 2), 
                //*** Revenue ya contiene YQ
                Revenue_YQ = Math.Round(s.Sum(r=>r.Revenue) + s.Sum(r => r.YR).Value,2),
                //Tax_sin_YQ = Math.Round(s.Sum(r => r.Tax).Value - s.Sum(r => r.YQ).Value, 2),
                Tax_sin_YQ = Math.Round(s.Sum(r => r.Tax).Value ,2)
            }).ToList();

            this.VentaGeneral = CanalGeneral;


            var CanalDetalle = ventaCanalDiario_Result.OrderBy(o=>o.Directa)
            .Select(s => new VentaDetalle
            {
                Id = s.IdTipoEmisor.Value,
                Canal = s.Canal,
                Fecha = s.Fecha.Value,
                //Revenue_YQ = Math.Round(s.Revenue + s.YQ.Value + s.YR.Value,2),
                Revenue_YQ = s.Revenue + s.YR.Value,
                //Tax_sin_YQ = Math.Round(s.Tax.Value - s.YQ.Value, 2)
                Tax_sin_YQ = s.Tax.Value 
            }).ToList();

            this.VentaCanal = CanalDetalle;






        }
        public List<DateTime> Fecha { get; set; }
        public List<VentaDetalle> VentaGeneral { get; set; }       
        public List<VentaDetalle> VentaCanal { get; set; }

        public class VentaDetalle
        {
            public int Id { get; set; }

            public string Canal { get; set; }
            public DateTime Fecha { get; set; }
            public Decimal Revenue_YQ { get; set; }
            public Decimal Tax_sin_YQ { get; set; }
            
            

        }
        public class VentaDiaria
        {
            public DateTime Fecha { get; set; }
            public Decimal Revenue_YQ { get; set; }
            public Decimal Tax_sin_YQ { get; set; }

        }

    }
}