using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Intranet.Helper;
using Intranet.Models.Data;

namespace Intranet.Controllers
{
    [Authorize]
    public class USUARIO_ROLController : Controller
    {
        private Usuario_Entities db = new Usuario_Entities();

        // GET: MetaVenta
        [HttpGet]
        public ActionResult Index()
        {
            //Utils utils = new Utils();

            //ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            //ViewBag.Year = utils.ListaAño();



            var uSUARIO_ROL = db.USUARIO_ROL.Include(m => m.ROL);/*.Where(w=>w.Mes==DateTime.Now.Month && w.Anio== DateTime.Now.Year);*/
            return View(uSUARIO_ROL.ToList());
        }
        //[HttpPost]
        //public ActionResult Index(int Year,int Month)
        //{
        //    Utils utils = new Utils();

        //    ViewBag.Month = utils.ListarMeses(Month);
        //    ViewBag.Year = utils.ListaAño();


        //    var mETA_VENTA = db.META_VENTA.Include(m => m.TIPOEMISOR).Where(w => w.Mes == Month && w.Anio == Year);
        //    return View(mETA_VENTA.ToList());
        //}


        // GET: MetaVenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_ROL uSUARIO_ROL = db.USUARIO_ROL.Find(id);
            if (uSUARIO_ROL == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO_ROL);
        }

        // GET: MetaVenta/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.ROL, "Id", "Descripcion");
            return View();
        }

        // POST: MetaVenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "Id,Correo,IdRol")] USUARIO_ROL uSUARIO_ROL)
        {
            if (ModelState.IsValid)
            {
                uSUARIO_ROL.Estado = true;
                db.USUARIO_ROL.Add(uSUARIO_ROL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.ROL, "Id", "Descripcion", uSUARIO_ROL.IdRol);
            return View(uSUARIO_ROL);
        }

        // GET: MetaVenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_ROL uSUARIO_ROL = db.USUARIO_ROL.Find(id);
            if (uSUARIO_ROL == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.ROL, "Id", "Descripcion", uSUARIO_ROL.IdRol);
            return View(uSUARIO_ROL);
        }

        // POST: MetaVenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "Id,Correo,IdRol")] USUARIO_ROL uSUARIO_ROL)
        {
            if (ModelState.IsValid)
            {
                //uSUARIO_ROL.Estado = true;    
                uSUARIO_ROL.IdRol = 1;
                
                db.Entry(uSUARIO_ROL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.ROL, "Id", "Descripcion",uSUARIO_ROL.IdRol);
            return View(uSUARIO_ROL);
        }

        // GET: MetaVenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO_ROL uSUARIO_ROL = db.USUARIO_ROL.Find(id);
            if (uSUARIO_ROL == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO_ROL);
        }

        // POST: MetaVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIO_ROL uSUARIO_ROL = db.USUARIO_ROL.Find(id);
            db.USUARIO_ROL.Remove(uSUARIO_ROL);
            db.SaveChanges();
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
