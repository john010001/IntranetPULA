using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intranet.Models.Data;

namespace Intranet.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TIPOEMISORController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: TIPOEMISOR
        public ActionResult Index()
        {
            return View(db.TIPOEMISOR.ToList());
        }

        // GET: TIPOEMISOR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOEMISOR tIPOEMISOR = db.TIPOEMISOR.Find(id);
            if (tIPOEMISOR == null)
            {
                return HttpNotFound();
            }
            return View(tIPOEMISOR);
        }

        // GET: TIPOEMISOR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPOEMISOR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdTipoEmisor,Descripcion,DescripcionEquivalente,Directa")] TIPOEMISOR tIPOEMISOR)
        {
            if (db.TIPOEMISOR.Where(w => w.Descripcion.Trim() == tIPOEMISOR.Descripcion.Trim()).Count() > 0)
            {
                ViewBag.Error = "Canal ya se encuentra registrada";
                return View(tIPOEMISOR);
            }

            if (ModelState.IsValid)
            {
                db.TIPOEMISOR.Add(tIPOEMISOR);


                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se realizar registro";
                    return View(tIPOEMISOR);

                }




                return RedirectToAction("Index");
            }

            return View(tIPOEMISOR);
        }

        // GET: TIPOEMISOR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOEMISOR tIPOEMISOR = db.TIPOEMISOR.Find(id);
            
            if (tIPOEMISOR == null)
            {
                return HttpNotFound();
            }
            
            return View(tIPOEMISOR);
        }

        // POST: TIPOEMISOR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdTipoEmisor,Descripcion,DescripcionEquivalente,Directa")] TIPOEMISOR tIPOEMISOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOEMISOR).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se pudo actuaalizar registro";
                    return View(tIPOEMISOR);

                }

                return RedirectToAction("Index");
            }
            return View(tIPOEMISOR);
        }

        // GET: TIPOEMISOR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOEMISOR tIPOEMISOR = db.TIPOEMISOR.Find(id);
            if (tIPOEMISOR == null)
            {
                return HttpNotFound();
            }
            return View(tIPOEMISOR);
        }

        // POST: TIPOEMISOR/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPOEMISOR tIPOEMISOR = db.TIPOEMISOR.Find(id);
            db.TIPOEMISOR.Remove(tIPOEMISOR);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(tIPOEMISOR);
                
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
