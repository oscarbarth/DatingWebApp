﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using DatingWebApp.Models;

namespace DatingWebApp.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            //var ctx = new DatingDbContext();
            var ctx = new ApplicationDbContext();
            var viewModel = new ProfileIndexViewModel
            {
                Profiles = ctx.Profiles.ToList()
            };
            return View(viewModel);
        }

        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(Profile model, HttpPostedFileBase image)
        {
            //var ctx = new DatingDbContext();
            var ctx = new ApplicationDbContext();

            if (image != null)
            {
                model.Image = image.FileName;
                string fileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"
                + fileName));
                image.SaveAs(path);
            }

            model.UserId = User.Identity.GetUserId();

            ctx.Profiles.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Profile model, HttpPostedFileBase image)
        {
            //var ctx = new DatingDbContext();
            var ctx = new ApplicationDbContext();

            if (image != null)
            {
                model.Image = image.FileName;
                string fileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"
                + fileName));
                image.SaveAs(path);
            }

            ctx.Profiles.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
    }
}