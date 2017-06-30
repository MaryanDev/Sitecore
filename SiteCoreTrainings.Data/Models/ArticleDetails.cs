using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace SiteCoreTrainings.Data.Models
{
    [SitecoreType]
    public interface IArticleDetails : IContentBase
    {
        [SitecoreField("Page Title")]
        string PageTitle { get; set; }

        [SitecoreField]
        DateTime Date { get; set; }

        [SitecoreField("Main Body")]
        string MainBody { get; set; }

        [SitecoreField("Article Image")]
        Image ArticleImage { get; set; }

        [SitecoreField("Author")]
        IAuthorDetails Author { get; set; }

        [SitecoreField("Likes")]
        int Likes { get; set; }

        [SitecoreChildren]
        IEnumerable<Comment> Comments { get; set; }
    }
}
