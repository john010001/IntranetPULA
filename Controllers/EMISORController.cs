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
    public class EMISORController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: EMISOR
        public ActionResult Index()
        {


            var eMISOR = db.EMISOR.Include(e => e.TIPOEMISOR);
            return View(eMISOR.ToList());
        }

        // GET: EMISOR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMISOR eMISOR = db.EMISOR.Find(id);
            if (eMISOR == null)
            {
                return HttpNotFound();
            }
            return View(eMISOR);
        }

        // GET: EMISOR/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion");
            return View();
        }

        // POST: EMISOR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "IdEmisor,Descripcion,IdTipoEmisor,Mostrar")] EMISOR eMISOR)
        {

            if (db.EMISOR.Where(w => w.Descripcion.Trim() == eMISOR.Descripcion.Trim()).Count()>0)
            {
                ViewBag.Error = "Descripción ya se encuentra registrada";
                ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", eMISOR.IdTipoEmisor);
                return View(eMISOR);
            }



            if (ModelState.IsValid)
            {
                db.EMISOR.Add(eMISOR);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", eMISOR.IdTipoEmisor); 
                    ViewBag.Error = "No se puede crear registro";
                    return View(eMISOR);

                }

                return RedirectToAction("Index");
            }

            ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", eMISOR.IdTipoEmisor);
            return View(eMISOR);
        }

        // GET: EMISOR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMISOR eMISOR = db.EMISOR.Find(id);
            if (eMISOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", eMISOR.IdTipoEmisor);
            return View(eMISOR);
        }

        // POST: EMISOR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "IdEmisor,Descripcion,IdTipoEmisor,Mostrar")] EMISOR eMISOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMISOR).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(eMISOR);

                }

                return RedirectToAction("Index");
            }
            ViewBag.IdTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", eMISOR.IdTipoEmisor);
            return View(eMISOR);
        }

        // GET: EMISOR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMISOR eMISOR = db.EMISOR.Find(id);
            if (eMISOR == null)
            {
                return HttpNotFound();
            }
            return View(eMISOR);
        }

        // POST: EMISOR/Delete/5
        [HttpPost, ActionName("Delete")]        
        public ActionResult DeleteConfirmed(int id)
        {
            EMISOR eMISOR = db.EMISOR.Find(id);
            db.EMISOR.Remove(eMISOR);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(eMISOR);

            }

            return RedirectToAction("Index");
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
