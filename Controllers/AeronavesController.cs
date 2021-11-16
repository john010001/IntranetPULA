using Intranet.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Intranet.Controllers
{
    [Authorize]
    public class AeronavesController : Controller
    {
        //Este es le controlador Aero//hola 
        // GET: Aeronaves
        private IntranetDBEntities db = new IntranetDBEntities();
        // GET: Aeronaves
        public ActionResult Index()
        {
            List<AERONAVES> aERONAVEs = db.AERONAVES.ToList();
            return View(aERONAVEs);
        }

        // GET: Aeronaves/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Aeronaves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aeronaves/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Matricula,Tipo,CabinaC,CabinaY")] AERONAVES aERONAVES)
        {
            if (db.AERONAVES.Where(w => w.Matricula == aERONAVES.Matricula.Trim()).Count() > 0)
            {
                ViewBag.Error = "Esta matricula ya se encuentra registrada";
                return View(aERONAVES);
            }

            if (ModelState.IsValid)
            {
                aERONAVES.Total = aERONAVES.CabinaC + aERONAVES.CabinaY;
                db.AERONAVES.Add(aERONAVES);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede crear registro";
                    return View(aERONAVES);
                }
                return RedirectToAction("Index");
            }

            return View(aERONAVES);
        }

        // GET: Aeronaves/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AERONAVES aERONAVES = db.AERONAVES.Find(id);
            if (aERONAVES == null)
            {
                return HttpNotFound();
            }

            //Este es le controlador Aero//hola 
            return View(aERONAVES);
        }

        // POST: Aeronaves/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Matricula,Tipo,CabinaC,CabinaY")] AERONAVES aERONAVES)
        {
            if (ModelState.IsValid)
            {
                aERONAVES.Total = aERONAVES.CabinaC + aERONAVES.CabinaY;
                db.Entry(aERONAVES).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    // TODO: Add update logic here
                    db.SaveChanges();

                }
                catch
                {
                    return View(aERONAVES);
                }
                return RedirectToAction("Index");
            }


            return View(aERONAVES);
        }

        // GET: Aeronaves/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AERONAVES aERONAVES = db.AERONAVES.Find(id);
            if (aERONAVES == null)
            {
                return HttpNotFound();
            }
            TempData["id"] = id;
            return View(aERONAVES);
        }

        // POST: Aeronaves/Delete/5
        [HttpPost]
        public ActionResult Delete()
        {
            var id = TempData["id"];
            AERONAVES aERONAVES = db.AERONAVES.Find(id);
            db.AERONAVES.Remove(aERONAVES);
            try
            {
                db.SaveChanges();
                // TODO: Add delete logic here
            }
            catch
            {
                ViewBag.Error = "No se puede eliminar registro";
                return View(aERONAVES);
            }
            return RedirectToAction("Index");
        }
    }
}
