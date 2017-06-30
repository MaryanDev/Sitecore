using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.SecurityModel;
using SiteCoreTrainings.Data.Constants;
using SiteCoreTrainings.Data.Models;
using SiteCoreTrainings.Data.Search;
using Sitecore.Data.Items;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;
using Comment = SiteCoreTrainings.Data.Models.Comment;

namespace SiteCoreTrainings.Data.Services
{
    public class AuthorService : BaseService
    {
        public IAuthorsPage GetAuthorsPage(int page, int pageSize, ID startPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item)))
            {
                IAuthorsPage authorsPage = _sitecoreWebDbService.CreateType<IAuthorsPage>(Sitecore.Context.Database.GetItem(startPath));
                authorsPage.AuthorsList = new List<IAuthorDetails>();

                var query = context.GetQueryable<SearchResultItem>();
                query = query.Where(i => i.Paths.Contains(startPath) && i.TemplateId == BlogConstants.AuthorDetailsTemplateId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                foreach (var item in query)
                {
                    IAuthorDetails author = _sitecoreWebDbService.CreateType<IAuthorDetails>(item.GetItem());
                    if (author != null)
                    {
                        authorsPage.AuthorsList.Add(author);
                    }
                }
                return authorsPage;
            }
        }

        public IAuthorDetails GetAuthorDetails(ID startPath, ID articlesPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item))
            )
            {
                var author =
                    _sitecoreWebDbService.CreateType<IAuthorDetails>(Sitecore.Context.Database.GetItem(startPath));
                author.Articles = new List<IArticleDetails>();

                var query = context.GetQueryable<ArticleSearchItem>();
                query = query.Where(a => a.Paths.Contains(articlesPath) && a.Author == author.Id.Guid && a.TemplateId == BlogConstants.ArticleDetailsTemplateId);

                foreach (var article in query)
                {
                    var concreteArticle = _sitecoreWebDbService.CreateType<IArticleDetails>(article.GetItem());

                    author.Articles.Add(concreteArticle);
                }
                return author;
            }
        }

        public void InsertComment(IAuthorDetails page, Comment comment)
        {
            using (new SecurityDisabler())
            {
                _sitecoreMasterDbService.Create(page, comment);
            }
        }
    }
}
