using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.ModelsApp;

namespace Intranet.Controllers
{
    public class ResumenController : Controller
    {
        // GET: Resumen
        [Authorize]
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            //You get the user’s first and last name below:
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            // The 'preferred_username' claim can be used for showing the username
            ViewBag.Username = userClaims?.FindFirst("preferred_username")?.Value;

            // The subject/ NameIdentifier claim can be used to uniquely identify the user across the web
            ViewBag.Subject = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // TenantId is the unique Tenant Id - which represents an organization in Azure AD
            ViewBag.TenantId = userClaims?.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;

            USUARIO uSUARIO = new USUARIO();

            uSUARIO.Name = userClaims?.FindFirst("name")?.Value;
            uSUARIO.Username= userClaims?.FindFirst("preferred_username")?.Value;
            uSUARIO.Subject = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            //Session["USUARIO"] = uSUARIO;



            List<string> _directivos = new List<string>();

            _directivos.Add("g.preziuso@plusultra.com");
            _directivos.Add("a.delgado@plusultra.com");
            _directivos.Add("r.roselli@plusultra.com");
            _directivos.Add("j.carrano@plusultra.com");
            _directivos.Add("f.arellano@plusultra.com");
            _directivos.Add("z.aroutin@plusultra.com");
            _directivos.Add("testpremier2@plusultra.com");

            List<string> _ventasVolados = new List<string>();

            _ventasVolados.Add("j.carrano@plusultra.com");



            List<string> _ocupaciones = new List<string>();

            _ocupaciones.Add("l.montilla@plusultra.com");



            if (_directivos.IndexOf(uSUARIO.Username) >= 0)
            {
                return RedirectToAction("resumen", "ventas");
            }
            if (_ventasVolados.IndexOf(uSUARIO.Username) >= 0)
            {
                return RedirectToAction("resumen", "ventas");
            }
            if (_ocupaciones.IndexOf(uSUARIO.Username) >= 0)
            {
                return RedirectToAction("resumen", "ocupacion");
            }



            return View();
        }

        // GET: Resumen/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Resumen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resumen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resumen/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Resumen/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resumen/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Resumen/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
