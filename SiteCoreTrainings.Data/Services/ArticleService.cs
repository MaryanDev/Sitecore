using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Sitecore.Web.UI.HtmlControls;
using SiteCoreTrainings.Data.Constants;
using SiteCoreTrainings.Data.Models;
using SiteCoreTrainings.Data.Search;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;
using Comment = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.Comment;

namespace SiteCoreTrainings.Data.Services
{
    public class ArticleService : BaseService
    {
        public Articles GetArticlesPage(int page, int pageSize, ID startPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item)))
            {
                Articles articlesPage = _sitecoreWebDbService.CreateType<Articles>(Sitecore.Context.Database.GetItem(startPath));
                articlesPage.ArticlesList = new List<Article_Details>();

                var query = context.GetQueryable<SearchResultItem>();
                query = query
                    .Where(i => i.Paths.Contains(startPath) && i.TemplateId == BlogConstants.ArticleDetailsTemplateId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                foreach (var item in query)
                {
                    var aritem = item.GetItem();
                    Article_Details article = _sitecoreWebDbService.CreateType<Article_Details>(aritem);
                    if (article != null)
                    {
                        articlesPage.ArticlesList.Add(article);
                    }
                }
                return articlesPage;
            }
        }

        public Articles GetArticlesPageV2(int page, int pageSize)
        {
            Item articlesPageItem =
                _database.SelectSingleItem(BlogConstants.FastArticlesPageSitecoreQuery);

            Articles articlesPage = _sitecoreWebDbService.CreateType<Articles>(articlesPageItem);
            articlesPage.ArticlesList = new List<Article_Details>();

            IQueryable<Item> articlesItems =
                _database.SelectItems(
                        "fast:/sitecore/content/#trainings-site#/home/articles/*[@@templatename = 'Article Details']")
                    .AsQueryable().Skip((page - 1) * pageSize).Take(pageSize);

            foreach (var article in articlesItems)
            {
                articlesPage.ArticlesList.Add(_sitecoreWebDbService.CreateType<Article_Details>(article));
            }

            return articlesPage;
        }

        public void InsertComment(Article_Details page, Comment comment)
        {
            using (new SecurityDisabler())
            {
                _sitecoreMasterDbService.Create(page, comment);
            }
        }

        public int LikeArticle(string articleId, bool likeOrDislike)
        {
            var articleToLikeWeb = _database.GetItem(articleId);
            var articleToLikeMaster = _masterDatabase.GetItem(articleId);
            using (new SecurityDisabler())
            {
                articleToLikeWeb.Editing.BeginEdit();
                articleToLikeMaster.Editing.BeginEdit();
                try
                {
                    articleToLikeWeb["Likes"] = (likeOrDislike ? Convert.ToInt32(articleToLikeWeb["Likes"]) + 1 : Convert.ToInt32(articleToLikeWeb["Likes"]) - 1).ToString();
                    articleToLikeMaster["Likes"] = (likeOrDislike ? Convert.ToInt32(articleToLikeMaster["Likes"]) + 1 : Convert.ToInt32(articleToLikeMaster["Likes"]) - 1).ToString();

                    articleToLikeWeb.Editing.EndEdit();
                    articleToLikeMaster.Editing.EndEdit();
                    return Convert.ToInt32(articleToLikeWeb["Likes"]);
                }
                catch (Exception)
                {
                    articleToLikeWeb.Editing.CancelEdit();
                    articleToLikeMaster.Editing.CancelEdit();
                    throw;
                }
            }
        }
    }
}
