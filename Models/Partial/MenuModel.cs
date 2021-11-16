//using AdminRP.Models.Profile;
using Intranet.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models.Partial
{
    public class MenuModel
    {
        public MenuModel() {
            this.Menu = new MENU();
            this.SubMenus = new List<MenuModel>();
        }

        public MENU Menu { get; set; }
        public List<MenuModel> SubMenus { get; set; }
        public int CodigoGrupo { get; set; }
        public bool Checked { get; set; }
    }
}