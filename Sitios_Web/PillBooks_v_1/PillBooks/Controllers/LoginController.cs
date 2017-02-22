using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PillBooks.Models;
using Newtonsoft.Json;
using System.Web.Security;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Configuration;

namespace PillBooks.Controllers
{
    public class LoginController : Controller
    {
        private PLMUsers db = new PLMUsers();
        private MedinetPB mdb = new MedinetPB();

        public UserCountries UserCountries1 = new UserCountries();
        public ApplicationUsers ApplicationUsers = new ApplicationUsers();
        public PillBooks.Models.Roles RolesUser = new PillBooks.Models.Roles();
        public Users Users = new Users();
        Functions Functions = new Functions();
        public Applications Applications = new Applications();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsValid(user.NickName, user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(Users.NickName, Users.Active = true);

                        if (ApplicationUsers.UserId == Users.UserId)
                        {
                            if (ApplicationUsers.RoleId == RolesUser.RoleId)
                            {
                                UserCont(ApplicationUsers, UserCountries1);

                                if (RolesUser.Description.Equals("Administrador"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);

                                    return RedirectToAction("Index", "PillBook");
                                }
                                else if (RolesUser.Description.Equals("Vendedor"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);
                                    return RedirectToAction("Index", "PillBook");
                                }
                                else if (RolesUser.Description.Equals("Laboratorio de Información"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);
                                    return RedirectToAction("Index", "PillBook");
                                }
                                else if (RolesUser.Description.Equals("Médico"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);
                                    return RedirectToAction("Index", "PillBook");
                                }
                                else if (RolesUser.Description.Equals("Diagramador"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);
                                    return RedirectToAction("Index", "PillBook");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Applications.ApplicationId != ApplicationUsers.ApplicationId)
                        {
                            ModelState.AddModelError("", "No tiene privilegios para entrar al sistema");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Inicio de Sesión Incorrecto");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var trace = new System.Diagnostics.StackTrace(e);

                string msj = e.Message;
            }
            return View("Login");
        }

        public bool IsValid(string _Nname, string _Password)
        {
            PLMCryptographyComponent.CryptographyComponent _criptography = new PLMCryptographyComponent.CryptographyComponent();

            _Password = _criptography.encrypt(_Password);

            var usr = db.Users.Where(x => x.NickName == _Nname && x.Password == _Password).ToList();

            if (usr.LongCount() > 0)
            {
                Users.Active = usr[0].Active;
                Users.CountryId = usr[0].CountryId;
                Users.Email = usr[0].Email;
                Users.LastName = usr[0].LastName;
                Users.Name = usr[0].Name;
                Users.NickName = usr[0].NickName;
                Users.Password = usr[0].Password;
                Users.UserId = usr[0].UserId;

                var Hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

                var apl = db.Applications.Where(x => x.HashKey == Hashkey).ToList();

                if (apl.LongCount() > 0)
                {
                    Applications.Active = apl[0].Active;
                    Applications.ApplicationId = apl[0].ApplicationId;
                    Applications.Description = apl[0].Description;
                    Applications.HashKey = apl[0].HashKey;

                    var applusr = db.ApplicationUsers.Where(x => x.ApplicationId == Applications.ApplicationId && x.UserId == Users.UserId).ToList();

                    if (applusr.LongCount() > 0)
                    {
                        ApplicationUsers.ApplicationId = applusr[0].ApplicationId;
                        ApplicationUsers.RoleId = applusr[0].RoleId;
                        ApplicationUsers.UserId = applusr[0].UserId;

                        var rls = db.Roles.Where(x => x.RoleId == ApplicationUsers.RoleId).ToList();

                        if (rls.LongCount() > 0)
                        {
                            RolesUser.Active = rls[0].Active;
                            RolesUser.Description = rls[0].Description;
                            RolesUser.RoleId = rls[0].RoleId;

                            var cu = db.CountriesUser.Where(x => x.CountryId == Users.CountryId).ToList();

                            if (cu.LongCount() > 0)
                            {
                                var c = db.UserCountries.Where(x => x.UserId == Users.UserId).ToList();

                                foreach (UserCountries _c in c)
                                {
                                    UserCountries1 = new UserCountries();

                                    UserCountries1.CountryId = c[0].CountryId;
                                    UserCountries1.UserId = c[0].UserId;
                                }

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void UserCont(Models.ApplicationUsers user, Models.UserCountries UserCountries1)
        {
            Functions function = new Functions();
            function.Countries(ApplicationUsers);

            int userId = ApplicationUsers.UserId;
            int ApplicationId = ApplicationUsers.ApplicationId;

            var list = function.lcoun;

            List<string> var = new List<string>();
            List<int> country = new List<int>();
            foreach (Countries ce in list)
            {
                var.Add(ce.ID);
                CountriesUsers c = new CountriesUsers(country, var, ApplicationId, userId);
                Session["CountriesUsers"] = c;
            }

            foreach (Countries cu in list)
            {
                country.Add(cu.CountryId);
                CountriesUsers c = new CountriesUsers(country, var, ApplicationId, userId);
                Session["CountriesUsers"] = c;
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Login");
        }
    }
}