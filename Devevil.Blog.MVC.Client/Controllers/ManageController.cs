using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    AuthorRepository ar = new AuthorRepository(uow.Current);
                    Author au = ar.GetAuthorByEmail(model.Email);

                    if (au!=null && au.ValidatePassword(model.Password))
                    {
                        //Login OK
                        if(FormsAuthentication.IsEnabled)
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index","Manage");
                    }
                    else
                    {
                        model.Message = "Email e/o password errate!";
                        return View(model);
                    }
                }
            }
            else return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (FormsAuthentication.IsEnabled)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Manage");
        }

        [Authorize]
        public ActionResult AccountManagment()
        {
            UserViewModel uvm = new UserViewModel();
            if (FormsAuthentication.IsEnabled)
            {

            }
            return View(uvm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AccountManagment(UserViewModel model)
        {
            if (FormsAuthentication.IsEnabled)
            {

            }
            return View(model);
        }
    }
}
