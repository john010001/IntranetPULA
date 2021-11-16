using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intranet.Models.Data;
using Intranet.Helper;

namespace Intranet.Controllers
{
    public class OcupacionMetaController : Controller
    {

        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: OcupacionMeta
        public ActionResult Index()
        {
            Utils utils = new Utils();
            int anio = Convert.ToInt32(DateTime.Now.Year);
            var idOcupacion = db.OCUPACION_RUTA.First();
            ViewBag.Año = utils.ListaAño();
            ViewBag.IdOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
            var rpta = (List<OCUPACION_META>)db.OCUPACION_META.Where(w => w.IdOcupacionRuta == idOcupacion.IdOcupacionRuta & w.Año == anio).ToList();
            var count = 1;
            if (rpta.Count == 0)
            {
                for (var item = 0; item < 12; item++)
                {

                    OCUPACION_META oCUPACION_META = new OCUPACION_META();
                    oCUPACION_META.IdOcupacionRuta = idOcupacion.IdOcupacionRuta;
                    oCUPACION_META.Año = anio;
                    oCUPACION_META.Mes = count;
                    oCUPACION_META.Meta = 0;
                    db.OCUPACION_META.Add(oCUPACION_META);

                    count += 1;

                }
                db.SaveChanges();
                rpta = (List<OCUPACION_META>)db.OCUPACION_META.Where(w => w.IdOcupacionRuta == idOcupacion.IdOcupacionRuta & w.Año == anio).ToList();
                return View(rpta);
            }
            return View(rpta);
        }


        // POST: OcupacionMeta/Index
        [HttpPost]
        public ActionResult Index(int IdOcupacionRuta, int Año)
        {
            Utils utils = new Utils();
            ViewBag.Año = utils.ListaAño();
            ViewBag.IdOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
            var rpta =(List<OCUPACION_META>)db.OCUPACION_META.Where(w => w.IdOcupacionRuta == IdOcupacionRuta & w.Año == Año).ToList();
            int count = 1;
            if (rpta.Count != 0)
            {
                var longitud = rpta.Count();
                if (longitud == 12)
                {
                    return View(rpta);
                }
                
            }
            if (rpta.Count==0)
            {
                for (var item = 0; item < 12; item++)
                {
                    
                        OCUPACION_META oCUPACION_META = new OCUPACION_META();
                        oCUPACION_META.IdOcupacionRuta = IdOcupacionRuta;
                        oCUPACION_META.Año = Año;
                        oCUPACION_META.Mes = count;
                        oCUPACION_META.Meta = 0;
                        db.OCUPACION_META.Add(oCUPACION_META);
                    
                    count += 1;

                }
                db.SaveChanges();
                rpta = (List<OCUPACION_META>)db.OCUPACION_META.Where(w => w.IdOcupacionRuta == IdOcupacionRuta & w.Año == Año).ToList();
                return View(rpta);
            }
            if (rpta.Count != 0)
            {
                return View(rpta);
            }


            return View(rpta);
        }


        // GET: OcupacionMeta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OcupacionMeta/Create
        public ActionResult Create()
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            return View();
        }

        // POST: OcupacionMeta/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "idOcupacionRuta,Año,Mes,Meta")] OCUPACION_META oCUPACION_META)
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            if (ModelState.IsValid)
            {
                db.OCUPACION_META.Add(oCUPACION_META);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                   

                    ViewBag.Error = "No se puede crear registro";
                    return View(oCUPACION_META);

                }

                return RedirectToAction("Index");
            }
            return View(oCUPACION_META);
        }

        // GET: OcupacionMeta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_META oCUPACION_META = db.OCUPACION_META.Find(id);
            if (oCUPACION_META == null)
            {
                return HttpNotFound();
            }
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            return View(oCUPACION_META);
        }

        // POST: OcupacionMeta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdOcupacionMETA,IdOcupacionRuta,Año,Mes,Meta")] OCUPACION_META oCUPACION_META)
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
            if (ModelState.IsValid)
            {
                db.Entry(oCUPACION_META).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(oCUPACION_META);

                }

                return RedirectToAction("Index");
            }
            
            return View(oCUPACION_META);
        }

        // GET: OcupacionMeta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_META oCUPACION_META = db.OCUPACION_META.Find(id);
            if (oCUPACION_META == null)
            {
                return HttpNotFound();
            }
           
            return View(oCUPACION_META);
        }

        // POST: OcupacionMeta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {

            OCUPACION_META oCUPACION_META = db.OCUPACION_META.Find(id);
            db.OCUPACION_META.Remove(oCUPACION_META);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(oCUPACION_META);

            }

            return RedirectToAction("Index");
        }
    }
}
