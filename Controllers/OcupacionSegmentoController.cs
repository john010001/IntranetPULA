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
    public class OcupacionSegmentoController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: OcupacionSegmento
        public ActionResult Index()
        {
            var OcupacionSergmento = db.OCUPACION_SEGMENTO.ToList();
            return View(OcupacionSergmento);
        }

        // GET: OcupacionSegmento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OcupacionSegmento/Create
        public ActionResult Create()
        {

            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
            return View();
        }

        // POST: OcupacionSegmento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdOcupacionRuta,Origen,Destino,Tramo")] OCUPACION_SEGMENTO oCUPACION_SEGMENTO)
        {
            if (ModelState.IsValid)
            {
                var busqueda = db.OCUPACION_SEGMENTO.Where(w => w.IdOcupacionRuta == oCUPACION_SEGMENTO.IdOcupacionRuta & w.Origen == oCUPACION_SEGMENTO.Origen & w.Destino == oCUPACION_SEGMENTO.Destino).FirstOrDefault();
                if (busqueda != null)
                {
                    ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
                    ViewBag.Error = "Este registro ya existe";
                    return View(oCUPACION_SEGMENTO);
                }
                db.OCUPACION_SEGMENTO.Add(oCUPACION_SEGMENTO);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception  ex)
                {
                    ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "IdOcupacionRuta", "Descripcion");
                    ViewBag.Error = "No se puede crear registro: " + ex;
                    return View(oCUPACION_SEGMENTO);

                }

                return RedirectToAction("Index");
            }
            ViewBag.Error = "No se puede crear registro";
            return View(oCUPACION_SEGMENTO);
        }

        // GET: OcupacionSegmento/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_SEGMENTO oCUPACION_SEGMENTO = db.OCUPACION_SEGMENTO.Find(id);
            if (oCUPACION_SEGMENTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion",oCUPACION_SEGMENTO.IdOcupacionRuta);
           

            return View(oCUPACION_SEGMENTO);
        }

        // POST: OcupacionSegmento/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdOcupacionSegmentos,IdOcupacionRuta,Origen,Destino,Tramo")] OCUPACION_SEGMENTO oCUPACION_SEGMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oCUPACION_SEGMENTO).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(oCUPACION_SEGMENTO);

                }

                return RedirectToAction("Index");
            }
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            return View(oCUPACION_SEGMENTO);
        }

        // GET: OcupacionSegmento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_SEGMENTO oCUPACION_SEGMENTO = db.OCUPACION_SEGMENTO.Find(id);
            if (oCUPACION_SEGMENTO == null)
            {
                return HttpNotFound();
            }
            return View(oCUPACION_SEGMENTO);
        }

        // POST: OcupacionSegmento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            OCUPACION_SEGMENTO oCUPACION_SEGMENTO = db.OCUPACION_SEGMENTO.Find(id);
            db.OCUPACION_SEGMENTO.Remove(oCUPACION_SEGMENTO);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(oCUPACION_SEGMENTO);

            }

            return RedirectToAction("Index");
        }
    }
}
