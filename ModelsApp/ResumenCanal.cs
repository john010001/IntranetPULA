using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Data;

namespace Intranet.ModelsApp
{
    public class ResumenCanal
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public ResumenCanal(int year, int month)
        {
            List<VentaCanal_Result>   ventaCanal_Result = db.VentaCanal(year, month).OrderByDescending(o=>o.Revenue).ToList();


            this.Año = year;
            this.Mes = month;


            Canal canal = new Canal ();
            List<Canal> canals = new List<Canal>() ;
            foreach (var item in ventaCanal_Result)
            {
                canal.Descripcion = item.Canal;
                canal.Revenue = item.Revenue;
                canal.Meta = item.Monto;
                canal.Directa = item.Directa;

                canals.Add(canal);
                canal = new Canal();
            }

            this.PorCanal = canals;
            this.TotalVenta = ventaCanal_Result.Sum(s => s.Revenue);
            //^*************TOTAL META************************//
            //this.TotalMeta = ventaCanal_Result.Sum(s => s.Monto);

            try
            {
                this.TotalMeta = db.META_VENTA.Where(w => w.Mes == month && w.Anio == year).Sum(s => s.Monto);
            }
            catch (Exception)
            {

                this.TotalMeta = 0;
            }

            this.TotalMetaPorc = this.TotalMeta == 0 ? 0 : Math.Round(this.TotalVenta * 100, 2) / this.TotalMeta;
            //************************************************//
            this.VentaDirecta = ventaCanal_Result.Where(w => w.Directa == 1).Sum(s => s.Revenue);
            this.VentaIndirecta = ventaCanal_Result.Where(w => w.Directa == 0).Sum(s => s.Revenue);

            this.VentaDirectaPorc= this.VentaDirecta == 0 ? 0: Math.Round(this.VentaDirecta / this.TotalVenta, 2) * 100;
            this.VentaIndirectaPorc = this.VentaIndirecta == 0 ? 0 : Math.Round(this.VentaIndirecta / this.TotalVenta, 2) * 100;

            this.ListaDirectas = ventaCanal_Result.Where(x => x.Directa == 1).ToList();
            this.PorCanalDirecta = canals.Where(x => x.Directa == 1).ToList();

            this.ListaIndirectas = ventaCanal_Result.Where(x => x.Directa == 0).ToList();
            this.PorCanalIndirecta = canals.Where(x => x.Directa == 0).ToList();

            List<VentaDiaria_Result> ventaDiaria_Result = db.VentaDiaria(year, month).OrderBy(o => o.Fecha).ToList();
            VentaDiaria ventaDiaria = new VentaDiaria();
            List<VentaDiaria> ventasDiaria = new List<VentaDiaria>();

            //^*************TOTAL META************************//
            //var _metaVentaDiaria = ventaCanal_Result.Sum(s => s.Monto);
            var _metaVentaDiaria = this.TotalMeta;
            //^*************TOTAL META************************//


            foreach (var item in ventaDiaria_Result)
            {
                ventaDiaria.Fecha = DateTime.Parse(item.Fecha);
                ventaDiaria.Revenue = item.Revenue.Value;
                ventaDiaria.Acumulado = item.Acumulado.Value;
                ventaDiaria.Meta = _metaVentaDiaria;

                ventasDiaria.Add(ventaDiaria);
                ventaDiaria = new VentaDiaria();
            }
            this.porFecha = ventasDiaria;


            List<VentaTotalDocumento_Result>   ventaTotalDocumento_Results = db.VentaTotalDocumento(year, month).OrderBy(o => o.Orden).ToList();

            VentaTotalDocumento ventaTotalDocumento = new VentaTotalDocumento();
            List<VentaTotalDocumento> ListaVentaTotalDocumento = new List<VentaTotalDocumento>();
            foreach (var item in ventaTotalDocumento_Results)
            {
                ventaTotalDocumento.Tipo = item.Tipo;
                try
                {
                    ventaTotalDocumento.Total = item.Total.Value;
                }
                catch (Exception)
                {

                    ventaTotalDocumento.Total = 0;
                }
                

                ListaVentaTotalDocumento.Add(ventaTotalDocumento);
                ventaTotalDocumento = new VentaTotalDocumento();
            }
            this.porDocumento = ListaVentaTotalDocumento;

            #region RFND
            List<VentaTotalDocumentoRFND_Result> ventaTotalDocumentoRFND_Results = db.VentaTotalDocumentoRFND(year, month).OrderBy(o => o.Orden).ToList();

                ventaTotalDocumento = new VentaTotalDocumento();
                ListaVentaTotalDocumento = new List<VentaTotalDocumento>();
            foreach (var item in ventaTotalDocumentoRFND_Results)
            {
                ventaTotalDocumento.Tipo = item.TIPO;
                try
                {
                    ventaTotalDocumento.Total = item.Total.Value;
                }
                catch (Exception)
                {

                    ventaTotalDocumento.Total = 0;
                }


                ListaVentaTotalDocumento.Add(ventaTotalDocumento);
                ventaTotalDocumento = new VentaTotalDocumento();
            }
            this.porDocumentoRFND = ListaVentaTotalDocumento;


            #endregion


            if (ventasDiaria.Count!=0)
            {
                this.FechaUltimaVenta = ventasDiaria.Max(m => m.Fecha); 
            }


            #region RutaMercado
            VentaRutaMercado ventaRutaMercado;
            List<VentaRutaMercado> ListaporRutaMercado = new List<VentaRutaMercado>(); ;
            foreach (var itemRuta in db.RUTA.OrderBy(o=>o.DescripcionRuta))
            {
                List<VentaTotalDocumentoMercado_Result> ventaTotalDocumentoMercado_Results = db.VentaTotalDocumentoMercado(year, month,itemRuta.IdRuta).OrderBy(o => o.Orden).ToList();               

                ventaRutaMercado = new VentaRutaMercado();
                
                ventaTotalDocumento = new VentaTotalDocumento();
                ListaVentaTotalDocumento = new List<VentaTotalDocumento>();
                foreach (var item in ventaTotalDocumentoMercado_Results)
                {
                 

                    ventaTotalDocumento.Tipo = item.Tipo;
                    try
                    {
                        ventaTotalDocumento.Total = item.Total.Value;
                    }
                    catch (Exception)
                    {

                        ventaTotalDocumento.Total = 0;
                    }


                    ListaVentaTotalDocumento.Add(ventaTotalDocumento);
                    ventaTotalDocumento = new VentaTotalDocumento();
                }

                ventaRutaMercado.CodigoRuta = itemRuta.CodigoRuta;
                ventaRutaMercado.DescripcionRuta = itemRuta.DescripcionRuta;
                ventaRutaMercado.porDocumento = ListaVentaTotalDocumento; ;
                ListaporRutaMercado.Add(ventaRutaMercado);
            }

            porRutaMercado = ListaporRutaMercado;


            #endregion


        }
        public List<Canal> PorCanal { get; set; }
        public Decimal TotalVenta { get; set; }
        public Decimal TotalMeta { get; set; }
        public Decimal VentaDirecta { get; set; }
        public Decimal VentaIndirecta { get; set; }
        public Decimal VentaDirectaPorc { get; set; }
        public Decimal VentaIndirectaPorc { get; set; }

