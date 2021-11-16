using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Data;

namespace Intranet.ModelsApp
{
    public class ResumenOcupacion
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public ResumenOcupacion(int year, int month)
        {

            this.Año = year;
            this.Mes = month;

            List<OcupacionDiaria> ocupaciones_Result = db.ResumenOcupacion(year, month)
                    .Select(s => new OcupacionDiaria
                    {
                        Descripcion = s.Descripcion,
                        FechaCarga = s.Fechacarga.Value,
                        Fecha = s.Fecha,
                        OfertaIda = s.OfertadosIda,
                        ReservaIda = s.ReservadosIda,



                        LoadFactorIda = s.OfertadosIda == null ?0:Math.Round((double)s.ReservadosIda.Value / s.OfertadosIda.Value, 2) * 100,

                        OfertaVuelta = s.OfertadosVuelta,
                        ReservaVuelta = s.ReservadosVuelta,
                        LoadFactorVuelta= s.OfertadosVuelta == null ? 0 : Math.Round((double)s.ReservadosVuelta.Value / s.OfertadosVuelta.Value, 2) * 100,
                    }).ToList();

            this.OcupacionesDiarias = ocupaciones_Result;

            if (ocupaciones_Result.Count()!=0)
            {
                this.FechaCarga = ocupaciones_Result.FirstOrDefault().FechaCarga;
            }
            else
            {
                this.FechaCarga = db.OCUPACION_ARCHIVO.OrderByDescending(o => o.FechaCarga).FirstOrDefault().FechaCarga;
            }
            this.Rutas = ocupaciones_Result.Select(s => s.Descripcion).Distinct().ToList();

        }

        public int Mes { get; set; }
        public int Año { get; set; }

        public DateTime FechaCarga { get; set; }
        public List<OcupacionDiaria> OcupacionesDiarias { get; set; }

        public List<string> Rutas { get; set; }

        public string RutaDescripcion(string ruta, int tramo)
        {

            string _rutaDescripcion = "";

            var busca = db.OCUPACION_RUTA.Where(w => w.Descripcion == ruta).FirstOrDefault().
                OCUPACION_SEGMENTO.Where(os => os.Tramo == tramo).ToList();


            if (busca != null)
            {
                foreach (var item in busca)
                {
                    _rutaDescripcion += item.Origen + "-";

                    _rutaDescripcion += item.Destino + " ";
                }

            }
            return _rutaDescripcion.Trim();
        }
        public string NombreRuta(string ruta)
        {

            string _rutaDescripcion = "";

            var busca = db.OCUPACION_RUTA.Where(w => w.Descripcion == ruta).ToList();
                


            if (busca != null)
            {
                _rutaDescripcion = busca.FirstOrDefault().DescripcionLarga;
            }
            return _rutaDescripcion.Trim();
        }


        public class OcupacionDiaria
        {
            public string Descripcion { get; set; }
            public DateTime FechaCarga { get; set; }
            public DateTime Fecha { get; set; }
            public int? OfertaIda { get; set; }
            public int? ReservaIda { get; set; }

            public double? LoadFactorIda { get; set; }
            public int? OfertaVuelta { get; set; }

            public int? ReservaVuelta { get; set; }

            public double? LoadFactorVuelta{ get; set; }
        }

        public class Rutasx {
            public string Descripcion { get; set; }
            public string RutaIda { get; set; }
            public string RutaVuelta { get; set; }
        }

    }
}