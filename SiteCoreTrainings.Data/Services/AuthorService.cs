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
using Comment = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.Comment;

namespace SiteCoreTrainings.Data.Services
{
    public class AuthorService : BaseService
    {
        public Authors GetAuthorsPage(int page, int pageSize, ID startPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item)))
            {
                Authors authorsPage = _sitecoreWebDbService.CreateType<Authors>(Sitecore.Context.Database.GetItem(startPath));
                authorsPage.AuthorsList = new List<Author>();

                var query = context.GetQueryable<SearchResultItem>();
                query = query.Where(i => i.Paths.Contains(startPath) && i.TemplateId == BlogConstants.AuthorDetailsTemplateId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                foreach (var item in query)
                {
                    Author author = _sitecoreWebDbService.CreateType<Author>(item.GetItem());
                    if (author != null)
                    {
                        authorsPage.AuthorsList.Add(author);
                    }
                }
                return authorsPage;
            }
        }

        public Author GetAuthorDetails(ID startPath, ID articlesPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item))
            )
            {
                var author =
                    _sitecoreWebDbService.CreateType<Author>(Sitecore.Context.Database.GetItem(startPath));
                author.Articles = new List<Article_Details>();

                var query = context.GetQueryable<ArticleSearchItem>();
                query = query.Where(a => a.Paths.Contains(articlesPath) && a.Author == author.Id && a.TemplateId == BlogConstants.ArticleDetailsTemplateId);

                foreach (var article in query)
                {
                    var concreteArticle = _sitecoreWebDbService.CreateType<Article_Details>(article.GetItem());

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
