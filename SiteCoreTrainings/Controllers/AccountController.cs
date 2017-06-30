using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Sitecore.Security.Authentication;
using SiteCoreTrainings.Models.ViewModels.Account;

namespace SiteCoreTrainings.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (Sitecore.Context.User.IsAuthenticated)
            {
                return Redirect("/");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string domainUser = Sitecore.Context.Domain.GetFullName(loginModel.UserName);
                if (Sitecore.Security.Authentication.AuthenticationManager.Login(domainUser, loginModel.Password))
                {
                    //string returnUrl = System.Web.HttpContext.Current.Request["url"];
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "/";
                    return Redirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string domainUser = Sitecore.Context.Domain.GetFullName(registerModel.UserName);

                    if (Sitecore.Security.Accounts.User.Exists(domainUser))
                        ModelState.AddModelError("", "User already exists.");

                    System.Web.Security.Membership.CreateUser(domainUser, registerModel.Password);
                    if (AuthenticationManager.Login(domainUser, registerModel.Password))
                        return Redirect("/");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.ToString());
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return Redirect("/");
        }
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Register(string userName, string password, string confirmPassword)
        //{
        //    string domainUser = Context.Domain.GetFullName(userName);

        //    System.Web.Security.Membership.CreateUser(domainUser, password);

        //    if (AuthenticationManager.Login(domainUser, password))
        //    {
        //        string returnUrl = System.Web.HttpContext.Current.Request["url"];
        //        if (!Url.IsLocalUrl(returnUrl))
        //            returnUrl = "/";
        //        return Redirect(returnUrl);
        //    }

        //    return View();
        //}

    }
}