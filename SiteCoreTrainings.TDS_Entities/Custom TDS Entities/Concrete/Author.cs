using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Author
    {
        public List<Article_Details> Articles { get; set; }

        [SitecoreChildren]
        public IEnumerable<Comment> Comments { get; set; }
    }
}
