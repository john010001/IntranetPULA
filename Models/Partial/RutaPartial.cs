using Intranet.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Models.Data
{
    [MetadataType(typeof(RutaMetadata))]
    public partial class RUTA
    {
        private IntranetDBEntities db = new IntranetDBEntities();
        public bool buscarRuta( string codigoRuta_)
        {
            List<RUTA> lRuta= db.RUTA.Where(x => x.CodigoRuta == codigoRuta_).ToList();
            if (lRuta.Count()>0 )
            {
                return true;
            }
           
          

            return false;
        }
    }
}