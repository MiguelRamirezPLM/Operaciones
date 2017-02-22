using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediVet.Models;

namespace MediVet.Models
{
    public class Functions
    {
        public UserCountries Ucountries = new UserCountries();
        ActivitySessions ActivitySessions = new ActivitySessions();

        PLMUsers db = new PLMUsers();

        CountriesUser Cusers = new CountriesUser();
        PEV PEV = new PEV();
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

            var UserC = from UserCountry in db.UserCountries
                        where UserCountry.UserId == user.UserId
                        select UserCountry;
            if (UserC.LongCount() > 0)
            {
                foreach (UserCountries UC in UserC)
                {
                    Ucountries.CountryId = UC.CountryId;
                    Ucountries.UserId = UC.UserId;

                    var CountU = from CountriesU in db.CountriesUser
                                 where CountriesU.CountryId == Ucountries.CountryId
                                 select CountriesU;

                    if (CountU.LongCount() > 0)
                    {
                        foreach (CountriesUser Count in CountU)
                        {
                            Cusers = new CountriesUser();
                            Cusers.Active = Count.Active;
                            Cusers.CountryName = Count.CountryName;
                            Cusers.CountryId = Convert.ToByte(Count.CountryId);
                            Cusers.ID = Count.ID;

                            var CountriesD = (from Country in PEV.Countries
                                              where Country.ID == Cusers.ID
                                              select Country).ToList();
                            if (CountU.LongCount() > 0)
                            {
                                foreach (Countries Coun in CountriesD)
                                {
                                    DCountries = new Countries();
                                    DCountries.Active = Coun.Active;
                                    DCountries.CountryName = Coun.CountryName;
                                    DCountries.CountryId = Coun.CountryId;
                                    DCountries.ID = Coun.ID;
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