using GuiaNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GuiaNet.Controllers.Login
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public UserCountries UserCountries1 = new UserCountries();
        public ApplicationUsers ApplicationUsers = new ApplicationUsers();
        public GuiaNet.Models.Roles RolesUser = new GuiaNet.Models.Roles();
        public Users Users = new Users();
        public Applications Applications = new Applications();
        public PLMUsers db = new PLMUsers();

        Functions Functions = new Functions();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        ////public ActionResult Login(Models.Users user, ActivitySessions ActivitySessions, Models.UserCountries UserCountries1, Models.CountriesUser Country)
        //public ActionResult Login(Models.Users user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (user.IsValid(user.NickName, user.Password))
        //            {
        //                FormsAuthentication.SetAuthCookie(user.NickName, user.Active = true);
        //                if (user.ApplicationsUsers.UserId == user.UserId)
        //                {
        //                    if (user.ApplicationsUsers.RoleId == user.RolesUser.RoleId)
        //                    {
        //                        //UserCont(user, UserCountries1, Country);
        //                        if (user.RolesUser.Description.Equals("Administrador"))
        //                        {
        //                            //Functions.ActivitySesions(user);
        //                            return RedirectToAction("Index", "SalesModule");
        //                        }
        //                        else if (user.RolesUser.Description.Equals("Vendedor"))
        //                        {
        //                            Functions.ActivitySesions(user);
        //                            return RedirectToAction("Index", "SalesModule");
        //                        }
        //                        else if (user.RolesUser.Description.Equals("Laboratorio de Información"))
        //                        {
        //                            Functions.ActivitySesions(user);
        //                            return RedirectToAction("Index", "InformationLaboratory");
        //                        }
        //                        else if (user.RolesUser.Description.Equals("Diagramador"))
        //                        {
        //                            Functions.ActivitySesions(user);
        //                            return RedirectToAction("Index", "Production");
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (user.Applications.ApplicationId != user.ApplicationsUsers.ApplicationId)
        //                {
        //                    ModelState.AddModelError("", "No tiene privilegios para entrar al sistema");
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "Inicio de Sesión Incorrecto");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return View("Login");
        //}
        //public ActionResult Logout()
        //{
        //    Session.Abandon();
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Login", "Login");
        //}

        //public void UserCont(Models.Users user, Models.UserCountries UserCountries1, Models.CountriesUser Country)
        //{
        //    Functions function = new Functions();
        //    function.Countries(user);

        //    int userId = user.UserId;
        //    int countryId = function.Ucountries.CountryId;
        //    int ApplicationId = user.ApplicationsUsers.ApplicationId;
        //    var list = function.lcoun;
        //    List<string> var = new List<string>();
        //    List<int> country = new List<int>();
        //    foreach (Countries ce in list)
        //    {

        //        var.Add(ce.ID);
        //        CountriesUsers c = new CountriesUsers(country, var, ApplicationId, userId);
        //        Session["CountriesUsers"] = c;
        //    }

        //    foreach (Countries cu in list)
        //    {
        //        country.Add(cu.CountryId);
        //        CountriesUsers c = new CountriesUsers(country, var, ApplicationId, userId);
        //        Session["CountriesUsers"] = c;
        //    }
        //}


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

                                    return RedirectToAction("Index", "SalesModule");
                                }
                                else if (RolesUser.Description.Equals("Vendedor"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);

                                    return RedirectToAction("Index", "SalesModule");
                                }
                                else if (RolesUser.Description.Equals("Laboratorio de Información"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);

                                    return RedirectToAction("Index", "InformationLaboratory");
                                }
                                else if (RolesUser.Description.Equals("Diagramador"))
                                {
                                    Functions.ActivitySesions(ApplicationUsers);

                                    return RedirectToAction("Index", "Production");
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