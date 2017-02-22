using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class CreateDivisionInformation
    {
        DivisionInformation DivisionInformation = new DivisionInformation();
        DEAQ db = new DEAQ();
        ActivityLog ActivityLog = new ActivityLog();

        public bool DivisionInformationC(string Address, string Suburb, string Location, string ZipCode, string Telephone, string Lada, string Fax,
            string Web, string City, string State, string Email, string DivisionId,int ApplicationId,int UsersId)
        {
            int Division = int.Parse(DivisionId);
            int DivisionInformationId = 0;
            string _address = Address.Trim();
            string _city = City.Trim();
            string _fax = Fax.Trim();
            string _lada = Lada.Trim();
            string _location = Location.Trim();
            string _state = State.Trim();
            string _suburb = Suburb.Trim();
            string _telephone = Telephone.Trim();
            string _web = Web.Trim();
            string _zipcode = ZipCode.Trim();
            string _email = Email.Trim();
            DivisionInformation.Active = true;
            DivisionInformation.Address = _address;
            DivisionInformation.City = _city;
            DivisionInformation.Fax = _fax;
            DivisionInformation.Lada = _lada;
            DivisionInformation.Location = _location;
            DivisionInformation.State = _state;
            DivisionInformation.Suburb = _suburb;
            DivisionInformation.Telephone = _telephone;
            DivisionInformation.Web = _web;
            DivisionInformation.ZipCode = _zipcode;
            DivisionInformation.DivisionId = Division;
            DivisionInformation.Email = _email;
            db.DivisionInformation.Add(DivisionInformation);
            db.SaveChanges();

            var DI = from DivInf in db.DivisionInformation
                     where 
                     DivInf.City == City.Trim()
                     && DivInf.Fax == Fax.Trim()
                     && DivInf.Lada == Lada.Trim()
                     && DivInf.Location == Location.Trim()
                     && DivInf.State == State.Trim()
                     && DivInf.Suburb == Suburb.Trim()
                     && DivInf.Telephone == Telephone.Trim()
                     && DivInf.Web == Web.Trim()
                     && DivInf.ZipCode == ZipCode.Trim()
                     && DivInf.DivisionId == Division
                     && DivInf.Email == Email.Trim()
                     select DivInf;

            foreach(DivisionInformation DIN in DI)
            {
                DivisionInformationId = DIN.DivisionInformationId;
            }

            ActivityLog.createnewdivisioninformation(Address.Trim(),
                City.Trim(),
                Fax.Trim(),
                Lada.Trim(),
                Location.Trim(),
                State.Trim(),
                Suburb.Trim(),
                Telephone.Trim(),
                Web.Trim(),
                ZipCode.Trim(),
                Division,
                DivisionInformationId,
                ApplicationId,
                UsersId,
                Email.Trim()
                );

            return true;
        }
    }
}