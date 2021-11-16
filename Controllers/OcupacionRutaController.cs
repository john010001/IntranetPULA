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
    [Authorize]
    public class OcupacionRutaController : Controller
    {
        
        private IntranetDBEntities db = new IntranetDBEntities();
        // GET: OcupacionRuta
        public ActionResult Index()
        {
            var OcupacionRuta = db.OCUPACION_RUTA.ToList();
            return View(OcupacionRuta);
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
        public ActionResult Create([Bind(Include = "Descripcion,DescripcionLarga")] OCUPACION_RUTA oCUPACION_RUTA)
        {
            if (db.OCUPACION_RUTA.Where(w => w.Descripcion.Trim() == oCUPACION_RUTA.Descripcion.Trim()).Count() > 0)
            {
                ViewBag.Error = "Descripción ya se encuentra registrada";

                return View(oCUPACION_RUTA);
            }



            if (ModelState.IsValid)
            {
                db.OCUPACION_RUTA.Add(oCUPACION_RUTA);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    ViewBag.Error = "No se puede crear registro";
                    return View(oCUPACION_RUTA);

                }

                return RedirectToAction("Index");
            }
            return View(oCUPACION_RUTA);
        }

        // GET: OcupacionRuta/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_RUTA oCUPACION_RUTA = db.OCUPACION_RUTA.Find(id);
            if (oCUPACION_RUTA == null)
            {
                return HttpNotFound();
            }
           
            return View(oCUPACION_RUTA);
        }

        // POST: OcupacionRuta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdOcupacionRuta,Descripcion,DescripcionLarga")] OCUPACION_RUTA oCUPACION_RUTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oCUPACION_RUTA).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(oCUPACION_RUTA);

                }

                return RedirectToAction("Index");
            }
            
            return View(oCUPACION_RUTA);
        }

        // GET: OcupacionRuta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OCUPACION_RUTA oCUPACION_RUTA = db.OCUPACION_RUTA.Find(id);
            if (oCUPACION_RUTA == null)
            {
                return HttpNotFound();
            }
            return View(oCUPACION_RUTA);
        }

        // POST: OcupacionRuta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            OCUPACION_RUTA oCUPACION_RUTA = db.OCUPACION_RUTA.Find(id);
            db.OCUPACION_RUTA.Remove(oCUPACION_RUTA);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                
                ViewBag.Error = "Este registro esta siendo en otra tabla";
                return View(oCUPACION_RUTA);

            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar el registro";
                return View(oCUPACION_RUTA);
            }

            return RedirectToAction("Index");
        }
    }
}
