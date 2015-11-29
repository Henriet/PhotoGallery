using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoGalery.Models;

namespace PhotoGalery.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.Roles.Any()) return;

            var role = context.Roles.Add(new IdentityRole("Admin"));

            var user = new ApplicationUser
            {
                Email = "user@test.ua",
                PasswordHash = "ANAjAOhPjYzwmB2wcq/6X8N/+AxifKbMHQ90xlnG1lFREJ2SBygAumMy2j3n/qDqJQ==",
                UserName = "user@test.ua",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user = context.Users.Add(user);
            context.SaveChanges();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id, role.Name);

            context.Users.AddOrUpdate(user);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }
    }
}
