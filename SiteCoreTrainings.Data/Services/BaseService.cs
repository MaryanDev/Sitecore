using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using SiteCoreTrainings.Data.Constants;

namespace SiteCoreTrainings.Data.Services
{
    public class BaseService
    {
        protected readonly SitecoreService _sitecoreWebDbService;
        protected readonly Database _database;

        protected readonly Database _masterDatabase;
        protected readonly SitecoreService _sitecoreMasterDbService;

        public BaseService()
        {
            this._database = Database.GetDatabase("web");
            this._sitecoreWebDbService = new SitecoreService(this._database);

            this._masterDatabase = Database.GetDatabase("master");
            this._sitecoreMasterDbService = new SitecoreService(this._masterDatabase);
        }

        public int GetCountOfPages(int pageSize, ID startPath)
        {
            using (
                var context =
                    Sitecore.ContentSearch.ContentSearchManager.CreateSearchContext(
                        new Sitecore.ContentSearch.SitecoreIndexableItem(Sitecore.Context.Item)))
            {
                var query = context.GetQueryable<SearchResultItem>();
                var count = query.Count(i => i.Paths.Contains(startPath)
                    && (i.TemplateId == BlogConstants.ArticleDetailsTemplateId
                    || i.TemplateId == BlogConstants.AuthorDetailsTemplateId));

                var pages = count / pageSize;
                int result = count % pageSize == 0 ? pages : ++pages;

                return result;
            }
        }
    }
}
