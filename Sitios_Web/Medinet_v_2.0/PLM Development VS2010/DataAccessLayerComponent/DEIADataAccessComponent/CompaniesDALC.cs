using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIADataAccessComponent
{
    public class CompaniesDALC
    {

        #region Constructor

        private CompaniesDALC() { }

        #endregion 

        #region Public Methods

        #region Providers

        //Retrieves All Companies By Type And By page
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProviders(int editionId, int page, int numberByPage, int companyTypeId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var providersRow = from provRow in db.roc_spGetProviders(editionId, page, numberByPage, companyTypeId)
                               select new DEIABusinessEntries.ROC.CompanyByPageInfo
                               {
                                   Total =(int) provRow.TOTAL,
                                   CompanyId = provRow.CompanyId,
                                   CompanyName = provRow.CompanyName,
                                   RowNumber = (int)provRow.RowNumber
                               };

            List<DEIABusinessEntries.ROC.CompanyByPageInfo> providers = providersRow.ToList();
            return  providers.Count() >  0 ? providers : null;
            
        }

        //Retrieves All Companies By Letter
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByLetter(int editionId, int page, int numberByPage, int companyTypeId, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var providersByLetterRow = from provByLetterRow in db.roc_spGetProvidersByLetter(editionId, page, numberByPage, companyTypeId, letter)
                                       select new DEIABusinessEntries.ROC.CompanyByPageInfo
                                       {
                                           Total = (int)provByLetterRow.TOTAL,
                                           CompanyId = provByLetterRow.CompanyId,
                                           CompanyName = provByLetterRow.CompanyName,
                                           RowNumber = (int)provByLetterRow.RowNumber
                                       };

            List<DEIABusinessEntries.ROC.CompanyByPageInfo> providersByText = providersByLetterRow.ToList();
            return providersByText.Count() > 0 ? providersByText : null; 
            

        } 

        //Retrieves All Companies By Fulltext
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByFulltext(int editionId, int page, int numberByPage, int companyTypeId, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var providerByFullRow = from provByFullRow in db.roc_spGetProvidersByFulltext(editionId, page, numberByPage, companyTypeId, text)
                                    select new DEIABusinessEntries.ROC.CompanyByPageInfo
                                    {
                                        Total = (int)provByFullRow.TOTAL,
                                        CompanyId = provByFullRow.CompanyId,
                                        CompanyName = provByFullRow.CompanyName,
                                        RowNumber = (int)provByFullRow.RowNumber
                                    };

            List<DEIABusinessEntries.ROC.CompanyByPageInfo> providersByFullText = providerByFullRow.ToList();
            return providersByFullText.Count() > 0 ? providersByFullText : null;

        }

        //Retrieves All Companies By Text
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByText(int editionId, int page, int numberByPage, int companyTypeId, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var providersByTexRow = from provByTextRow in db.roc_spGetProvidersByText(editionId, page, numberByPage, companyTypeId, text)
                                    select new DEIABusinessEntries.ROC.CompanyByPageInfo
                                    {
                                        Total = (int)provByTextRow.TOTAL,
                                        CompanyId = provByTextRow.CompanyId,
                                        CompanyName = provByTextRow.CompanyName,
                                        RowNumber = (int)provByTextRow.RowNumber
                                    };

            List<DEIABusinessEntries.ROC.CompanyByPageInfo> providersByText = providersByTexRow.ToList();
            return providersByText.Count() > 0 ? providersByText : null;
               
        }

        #endregion 

        #region Companies

        //Retrieves All Information The Companies
        public DEIABusinessEntries.ROC.CompanyByEditionInfo rocGetCompany(int companyId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var company = from companyRow in db.roc_spGetCompany(companyId, editionId)
                             select new DEIABusinessEntries.ROC.CompanyByEditionInfo
                             {
                                 CompanyId = companyRow.CompanyId,
                                 ParentId = companyRow.ParentId,
                                 CompanyName = companyRow.CompanyName,
                                 CompanyShortName = companyRow.CompanyShortName,
                                 CompanyTypeId = companyRow.CompanyTypeId,
                                 CommercialBusiness = companyRow.CommercialBusiness,
                                 CityId = companyRow.CityId,
                                 Address = companyRow.Address,
                                 Suburb = companyRow.Suburb,
                                 Location = companyRow.Location,
                                 ZipCode = companyRow.ZipCode,
                                 Email = companyRow.Email,
                                 Web = companyRow.Web,
                                 ClientId = companyRow.ClientId,
                                 Active = companyRow.Active,
                                 HtmlFile = companyRow.HtmlFile,
                                 HtmlContent = companyRow.HtmlContent
                             };

            List<DEIABusinessEntries.ROC.CompanyByEditionInfo> companies = company.ToList();

            return companies.Count() > 0 ? companies[0] : null;

        }

        //Retrieves All Information The Companies By Section
        public List<DEIABusinessEntries.ROC.CompanyBySectionInfo> rocGetCompaniesBySection(int editionId, int sectionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var companies = from companyRow in db.roc_spGetCompaniesBySection(editionId, sectionId)
                                   select new DEIABusinessEntries.ROC.CompanyBySectionInfo
                                   {
                                       CompanyId = companyRow.CompanyId,
                                       CompanyName = companyRow.CompanyName,
                                       CompanyShortName = companyRow.CompanyShortName,
                                       HtmlFile = companyRow.HtmlFile,
                                       HtmlContent = companyRow.HtmlContent,
                                       Address = companyRow.Address,
                                       Suburb = companyRow.Suburb,
                                       ZipCode = companyRow.Zipcode,
                                       CityId = (int)companyRow.CityId,
                                       CityName = companyRow.CityName,
                                       StateId = companyRow.StateId,
                                       StateName = companyRow.StateName,
                                       Web = companyRow.Web,
                                       CommercialBusiness = companyRow.CommercialBusiness
                                   };

            List<DEIABusinessEntries.ROC.CompanyBySectionInfo> companiesBySection = companies.ToList();

            return companiesBySection.Count() > 0 ? companiesBySection : null;

        }

        //Retrieves All Information The Companies By City
        public List<DEIABusinessEntries.CompanyInfo> rocGetCompaniesByCityAndSection(int sectionId, int cityId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var companyByCityRow = from companyRow in db.roc_spGetCompaniesByCityAndSection(sectionId, cityId, editionId)
                                   select new DEIABusinessEntries.CompanyInfo
                                   {
                                       CompanyId = companyRow.CompanyId,
                                       ParentId = companyRow.ParentId,
                                       CompanyName = companyRow.CompanyName,
                                       CompanyShortName = companyRow.CompanyName,
                                       CompanyTypeId = companyRow.CompanyTypeId,
                                       CommercialBusiness = companyRow.CommercialBusiness,
                                       Address = companyRow.Address,
                                       CityId = companyRow.CityId,
                                       Suburb = companyRow.Suburb,
                                       Location = companyRow.Location,
                                       ZipCode = companyRow.ZipCode,
                                       Email = companyRow.Email,
                                       Web = companyRow.Web,
                                       ClientId = companyRow.ClientId,
                                       HtmlFile = companyRow.HtmlFile,
                                       EmpresaId = companyRow.EmpresaId,
                                       Active = companyRow.Active
                                   };

            List<DEIABusinessEntries.CompanyInfo> companyByCity = companyByCityRow.ToList();
            return companyByCity.Count() > 0 ? companyByCity : null;
        
        }

        #endregion

        #endregion

        public static readonly CompaniesDALC Instance = new CompaniesDALC();

    }
}
