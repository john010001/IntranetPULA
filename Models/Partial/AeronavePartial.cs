using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Intranet.Models.Metadata;

namespace Intranet.Models.Data
{
    [MetadataType(typeof(AeronaveMetadata))]
    public partial class AERONAVES
    {
    }
}