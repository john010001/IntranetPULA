using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Intranet.Models.Data;

namespace Intranet.ModelsApp
{
    public class OcupacionVuelosRegulares
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public OcupacionVuelosRegulares(string origen, string destino)
        {
            
            List<OcupacionMensualUltima_Result> mensual_Result = db.OcupacionMensualUltima(origen, destino).ToList();

            List<OcupacionMensualPenultima_Result> mensual_Penultima_Result = db.OcupacionMensualPenultima(origen, destino).ToList();

            this.FechaCarga = mensual_Result.FirstOrDefault().FechaCarga.Value;
            this.Origen = origen;
            this.Destino = destino;
            
            this.FechaCargaAnterior= mensual_Penultima_Result.FirstOrDefault().FechaCarga.Value;


            List<OcupacionMensual> _listaocupacionMensual = new List<OcupacionMensual>();
            OcupacionMensual _ocupacionMensual = new OcupacionMensual();
            
            foreach (var item in mensual_Result)
            {
                _ocupacionMensual = new OcupacionMensual();
                _ocupacionMensual.Año = item.Anio.Value;
                _ocupacionMensual.Mes = item.Mes.Value;
                _ocupacionMensual.MesCadena = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Mes.Value)) + ' '+ _ocupacionMensual.Año.ToString(); 
                _ocupacionMensual.Ofertados = item.Ofertados.Value;
                _ocupacionMensual.Reservados = item.Reservados.Value;
                _ocupacionMensual.Disponibles = _ocupacionMensual.Ofertados - _ocupacionMensual.Reservados;
                _ocupacionMensual.LoadFactor = Math.Round(_ocupacionMensual.Reservados / _ocupacionMensual.Ofertados,2)*100;

                if (mensual_Penultima_Result.Where(w => w.Anio == item.Anio.Value && w.Mes == item.Mes.Value).Count() != 0)
                {
                    _ocupacionMensual.Diferencia = _ocupacionMensual.Reservados - mensual_Penultima_Result.
                                                    Where(w => w.Anio == item.Anio.Value && w.Mes == item.Mes.Value).FirstOrDefault().Reservados.Value;

                    _ocupacionMensual.ReservadosAnterior = mensual_Penultima_Result.
                                                            Where(w => w.Anio == item.Anio.Value && w.Mes == item.Mes.Value)
                                                            .FirstOrDefault().Reservados.Value;
                }
                else {
                    _ocupacionMensual.Diferencia = _ocupacionMensual.Reservados;
                    _ocupacionMensual.ReservadosAnterior = 0;

                }


                _listaocupacionMensual.Add(_ocupacionMensual);
                _ocupacionMensual = new OcupacionMensual();
            }
            this.Ocupacion = _listaocupacionMensual;

            List<OcupacionDiaria> diariaUltima_Results = db.OcupacionDiariaUltima(origen, destino)
                .Select(s => new OcupacionDiaria
                {
                    DiaSemana = s.DiaSemana,
                    Fecha = s.fecha,
                    CabinaC = s.C,
                    CabinaY = s.Y,
                    TotalPax = s.Y + s.C,
                    Ofertado = s.Ofertados

                }).ToList();
            this.OcupacionDiariaActual = diariaUltima_Results;


            List<OcupacionDiaria> diariaPeUltima_Results = db.OcupacionDiariaPenultima(origen, destino)
                .Select(s => new OcupacionDiaria
                {
                    DiaSemana = s.DiaSemana,
                    Fecha = s.fecha,
                    CabinaC = s.C,
                    CabinaY = s.Y,
                    TotalPax = s.Y + s.C,
                    Ofertado = s.Ofertados
                   
                }).ToList();
            this.OcupacionDiariaAnterior = diariaPeUltima_Results;






        }
        public DateTime FechaCarga { get; set; }
        public string  Origen { get; set; }
        public string Destino { get; set; }
        public List<OcupacionMensual> Ocupacion { get; set; }

        public DateTime FechaCargaAnterior { get; set; }

        public List<OcupacionDiaria> OcupacionDiariaActual { get; set; }
        public List<OcupacionDiaria> OcupacionDiariaAnterior { get; set; }




        public class OcupacionMensual
        {
            public int Año { get; set; }
            public int Mes { get; set; }

            public string  MesCadena { get; set; }

            public double Ofertados { get; set; }
            public double Reservados { get; set; }

            public double Disponibles  { get; set; }

            public double LoadFactor { get; set; }


            public double ReservadosAnterior { get; set; }
            public double Diferencia { get; set; }
        }

        public class OcupacionDiaria
        {
            public string DiaSemana { get; set; }
            public DateTime Fecha { get; set; }

            public int  CabinaC { get; set; }

            public int CabinaY { get; set; }
            public double TotalPax { get; set; }

            public double Ofertado { get; set; }

            public double LoadFactor { get; set; }
            
            public double Diferencia { get; set; }
        }



    }
}