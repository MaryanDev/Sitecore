using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Author
    {
        public List<Article_Details> Articles { get; set; }

        [SitecoreChildren]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
