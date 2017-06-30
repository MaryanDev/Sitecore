using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCoreTrainings.Data.Models
{
    public interface IAuthorsPage : IContentBase
    {
        string Title { get; set; }
        List<IAuthorDetails> AuthorsList { get; set; }

        PaginationDetails Pagination { get; set; }
    }
}
