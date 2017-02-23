using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        ApplicationUsers ApplicationUsers = new ApplicationUsers();
        Applications Applications = new Applications();
        Web.Models.Roles Roles = new Web.Models.Roles();
        private PLMUsers_20111213Entities2 db = new PLMUsers_20111213Entities2();
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

        [HttpPost]
        public ActionResult Login(Models.Users user, ActivitySessions ActivitySessions, Models.UserCountries UserCountries1, Models.Countries Country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.IsValid(user.NickName, user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.NickName, user.Active = true);
                        if (user.ApplicationsUsers.UserId == user.UserId)
                        {
                            if (user.ApplicationsUsers.RoleId == user.RolesUser.RoleId)
                            {
                                UserCont(user, UserCountries1, Country);
                                if (user.RolesUser.Description.Equals("Administrador"))
                                {
                                    Functions.ActivitySesions(user);
                                    return RedirectToAction("Selección", "Selección");
                                }
                                else if (user.RolesUser.Description.Equals("Vendedor"))
                                {
                                    Functions.ActivitySesions(user);
                                    return RedirectToAction("Pais", "FiltroVentas");
                                }
                                else if (user.RolesUser.Description.Equals("Diagramador"))
                                {
                                    Functions.ActivitySesions(user);
                                    return RedirectToAction("Pais", "FiltroProducción");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (user.Applications.ApplicationId != user.ApplicationsUsers.ApplicationId)
                        {
                            ModelState.AddModelError("", "Debe tener autorización para ingersar al sistema");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ingrese los datos correctamente");
                        }
                    }
                }
            }
            catch
            {
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        public void UserCont(Models.Users user, Models.UserCountries UserCountries1, Models.Countries Country)
        {
            Functions function = new Functions();
            function.Countries(user);

            int userId = user.UserId;
            int countryId = function.Ucountries.CountryId;
            int ApplicationId = user.ApplicationsUsers.ApplicationId;
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
    }
}
