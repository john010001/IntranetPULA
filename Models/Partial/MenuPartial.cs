using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//using AdminRP.Models.Metadata;
//using AdminRP.Models.Profile;
using System.Web.Mvc;
//using AdminRP.Models.Partial;
using Microsoft.Ajax.Utilities;
using Intranet.Models.Data;
using Intranet.Models.Metadata;
using Intranet.Models.Partial;

namespace Intranet.Models.Data
{

    [MetadataType(typeof(MenuMetadata))]
    public partial class MENU
    {

        private Usuario_Entities db = new Usuario_Entities();
        public List<MENU> ListarMenu(/*int IdUsuario*/)
        {
            List<MENU> _MENU = db.MENU.ToList();
            return _MENU;
        }

        public List<MENU> ListarMenuPrincipal()
        {
            int[] _MenuRol = db.MENU_ROL.Select(s => s.Id).ToArray();

            List<MENU> _MENU = db.MENU.Where(w => w.Nivel == "1" && _MenuRol.Contains(w.Id)).ToList();
            return _MENU;
        }
        public List<MENU> ListarMenuPrincipal(/*int grupo,*/ int rol)
        {
            int[] _MenuRol = db.MENU_ROL.Where(w => w.IdRol == rol /*w.codigo_rol == rol*/).Select(s => s.Id).ToArray();

            List<MENU> _MENU = db.MENU.Where(w => w.Nivel == "1" && _MenuRol.Contains(w.Id)).ToList();

            return _MENU;
        }
        //public List<MENU> ListarMenuPrincipal(int IdUsuario)
        //{
        //    USUARIO _usuario = db.USUARIO.Where(w => w.id_usuario == IdUsuario).FirstOrDefault();

        

        //    int[] _MenuGrupo = db.MENU_ROL_GRUPO.Where(w => w.codigo_grupo == _usuario.codigo_grupo
        //    && w.codigo_rol == _usuario.codigo_rol && w.MENU.principal_menu.HasValue && w.MENU.principal_menu.Value > 0).GroupBy(s => s.MENU.principal_menu.Value).Select(t => t.Key).ToArray();

        //    List<MENU> _MENU = db.MENU.Where(w => _MenuGrupo.Contains(w.Id_Menu)).ToList();
        //    return _MENU;
        //}
        public List<MENU> ListarMenuSecundario(int MenuPadre)
        {
            //int[] _MenuGrupo = db.MENU_ROL_GRUPO.Select(s => s.Id_Menu).ToArray();

            List<MENU> _MENU = db.MENU.Where(w => w.Principal == MenuPadre).ToList();
            //  OrderBy(o=>o.descripcion_menu).ToList();
            return _MENU;
        }
        public List<MENU> ListarMenuSecundario( int MenuPadre/*, int grupo*/, int rol)
        {
            int[] _MenuRol = db.MENU_ROL.Where(w => w.IdRol == rol/* && w.codigo_rol == rol*/).Select(s => s.Id).ToArray();

            List<MENU> _MENU = db.MENU.Where(w => w.Principal == MenuPadre && _MenuRol.Contains(w.Id)).ToList();
            //  OrderBy(o=>o.descripcion_menu).ToList();
            return _MENU;
        }
        //public List<MENU> ListarMenuSecundario(int IdUsuario, int MenuPadre)
        //{

        //    USUARIO _usuario = db.USUARIO.Where(w => w.id_usuario == IdUsuario).FirstOrDefault();

        //    int[] _MenuGrupo = db.MENU_ROL_GRUPO.Where(w => w.codigo_grupo == _usuario.codigo_grupo && w.codigo_rol == _usuario.codigo_rol).Select(s => s.Id_Menu).ToArray();

        //    List<MENU> _MENU = db.MENU.Where(w => w.principal_menu == MenuPadre && _MenuGrupo.Contains(w.Id_Menu)).ToList();
        //    //  OrderBy(o=>o.descripcion_menu).ToList();
        //    return _MENU;
        //}

        public List<MenuModel> ListarMenuSecundarioTreeView(List<int> idList, int idMenuPadre)
        {
            return this.ListarMenuSecundario(idMenuPadre).Select(a => new MenuModel {
                Menu = a,
                Checked = idList.Contains(a.Id),
            }).ToList();
        }
        public List<SelectListItem> ListaNiveles()
        {
            
            List<SelectListItem> _ListaAnio = new List<SelectListItem>();
            _ListaAnio.Add(new SelectListItem { Text = "", Value = "0" });
            _ListaAnio.Add(new SelectListItem { Text = "1", Value = "1" });
            _ListaAnio.Add(new SelectListItem { Text = "2", Value = "2" });


            return _ListaAnio;

        }

        public List<SelectListItem> ListarMenuPadre()
        {

            List<SelectListItem> _ListaPaisesPresencia = new List<SelectListItem>();
            var _paises = db.MENU.Where(w => w.Principal == 0).OrderBy(o => o.Descripcion);

            _ListaPaisesPresencia.Add(new SelectListItem { Text ="" , Value = "0" });

            foreach (var item in _paises)
            {
                _ListaPaisesPresencia.Add(new SelectListItem { Text = item.Descripcion, Value = item.Id.ToString()});
            }

            return _ListaPaisesPresencia;

        }


        public List<MenuModel> ListarMenuTreeView(int codigoRol)
        {
            var _MenuRol = db.MENU_ROL.Where(s => s.IdRol == codigoRol).Select(b => b.Id).ToList();
            //var _IdsMenuGrupo = db.MENU_ROL.Where(s => s.IdRol == codigoRol).Select(b => b.Id).ToList();
            var list = db.MENU.Where(b => b.Nivel == "1").ToList();

            //List<MENU> _listaTreeView = new List<MENU>();
            List<MenuModel> _listaTreeView = db.MENU.Where(b => b.Nivel == "1" )
                .Select(a => 
                    new MenuModel { 
                        Menu = a,
                        Checked = _MenuRol.Contains(a.Id),
                    })
                .ToList();

            foreach(var item in _listaTreeView)
            {
                item.SubMenus = item.Menu.ListarMenuSecundarioTreeView(_MenuRol, item.Menu.Id);
            }

            //using (var bd = new DB_ProfileRP())
            //{

                //_listaTreeView = (from menu_rol_grupo in db.MENU_ROL_GRUPO 
                //            join menu in db.MENU
                //            on menu_rol_grupo.Id_Menu equals menu.Id_Menu
                //            //where rolpagina.BHABILITADO == 1
                //            select new MENU
                //            {
                //                Id_Menu=menu_rol_grupo.Id_Menu,
                                


                //                //iidrolPagina = rolpagina.IIDROLPAGINA,
                //                //iidRol = rol.IIDROL,
                //                //iidPagina = pagina.IIDPAGINA
                //            }).ToList();
            //}
            return _listaTreeView;
        }
    }
        //int[] _MenuGrupo = db.MENU_ROL_GRUPO.Select(s => s.Id_Menu).ToArray();

        //    List<MENU> _MENU = db.MENU.Where(w => w.nivel_menu == "1" && _MenuGrupo.Contains(w.Id_Menu)).ToList();
        //    return _MENU;

       

}

