using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace SiteCoreTrainings.Data.Models
{
    [SitecoreType(AutoMap = true)]
    public interface IContentBase : IGlassBase
    {
        [SitecoreId]
        ID Id { get; set; }
        [SitecoreInfo(SitecoreInfoType.Url)]
        string Url { get; set; }
    }
}
