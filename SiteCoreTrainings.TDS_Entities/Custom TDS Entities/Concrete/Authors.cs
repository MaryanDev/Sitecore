using System.Collections.Generic;

namespace SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages
{
    public partial class Authors
    {
        public List<Author> AuthorsList { get; set; }

        public PaginationDetails Pagination { get; set; }
    }
}
