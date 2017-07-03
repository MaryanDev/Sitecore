using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Authors
    {
        public List<Author> AuthorsList { get; set; }

        public PaginationDetails Pagination { get; set; }
    }
}
