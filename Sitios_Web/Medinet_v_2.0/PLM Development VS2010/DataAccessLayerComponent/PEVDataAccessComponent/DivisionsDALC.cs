using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class DivisionsDALC
    {
        #region Constructor

        private DivisionsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves information about an Division by DivisionId
        public List<PEVBusinessEntries.ROC.DivInformationByDivisionInfo> rocGetDivisionInformation(int divisionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var divisionInformation = from divInformation in db.roc_spGetDivision(divisionId)
                                      select new PEVBusinessEntries.ROC.DivInformationByDivisionInfo
                                      {
                                          DivisionName = divInformation.DivisionName,
                                          DivisionShortName = divInformation.ShortName,
                                          DivisionInfId = divInformation.DivisionInfId,
                                          DivisionId = divInformation.DivisionId,
                                          Address = divInformation.Address,
                                          Suburb = divInformation.Suburb,
                                          ZipCode = divInformation.ZipCode,
                                          City = divInformation.City,
                                          State = divInformation.State,
                                          Telephone = divInformation.Telephone,
                                          Fax = divInformation.Fax,
                                          Web = divInformation.Web,
                                          Image = divInformation.Image,
                                          Email = divInformation.Email,
                                          Lada = divInformation.Lada,
                                          Text = divInformation.Text,
                                          Active = divInformation.Active
                                      };

            List<PEVBusinessEntries.ROC.DivInformationByDivisionInfo> division = divisionInformation.ToList();

            return division.Count() > 0 ? division : null;

        }

        //Retrieves all Divisions by EditionId and CountryId
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByEditionId(int editionId, int countryId, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var divisionInfo = from divInfo in db.roc_spGetDivisions(editionId, countryId, page, numberByPage)
                               select new PEVBusinessEntries.ROC.DivisionsInfo
                               {
                                   DivisionId = divInfo.DivisionId,
                                   DivisionName = divInfo.DivisionName,
                                   ShortName = divInfo.ShortName,
                                   RowNumber = (int)divInfo.RowNumber,
                                   Total = (int)divInfo.TOTAL
                               };

            List<PEVBusinessEntries.ROC.DivisionsInfo> division = divisionInfo.ToList();

            return division.Count() > 0 ? division : null;

        }

        //Retrieves all Divisions by EditionId and CountryId and Letter
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByLetter(int editionId, int countryId, string letter, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var divisionInfo = from divInfo in db.roc_spGetDivisionsByLetter(editionId, countryId, letter, page, numberByPage)
                               select new PEVBusinessEntries.ROC.DivisionsInfo
                               {
                                   DivisionId = divInfo.DivisionId,
                                   DivisionName = divInfo.DivisionName,
                                   ShortName = divInfo.ShortName,
                                   RowNumber = (int)divInfo.RowNumber,
                                   Total = (int)divInfo.TOTAL
                               };

            List<PEVBusinessEntries.ROC.DivisionsInfo> division = divisionInfo.ToList();

            return division.Count() > 0 ? division : null;
 
        }

        //Retrieves all Divisions by EditionId and CountryId and Text
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByText(int editionId, int countryId, string text, int page, int numberByPage) 
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var divisionInfo = from divInfo in db.roc_spGetDivisionsByText(editionId, countryId, text, page, numberByPage)
                               select new PEVBusinessEntries.ROC.DivisionsInfo
                               {
                                   DivisionId = divInfo.DivisionId,
                                   DivisionName = divInfo.DivisionName,
                                   ShortName = divInfo.ShortName,
                                   RowNumber = (int)divInfo.RowNumber,
                                   Total = (int)divInfo.TOTAL
                               };

            List<PEVBusinessEntries.ROC.DivisionsInfo> division = divisionInfo.ToList();

            return division.Count() > 0 ? division : null;

        }

        //Retrieves all Divisions by EditionId and CountryId and FullText
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByFullText(int editionId, int countryId, string fullText, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var divisionInfo = from divInfo in db.roc_spGetDivisionsByFullText(editionId, countryId, fullText, page, numberByPage)
                               select new PEVBusinessEntries.ROC.DivisionsInfo
                               {
                                   DivisionId = divInfo.DivisionId,
                                   DivisionName = divInfo.DivisionName,
                                   ShortName = divInfo.ShortName,
                                   RowNumber = (int)divInfo.RowNumber,
                                   Total = (int)divInfo.TOTAL
                               };

            List<PEVBusinessEntries.ROC.DivisionsInfo> division = divisionInfo.ToList();

            return division.Count() > 0 ? division : null;

        }

        #endregion

        public static readonly DivisionsDALC Instance = new DivisionsDALC();
    }
}
