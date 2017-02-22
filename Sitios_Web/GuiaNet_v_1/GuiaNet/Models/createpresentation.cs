using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GuiaNet.Models;
namespace GuiaNet.Models
{
    public class createpresentation
    {
        Guia db = new Guia();
        Presentations Presentations = new Presentations();
        public bool _createpresentation(string Description, int ApplicationId, int UsersId)
        {
            string _description = Description;

            _description = classreplace.replace(_description.ToUpper());

            string descriptiondatabase = string.Empty;
            string _presentation = string.Empty;

            List<Presentations> la = new List<Presentations>();
            Presentations pa = new Presentations();

            var pre = (from p in db.Presentations
                       select p).ToList();

           

            foreach (Presentations pp in pre)
            {
                pa = new Models.Presentations();
                descriptiondatabase = pp.Description;
                descriptiondatabase = classreplace.descriptiondatabasereplace(descriptiondatabase.ToUpper());
                pa.Description = descriptiondatabase;
                la.Add(pa);
            }

            foreach (Presentations PPP in la)
            {
                if (_description.Equals(PPP.Description))
                {
                    _presentation = PPP.Description;
                }
            }
            var pd = (from pps in la
                      where pps.Description == _presentation
                      select pps).ToList();
            if (pd.LongCount() > 0)
            {                        
                return false;
            }
            else
            {
                Presentations.Description = Description.Trim();
                Presentations.Active = true;

                //    db.Presentations.Add(Presentations);
                //    db.SaveChanges();
            }
            return true;
        }
    }
}