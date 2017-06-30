using System;
using Sitecore.ContentSearch;

namespace SiteCoreTrainings.Data.Search
{
    //[PredefinedQuery("Author", ComparisonType.Equal, "{37CC391D-05E8-42B1-8FFA-97A1BEDFD258}", typeof(Guid))]
    public class ArticleSearchItem : BaseSearchResultItem
    {
        [IndexField("author")]
        //[SitecoreField("Author")]
        public Guid Author { get; set; }
    }
}
