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
    public class VoladosSegmentoController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: OcupacionSegmento
        public ActionResult Index()
        {
            var VoladosSergmento = db.VOLADOS_SEGMENTO.ToList();
            return View(VoladosSergmento);
        }

        // GET: OcupacionSegmento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OcupacionSegmento/Create
        public ActionResult Create()
        {

            ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
            return View();
        }

        // POST: OcupacionSegmento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdVoladoRuta,Origen,Destino,Tramo")] VOLADOS_SEGMENTO vOLADOS_SEGMENTO)
        {
            if (ModelState.IsValid)
            {
                var busqueda= db.VOLADOS_SEGMENTO.Where(w => w.IdVoladoRuta == vOLADOS_SEGMENTO.IdVoladoRuta & w.Origen == vOLADOS_SEGMENTO.Origen & w.Destino == vOLADOS_SEGMENTO.Destino).FirstOrDefault();
                if (busqueda != null)
                {
                    ViewBag.IdVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
                    ViewBag.Error = "Este registro ya existe";
                    return View(vOLADOS_SEGMENTO);
                }
                db.VOLADOS_SEGMENTO.Add(vOLADOS_SEGMENTO);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception  ex)
                {
                    ViewBag.IdVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
                    ViewBag.Error = "No se puede crear registro: " + ex;
                    return View(vOLADOS_SEGMENTO);

                }

                return RedirectToAction("Index");
            }
            ViewBag.IdVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
            ViewBag.Error = "No se puede crear registro";
            return View(vOLADOS_SEGMENTO);
        }

        // GET: OcupacionSegmento/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLADOS_SEGMENTO vOLADOS_SEGMENTO = db.VOLADOS_SEGMENTO.Find(id);
            if (vOLADOS_SEGMENTO == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion",vOLADOS_SEGMENTO.IdVoladoRuta);

            return View(vOLADOS_SEGMENTO);
        }

        // POST: OcupacionSegmento/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdVoladoSegmentos,IdVoladoRuta,Origen,Destino,Tramo")] VOLADOS_SEGMENTO vOLADOS_SEGMENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vOLADOS_SEGMENTO).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(vOLADOS_SEGMENTO);

                }

                return RedirectToAction("Index");
            }
           
            ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
            return View(vOLADOS_SEGMENTO);
        }

        // GET: OcupacionSegmento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLADOS_SEGMENTO vOLADOS_SEGMENTO = db.VOLADOS_SEGMENTO.Find(id);
            if (vOLADOS_SEGMENTO == null)
            {
                return HttpNotFound();
            }
            return View(vOLADOS_SEGMENTO);
        }

        // POST: OcupacionSegmento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            VOLADOS_SEGMENTO vOLADOS_SEGMENTO = db.VOLADOS_SEGMENTO.Find(id);
            db.VOLADOS_SEGMENTO.Remove(vOLADOS_SEGMENTO);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(vOLADOS_SEGMENTO);

            }

            return RedirectToAction("Index");
        }
    }
}
