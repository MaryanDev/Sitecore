using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using SiteCoreTrainings.TDS_Entities.TDS_Gen;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen
{
    public abstract partial class GlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Url)]
        public string Url { get; private set; }
    }
}
