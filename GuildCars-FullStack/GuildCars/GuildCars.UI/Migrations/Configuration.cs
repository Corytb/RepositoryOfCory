namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            // Load the user and role managers with our custom models
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleMgr = new RoleManager<IdentityUserRole>(new RoleStore<IdentityUserRole>(context));

            // have we loaded roles already?
            //if (roleMgr.RoleExists("admin"))
            //    return;

            // create the admin role
            //roleMgr.Create(new AppRole() { Name = "admin" });

            // create the default user
            var user = new ApplicationUser()
            {
                UserName = "admin@guildcars.com"
            };

            // create the user with the manager class
            userMgr.Create(user, "Testing123!");

            // add the user to the admin role
            //userMgr.AddToRole(user.Id, "admin");
        }
    }
}
