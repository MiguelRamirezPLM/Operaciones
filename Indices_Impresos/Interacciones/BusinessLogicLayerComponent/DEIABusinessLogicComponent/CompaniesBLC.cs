using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIABusinessLogicComponent
{
    public class CompaniesBLC
    {

        #region Constructor

        private CompaniesBLC() { }

        #endregion

        #region Public Methods

        #region Providers

        //Retrieves All Companies By Type And By page
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProviders(string code, int editionId, int page, int numberByPage, int companyTypeId)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || companyTypeId <= 0)
            {
                throw new ArgumentException("The book edition, page, number page or type company does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CompaniesDALC.Instance.rocGetProviders(editionId, page, numberByPage, companyTypeId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Companies By Letter
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByLetter(string code, int editionId, int page, int numberByPage, int companyTypeId, string letter)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || companyTypeId <= 0)
            {
                throw new ArgumentException("The book edition, page, number page or type company does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CompaniesDALC.Instance.rocGetProvidersByLetter(editionId, page, numberByPage, companyTypeId, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Companies By Fulltext
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByFulltext(string code, int editionId, int page, int numberByPage, int companyTypeId, string text)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || companyTypeId <= 0)
            {
                throw new ArgumentException("The book edition, page, number page or type company does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CompaniesDALC.Instance.rocGetProvidersByFulltext(editionId, page, numberByPage, companyTypeId, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Companies By Text
        public List<DEIABusinessEntries.ROC.CompanyByPageInfo> rocGetProvidersByText(string code, int editionId, int page, int numberByPage, int companyTypeId, string text)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || companyTypeId <= 0)
            {
                throw new ArgumentException("The book edition, page, number page or type company does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CompaniesDALC.Instance.rocGetProvidersByText(editionId, page, numberByPage, companyTypeId, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #region Companies

        //Retrieves All Information The Companies
        public DEIABusinessEntries.ROC.CompanyByEditionInfo rocGetCompany(string code, int companyId, int editionId)
        {
            if (companyId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The Company or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    DEIABusinessEntries.ROC.CompanyByEditionInfo companies = DEIADataAccessComponent.CompaniesDALC.Instance.rocGetCompany(companyId, editionId);
                    

                    if (companies != null)
                    {
                        if(companies.HtmlContent != null)
                        
                        {
                            companies.HtmlContent = companies.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");
          
                            
                        }
                            
                    }
                    return companies;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Information The Companies By Section
        public List<DEIABusinessEntries.ROC.CompanyBySectionInfo> rocGetCompaniesBySection(string code, int editionId, int sectionId)
        {
            if (editionId <= 0 || sectionId <= 0)
            {
                throw new ArgumentException("The book edition or section does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.CompanyBySectionInfo> companies = DEIADataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesBySection(editionId, sectionId);

                  
                    if (companies != null)
                    {
                        foreach (DEIABusinessEntries.ROC.CompanyBySectionInfo company in companies)
                        
                        {
                            if(company.HtmlContent != null)
                            
                            {
                                company.HtmlContent = company.HtmlContent.Replace("src=\"imagenes/", "src=\"" +  
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");
            
                            }
                        }
                    }
                    return companies;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Information The Companies By City
        public List<DEIABusinessEntries.CompanyInfo> rocGetCompaniesByCityAndSection(string code, int sectionId, int cityId, int editionId)
        {
            if (sectionId <= 0 || cityId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The book edition, section or city does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByCityAndSection(sectionId, cityId, editionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #endregion

        public static readonly CompaniesBLC Instance = new CompaniesBLC();

    }
}
