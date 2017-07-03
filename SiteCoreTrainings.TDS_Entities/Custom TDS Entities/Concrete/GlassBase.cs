using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen
{
    public partial interface IGlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Url)]
        string Url { get; }
    }

    public abstract partial class GlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; private set; }
    }

}
