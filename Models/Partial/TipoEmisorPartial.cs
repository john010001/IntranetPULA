using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models.Metadata;
using Intranet.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.Data
{
    [MetadataType(typeof(TipoEmisorMetadata))]
    
    public partial class TIPOEMISOR
    {
        public bool mostrar()
        {
            return true;

        }
    }
}