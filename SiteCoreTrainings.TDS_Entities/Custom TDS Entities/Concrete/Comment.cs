using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration.Fluent;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Comment
    {
        public virtual DateTime DateCreated { get; set; }
        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string Name { get; set; }
    }
}
