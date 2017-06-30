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

namespace SiteCoreTrainings.Data.Services
{
    public class ArticleService : BaseService
    {
        public IArticlesPage GetArticlesPage(int page, int pageSize, ID startPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item)))
            {
                IArticlesPage articlesPage = _sitecoreWebDbService.CreateType<IArticlesPage>(Sitecore.Context.Database.GetItem(startPath));
                articlesPage.ArticlesList = new List<IArticleDetails>();

                var query = context.GetQueryable<SearchResultItem>();
                query = query
                    .Where(i => i.Paths.Contains(startPath) && i.TemplateId == BlogConstants.ArticleDetailsTemplateId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                foreach (var item in query)
                {
                    var aritem = item.GetItem();
                    IArticleDetails article = _sitecoreWebDbService.CreateType<IArticleDetails>(aritem);
                    if (article != null)
                    {
                        articlesPage.ArticlesList.Add(article);
                    }
                }
                return articlesPage;
            }
        }

        public IArticlesPage GetArticlesPageV2(int page, int pageSize)
        {
            Item articlesPageItem =
                _database.SelectSingleItem(BlogConstants.FastArticlesPageSitecoreQuery);

            IArticlesPage articlesPage = _sitecoreWebDbService.CreateType<IArticlesPage>(articlesPageItem);
            articlesPage.ArticlesList = new List<IArticleDetails>();

            IQueryable<Item> articlesItems =
                _database.SelectItems(
                        "fast:/sitecore/content/#trainings-site#/home/articles/*[@@templatename = 'Article Details']")
                    .AsQueryable().Skip((page - 1) * pageSize).Take(pageSize);

            foreach (var article in articlesItems)
            {
                articlesPage.ArticlesList.Add(_sitecoreWebDbService.CreateType<IArticleDetails>(article));
            }

            return articlesPage;
        }

        public void InsertComment(IArticleDetails page, Comment comment)
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
