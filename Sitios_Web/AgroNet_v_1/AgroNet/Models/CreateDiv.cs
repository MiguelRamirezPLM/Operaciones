using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class CreateDiv
    {
        DEAQ db = new DEAQ();
        Divisions Divs = new Divisions();
        ActivityLog ActivityLog = new ActivityLog();

        public bool CreateDivs(string DivisionN, string ShortName, string Country, int LabId, int ApplicationId, int UsersId)
        {
            string DivisionName = DivisionN.Trim();
            string ShortN = ShortName.Trim();
            int CountryId = int.Parse(Country);
            int DivisionId = 0;
            var Div = from Division in db.Divisions
                      where Division.DivisionName == DivisionName
                      && Division.ShortName == ShortN
                      && Division.CountryId == CountryId
                      select Division;
            if (Div.LongCount() > 0)
            {
                return false;
            }
            else
            {
                Divs.Active = true;
                Divs.DivisionName = DivisionName;
                Divs.ShortName = ShortN;
                Divs.LaboratoryId = LabId;
                Divs.CountryId = CountryId;
                db.Divisions.Add(Divs);
                db.SaveChanges();

                var Divisions = from Divss in db.Divisions
                                where Divss.DivisionName == DivisionName
                                && Divss.ShortName == ShortN
                                select Divss;
                foreach(Divisions D in Divisions)
                {
                    DivisionId = D.DivisionId;
                }
                ActivityLog.createdivision(DivisionName, ShortN, CountryId, LabId, ApplicationId, UsersId, DivisionId);
            }
            return true;
        }
    }
}