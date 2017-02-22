using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIABusinessLogicComponent
{
    public class SectionsBLC
    {

        #region Constructor

        private SectionsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves all Materials by ParentId
        public List<DEIABusinessEntries.SectionInfo> rocGetSubRawMaterialsByParentId(string code, int parentId, int editionId)
        {
            if (parentId <= 0 || editionId <= 0)
                throw new ArgumentException("The parent or edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubRawMaterialsByParentId(parentId, editionId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

         //Retrieves all Services by Company
        public List<DEIABusinessEntries.ROC.ServiceByCompanyInfo> rocGetServicesByCompany(string code, int sectionId, int editionId)
        {
            if (sectionId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The section or edition does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    List<DEIABusinessEntries.ROC.ServiceByCompanyInfo> services = DEIADataAccessComponent.SectionsDALC.Instance.rocGetServicesByCompany(sectionId, editionId);

                    foreach (DEIABusinessEntries.ROC.ServiceByCompanyInfo serviceItem in services)
                    {
                        if (serviceItem.HtmlContent != null)
                        {

                            serviceItem.HtmlContent = serviceItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }

                    return services;

                }


                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }

        }


  




        //Retrieves information from a Section
        public DEIABusinessEntries.SectionInfo rocGetSectionById(string code, int sectionId)
        {
            if (sectionId < 1)
            {
                throw new ArgumentException("The Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionById(sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections or products By fulltext
        public List<DEIABusinessEntries.ROC.ProductByTextInfo> rocGetSectionsAndProductsByFullText(string code, int editionId, int parentId, string fullText, int numberByPage, int page)
        {
            if (editionId < 1 || parentId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or Parent or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsAndProductsByFullText(editionId, parentId, fullText, numberByPage, page);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections or products By text
        public List<DEIABusinessEntries.ROC.ProductByTextInfo> rocGetSectionsAndProductsByText(string code, int editionId, int parentId, string text, int numberByPage, int page)
        {
            if (editionId < 1 || parentId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or Parent or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsAndProductsByText(editionId, parentId, text, numberByPage, page);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #region Sections By Parent

        //Retrieves information from Section List by ParentId
        public List<DEIABusinessEntries.SectionInfo> rocGetSectionListByParentId(string code, int parentId)
        {
            if (parentId < 1)
            {
                throw new ArgumentException("The Parent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionListByParentId(parentId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentId(string code, int editionId, int parentId, int page, int numberByPage)
        {
            if (editionId < 1 || parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition, parent or page does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsByParentId(editionId, parentId, numberByPage, page);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections by ParentId and FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByFulltext(string code, int editionId, int parentId, int page, int numberByPage, string fullText)
        {
            if (editionId < 1 || parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition, parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsByParentIdByFulltext(editionId, parentId, page, numberByPage, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections by ParentId and Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByLetter(string code, int editionId, int parentId, int page, int numberByPage, string letter)
        {
            if (editionId < 1 || parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition, parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsByParentIdByLetter(editionId, parentId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections by ParentId and Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByText(string code, int editionId, int parentId, int page, int numberByPage, string text)
        {
            if (editionId < 1 || parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition, parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsByParentIdByText(editionId, parentId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #endregion

        #region Sections with Companies

        //Retrieves information from all Sections with Companies by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompanies(string code, int editionId, int parentId, int page, int numberByPage)
        {
            if (editionId < 1 || parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition, parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsWithCompanies(editionId, parentId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections with Companies by FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByFulltext(string code, int parentId, int editionId, int page, int numberByPage, string fullText)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition  or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsWithCompaniesByFulltext(parentId, editionId, page, numberByPage, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections with Companies by Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByLetter(string code, int parentId, int editionId, int page, int numberByPage, string letter)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsWithCompaniesByLetter(parentId, editionId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections with Companies by Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByText(string code, int parentId, int editionId, int page, int numberByPage, string text)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSectionsWithCompaniesByText(parentId, editionId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #endregion

        #region SubSections

        //Retrieves information from all SubSections by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsection(string code, int parentId, int editionId, int page, int numberByPage)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubsection(parentId, editionId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all SubSections by ParentId and FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByFulltext(string code, int parentId, int editionId, int page, int numberByPage, string fullText)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubsectionByFulltext(parentId, editionId, page, numberByPage, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all SubSections by ParentId and Letter.
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByLetter(string code, int parentId, int editionId, int page, int numberByPage, string letter)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubsectionByLetter(parentId, editionId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all SubSections by ParentId and Text.
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByText(string code, int parentId, int editionId, int page, int numberByPage, string text)
        {
            if (parentId < 1 || editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, edition or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubsectionByText(parentId, editionId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections by SectionId
        public List<DEIABusinessEntries.SectionInfo> rocGetSubsectionsBySectionId(string code, int editionId, int parentId)
        {

            if (editionId < 1 || parentId < 1)
            {
                throw new ArgumentException("The Edition or parent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetSubsectionsBySectionId(editionId, parentId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #endregion

        #region Providers And Manufactures

        //Retrieves all Manufactues
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufactures(string code, int parentId, int page, int numberByPage)
        {
            if (parentId <= 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, page or number page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetProvidersAndManufactures(parentId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

         //Retrieves all Manufactues by Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByLetter(string code, int parentId, int page, int numberByPage, string letter)
        {
            if (parentId <= 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent,  page or number page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetProvidersAndManufacturesByLetter(parentId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

         //Retrieves all Manufactues by Fulltext
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByFulltext(string code, int parentId, int page, int numberByPage, string text)
        {

            if (parentId <= 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, page o number page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetProvidersAndManufacturesByFulltext(parentId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            } 
        }

        //Retrieves all Manufactues by Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByText(string code, int parentId, int page, int numberByPage, string text)
        {
            if (parentId <= 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent, page or number page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetProvidersAndManufacturesByText(parentId, page, numberByPage, text); 
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            } 
        }

        #endregion

        #region Quality Control

        //Retrieves information from all Sections By Parent
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndex(string code, int parentId, int page, int numberByPage)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlIndex(parentId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections By Parent and letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByLetter(string code, int parentId, int page, int numberByPage, string letter)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlIndexByLetter(parentId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections By Parent and fulltext
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByFullText(string code, int parentId, int page, int numberByPage, string fullText)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlIndexByFullText(parentId, page, numberByPage, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Sections By Parent and text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByText(string code, int parentId, int page, int numberByPage, string text)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlIndexByText(parentId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Section By Parent
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControl(string code, int parentId, int page, int numberByPage)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControl(parentId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        
        }

        //Retrieves information from all Section By Parent And By Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByLetter(string code, int parentId, int page, int numberByPage, string letter)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlByLetter(parentId, page, numberByPage, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }

        }

        //Retrieves information from all Section By Parent and By Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByText(string code, int parentId, int page, int numberByPage, string text)
        {
            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlByText(parentId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }

        }

         //Retrieves information from all Section By Parent and By FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByFullText(string code, int parentId, int page, int numberByPage, string text)
        {

            if (parentId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Parent or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.SectionsDALC.Instance.rocGetQualityControlByFullText(parentId, page, numberByPage, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #endregion

        #endregion

        public static readonly SectionsBLC Instance = new SectionsBLC();

    }
}
