using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyApp_Auth.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db;
        public AccountController()
        {
            db = new DatabaseContext();
        }

        public ActionResult Register()
        {
            if (IsAuthenticated)
                if (HttpContext.User.IsInRole("admin"))
                    return RedirectToAction("Panel", "Admin");
                else
                    return RedirectToAction("Index", "UserPanel");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");
                if (!db.Users.Any(u => u.Phone == model.Phone))
                {
                    string guid = Guid.NewGuid().ToString();
                    db.Users.Add(new User()
                    {
                        Phone = model.Phone,
                        Password = hashpassword,
                        RoleID = db.Roles.Single(u => u.Name == "Member").RoleID,
                        ActiveLink = guid
                    });
                    db.SaveChanges();

                    Email.Send("وبسایت من", "koodla3e@gmail.com", "خوش آمدید", "ثبت نام شما با موفقیت انجام شد"
                        + Environment.NewLine + "لینک تایید ایمیل شما: " + Email.GetBaseUrl() + "Account/Active?g=" + guid);

                    return RedirectToAction("Index", "UserPanel");
                }
                else
                {
                    User user = db.Users.Single(u => u.Phone == model.Phone);
                    if (!user.IsActive)
                    {
                        string guid = Guid.NewGuid().ToString();
                        user.ActiveLink = guid;
                        db.Entry(guid).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Email.Send("وبسایت من", "koodla3e@gmail.com", "خوش آمدید", "ثبت نام شما با موفقیت انجام شد"
                            + Environment.NewLine + "لینک تایید ایمیل شما: " + Email.GetBaseUrl() + "Account/Active?g=" + guid);

                        return RedirectToAction("Index", "UserPanel");
                    }
                    else
                    {
                        ModelState.AddModelError("Phone", "کاربر دیگری با این شماره ثبت نام کرده است");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Active(string g)
        {
            User user = db.Users.FirstOrDefault(u => u.ActiveLink == g);
            if (user != null && !user.IsActive)
            {
                user.IsActive = true;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Phone, true);

                return RedirectToAction("Index", "UserPanel");
            }
            else
            {
                return new HttpNotFoundResult("This link has expired");
            }
        }

        public ActionResult Login()
        {
            if (IsAuthenticated)
                if (HttpContext.User.IsInRole("admin"))
                    return RedirectToAction("Panel", "Admin");
                else
                    return RedirectToAction("Index", "UserPanel");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "MD5");
                var user = db.Users.FirstOrDefault(u => u.Phone == model.Phone && u.Password == hashpassword);
                if (user != null)
                {
                    // set cookie login
                    FormsAuthentication.SetAuthCookie(model.Phone, true);

                    if (user.Role.Name == "admin")
                        return Redirect("/Admin/Panel/Index");
                    else
                        return RedirectToAction("Index", "UserPanel");
                }
                else ModelState.AddModelError("Phone", "نام کاربری یا کلمه عبور اشتباه است");
            }
            else ModelState.AddModelError("Phone", "مشکلی پیش آمد لطفا بعدا امتحان کنید");

            return View(model);
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.HttpContext.User.Identity.IsAuthenticated;
            }
        }
    }
}