using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guianet.Models;

namespace Guianet.Models
{
    public class Functions
    {
        public GuiaEntities dbm = new GuiaEntities();
        public PLMUsers db = new PLMUsers();

        ActivitySessions ActivitySessions = new ActivitySessions();
        public CountriesUser Cusers = new CountriesUser();
        public UserCountries Ucountries = new UserCountries();
        Countries DCountries = new Countries();
        public List<Countries> lcoun = new List<Countries>();
        public Users users = new Users();

        public void ActivitySesions(Models.ApplicationUsers user)
        {
            ActivitySessions.UserId = user.UserId;
            ActivitySessions.ApplicationId = user.ApplicationId;
            ActivitySessions.Date = DateTime.Now;

            db.ActivitySessions.Add(ActivitySessions);
            db.SaveChanges();
        }

        public void Countries(Models.ApplicationUsers user)
        {
            var UserC = (from UserCountry in db.UserCountries
                         where UserCountry.UserId == user.UserId
                         select UserCountry).ToList();
            if (UserC.LongCount() > 0)
            {
                foreach (UserCountries UC in UserC)
                {
                    Ucountries.CountryId = UC.CountryId;
                    Ucountries.UserId = UC.UserId;

                    var CountU = (from CountriesU in db.CountriesUser
                                  where CountriesU.CountryId == Ucountries.CountryId
                                  select CountriesU).ToList();

                    if (CountU.LongCount() > 0)
                    {
                        Cusers = new CountriesUser();

                        Cusers.Active = CountU[0].Active;
                        Cusers.CountryName = CountU[0].CountryName;
                        Cusers.CountryId = CountU[0].CountryId;
                        Cusers.ID = CountU[0].ID;

                        var CountriesD = (from Country in dbm.Countries
                                          where Country.ID == Cusers.ID
                                          select Country).ToList();

                        if (CountriesD.LongCount() > 0)
                        {
                            DCountries = new Countries();

                            DCountries.Active = CountriesD[0].Active;
                            DCountries.CountryName = CountriesD[0].CountryName;
                            DCountries.CountryId = CountriesD[0].CountryId;
                            DCountries.ID = CountriesD[0].ID;

                            lcoun.Add(DCountries);
                        }
                    }
                }
            }
        }
    }
}