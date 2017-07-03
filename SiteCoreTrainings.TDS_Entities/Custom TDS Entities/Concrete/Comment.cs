using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Comment
    {
        public virtual DateTime DateCreated { get; set; }
        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string Name { get; set; }
    }
}
