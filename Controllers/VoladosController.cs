using Intranet.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using EntityFramework.BulkInsert.Extensions;
using Intranet.Helper;
using System.Data.Entity.Validation;
using Intranet.ModelsApp;

namespace Intranet.Controllers
{
    [Authorize]
    public class VoladosController : Controller
    {
        IntranetDBEntities db = new IntranetDBEntities();
        // GET: Volados
        public ActionResult Index()
        {
            return View();
        }

        // GET: Volados/Importar
        public ActionResult Importar()
        {

            return View(db.VoladosKiuAdminfile.OrderByDescending(o =>o.UserFec));
        }
        // Post: Volados/Create
        [HttpPost]
        public ActionResult Importar(HttpPostedFileBase postedFile, string btnOption)
        {
            string filename = Path.GetFileName(postedFile.FileName);
            string filenameMayuscula = filename.Substring(0, 6).ToUpper();
            string extension = Path.GetExtension(postedFile.FileName);
            

            if (filenameMayuscula!="VOLADO" || extension!=".csv")
            {
                ViewBag.Error = "Error de archivo";
                return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
            }

            const int batchSave = 500;
            string filePath = string.Empty;

            List<VOLADOS> volados = new List<VOLADOS>();

            string path = Server.MapPath("~/Uploads/Volados");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            filePath = path + Path.GetFileName(postedFile.FileName);
            
           

            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            var usuario = userClaims.FindFirst("preferred_username").Value;


            postedFile.SaveAs(filePath);

            string csvData = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

            string[] arraySale;

            bool swFile = false;
            int contador = 1;

            foreach(string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    try
                    {
                        arraySale = row.Split(';');

                        if(swFile == false)
                        {
                            if (arraySale.Length != 40 && arraySale.Length != 41)
                            {
                                ViewBag.Error = "Estructura de Archivo incorrecta";
                                return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            }
                            if (db.VoladosKiuAdminfile.Where(w => w.Nombre == filename).Count() > 0)
                            {
                                ViewBag.Error = "Archivo cargado anteriormente";
                                return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            }
                            else
                            {
                                VoladosKiuAdminfile voladosKiuAdminfile = new VoladosKiuAdminfile();
                                voladosKiuAdminfile.Nombre = filename;
                                voladosKiuAdminfile.UserFec = DateTime.Now;
                                voladosKiuAdminfile.NomUser = usuario;
                                voladosKiuAdminfile.Estado = 0; //Validado
                                voladosKiuAdminfile.Procesado = 0;
                                voladosKiuAdminfile.Nuevo = 0;
                                voladosKiuAdminfile.Actualizado = 0;

                                db.VoladosKiuAdminfile.Add(voladosKiuAdminfile);
                                db.SaveChanges();
                            }

                            swFile = true;
                        }


                        
                        if (contador != 1)
                        {
                            var  _documento = arraySale[14];
                            var _origen = arraySale[3];
                            var _destino = arraySale[4];
                            string matricula = arraySale[6];
                            var _cupon = arraySale[17] is "" ? 0 : Convert.ToInt32(arraySale[17]);
                            var rptaMatricula = db.AERONAVES.Where(w => w.Matricula == matricula).FirstOrDefault();
                            if(rptaMatricula==null)
                            {
                                AERONAVES aERONAVES = new AERONAVES();
                                aERONAVES.Matricula = matricula;
                                aERONAVES.CabinaY = 0;
                                aERONAVES.CabinaC = 0;
                                aERONAVES.Total = 0;
                                aERONAVES.Tipo = "";

                                db.AERONAVES.Add(aERONAVES);
                                db.SaveChanges();
                                
                            }
                            //if (_documento == "")
                            //{
                            //    ViewBag.Error = "El documento no puede estar vacio";
                            //    return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            //}
                            //else if(_origen=="")
                            //{
                            //    ViewBag.Error = "El origen no puede estar vacio";
                            //    return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            //}
                            //else if (_destino == "")
                            //{
                            //    ViewBag.Error = "El destino no puede estar vacio";
                            //    return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            //}
                            //else if (_cupon == 0)
                            //{
                            //    ViewBag.Error = "El cupon no puede estar vacio";
                            //    return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                            //}


                            var objVolados = db.VOLADOS.Where(w => w.Documento == _documento && w.Origen == _origen && w.Destino == _destino && w.Cupon == _cupon).FirstOrDefault();

                            if (objVolados != null)
                            {
                                db.VOLADOS.Remove(objVolados);
                                VOLADOS volados3 = new VOLADOS();
                                volados3.FechaReporte = arraySale[0] is null ? (DateTime?)null : Convert.ToDateTime(arraySale[0]);
                                volados3.Vuelo = arraySale[1];
                                volados3.OrigenVenta = arraySale[2];
                                volados3.Origen = arraySale[3];
                                volados3.Destino = arraySale[4];
                                volados3.Equipo = arraySale[5];
                                volados3.Matricula = arraySale[6];
                                volados3.Demora = arraySale[7] is "" ? 0 : Convert.ToInt32(arraySale[7]);
                                volados3.PaisEmision = arraySale[8];
                                volados3.Emisor = arraySale[9];
                                volados3.Agente = arraySale[10];
                                volados3.FechaVenta = arraySale[11] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[11]);
                                volados3.FechaVueloReal = arraySale[12] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[12]);
                                volados3.FechaVueloProgramada = arraySale[13] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[13]);
                                volados3.Documento = arraySale[14];
                                volados3.Boleto = arraySale[15] is "" ? 0 : Convert.ToDecimal(arraySale[15]);
                                volados3.Fim = arraySale[16] is "" ? 0 : Convert.ToInt32(arraySale[16]);
                                volados3.Cupon = arraySale[17] is "" ? 0 : Convert.ToInt32(arraySale[17]);
                                volados3.TipoPasajero = arraySale[18];
                                volados3.Pasajero = arraySale[19];
                                volados3.Contacto = arraySale[20];
                                volados3.Clase = arraySale[21];
                                volados3.TarifaBase = arraySale[22];
                                volados3.TourCode = arraySale[23];
                                volados3.Moneda = arraySale[24];
                                volados3.Importe = arraySale[25] is "" ? 0 : Convert.ToDecimal(arraySale[25]);
                                volados3.PeriodoFi = arraySale[26];
                                volados3.FiMoneda = arraySale[27];
                                volados3.FiImporte = arraySale[28];
                                volados3.Localizador = arraySale[29];
                                volados3.Aerolinea = arraySale[30];
                                volados3.MonedaLocal = arraySale[31];
                                volados3.ImporteLocal = arraySale[32] is "" ? 0 : Convert.ToDecimal(arraySale[32]);
                                volados3.Endoso = arraySale[33];
                                volados3.InfoAdicionalFc = arraySale[34];
                                volados3.Sac = arraySale[35];
                                volados3.MonedaQ = arraySale[36];
                                volados3.ImporteQ = arraySale[37];
                                volados3.MonedaQI = arraySale[38];
                                volados3.ImporteQLocal = arraySale[39];
                                db.VOLADOS.Add(volados3);

                            }
                            else
                            {
                                volados.Add(new VOLADOS
                                {
                                    FechaReporte = arraySale[0] is null ? (DateTime?)null : Convert.ToDateTime(arraySale[0]),
                                    Vuelo = arraySale[1],
                                    OrigenVenta = arraySale[2],
                                    Origen = arraySale[3],
                                    Destino = arraySale[4],
                                    Equipo = arraySale[5],
                                    Matricula = arraySale[6],
                                    //Demora = arraySale[7] is "" ? 0 : Convert.ToInt32(arraySale[7]),
                                    PaisEmision = arraySale[8],
                                    Emisor = arraySale[9],
                                    Agente = arraySale[10],
                                    FechaVenta = arraySale[11] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[11]),
                                    FechaVueloReal = arraySale[12] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[12]),
                                    FechaVueloProgramada = arraySale[13] is "" ? (DateTime?)null : Convert.ToDateTime(arraySale[13]),
                                    //Documento = arraySale[14],
                                    Boleto = arraySale[15] is "" ? 0 : Convert.ToDecimal(arraySale[15]),
                                    Fim = arraySale[16] is "" ? 0 : Convert.ToInt32(arraySale[16]),
                                    Cupon = arraySale[17] is "" ? 0 : Convert.ToInt32(arraySale[17]),
                                    TipoPasajero = arraySale[18],
                                    Pasajero = arraySale[19],
                                    Contacto = arraySale[20],
                                    Clase = arraySale[21],
                                    TarifaBase = arraySale[22],
                                    TourCode = arraySale[23],
                                    Moneda = arraySale[24],
                                    Importe = arraySale[25] is "" ? 0 : Convert.ToDecimal(arraySale[25]),
                                    //PeriodoFi = arraySale[26],
                                    FiMoneda = arraySale[27],
                                    FiImporte = arraySale[28],
                                    Localizador = arraySale[29],
                                    Aerolinea = arraySale[30],
                                    MonedaLocal = arraySale[31],
                                    ImporteLocal = arraySale[32] is "" ? 0 : Convert.ToDecimal(arraySale[32]),
                                    Endoso = arraySale[33],
                                    InfoAdicionalFc = arraySale[34],
                                    Sac = arraySale[35],
                                    MonedaQ = arraySale[36],
                                    ImporteQ = arraySale[37],
                                    MonedaQI = arraySale[38],
                                    ImporteQLocal = arraySale[39]

                                });
                            }

                            
                            

                           
                        }

                        contador += 1;
                        

                    }
                    //catch (Exception ex)
                    //{




                    //    ViewBag.Error = "Algunos registro no pudieron se cargados: "+ex;
                    //    return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
                    //}
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
            }
            
            using ( var context = new IntranetDBEntities())
            {
                List<VOLADOS> volados1 = volados;
                context.BulkInsert<VOLADOS>(volados1);
                //ViewBag.Error = "Archivo cargado correctamente";
                ViewBag.Mensaje = "Archivo cargado correctamente";
            }

            db.SaveChanges();

            VoladosKiuAdminfile _voladosKiuAdminfile = db.VoladosKiuAdminfile.First(w => w.Nombre == filename);
            _voladosKiuAdminfile.Estado = 1;


            return View(db.VoladosKiuAdminfile.OrderByDescending(o => o.UserFec));
        }

        // GET: Volados/Create
        public ActionResult Resumen()
        {
            Utils utils = new Utils();
            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();
            //List<ResumenVolados> Rvolados = new List<ResumenVolados>();
            ResumenVolados resumenVolados = new ResumenVolados(DateTime.Now.Year, DateTime.Now.Month);
            ViewBag.LISTAVOLADOS = resumenVolados;
            return View(resumenVolados);
        }

        // POST: Volados/Create
        [HttpPost]
        public ActionResult Resumen(int Year, int Month)
        {


            Utils utils = new Utils();
            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();

            ResumenVolados resumenVolados = new  ResumenVolados(Year, Month);
            return View(resumenVolados);



        }

        // GET: Volados/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Volados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Volados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Volados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
