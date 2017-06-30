using System;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;

namespace SiteCoreTrainings.Data.Models
{
    [SitecoreType(TemplateId = "{6B2711C9-67D8-43A2-AA0C-8AFE9393D723}")]
    public class Comment : IContentBase
    {
        [SitecoreField("Comment Text", Setting = SitecoreFieldSettings.RichTextRaw)]
        public string CommentText { get; set; }

        [SitecoreField("Comment Author")]
        public string CommentAuthor { get; set; }

        [SitecoreField("__Created")]
        public DateTime DateCreated { get; set; }
        public ID Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
    }
}
