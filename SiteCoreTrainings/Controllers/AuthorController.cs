﻿using System;
using System.Web.Mvc;
using Sitecore.Data;
using SiteCoreTrainings.Data.Services;
using SiteCoreTrainings.Models.ViewModels;
using SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages;
using PaginationDetails = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.PaginationDetails;
using Comment = SiteCoreTrainings.TDS_Entities.TDS_Gen.Trainings.Pages.Comment;

namespace SiteCoreTrainings.Controllers
{
    [Authorize]
    public class AuthorController : BaseController
    {
        private const int PageSize = 3;
        private readonly AuthorService _authorService;

        public AuthorController()
        {
            _authorService = new AuthorService();
        }

        public ActionResult AuthorsListing(int page = 1)
        {
            var authors = _authorService.GetAuthorsPage(page, PageSize, Sitecore.Context.Item.ID);
            authors.Pagination = new PaginationDetails
            {
                AllPages = _authorService.GetCountOfPages(PageSize, Sitecore.Context.Item.ID),
                CurrentPage = page,
                Url = authors.Url
            };
            return View(authors);
        }

        public ActionResult AuthorDetails()
        {
            var author = _authorService.GetAuthorDetails(Sitecore.Context.Item.ID, new ID("{83AAC039-6631-4E5D-81F3-8643EA4079EC}"));
            return View(author);
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
            Author page = SitecoreContext.GetCurrentItem<Author>();
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
                _authorService.InsertComment(page, commentToAdd);
            }

            //var url = LinkManager.GetItemUrl(page);
            return Redirect(page.Url);
        }
    }
}