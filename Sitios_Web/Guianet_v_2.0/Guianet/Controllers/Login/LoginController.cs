using Guianet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Guianet.Controllers.Login
{
    public class LoginController : Controller
    {
        public PLMUsers db = new PLMUsers();
        public UserCountries UserCountries1 = new UserCountries();
        public ApplicationUsers ApplicationUsers = new ApplicationUsers();
        public Guianet.Models.Roles RolesUser = new Guianet.Models.Roles();
        public Users Users = new Users();
        Functions Functions = new Functions();
        public Applications Applications = new Applications();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string _response = IsValid(User.NickName, User.Password);

                    if (_response == "ok")
                    {
                        FormsAuthentication.SetAuthCookie(User.NickName, Users.Active = true);

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

                                    return RedirectToAction("Index", "LI");
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
                        if (_response == "_errorUser")
                        {
                            ModelState.AddModelError("", "Inicio de Sesión Incorrecto");
                        }
                        if (_response == "_errorAccess")
                        {
                            ModelState.AddModelError("", "No tiene privilegios para entrar al sistema");
                        }
                        if (_response == "_errorapplication")
                        {
                            ModelState.AddModelError("", "Error en Aplicación");
                        }
                        if (_response == "error")
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

        public string IsValid(string _Nname, string _Password)
        {
            PLMCryptographyComponent.CryptographyComponent _criptography = new PLMCryptographyComponent.CryptographyComponent();

            _Password = _criptography.encrypt(_Password);

            var Hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

            try
            {
                var apl = db.Database.SqlQuery<JoinLogin>("plm_spLoginApplications @User = '" + _Nname + "', @Password ='" + _Password + "', @HashKey = '" + Hashkey + "'").ToList();

                Users.Active = apl[0].UserActive;
                Users.CountryId = apl[0].UserCountryId;
                Users.Email = apl[0].UserEmail;
                Users.LastName = apl[0].UserLastName;
                Users.Name = apl[0].UserName;
                Users.NickName = apl[0].UserNickName;
                Users.Password = apl[0].UserPassword;
                Users.UserId = apl[0].UsrUserId;

                Applications.Active = apl[0].ApplicationsActive;
                Applications.ApplicationId = apl[0].ApplicationsApplicationId;
                Applications.Description = apl[0].ApplicationsDescription;
                Applications.HashKey = apl[0].ApplicationsHashKey;

                ApplicationUsers.ApplicationId = apl[0].UserApplicationId;
                ApplicationUsers.RoleId = apl[0].UserRoleId;
                ApplicationUsers.UserId = apl[0].UserUserId;

                RolesUser.Active = apl[0].RoleActive;
                RolesUser.Description = apl[0].RoleDescription;
                RolesUser.RoleId = apl[0].RoleId;

                return "ok";
            }
            catch (SqlException ex)
            {
                if (ex.Message == "_errorUser")
                {
                    return ex.Message.ToString();
                }

                if (ex.Message == "_errorAccess")
                {
                    return ex.Message.ToString();
                }

                if (ex.Message == "_errorapplication")
                {
                    return ex.Message.ToString();
                }
            }

            return "error";
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