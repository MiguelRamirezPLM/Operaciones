using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class CreateLab
    {
        DEAQ db = new DEAQ();
        Laboratories Labs = new Laboratories();
        ActivityLog ActivityLog = new ActivityLog();
        public bool CreateLabs(string LabName, int  ApplicationId,int UsersId)
        {
            string LaboratoryName = LabName.Trim();
            int LaboratoryId = 0;
            if(LaboratoryName == string.Empty)
            {
                return false;
            }
            var Lab = from Laboratory in db.Laboratories
                      where Laboratory.LaboratoryName.ToUpper().Trim() == LaboratoryName.ToUpper().Trim()
                      select Laboratory;
            if (Lab.LongCount() > 0)
            {
                return false;
            }
            else
            {
                Labs.Active = true;
                Labs.LaboratoryName = LaboratoryName;

                db.Laboratories.Add(Labs);
                db.SaveChanges();
            }

            var Labb = from Labss in db.Laboratories
                       where Labss.LaboratoryName.ToUpper().Trim() == LabName.ToUpper().Trim()
                       select Labss;
            foreach(Laboratories L in Labb)
            {
                LaboratoryId = L.LaboratoryId;
            }
            ActivityLog.createlab(LabName,ApplicationId,UsersId,LaboratoryId);

            return true;
        }
    }
}