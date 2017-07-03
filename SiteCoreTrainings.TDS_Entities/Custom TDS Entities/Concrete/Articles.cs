using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Articles
    {
        public List<Article_Details> ArticlesList { get; set; }
        public PaginationDetails Pagination { get; set; }
    }
}