        public Decimal TotalMetaPorc { get; set; }

        public List<VentaDiaria> porFecha { get; set; }
        public List<VentaTotalDocumento> porDocumento { get; set; }

        public List<VentaCanal_Result> ListaDirectas { get; set; }
        public List<VentaCanal_Result> ListaIndirectas { get; set; }
        public List<Canal> PorCanalDirecta { get; set; }
        public List<Canal> PorCanalIndirecta { get; set; }

        public DateTime? FechaUltimaVenta { get; set; }

        public List<VentaTotalDocumento> porDocumentoRFND { get; set; }

        public int Mes { get; set; }
        public int Año { get; set; }

        public List<VentaRutaMercado> porRutaMercado { get; set; }

        public class Canal
        {
            public String Descripcion { get; set; }
            public Decimal Revenue { get; set; }

            public Decimal Meta { get; set; }
            public Decimal Directa { get; set; }

        }
        public class VentaDiaria
        {
            public DateTime Fecha { get; set; }
            public Decimal Revenue { get; set; }
            public Decimal Acumulado { get; set; }
            public Decimal Meta { get; set; }

        }
        public class VentaTotalDocumento
        {
            public Decimal Total { get; set; }
            public String Tipo { get; set; }

        }
        public class VentaRutaMercado        {
            public String CodigoRuta { get; set; }
            public String DescripcionRuta { get; set; }
            public List<VentaTotalDocumento> porDocumento { get; set; }
        }


    }
}