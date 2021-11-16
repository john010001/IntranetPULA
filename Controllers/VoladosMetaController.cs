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
    public class VoladosMetaController : Controller
    {

        private IntranetDBEntities db = new IntranetDBEntities();

        // GET: OcupacionMeta
        public ActionResult Index()
        {
            Utils utils = new Utils();
            int anio = Convert.ToInt32(DateTime.Now.Year);
            var ruta = db.VOLADOS_RUTA.FirstOrDefault();
            ViewBag.Año = utils.ListaAño();
            ViewBag.IdVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
        
            var rpta = (List<VOLADOS_META>)db.VOLADOS_META.Where(w => w.IdVoladoRuta == ruta.IdVoladoRuta & w.Año == anio).ToList();
            var count = 1;
            if (rpta.Count == 0)
            {
                for (var item = 0; item < 12; item++)
                {

                    VOLADOS_META vOLADOS_META = new VOLADOS_META();
                    vOLADOS_META.IdVoladoRuta = ruta.IdVoladoRuta;
                    vOLADOS_META.Año = anio;
                    vOLADOS_META.Mes = count;
                    vOLADOS_META.Meta = 0;
                    db.VOLADOS_META.Add(vOLADOS_META);

                    count += 1;

                }
                db.SaveChanges();
                rpta = (List<VOLADOS_META>)db.VOLADOS_META.Where(w => w.IdVoladoRuta == ruta.IdVoladoRuta & w.Año == anio).ToList();
                return View(rpta);
            }
            return View(rpta);
        }


        // POST: OcupacionMeta/Index
        [HttpPost]
        public ActionResult Index(int IdVoladoRuta, int Año)
        {
            Utils utils = new Utils();
            ViewBag.Año = utils.ListaAño();
            ViewBag.IdVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
            var rpta = (List<VOLADOS_META>)db.VOLADOS_META.Where(w => w.IdVoladoRuta == IdVoladoRuta & w.Año == Año).ToList();
            int count = 1;
            if (rpta.Count != 0)
            {
                var longitud = rpta.Count();
                if (longitud == 12)
                {
                    return View(rpta);
                }
                
            }
            if (rpta.Count==0)
            {
                for (var item = 0; item < 12; item++)
                {
                    
                        VOLADOS_META vOLADOS_META = new VOLADOS_META();
                        vOLADOS_META.IdVoladoRuta = IdVoladoRuta;
                        vOLADOS_META.Año = Año;
                        vOLADOS_META.Mes = count;
                        vOLADOS_META.Meta = 0;
                        db.VOLADOS_META.Add(vOLADOS_META);
                    
                    count += 1;

                }
                db.SaveChanges();
                rpta = (List<VOLADOS_META>)db.VOLADOS_META.Where(w => w.IdVoladoRuta == IdVoladoRuta & w.Año == Año).ToList();
                return View(rpta);
            }
            if (rpta.Count != 0)
            {
                return View(rpta);
            }


            return View(rpta);
        }


        // GET: OcupacionMeta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OcupacionMeta/Create
        public ActionResult Create()
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            return View();
        }

        // POST: OcupacionMeta/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "idOcupacionRuta,Año,Mes,Meta")] OCUPACION_META oCUPACION_META)
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idOcupacionRuta = new SelectList(db.OCUPACION_RUTA, "idOcupacionRuta", "Descripcion");
            if (ModelState.IsValid)
            {
                db.OCUPACION_META.Add(oCUPACION_META);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                   

                    ViewBag.Error = "No se puede crear registro";
                    return View(oCUPACION_META);

                }

                return RedirectToAction("Index");
            }
            return View(oCUPACION_META);
        }

        // GET: OcupacionMeta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VOLADOS_META vOLADOS_META = db.VOLADOS_META.Find(id);
            if (vOLADOS_META == null)
            {
                return HttpNotFound();
            }
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "idVoladoRuta", "Descripcion");
            return View(vOLADOS_META);
        }

        //POST: OcupacionMeta/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdVoladoMETA,IdVoladoRuta,Año,Mes,Meta")] VOLADOS_META vOLADOS_META)
        {
            Utils utils = new Utils();
            ViewBag.Mes = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Año = utils.ListaAño();
            ViewBag.idVoladoRuta = new SelectList(db.VOLADOS_RUTA, "IdVoladoRuta", "Descripcion");
            if (ModelState.IsValid)
            {
                db.Entry(vOLADOS_META).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Error = "No se puede actualizar registro";
                    return View(vOLADOS_META);

                }

                return RedirectToAction("Index");
            }

            return View(vOLADOS_META);
        }

    }
}
