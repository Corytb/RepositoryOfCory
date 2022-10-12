using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Utilities
{
    public class AuthorizeUtilities
    {

        //when needing the current userId, method will look something like
        //model.Make.UserId = AuthorieUtilities.GetUserId(this); OR
        //var userId = AuthorizeUtilities.GetUserId(this);
        public static string GetUserId(Controller controller)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(controller.User.Identity.Name);
            return user.Id;
        }

    }
}