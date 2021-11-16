using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Data;


namespace Intranet.ModelsApp
{
    public class ResumenVolados
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public ResumenVolados(int year, int month)
        {

            this.Año = year;
            this.Mes = month;

            List<DetalleVoladoRuta> volados_Results = db.ResumenVolados(year, month)
                .Select (s => new DetalleVoladoRuta
                {
                  Origen=s.Origen,
                  Destino=s.Destino,
                  Frecuencia=s.Frecuencia.Value,
                  AsientosOfrecidos=s.Total.Value,
                  PasajerosRevenue=s.RevenuePax.Value,
                  YQ=s.YQ.Value + s.Revenue.Value,                  
                  AVG = (s.YQ.Value + s.Revenue.Value) / s.RevenuePax.Value,
                  LF= (s.RevenuePax.Value * 100)/s.Total.Value,
                   DescripcionRuta= s.DescripcionRuta

                }).ToList();

            this.DetalleRutas = volados_Results;

            this.Rutas = volados_Results.Select(s => s.DescripcionRuta).Distinct().ToList();

            if (volados_Results.Count!=0)
            {
                this.FechaUltimaVolados = db.VOLADOS.Where(w => w.FechaReporte.Value.Month == month
                && w.FechaReporte.Value.Year == year).Max(m => m.FechaReporte);
            }
            
           
        }


        public int Mes { get; set; }
        public int Año { get; set; }

        public List<string> Rutas { get; set; }

        public List<DetalleVoladoRuta> DetalleRutas { get; set; }
        
        public DateTime? FechaUltimaVolados { get; set; }

        public class DetalleVoladoRuta {

            public string DescripcionRuta { get; set; }
            public string Origen { get; set; }
            public string Destino { get; set; }
            public int Frecuencia { get; set; }
            public int AsientosOfrecidos { get; set; }

            public int PasajerosRevenue { get; set; }

            public Decimal YQ { get; set; }

            public Decimal AVG { get; set; }

            public Decimal LF { get; set; }


        }
    }
}