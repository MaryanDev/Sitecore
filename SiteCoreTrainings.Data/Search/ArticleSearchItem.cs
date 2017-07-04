using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.ContentSearch;

namespace SiteCoreTrainings.Data.Search
{
    public class ArticleSearchItem : BaseSearchResultItem
    {
        [SitecoreField("Article Author")]
        [IndexField("article_author")]
        public Guid Article_Author { get; set; }
    }
}
