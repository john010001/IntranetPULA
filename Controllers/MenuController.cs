using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using AdminRP.Models.Profile;
using Intranet.Models.Data;

namespace Intranet.Controllers
{
    public class MenuController : Controller
    {
        private Usuario_Entities db = new Usuario_Entities();
        //private BD_Profile db = new BD_Profile();

        // GET: Menu
        [HttpGet]
        
        public ActionResult Index()
        {
            //USUARIO _Usuario = (USUARIO)Session["USUARIO"];
            //var usuario = userClaims.FindFirst("preferred_username").Value;

            MENU _menu = new MENU();
            ViewBag.Niveles = _menu.ListaNiveles();
            ViewBag.MenuPadre = _menu.ListarMenuPadre();
            return View(db.MENU.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Descripcion,string Niveles,string MenuPadre,string Boton)
        {
               ViewBag.Descripcion =( Boton == "Limpia") ?"" :Descripcion;

            MENU _menu = new MENU();
            ViewBag.Niveles = _menu.ListaNiveles();
            ViewBag.MenuPadre = _menu.ListarMenuPadre();

            var _filtroMenu = db.MENU.Where(w => w.Descripcion.Contains(Descripcion));
            if (Niveles!="0")
            {
                _filtroMenu = _filtroMenu.Where(w => w.Nivel == Niveles);
            }
            if (MenuPadre != "0")
            {
                int _menuPadre = int.Parse(MenuPadre);
                _filtroMenu = _filtroMenu.Where(w => w.Principal.Value == _menuPadre);
            }
                        
            return View(_filtroMenu.ToList());
        }


        // GET: Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // GET: Menu/Create
        [HttpGet]
        public ActionResult Create()
        {
            MENU _menu = new MENU();
            ViewBag.nivel_menu = _menu.ListaNiveles();
            ViewBag.principal_menu= _menu.ListarMenuPadre();


            return View();
        }

        // POST: Menu/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,Nivel,Url,Principal,Imagen")] MENU mENU)
        {
         

            if (ModelState.IsValid)
            {
                db.MENU.Add(mENU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            MENU _menu = new MENU();
            ViewBag.nivel_menu = _menu.ListaNiveles();
            ViewBag.principal_menu = _menu.ListarMenuPadre();
            return View(mENU);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: Menu/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Menu,descripcion_menu,nivel_menu,url_menu,principal_menu,imagen_menu")] MENU mENU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mENU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mENU);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MENU mENU = db.MENU.Find(id);
            db.MENU.Remove(mENU);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Asignar(int rol = 0, int grupo = 0)
        {
           
            
            //USUARIO _Usuario = (USUARIO)Session["USUARIO"];

            
            if (rol == 0 /*&& grupo == 0*/)
            {
                //rol = _Usuario.codigo_rol;
                //grupo = _Usuario.codigo_grupo;
            }

            //ViewBag.codigo_grupo = new SelectList(db.GRUPO, "codigo_grupo", "descripcion_grupo", grupo);
            ViewBag.codigo_rol = new SelectList(db.ROL, "codigo_rol", "descripcion_rol", rol);

            
            MENU _menu = new MENU();

            var menuTreeView = _menu.ListarMenuTreeView(rol);
            var listMenu = menuTreeView.Where(a => a.Checked).Select(b => b.Menu.Id + "-" + b.Menu.Principal.Value).ToList();
            var listSubMenus = menuTreeView.SelectMany(a => a.SubMenus.Where(b => b.Checked).Select(b => b.Menu.Id + "-" + b.Menu.Principal.Value).ToList()).ToList();
            string selectedIds = "";
            if(listMenu.Count > 0)
            {
                selectedIds += listMenu.Aggregate((a, b) => a + "&" + b) + "&";
            }

            if (listSubMenus.Count > 0)
            {
                selectedIds += listSubMenus.Aggregate((a, b) => a + "&" + b) + "&";
            }

            ViewBag.Ids = selectedIds;
            //ViewBag.MenuPrincipalUsuario = _menu.ListarMenuPrincipal(_Usuario.id_usuario);
            ViewBag.MenuPrincipal = menuTreeView;

            
            //Esta linea se agrego 
            //ViewBag.listarMenuTreeView = _menu.ListarMenuTreeView();

            return View();
        }

        //public JsonResult GetArbol(int idgrupo, int idrol)
        //{

        //    MENU _menu = new MENU();
        //    var menuTreeView = _menu.ListarMenuTreeView(idrol);
        //    var listMenu = menuTreeView.Where(a => a.Checked).Select(b => b.Menu.Id_Menu + "-" + idgrupo).ToList();
        //    var listSubMenus = menuTreeView.SelectMany(a => a.SubMenus.Where(b => b.Checked).Select(b => b.Menu.Id_Menu + "-" + idgrupo).ToList())/*.ToList()*/;
        //    ViewBag.MenuPrincipalUsuario = new List<MENU>();
        //    ViewBag.MenuPrincipal = menuTreeView;

        //    string html = Helper.RazorViewToString.RenderRazorViewToString(this, "parcial/_MenuOpciones", null);
        //    return Json(html);
        //}


        [HttpGet]
        public ActionResult Buscar()
        {
            return RedirectToAction("Asignar");
        }


        [HttpPost]
        public ActionResult Buscar(int codigo_rol/*, int codigo_grupo*/)
        {
            MENU _menu = new MENU();
            var menuTreeView = _menu.ListarMenuTreeView(codigo_rol);
            var listMenu = menuTreeView.Where(a => a.Checked).Select(b => b.Menu.Id + "-" + b.Menu.Principal).ToList();
            var listSubMenus = menuTreeView.SelectMany(a => a.SubMenus.Where(b => b.Checked).Select(b => b.Menu.Id + "-" + b.Menu.Principal).ToList()).ToList();
            string selectedIds = "";
            if (listMenu.Count > 0)
            {
                selectedIds += listMenu.Aggregate((a, b) => a + "&" + b) + "&";
            }

            if (listSubMenus.Count > 0)
            {
                selectedIds += listSubMenus.Aggregate((a, b) => a + "&" + b) + "&";
            }

            ViewBag.Ids = selectedIds;
            //ViewBag.codigo_grupo = new SelectList(db.GRUPO, "codigo_grupo", "descripcion_grupo");
            ViewBag.codigo_rol = new SelectList(db.ROL, "Id", "Descripcion");
            ViewBag.MenuPrincipalUsuario = _menu.ListarMenuPrincipal( /*codigo_grupo, */ codigo_rol );
            ViewBag.MenuPrincipal = menuTreeView;

            return View("Asignar");
            //return RedirectToAction(menucheck);
        }

        [HttpPost]
        public ActionResult AsignarMenu(int codigoRol/*, int codigoGrupo*/, string menucheck) 
        {
            //int codRol = Convert.ToInt32(codigoRol);
            //int codGrupo = Convert.ToInt32(codigoGrupo);

            //USUARIO _Usuario = (USUARIO)Session["USUARIO"];
            var menus = menucheck.Split(',');
            var listmenu = new List<string>();
            var menusBD = db.MENU.ToList();
            var menuRolGrupo = db.MENU_ROL.Where(a => a.IdRol == codigoRol /*&& a.codigo_grupo == codigoGrupo*/).ToList();

            foreach (var menu in menusBD)
            {
                var codeMenu = menu.Id + "-" + menu.Principal.Value;
                var existe = menuRolGrupo.Where(a => a.Id == menu.Id).FirstOrDefault();

                if (!menus.Contains(codeMenu))
                {
                    if(existe != null)
                    {
                        //if(menu.nivel_menu.Trim() == "1")
                        //{
                        //    var tieneHijos = menuRolGrupo.Where(a => a.MENU.principal_menu == menu.Id_Menu).FirstOrDefault();
                        //    if(tieneHijos == null)
                        //    {
                        //        db.MENU_ROL_GRUPO.Remove(existe);
                        //    }
                        //}
                        //else if(menu.nivel_menu.Trim() != "1")
                        //{
                            db.MENU_ROL.Remove(existe);
                        //}
                    }
                }
                else
                {
                    if(existe == null)
                    {
                        db.MENU_ROL.Add(
                        new MENU_ROL()
                        {
                            Id = menu.Id,
                            //codigo_grupo = codigoGrupo,
                            IdRol = codigoRol,
                            //Usuario = _Usuario.nom_user,
                            FechaCreacion = DateTime.Now
                        });
                    }
                }
            }
          
            db.SaveChanges();
            return RedirectToAction("Asignar", new { rol = codigoRol/*, grupo = codigoGrupo  */}); 
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
