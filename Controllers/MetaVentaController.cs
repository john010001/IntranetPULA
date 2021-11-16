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
    [System.Web.Mvc.Authorize]
    public class MetaVentaController : Controller
    {
        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: MetaVenta
        [HttpGet]
        public ActionResult Index()
        {
            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();

            

            var mETA_VENTA = db.META_VENTA.Include(m => m.TIPOEMISOR).Where(w=>w.Mes==DateTime.Now.Month && w.Anio== DateTime.Now.Year);
            return View(mETA_VENTA.ToList());
        }
        [HttpPost]
        public ActionResult Index(int Year,int Month)
        {
            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(Month);
            ViewBag.Year = utils.ListaAño();


            var mETA_VENTA = db.META_VENTA.Include(m => m.TIPOEMISOR).Where(w => w.Mes == Month && w.Anio == Year);
            return View(mETA_VENTA.ToList());
        }


        // GET: MetaVenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            META_VENTA mETA_VENTA = db.META_VENTA.Find(id);
            if (mETA_VENTA == null)
            {
                return HttpNotFound();
            }
            return View(mETA_VENTA);
        }

        // GET: MetaVenta/Create
        public ActionResult Create()
        {
            ViewBag.idTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion");
            return View();
        }

        // POST: MetaVenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "IdMetaVenta,mes,anio,idTipoEmisor,monto")] META_VENTA mETA_VENTA)
        {
            if (ModelState.IsValid)
            {
                db.META_VENTA.Add(mETA_VENTA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", mETA_VENTA.idTipoEmisor);
            return View(mETA_VENTA);
        }

        // GET: MetaVenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            META_VENTA mETA_VENTA = db.META_VENTA.Find(id);
            if (mETA_VENTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", mETA_VENTA.idTipoEmisor);
            return View(mETA_VENTA);
        }

        // POST: MetaVenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "IdMetaVenta,mes,anio,idTipoEmisor,monto")] META_VENTA mETA_VENTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mETA_VENTA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoEmisor = new SelectList(db.TIPOEMISOR, "IdTipoEmisor", "Descripcion", mETA_VENTA.idTipoEmisor);
            return View(mETA_VENTA);
        }

        // GET: MetaVenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            META_VENTA mETA_VENTA = db.META_VENTA.Find(id);
            if (mETA_VENTA == null)
            {
                return HttpNotFound();
            }
            return View(mETA_VENTA);
        }

        // POST: MetaVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            META_VENTA mETA_VENTA = db.META_VENTA.Find(id);
            db.META_VENTA.Remove(mETA_VENTA);
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
