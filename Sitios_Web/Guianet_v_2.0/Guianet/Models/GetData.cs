using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Guianet.Models;

namespace Guianet.Models
{
    public class GetData
    {
        GuiaEntities db = new GuiaEntities();

        public int GetAlphabetId(string Letter)
        {
            if (Letter == "0")
            {
                Letter = "C";
            }

            if (Letter == "1")
            {
                Letter = "U";
            }

            if (Letter == "2")
            {
                Letter = "D";
            }

            if (Letter == "3")
            {
                Letter = "T";
            }

            if (Letter == "4")
            {
                Letter = "C";
            }

            if (Letter == "5")
            {
                Letter = "C";
            }

            if (Letter == "6")
            {
                Letter = "S";
            }

            if (Letter == "7")
            {
                Letter = "S";
            }

            if (Letter == "8")
            {
                Letter = "O";
            }

            if (Letter == "9")
            {
                Letter = "N";
            }



            var Alphabet = db.Database.SqlQuery<Alphabet>("plm_spGetAlphabetByLetter @letter=" + Letter + "").Select(x => x.AlphabetId).ToList();

            int AlphabetId = Convert.ToInt32(Alphabet[0]);

            return AlphabetId;
        }

        public List<ClientsByEdition> GetAddressType(string Type, int country, int ClientTypeId, int edition, string Letter, string CompanyName)
        {
            List<ClientsByEdition> LC = new List<ClientsByEdition>();

            if (Type == "1")
            {
                LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                LC = LC.Where(x => x.CountAddress == 1).ToList();
            }

            if (Type == "2")
            {
                LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                LC = LC.Where(x => x.CountAddress == 0).ToList();
            }

            if (Type == "3")
            {
                LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                LC = LC.Where(x => x.Location == 1).ToList();
            }

            if (Type == "4")
            {
                LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                LC = LC.Where(x => x.Location == 0).ToList();
            }

            return LC;
        }

        public List<GetClients> GetClientTypes(string Type, int country, int _ClientTypeId, string CompanyName, int edition)
        {
            List<GetClients> LC = new List<GetClients>();

            if (Type == "1")
            {
                LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();

                LC = LC.Where(x => x.CountAddress == 1).ToList();
            }

            if (Type == "2")
            {
                LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();

                LC = LC.Where(x => x.CountAddress == 0).ToList();
            }

            if (Type == "3")
            {
                LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();

                LC = LC.Where(x => x.Location == 1).ToList();
            }

            if (Type == "4")
            {
                LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();

                LC = LC.Where(x => x.Location == 0).ToList();
            }

            return LC;
        }

        public List<Work> GetWorks()
        {
            List<Work> LS = db.Database.SqlQuery<Work>("plm_spGetWorks").ToList();
            Work Works = new Work();
            List<Work> LW = new List<Work>();

            foreach (Work item in LS)
            {
                Works = new Models.Work();

                Works.Active = Convert.ToBoolean(item.Active);
                Works.AddedDate = item.AddedDate;
                Works.Module = item.Module;
                Works.WorkDescription = item.WorkDescription;
                Works.WorkId = Convert.ToInt32(item.WorkId);
                Works.ClientId = Convert.ToInt32(item.ClientId);
                Works.ProductId = Convert.ToInt32(item.ProductId);
                Works.ProductName = item.ProductName;
                Works.CompanyName = item.CompanyName;

                if ((!string.IsNullOrEmpty(item.WorkDescriptionLevelThree)) && (!string.IsNullOrEmpty(item.WorkDescription)))
                {
                    Works.UserName = "El usuario <i>" + item.UserName + "</i> del M&oacute;dulo <i>" + item.Module +
                                "</i> solicito el d&iacute;a " + item.Added +
                                " para la Categor&iacute;a a Nivel 3: <i>" + item.WorkDescriptionLevelThree + "</i> la Categor&iacute;a a Nivel 4: <span style=\"color:#b8c7ce\">" + item.WorkDescription +
                                "</span> para el Producto: <i>" + item.ProductName + "</i>, del Cliente: <i>" + item.CompanyName + "</i>";
                }
                else if (!string.IsNullOrEmpty(item.WorkDescriptionLevelThree))
                {
                    Works.UserName = "El usuario <i>" + item.UserName + "</i> del M&oacute;dulo <i>" + item.Module +
                                "</i> solicito el d&iacute;a " + item.Added +
                                " la Categor&iacute;a a Nivel 3: <span style=\"color:#b8c7ce\">" + item.WorkDescriptionLevelThree +
                                "</span> para el Producto: <i>" + item.ProductName + "</i>, del Cliente: <i>" + item.CompanyName + "</i>";
                }
                else if (!string.IsNullOrEmpty(item.WorkDescription))
                {
                    Works.UserName = "El usuario <i>" + item.UserName + "</i> del M&oacute;dulo <i>" + item.Module +
                                "</i> solicito el d&iacute;a " + item.Added +
                                " la Categor&iacute;a a Nivel 4: <span style=\"color:#b8c7ce\">" + item.WorkDescription +
                                "</span> para el Producto: <i>" + item.ProductName + "</i>, del Cliente: <i>" + item.CompanyName + "</i>";
                }




                LW.Add(Works);
            }

            return LW;
        }

        public List<Work> GetWorkById(int WorkId)
        {
            List<Work> LS = db.Database.SqlQuery<Work>("plm_spGetWorks @WorkId=" + WorkId + "").ToList();

            return LS;
        }
    }
}