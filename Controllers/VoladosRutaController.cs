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
    public class VoladosRutaController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        // GET: OcupacionRuta
        public ActionResult Index()
        {
            var VoladosRuta = db.VOLADOS_RUTA.ToList();
            return View(VoladosRuta);
        }

        // GET: OcupacionRuta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OcupacionRuta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OcupacionRuta/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Descripcion,DescripcionLarga")] VOLADOS_RUTA vOLADOS_RUTA)
        {
            if (db.VOLADOS_RUTA.Where(w => w.Descripcion.Trim() == vOLADOS_RUTA.Descripcion.Trim()).Count() > 0)
            {
                ViewBag.Error = "Descripción ya se encuentra registrada";

                return View(vOLADOS_RUTA);
            }



            if (ModelState.IsValid)
            {
                db.VOLADOS_RUTA.Add(vOLADOS_RUTA);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    ViewBag.Error = "No se puede crear registro";
                    return View(vOLADOS_RUTA);

                }

                return RedirectToAction("Index");
            }
            return View(vOLADOS_RUTA);
        }

        // GET: OcupacionRuta/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLADOS_RUTA vOLADOS_RUTA = db.VOLADOS_RUTA.Find(id);
            if (vOLADOS_RUTA == null)
            {
                return HttpNotFound();
            }
           
            return View(vOLADOS_RUTA);
        }

        // POST: OcupacionRuta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdVoladoRuta,Descripcion,DescripcionLarga")] VOLADOS_RUTA vOLADOS_RUTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vOLADOS_RUTA).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(vOLADOS_RUTA);

                }

                return RedirectToAction("Index");
            }
            
            return View(vOLADOS_RUTA);
        }

        // GET: OcupacionRuta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLADOS_RUTA vOLADOS_RUTA = db.VOLADOS_RUTA.Find(id);
            if (vOLADOS_RUTA == null)
            {
                return HttpNotFound();
            }
            return View(vOLADOS_RUTA);
        }

        // POST: OcupacionRuta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            VOLADOS_RUTA vOLADOS_RUTA = db.VOLADOS_RUTA.Find(id);
            db.VOLADOS_RUTA.Remove(vOLADOS_RUTA);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                
                ViewBag.Error = "Este registro esta siendo en otra tabla";
                return View(vOLADOS_RUTA);

            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar el registro";
                return View(vOLADOS_RUTA);
            }

            return RedirectToAction("Index");
        }
    }
}
