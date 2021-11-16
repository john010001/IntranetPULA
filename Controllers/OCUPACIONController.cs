using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EntityFramework.BulkInsert.Extensions;
using Intranet.Helper;
using Intranet.Models.Data;
using Intranet.ModelsApp;

namespace Intranet.Controllers
{
    [Authorize]
    public class OCUPACIONController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: OCUPACION_ARCHIVO
        [HttpGet]
        public ActionResult Index()
        { 
            

            return View();


        }
        [HttpPost]
        public ActionResult Index(string origen,string destino)
        {
            OcupacionVuelosRegulares ocupacionVuelosRegulares = new OcupacionVuelosRegulares(origen,destino);

            return View(ocupacionVuelosRegulares);
            
        }
        [HttpGet]
        public ActionResult Resumen()
        {
            Utils utils = new Utils();





            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);


            List<SelectListItem> lyear = new List<SelectListItem>();
            lyear.Add(new SelectListItem() { Text = "2021", Value = "2021" });
            lyear.Add(new SelectListItem() { Text = "2022", Value = "2022" });
            
            //ViewBag.Year = utils.ListaAño();
            ViewBag.Year = new SelectList(lyear, "Value", "Text");

            ResumenOcupacion resumenOcupacion = new ResumenOcupacion(DateTime.Now.Year, DateTime.Now.Month);
            return View(resumenOcupacion);

        }
        [HttpPost]
        public ActionResult Resumen(int Year, int Month)
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(Month);

            List<SelectListItem> lyear = new List<SelectListItem>();
            lyear.Add(new SelectListItem() { Text = "2021", Value = "2021" });
            lyear.Add(new SelectListItem() { Text = "2022", Value = "2022" });

            //ViewBag.Year = utils.ListaAño();
            ViewBag.Year = new SelectList(lyear, "Value", "Text");
            //ViewBag.Year = utils.ListaAño();
            
            ResumenOcupacion resumenOcupacion = new ResumenOcupacion(Year, Month);
            return View(resumenOcupacion);
        }



        [HttpGet]
        public ActionResult Importar()
        {
            string path = Server.MapPath("~/Ftp/Ocupacion/MyFiles");
            string pathNuevo = Server.MapPath("~/Ftp/OcupacionPro");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            DirectoryInfo di = new DirectoryInfo(path);
            DateTime horaCarga = DateTime.Now;

            List<OCUPACION> oCUPACIONs = new List<OCUPACION>();

            string[] arrayOcupacion;
            foreach (var files in di.GetFiles("*.csv", SearchOption.TopDirectoryOnly))
            {
                string csvOcupacion = System.IO.File.ReadAllText(files.FullName, Encoding.UTF8);

                OCUPACION_ARCHIVO oCUPACION_ARCHIVO = new OCUPACION_ARCHIVO();
                oCUPACION_ARCHIVO.NombreArchivo = files.Name;
                oCUPACION_ARCHIVO.Origen = files.Name.Substring(13, 3);
                oCUPACION_ARCHIVO.Destino = files.Name.Substring(17, 3);
                oCUPACION_ARCHIVO.FechaCarga = horaCarga.Date;
                oCUPACION_ARCHIVO.HoraCarga = horaCarga;
                oCUPACION_ARCHIVO.FechaArchivo = files.CreationTime;
                db.OCUPACION_ARCHIVO.Add(oCUPACION_ARCHIVO);
                db.SaveChanges();

                oCUPACIONs = new List<OCUPACION>();

                foreach (string row in csvOcupacion.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        arrayOcupacion = row.Split(',');

                        
                        if (arrayOcupacion[0].Trim()== "DATE")
                        {
                            continue;
                        }

                        try
                        {
                            
                            oCUPACIONs.Add(new OCUPACION
                            {
                                IdOcupacionArchivo= oCUPACION_ARCHIVO.IdOcupacionArchivo,
                                Fecha= DateTime.ParseExact(arrayOcupacion[0], "ddMMMy", CultureInfo.InvariantCulture),
                                //Fecha = DateTime.Now,
                                DiaSemana = arrayOcupacion[1],
                                Vuelo = arrayOcupacion[2],
                                Origen= files.Name.Substring(13,3),
                                Destino = files.Name.Substring(17, 3),
                                NLG = int.Parse(arrayOcupacion[3]),
                                CAB = arrayOcupacion[4],
                                AU = int.Parse(arrayOcupacion[6]),
                                BS = int.Parse(arrayOcupacion[7]),
                                OS = int.Parse(arrayOcupacion[8]),
                                SS = int.Parse(arrayOcupacion[9]),
                                ABS = arrayOcupacion[10].Trim() !="*" ?  int.Parse(arrayOcupacion[10]): 0,
                                SA = int.Parse(arrayOcupacion[11])


                            });



                        }
                        catch (Exception ex)
                        {
                            var xx = files;
                            throw;
                        }

                    }


                }


                using (var context = new IntranetDBEntities())
                {
                    List<OCUPACION> _Ocupacion = oCUPACIONs;
                    context.BulkInsert<OCUPACION>(_Ocupacion);
                    ViewBag.Error = "Archivo cargado correctamente";
                    ViewBag.Mensaje = "Archivo cargado correctamente";
                }


                files.CopyTo(pathNuevo + "\\P"  + oCUPACION_ARCHIVO.IdOcupacionArchivo.ToString() + "_" + files.Name);
                files.Delete();



                //files.MoveTo(pathNuevo);
            }


            db.SaveChanges();



            return View();
            //return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec));

        }
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
