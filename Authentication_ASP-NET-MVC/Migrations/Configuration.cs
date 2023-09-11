namespace MyApp_Auth.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MyApp_Auth.DatabaseContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            DatabaseContext db = new DatabaseContext();

            if (db.Roles.Count() == 0)
            {
                db.Roles.Add(new Role() { Name = "Admin", Title = "مدیر" });
                db.Roles.Add(new Role() { Name = "Member", Title = "کاربر" });
                db.SaveChanges();

                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile("123123", "MD5");
                db.Users.Add(new User() { RoleID = db.Roles.First().RoleID, Phone = "09017848766", Password = hashpassword, ActiveLink = Guid.NewGuid().ToString(), IsActive = true });
                db.SaveChanges();

                db.Dispose();
            }
        }
    }
}
