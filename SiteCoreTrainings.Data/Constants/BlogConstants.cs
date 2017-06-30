using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace SiteCoreTrainings.Data.Constants
{
    public static class BlogConstants
    {
        public static string HomePagePath = "/sitecore/content/#trainings-site#/home";

        public static string ArticlesPagePath = "/sitecore/content/#trainings-site#/home/articles";

        public static string AuthorsPagePath = "/sitecore/content/#trainings-site#/home/authors";

        public static readonly string HomePageSitecoreQuery = string.Concat("query:", HomePagePath);
        public static readonly string FastHomePageSitecoreQuery = string.Concat("fast:", HomePagePath);

        public static string ArticlesPageSitecoreQuery = string.Concat("query:", ArticlesPagePath);
        public static string FastArticlesPageSitecoreQuery = string.Concat("fast:", ArticlesPagePath);

        public static string AuthorsPageSitecoreQuery = string.Concat("query:", AuthorsPagePath);
        public static string FastAuthorsPageSitecoreQuery = string.Concat("fast:", AuthorsPagePath);

        public static ID ArticleDetailsTemplateId = new ID("{11A7B1BF-F409-4A99-9706-6B24224EAE33}");
        public static ID AuthorDetailsTemplateId = new ID("{37CC391D-05E8-42B1-8FFA-97A1BEDFD258}");

    }
}
