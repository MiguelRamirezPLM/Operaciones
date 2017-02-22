using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEACIBusinessLogicComponent
{
   public class CompaniesBLC
    {

       #region Constructors
       
       private CompaniesBLC(){ }

       #endregion

       #region Public Methods

       #region Participant Companies

       //Retrieves all Participant Companies By Edition
       public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompanies(string code, int page, int numberByPage, int editionId, byte companyTypeId)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || companyTypeId == 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetParticipantCompanies(page, numberByPage, editionId, companyTypeId);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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

       //Retrieves all Participant Companies By Letter
       public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByLetter(string code, int editionId, byte companyTypeId, string letter, int page, int numberByPage)
        {
            if (editionId <= 0 || companyTypeId == 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetParticipantCompaniesByLetter(editionId, companyTypeId, letter, page, numberByPage);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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

       //Retrieves all Participant Companies By Text
       public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByText(string code, int editionId, byte companyTypeId, string text, int page, int numberByPage)
        {
            if (editionId <= 0 || companyTypeId == 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetParticipantCompaniesByText(editionId, companyTypeId, text, page, numberByPage);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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

       //Retrieves all Participant Companies By Fulltext
       public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompaniesByFulltex(string code, int editionId, int page, int numberByPage, byte companytypeId, string text)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || companytypeId == 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetParticipantCompaniesByFulltex(editionId, page, numberByPage, companytypeId, text);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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
        
       //Retrives All Participant Companies By Edition
       public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetParticipantCompany(string code, int editionId, byte companyTypeId, int companyId)
        {
            if (editionId <= 0 || companyTypeId == 0 || companyId <= 0)
            {
                throw new ArgumentException("The company, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetParticipantCompany(editionId, companyTypeId, companyId);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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

       #endregion 

       #region Companies

        //Retrives All Information From Companies 
        public List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> rocGetCompany(string code, int editionId, int companyId)
        {
            if (editionId <= 0 || companyId <= 0)
            {
                throw new ArgumentException("The edition or company does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ParticipantCompanyInfo> companies = DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompany(editionId, companyId);

                    if (companies != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ParticipantCompanyInfo company in companies)
                        {
                            if (company.HtmlFile != null)
                            {
                                company.HtmlFile = company.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/");

                                company.HtmlFile = company.HtmlFile.Replace("src=\"anuncios/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "anuncios/");
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

        //Retrives All Information From Companies By Edition
        public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompanies(string code, int page, int numberByPage, int editionId)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0)
            {
                throw new ArgumentException("The page, number page or edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompanies(page, numberByPage, editionId);
                       
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        //Retrives All Information From Companies By Text
        public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByTex(string code, int page, int numberByPage, int editionId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0)
            {
                throw new ArgumentException("The page or edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByTex(page, numberByPage, editionId, text);

                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrives All Information From Companies By Fulltext
        public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByFullText(string code, int page, int numberByPage, int editionId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0)
            {
                throw new ArgumentException("The page, number page or edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByFullText(page, numberByPage, editionId, text);
                   
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Information From Companies By Letter
        public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByLetter(string code, int page, int numberByPage, int editionId, string letter)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0)
            {
                throw new ArgumentException("The page, number page or edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByLetter(page, numberByPage, editionId, letter);
                    
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        //Retrieves All Information From Companies By Brand
        public List<DEACIBusinessEntries.ROC.CompanyByBrandInfo> rocGetCompaniesByBrand(string code, int editionId, int brandId)
        {
            if (editionId <= 0 || brandId <= 0)
            {
                throw new ArgumentException("The edition or brand does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByBrand(editionId, brandId);
                
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves All Information From Companies By State
        public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetCompaniesByState(string code, int page, int numberByPage, int editionId, int stateId)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || stateId <= 0)
            {
                throw new ArgumentException("The page, number page, edition or state does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetCompaniesByState(page, numberByPage, editionId, stateId);
                      
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

       #region Laboratories
       
       //Retrieves All Information From Companies By Type
       public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratories(string code, int page, int numberByPage, int editionId, byte companyTypeId)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || companyTypeId == 0)
            {
                throw new ArgumentException("The page, number page,  edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetLaboratories(page, numberByPage, editionId, companyTypeId);                        
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

       //Retrieves All Information From Companies By Letter
       public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByLetter(string code, int page, int numberByPage, int editionId, byte companyTypeId, string letter)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || companyTypeId == 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetLaboratoriesByLetter(page, numberByPage, editionId, companyTypeId, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

       //Retrieves All Information From Companies By Text
       public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByText(string code, int page, int numberByPage, int editionId, byte companyTypeId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || companyTypeId == 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetLaboratoriesByText(page, numberByPage, editionId, companyTypeId, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

       //Retrieves All Information From Companies By Fulltext
       public List<DEACIBusinessEntries.ROC.CompanyInfo> rocGetLaboratoriesByFullText(string code, int page, int numberByPage, int editionId, byte companyTypeId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || companyTypeId == 0)
            {
                throw new ArgumentException("The page, number page, edition or company type does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CompaniesDALC.Instance.rocGetLaboratoriesByFullText(page, numberByPage, editionId, companyTypeId, text);
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
