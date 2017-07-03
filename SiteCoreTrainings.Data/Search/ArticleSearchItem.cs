using System;
using Sitecore.ContentSearch;

namespace SiteCoreTrainings.Data.Search
{
    public class ArticleSearchItem : BaseSearchResultItem
    {
        [IndexField("article_author")]
        public Guid ArticleAuthor { get; set; }
    }
}
