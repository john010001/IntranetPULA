using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Intranet.Helper
{
    public class Utils
    {
        //private DB_DataRP dbData = new DB_DataRP();
        public DateTime FechaHoraLocal()
        {
            int HoraAdicional = int.Parse(WebConfigurationManager.AppSettings["HorasAdicionar"].ToString());
            DateTime _dateTime = DateTime.Now.AddHours(HoraAdicional);
            return _dateTime;
        }
        public int AñoInicio()
        {
            return int.Parse(WebConfigurationManager.AppSettings["InitialYear"].ToString());
        }
        public List<SelectListItem> ListaAño()
        {
            int _AnioInicio = AñoInicio();
            List<SelectListItem> _ListaAnio = new List<SelectListItem>();

            for (int i = DateTime.Now.Year; i >= _AnioInicio; i--)
            {
                _ListaAnio.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            return _ListaAnio;

        }
        //public List<SelectListItem> ListaPaisesPresencia()
        //{

        //    List<SelectListItem> _ListaPaisesPresencia = new List<SelectListItem>();
        //    var _paises = dbData.PAIS.Where(w => w.prescencia == true).OrderBy(o => o.Descripcion);


        //    foreach (var item in _paises)
        //    {
        //        _ListaPaisesPresencia.Add(new SelectListItem { Text = item.Descripcion, Value = item.IdPais.ToString() });
        //    }

        //    return _ListaPaisesPresencia;

        //}
        public List<SelectListItem> ListarMeses()
        {
            List<SelectListItem> _ListaMeses = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)

                _ListaMeses.Add(new SelectListItem
                {
                    Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i))
                    ,
                    Value = i.ToString()
                });
            {
            }

            return _ListaMeses;
        }
        public List<SelectListItem> ListarMeses(int mesSeleccionado)
        {
            bool _mesSeleccionado = false; ;
            List<SelectListItem> _ListaMeses = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {

                if (i == mesSeleccionado)
                { _mesSeleccionado = true; }

                _ListaMeses.Add(new SelectListItem
                {
                    Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)),
                    Value = i.ToString(),
                    Selected = _mesSeleccionado
                });


                _mesSeleccionado = false;

            }

            return _ListaMeses;
        }


        public List<SelectListItem> ListarDiasMes(int Year, int Month)
        {
            List<SelectListItem> _ListarDiasMes = new List<SelectListItem>();

            for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
            {
                _ListarDiasMes.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return _ListarDiasMes;
        }
    }
}