using System;
using System.Security.Authentication;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Security.Authentication;
using Sitecore.SecurityModel;
using SiteCoreTrainings.Data.Models;
using SiteCoreTrainings.Data.Services;
using SiteCoreTrainings.Models.ViewModels;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;
using Comment = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.Comment;
using PaginationDetails = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.PaginationDetails;

namespace SiteCoreTrainings.Controllers
{
    [Authorize]
    public class ArticleController : BaseController
    {
        private readonly ArticleService _articleService;
        private const int PageSize = 3;

        public ArticleController()
        {
            _articleService = new ArticleService();
        }
        public ActionResult Listing(int page = 1)
        {
            var articlesPage = _articleService.GetArticlesPage(page, PageSize, Sitecore.Context.Item.ID);

            articlesPage.Pagination = new PaginationDetails
            {
                AllPages = _articleService.GetCountOfPages(PageSize, Sitecore.Context.Item.ID),
                CurrentPage = page,
                Url = articlesPage.Url
            };

            return View(articlesPage);
        }

        [AllowAnonymous]
        public ActionResult Navbar()
        {
            var context = SitecoreContext.GetHomeItem<IMenuItem>();
            ViewBag.loginPageNavbarItem = SitecoreContext.GetItem<IMenuItem>("{C1CD4D57-82A3-4AEE-9049-DFADCDCB7B44}");
            ViewBag.registerPageNavbarItem = SitecoreContext.GetItem<IMenuItem>("{F2CD451B-F96A-4509-95A6-D0FAF3D082D9}");
            return View(context);
        }

        public ActionResult ArticleDetails()
        {
            var context = SitecoreContext.GetCurrentItem<Article_Details>();
            return View(context);
        }

        [HttpGet]
        public ActionResult CommentsForm()
        {
            return View();
        }

        [HttpPost]
        //[ValidateFormHandler]
        public ActionResult CommentsForm(CommentViewModel comment)
        {
            Article_Details page = SitecoreContext.GetCurrentItem<Article_Details>();
            if (ModelState.IsValid)
            {
                var commentToAdd = new Comment
                {
                    Id = Guid.NewGuid(),
                    Comment_Text = comment.CommentText,
                    Comment_Author = comment.CommentAuthor,
                    DateCreated = DateTime.Now,
                    Name = comment.CommentAuthor + " " + DateTime.Now.ToString("yyyy MMMM dd")
                };
                _articleService.InsertComment(page, commentToAdd);
            }

            //var url = LinkManager.GetItemUrl(page);
            return Redirect(page.Url);
        }

        [HttpPost]
        public JsonResult UpdateArticleLikes(string articleId)
        {
            try
            {
                int likes;
                if (Session[articleId] == null || !(bool)Session[articleId])
                {
                    likes = _articleService.LikeArticle(articleId, true);
                    Session[articleId] = true;
                }
                else
                {
                    likes = _articleService.LikeArticle(articleId, false);
                    Session[articleId] = false;
                }
                
                return Json(new { success = true, likes = likes });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

    }
}