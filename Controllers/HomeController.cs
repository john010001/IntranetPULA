using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace Intranet.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        //[Authorize]
        public ActionResult Index(string id_token, string toke,string opt)
        {
            //var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            ////You get the user’s first and last name below:
            //ViewBag.Name = userClaims?.FindFirst("name")?.Value;


            //if (ViewBag.Name == null)
            //{
            //  //  HttpContext.GetOwinContext().Authentication.SignOut(
            //  //  OpenIdConnectAuthenticationDefaults.AuthenticationType,
            //  //  CookieAuthenticationDefaults.AuthenticationType);

            //   return RedirectToAction("SignIn");
            //}


            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult Index(string id_token, string token)
        {
            return View();
        }
        /// <summary>
        /// Send an OpenID Connect sign-in request.
        /// Alternatively, you can just decorate the SignIn method with the [Authorize] attribute
        /// </summary>
        [HttpGet]
        public ActionResult Logout(string id_token, string toke, string opt)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            //You get the user’s first and last name below:
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

           

            return View();
        }
        //[Authorize]
        public void SignIn()
        {

            if (!Request.IsAuthenticated)
            {
                //HttpContext.Response.SuppressFormsAuthenticationRedirect = true;                
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
         }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);



        }
    }
}