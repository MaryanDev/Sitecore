using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCoreTrainings.Data.Models
{
    public interface IArticlesPage : IContentBase
    {
        string Title { get; set; }

        List<IArticleDetails> ArticlesList { get; set; }

        PaginationDetails Pagination { get; set; }
    }
}
