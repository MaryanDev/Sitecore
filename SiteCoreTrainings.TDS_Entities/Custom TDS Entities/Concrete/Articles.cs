using System.Collections.Generic;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Articles
    {
        public List<Article_Details> ArticlesList { get; set; }
        public PaginationDetails Pagination { get; set; }
    }
}
