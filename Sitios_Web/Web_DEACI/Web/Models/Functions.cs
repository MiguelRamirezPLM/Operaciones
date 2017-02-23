using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Models
{
    public class Functions
    {
        private DEACI_20150917Entities DEACI = new DEACI_20150917Entities();
        private PLMUsers  db = new PLMUsers();
        ActivitySessions ActivitiSessions = new ActivitySessions();
        public Countries_Users Cusers = new Countries_Users();
        public UserCountries Ucountries = new UserCountries();
        Countries DCountries = new Countries();
        public List<Countries> lcoun = new List<Countries>();
        public Users users = new Users();

        public void ActivitySesions(Models.Users user)
        {
            ActivitiSessions.Date = DateTime.Now;
            ActivitiSessions.UserId = user.UserId;
            ActivitiSessions.ApplicationId = user.Applications.ApplicationId;

            db.ActivitySessions.Add(ActivitiSessions);
            db.SaveChanges();
        }

        public void Countries(Models.Users user)
        {
            var UserC = from UserCountry in db.UserCountriesSet
                        where UserCountry.UserId == user.UserId
                        select UserCountry;

            if (UserC.LongCount() > 0)
            {
                foreach (UserCountries UC in UserC)
                {
                    Ucountries.CountryId = UC.CountryId;
                    Ucountries.UserId = UC.UserId;

                    var CountU = from CountriesU in db.Countries_Users
                                 where CountriesU.CountryId == Ucountries.CountryId
                                 select CountriesU;
                    if (CountU .LongCount() > 0)
                    {
                        foreach (Countries_Users Count in CountU)
                        {
                            Cusers = new Countries_Users();
                            Cusers.Active = Count.Active;
                            Cusers.CountryName = Count.CountryName;
                            Cusers.CountryId = Count.CountryId;
                            Cusers.ID = Count.ID;

                            var CountriesD = from Country in DEACI.Countries
                                             where Country.ID == Cusers.ID
                                             select Country;


                            if (CountU.LongCount() > 0)
                            { 
                                foreach (Countries coun in CountriesD)
                                {
                                    DCountries = new Countries();
                                    DCountries.Active = coun.Active;
                                    DCountries.CountryName = coun.CountryName;
                                    DCountries.CountryId = coun.CountryId;
                                    DCountries.ID = coun.ID;
                                    lcoun.Add(DCountries);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}