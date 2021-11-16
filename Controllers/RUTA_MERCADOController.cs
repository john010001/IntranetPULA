using Intranet.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Controllers
{
    public class RUTA_MERCADOController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        // GET: RUTA_MERCADO
        public ActionResult Index()
        {
            var ruta_mercado = db.RUTA_MERCADO.ToList();
            return View(ruta_mercado);
        }

        // GET: RUTA_MERCADO/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RUTA_MERCADO/Create
        public ActionResult Create()
        {
            ViewBag.IdRuta = new SelectList(db.RUTA, "IdRuta", "DescripcionRuta");
            return View();
        }

        // POST: RUTA_MERCADO/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdRuta,TramoRutaMercado")] RUTA_MERCADO ruta_mercado)
        {
            ViewBag.Ruta2 = new SelectList(db.RUTA, "IdRuta", "DescripcionRuta");
            
            
                try
                {
                    // TODO: Add insert logic here
                    db.RUTA_MERCADO.Add(ruta_mercado);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "No se puede crear registro";
                    
                    return View();
                }
            
            

        }

        // GET: RUTA_MERCADO/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA_MERCADO ruta_mercado = db.RUTA_MERCADO.Find(id);
            if(ruta_mercado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRuta = new SelectList(db.RUTA, "IdRuta", "DescripcionRuta",ruta_mercado.IdRuta);
            return View(ruta_mercado);
        }

        // POST: RUTA_MERCADO/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdRuta,TramoRutaMercado,IdRutaMercado")] RUTA_MERCADO ruta_mercado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ruta_mercado).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    // TODO: Add update logic here
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(ruta_mercado);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: RUTA_MERCADO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA_MERCADO ruta_mercado = db.RUTA_MERCADO.Find(id);
            if (ruta_mercado == null)
            {
                return HttpNotFound();
            }
            return View(ruta_mercado);
        }

        // POST: RUTA_MERCADO/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            RUTA_MERCADO ruta_mercado = db.RUTA_MERCADO.Find(id);
            db.RUTA_MERCADO.Remove(ruta_mercado);
            try
            {
                // TODO: Add delete logic here
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(ruta_mercado);
            }
        }
    }
}
