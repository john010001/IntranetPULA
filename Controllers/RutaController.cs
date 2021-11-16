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
    public class RutaController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        // GET: Ruta
        public ActionResult Index()
        {

            var ruta = db.RUTA.ToList();
            return View(ruta);
        }

        // GET: Ruta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ruta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ruta/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "CodigoRuta,DescripcionRuta")] RUTA ruta )
        {
            if(db.RUTA.Where(w => w.DescripcionRuta.Trim() == ruta.DescripcionRuta.Trim()).Count() > 0)
            {
                ViewBag.Error = "Descripción ya se encuentra registrada";
                return View(ruta);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    db.RUTA.Add(ruta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "No se puede crear registro";

                    return View(ruta);
                }
            }
            return View(ruta);

        }

        // GET: Ruta/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA ruta = db.RUTA.Find(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View(ruta);
        }

        // POST: Ruta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdRuta,CodigoRuta,DescripcionRuta")] RUTA ruta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ruta).State = EntityState.Modified;
            }
            try
            {
                // TODO: Add update logic here
                db.SaveChanges();
                
            }
            catch (Exception)
            {
                return View(ruta);
            }
            return RedirectToAction("Index");
        }

        // GET: Ruta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RUTA ruta = db.RUTA.Find(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View(ruta);
        }

        // POST: Ruta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            RUTA ruta = db.RUTA.Find(id);
            db.RUTA.Remove(ruta);
            try
            {
                // TODO: Add delete logic here
                db.SaveChanges();
                
            }
            catch
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(ruta);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Buscar()
        {


            return View();

        }
        [HttpPost]
        public ActionResult Buscar(string cadena)
        {
            RUTA ruta = new RUTA();
            Boolean resultado = ruta.buscarRuta(cadena);
            if (resultado)
            {
                ViewBag.RESULTADO = "Encontrado";

            }
            else
            {
                ViewBag.RESULTADO = "No encontrado";
            }


            return View();
        }
    }
}
