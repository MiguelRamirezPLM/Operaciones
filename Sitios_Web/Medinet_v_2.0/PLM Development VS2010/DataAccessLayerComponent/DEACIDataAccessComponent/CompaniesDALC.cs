using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEACIBusinessEntries;

namespace DEACIDataAccessComponent
{
  public class CompaniesDALC
  {

      #region Constructors

      private CompaniesDALC() { }

      #endregion

      #region Public Methods

      #region Participant Companies

      //Retrieves All Participant Companies By Edition
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompanies(int page, int numberByPage, int EditionId, byte companyTypeId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var partCompany = from partCompanyRow in db.roc_spGetParticipantCompanies(page, numberByPage, EditionId, companyTypeId)
                            select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                            {
                                CompanyId = partCompanyRow.CompanyId,
                                CompanyTypeId = partCompanyRow.CompanyTypeId,
                                CompanyIdParent = partCompanyRow.CompanyIdParent,
                                Address = partCompanyRow.Address,
                                Suburb = partCompanyRow.Suburb,
                                Ubication = partCompanyRow.Ubication,
                                PostalCode = partCompanyRow.PostalCode,
                                Email = partCompanyRow.Email,
                                Web = partCompanyRow.Web,
                                Contact = partCompanyRow.Contact,
                                CompanyName = partCompanyRow.CompanyName,
                                CompanyShortName = partCompanyRow.CompanyShortName,
                                CityId = partCompanyRow.CityId,
                                ClientID = partCompanyRow.Client_ID,
                                Active = partCompanyRow.Active,
                                HtmlFile = partCompanyRow.HTMLContent,
                                RowNumber = (int)partCompanyRow.RowNumber,
                                Total = (int)partCompanyRow.TOTAL

                            };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> partCompanyInformation = partCompany.ToList();

          return partCompanyInformation.Count > 0 ? partCompanyInformation : null;
      }
      
      //Retrieves All Participant Companies By Letter
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByLetter(int editionId, byte companyTypeId, string letter, int page, int numberByPage) 
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var partCompany = from partCompanyRow in db.roc_spGetParticipantCompaniesByLetter(editionId, companyTypeId, letter, page, numberByPage)
                            select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                            {
                            CompanyId = partCompanyRow.CompanyId,
                            CompanyTypeId = partCompanyRow.CompanyTypeId,
                            CompanyIdParent = partCompanyRow.CompanyIdParent,
                            Address = partCompanyRow.Address,
                            Suburb = partCompanyRow.Suburb,
                            Ubication = partCompanyRow.Ubication,
                            PostalCode = partCompanyRow.PostalCode,
                            Email = partCompanyRow.Email,
                            Web = partCompanyRow.Web,
                            Contact = partCompanyRow.Contact,
                            CompanyName = partCompanyRow.CompanyName,
                            CompanyShortName = partCompanyRow.CompanyShortName,
                            CityId = partCompanyRow.CityId,
                            ClientID = partCompanyRow.Client_ID,
                            Active = partCompanyRow.Active,
                            HtmlFile = partCompanyRow.HTMLContent,
                            RowNumber =(int)partCompanyRow.RowNumber,
                            Total =(int)partCompanyRow.TOTAL
                            
                            };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> partCompanyInformation = partCompany.ToList();

          return partCompanyInformation.Count > 0 ? partCompanyInformation : null;

      }
  
      //Retrieves All Participant Companies By Text
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByText(int editionId, byte companyTypeId, string text, int page, int numberByPage)
      {

          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var partCompany = from partCompanyRow in db.roc_spGetParticipantCompaniesByText(editionId, companyTypeId, text, page, numberByPage)
                            select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                            {
                                CompanyId = partCompanyRow.CompanyId,
                                CompanyTypeId = partCompanyRow.CompanyTypeId,
                                CompanyIdParent = partCompanyRow.CompanyIdParent,
                                Address = partCompanyRow.Address,
                                Suburb = partCompanyRow.Suburb,
                                Ubication = partCompanyRow.Ubication,
                                PostalCode = partCompanyRow.PostalCode,
                                Email = partCompanyRow.Email,
                                Web = partCompanyRow.Web,
                                Contact = partCompanyRow.Contact,
                                CompanyName = partCompanyRow.CompanyName,
                                CompanyShortName = partCompanyRow.CompanyShortName,
                                CityId = partCompanyRow.CityId,
                                ClientID = partCompanyRow.Client_ID,
                                Active = partCompanyRow.Active,
                                HtmlFile = partCompanyRow.HTMLContent,
                                RowNumber = (int)partCompanyRow.RowNumber,
                                Total = (int)partCompanyRow.TOTAL
                            };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> partCompanyInformation = partCompany.ToList();

          return partCompanyInformation.Count > 0 ? partCompanyInformation : null;

      }

