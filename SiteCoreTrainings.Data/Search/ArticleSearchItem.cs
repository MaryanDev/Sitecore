using System;
using Sitecore.ContentSearch;

namespace SiteCoreTrainings.Data.Search
{
    public class ArticleSearchItem : BaseSearchResultItem
    {
        [IndexField("author")]
        public Guid Author { get; set; }
    }
}