      //Retrieves All Participant Companies By Fulltext
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByFulltex(int editionId, int page, int numberByPage, byte companytypeId, string text)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

         var partCompany = from partCompanyRow in db.roc_spGetParticipantCompaniesByFulltext(editionId, page, numberByPage, companytypeId, text)
                            select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                            {
                                CompanyId = partCompanyRow.CompanyId,
                                CompanyTypeId = partCompanyRow.CompanyTypeId,
                                CompanyIdParent = partCompanyRow.CompanyIdParent,
                                Address = partCompanyRow.Address,
                                Suburb = partCompanyRow.Suburb,
                                Ubication = partCompanyRow.Ubication,
                                PostalCode = partCompanyRow.PostalCode,
                                Email = partCompanyRow.Email,
                                Web = partCompanyRow.Web,
                                Contact = partCompanyRow.Contact,
                                CompanyName = partCompanyRow.CompanyName,
                                CompanyShortName = partCompanyRow.CompanyShortName,
                                CityId = partCompanyRow.CityId,
                                ClientID = partCompanyRow.Client_ID,
                                Active = partCompanyRow.Active,
                                HtmlFile = partCompanyRow.HTMLContent,
                                RowNumber =(int)partCompanyRow.RowNumber,
                                Total = (int)partCompanyRow.TOTAL
                            };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> partCompanyInfo = partCompany.ToList();

          return partCompanyInfo.Count > 0 ? partCompanyInfo : null;
      }

      //Retrives All Participant Companies By Edition
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompany(int editionId, byte companyTypeId, int companyId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var partCompany = from partCompanyRow in db.roc_spGetParticipantCompany(editionId, companyTypeId, companyId)
                            select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                            {
                                CompanyId = partCompanyRow.CompanyId,
                                CompanyTypeId = partCompanyRow.CompanyTypeId,
                                CompanyIdParent = partCompanyRow.CompanyIdParent,
                                Address = partCompanyRow.Address,
                                Suburb = partCompanyRow.Suburb,
                                Ubication = partCompanyRow.Ubication,
                                PostalCode = partCompanyRow.PostalCode,
                                Email = partCompanyRow.Email,
                                Web = partCompanyRow.Web,
                                Contact = partCompanyRow.Contact,
                                CompanyName = partCompanyRow.CompanyName,
                                CompanyShortName = partCompanyRow.CompanyShortName,
                                CityId = partCompanyRow.CityId,
                                ClientID = partCompanyRow.Client_ID,
                                Active = partCompanyRow.Active,
                                HtmlFile = partCompanyRow.HTMLContent,
                                RowNumber = (int)partCompanyRow.RowNumber,
                                Total = (int)partCompanyRow.TOTAL
                            };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> partCompanyInfo = partCompany.ToList();

          return partCompanyInfo.Count > 0 ? partCompanyInfo : null;

      }
      
      #endregion

      #region Companies

      //Retrieves All Information From Companies 
      public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetCompany(int editionId, int companyId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompany(editionId, companyId)
                        select new DEACIBusinessEntries.ROC.ParticipantCompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            HtmlFile = companyRow.HTMLContent,
                            RowNumber = (int)companyRow.RowNumber,
                            Total = (int)companyRow.TOTAL
                        };

          List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;
      }

      //Retrieves All Information From Companies By Edition
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompanies(int page, int numberByPage, int editionId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompanies(page, numberByPage, editionId)
                        select new DEACIBusinessEntries.ROC.CompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            CityName = companyRow.ciudad,
                            StateId = companyRow.StateId,
                            StateName = companyRow.Name,
                            RowNumber =(int)companyRow.RowNumber,
                            Total =(int)companyRow.TOTAL

                        };

          List<DEACIBusinessEntries.ROC.CompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;
      }

      //Retrieves All Information From Companies By Text
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByTex(int page, int numberByPage, int editionId, string text)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompaniesByText(page, numberByPage, editionId, text)
                        select new DEACIBusinessEntries.ROC.CompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            CityName = companyRow.ciudad,
                            StateId = companyRow.StateId,
                            StateName = companyRow.Name,
                            RowNumber =(int)companyRow.RowNumber,
                            Total =(int)companyRow.TOTAL
                        };

          List<DEACIBusinessEntries.ROC.CompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;
      }

      //Retrieves All Information From Companies By Fulltext
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByFullText(int page, int numberByPage, int editionId, string text)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompaniesByFulltext(page, numberByPage, editionId, text)
                        select new DEACIBusinessEntries.ROC.CompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            CityName = companyRow.ciudad,
                            StateId = companyRow.StateId,
                            StateName = companyRow.Name,
                            RowNumber = (int)companyRow.RowNumber,
                            Total = (int)companyRow.TOTAL

                        };

          List<DEACIBusinessEntries.ROC.CompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;
      }

      //Retrieves All Information From Companies By Letter
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByLetter(int page, int numberByPage, int editionId, string letter)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompaniesByLetter(page, numberByPage, editionId, letter)
                        select new DEACIBusinessEntries.ROC.CompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            CityName = companyRow.ciudad,
                            StateId = companyRow.StateId,
                            StateName = companyRow.Name,
                            RowNumber = (int)companyRow.RowNumber,
                            Total = (int)companyRow.TOTAL
                        };

          List<DEACIBusinessEntries.ROC.CompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;
      }

      //Retrieves All Information From Companies By Brand
      public List<DEACIBusinessEntries.ROC.CompanyByBrandInfo> rocGetCompaniesByBrand(int editionId, int brandId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompaniesByBrand(editionId, brandId)
                        select new DEACIBusinessEntries.ROC.CompanyByBrandInfo
                        
                        
                        {
                           BrandId = companyRow.BrandId,
                           CompanyId = companyRow.CompanyId,
                           Address = companyRow.Address,
                           SubUrb = companyRow.Suburb,
                           PostalCode = companyRow.PostalCode,
                           Email = companyRow.Email,
                           Web = companyRow.Web,
                           CompanyName = companyRow.CompanyName,
                           CompanyShortName = companyRow.CompanyShortName,
                           CompanyTypeId = companyRow.CompanyTypeId,
                           Client_Id =(int?)companyRow.Client_ID

                        };

          List<DEACIBusinessEntries.ROC.CompanyByBrandInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;

      }

      //Retrieves All Information From Companies By State
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByState(int page, int numberByPage, int editionId, int stateId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var company = from companyRow in db.roc_spGetCompaniesByState(page, numberByPage, editionId, stateId)
                        select new DEACIBusinessEntries.ROC.CompanyInfo
                        {
                            CompanyId = companyRow.CompanyId,
                            CompanyTypeId = companyRow.CompanyTypeId,
                            CompanyIdParent = companyRow.CompanyIdParent,
                            Address = companyRow.Address,
                            Suburb = companyRow.Suburb,
                            Ubication = companyRow.Ubication,
                            PostalCode = companyRow.PostalCode,
                            Email = companyRow.Email,
                            Web = companyRow.Web,
                            Contact = companyRow.Contact,
                            CompanyName = companyRow.CompanyName,
                            CompanyShortName = companyRow.CompanyShortName,
                            CityId = companyRow.CityId,
                            ClientID = companyRow.Client_ID,
                            Active = companyRow.Active,
                            CityName = companyRow.ciudad,
                            StateId = companyRow.StateId,
                            StateName = companyRow.Name,
                            RowNumber = (int)companyRow.RowNumber,
                            Total = (int)companyRow.TOTAL, 
                        };
          List<DEACIBusinessEntries.ROC.CompanyInfo> companyInformation = company.ToList();

          return companyInformation.Count > 0 ? companyInformation : null;

      } 

      #endregion

      #region Laboratories

      //Retrieves All Information From Companies By Type
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratories(int page, int numberByPage, int editionId, byte companyTypeId)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var laboratories = from labRow in db.roc_spGetLaboratories(page, numberByPage, editionId, companyTypeId)
                             select new DEACIBusinessEntries.ROC.CompanyInfo
                             {
                                 CompanyId = labRow.CompanyId,
                                 CompanyTypeId = labRow.CompanyTypeId,
                                 CompanyIdParent = labRow.CompanyIdParent,
                                 Address = labRow.Address,
                                 Suburb = labRow.Suburb,
                                 Ubication = labRow.Ubication,
                                 PostalCode = labRow.PostalCode,
                                 Email = labRow.Email,
                                 Web = labRow.Web,
                                 Contact = labRow.Contact,
                                 CompanyName = labRow.CompanyName,
                                 CompanyShortName = labRow.CompanyShortName,
                                 CityId = labRow.CityId,
                                 ClientID = labRow.Client_ID,
                                 Active = labRow.Active,
                                 StateId = labRow.StateId,
                                 StateName = labRow.estado,
                                 CityName = labRow.Name,
                                 RowNumber = (int)labRow.RowNumber,
                                 Total = (int)labRow.TOTAL
                             };

          List<DEACIBusinessEntries.ROC.CompanyInfo> labInformation = laboratories.ToList();

          return labInformation.Count > 0 ? labInformation : null;

      }

      //Retrieves All Information From Companies By Letter
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByLetter(int page, int numberByPage, int editionId, byte companyTypeId, string letter)
      {
          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var laboratories = from labRow in db.roc_spGetLaboratoriesByLetter(page, numberByPage, editionId, companyTypeId, letter)
                             select new DEACIBusinessEntries.ROC.CompanyInfo
                             {
                                 CompanyId = labRow.CompanyId,
                                 CompanyTypeId = labRow.CompanyTypeId,
                                 CompanyIdParent = labRow.CompanyIdParent,
                                 Address = labRow.Address,
                                 Suburb = labRow.Suburb,
                                 Ubication = labRow.Ubication,
                                 PostalCode = labRow.PostalCode,
                                 Email = labRow.Email,
                                 Web = labRow.Web,
                                 Contact = labRow.Contact,
                                 CompanyName = labRow.CompanyName,
                                 CompanyShortName = labRow.CompanyShortName,
                                 CityId = labRow.CityId,
                                 ClientID = labRow.Client_ID,
                                 Active = labRow.Active,
                                 StateName = labRow.estado,
                                 StateId = labRow.StateId,
                                 CityName = labRow.Name,
                                 RowNumber = (int)labRow.RowNumber,
                                 Total = (int)labRow.TOTAL
                             };

          List<DEACIBusinessEntries.ROC.CompanyInfo> labInformation = laboratories.ToList();

          return labInformation.Count > 0 ? labInformation : null;

      }

      //Retrieves All Information From Companies By Text
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByText(int page, int numberByPage, int editionId, byte companyTypeId, string text)
      {

          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var laboratories = from labRow in db.roc_spGetLaboratoriesByText(page, numberByPage, editionId, companyTypeId, text)
                             select new DEACIBusinessEntries.ROC.CompanyInfo
                             {
                                 CompanyId = labRow.CompanyId,
                                 CompanyTypeId = labRow.CompanyTypeId,
                                 CompanyIdParent = labRow.CompanyIdParent,
                                 Address = labRow.Address,
                                 Suburb = labRow.Suburb,
                                 Ubication = labRow.Ubication,
                                 PostalCode = labRow.PostalCode,
                                 Email = labRow.Email,
                                 Web = labRow.Web,
                                 Contact = labRow.Contact,
                                 CompanyName = labRow.CompanyName,
                                 CompanyShortName = labRow.CompanyShortName,
                                 CityId = labRow.CityId,
                                 ClientID = labRow.Client_ID,
                                 Active = labRow.Active,
                                 StateName = labRow.estado,
                                 StateId = labRow.StateId,
                                 CityName = labRow.Name,
                                 RowNumber = (int)labRow.RowNumber,
                                 Total = (int)labRow.TOTAL
                             };

          List<DEACIBusinessEntries.ROC.CompanyInfo> labInformation = laboratories.ToList();

          return labInformation.Count > 0 ? labInformation : null;

      }

      //Retrieves All Information From Companies By Fulltext
      public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByFullText(int page, int numberByPage, int editionId, byte companyTypeId, string text)
      {

          DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

          var laboratories = from labRow in db.roc_spGetLaboratoriesByFulltext(page, numberByPage, editionId, companyTypeId, text)
                             select new DEACIBusinessEntries.ROC.CompanyInfo
                             {
                                 CompanyId = labRow.CompanyId,
                                 CompanyTypeId = labRow.CompanyTypeId,
                                 CompanyIdParent = labRow.CompanyIdParent,
                                 Address = labRow.Address,
                                 Suburb = labRow.Suburb,
                                 Ubication = labRow.Ubication,
                                 PostalCode = labRow.PostalCode,
                                 Email = labRow.Email,
                                 Web = labRow.Web,
                                 Contact = labRow.Contact,
                                 CompanyName = labRow.CompanyName,
                                 CompanyShortName = labRow.CompanyShortName,
                                 CityId = labRow.CityId,
                                 ClientID = labRow.Client_ID,
                                 Active = labRow.Active,
                                 StateName = labRow.estado,
                                 StateId = labRow.StateId,
                                 CityName = labRow.Name,
                                 RowNumber = (int)labRow.RowNumber,
                                 Total = (int)labRow.TOTAL
                             };

          List<DEACIBusinessEntries.ROC.CompanyInfo> labInformation = laboratories.ToList();

          return labInformation.Count > 0 ? labInformation : null;

      }
    
      #endregion

      #endregion

      public static readonly CompaniesDALC Instance = new CompaniesDALC();

  }
}
