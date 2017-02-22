using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIADataAccessComponent
{
    public class SectionsDALC
    {

        #region Constructor

        private SectionsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves all Materials by ParentId
        public List<DEIABusinessEntries.SectionInfo> rocGetSubRawMaterialsByParentId(int parentId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var materials = from matRow in db.roc_spGetSubRawMaterialsByParentId(parentId, editionId)
                            select new DEIABusinessEntries.SectionInfo
                              {
                                  SectionId = matRow.SectionId,
                                  ParentId = (int)matRow.ParentId,
                                  SectionName = matRow.SectionName,
                                  Active = matRow.Active
                              };

            List<DEIABusinessEntries.SectionInfo> matInfo = materials.ToList();

            return matInfo.Count() > 0 ? matInfo : null;
        }

        //Retrieves all Services by Company
        public List<DEIABusinessEntries.ROC.ServiceByCompanyInfo> rocGetServicesByCompany(int sectionId, int editioId)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var services = from servRow in db.roc_spGetServicesByCompany(sectionId, editioId)
                           select new DEIABusinessEntries.ROC.ServiceByCompanyInfo
                           {
                               ProductId = servRow.ProductId,
                               ProductName = servRow.ProductName,
                               ProductTypeId = servRow.ProductTypeId,
                               CompanyId = servRow.CompanyId,  
                               Description = servRow.Description,
                               EdProdId = servRow.EdProdId,
                               HtmlFile = servRow.HtmlFile,
                               HtmlContent = servRow.HtmlContent,
                               CompanyName = servRow.CompanyName,
                               CompanyTypeId = servRow.CompanyTypeId,

                           };
            List<DEIABusinessEntries.ROC.ServiceByCompanyInfo> serInfo = services.ToList();

            return serInfo.Count() > 0 ? serInfo : null;


        }
        
        //Retrieves information from a Section
        public DEIABusinessEntries.SectionInfo rocGetSectionById(int sectionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetSectionById(sectionId)
                             select new DEIABusinessEntries.SectionInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = (int)sectRow.ParentId,
                                 Active = sectRow.Active
                             };

            List<DEIABusinessEntries.SectionInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections[0] : null;
        }

        //Retrieves information from all Sections or products By fulltext
        public List<DEIABusinessEntries.ROC.ProductByTextInfo> rocGetSectionsAndProductsByFullText(int editionId, int parentId, string fullText, int numberByPage, int page)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetSectionsAndProductsByFullText(editionId, parentId, fullText, numberByPage, page)
                             select new DEIABusinessEntries.ROC.ProductByTextInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ProductId = sectRow.ProductId,
                                 ParentProductId = sectRow.ParentId,
                                 ProductTypeId = sectRow.ProductTypeId,
                                 ProductName = sectRow.ProductName,
                                 ProductDescription = sectRow.Description,
                                 HmtlFile = sectRow.HtmlFile,
                                 CompanyId = sectRow.CompanyId,
                                 CompanyName = sectRow.CompanyName,
                                 CompanyTypeId = sectRow.CompanyTypeId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.total
                             };

            List<DEIABusinessEntries.ROC.ProductByTextInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        //Retrieves information from all Sections or products By text
        public List<DEIABusinessEntries.ROC.ProductByTextInfo> rocGetSectionsAndProductsByText(int editionId, int parentId, string text, int numberByPage, int page)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetSectionsAndProductsByText(editionId, parentId, text, numberByPage, page)
                             select new DEIABusinessEntries.ROC.ProductByTextInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ProductId = sectRow.ProductId,
                                 ParentProductId = sectRow.ParentId,
                                 ProductTypeId = sectRow.ProductTypeId,
                                 ProductName = sectRow.ProductName,
                                 ProductDescription = sectRow.Description,
                                 HmtlFile = sectRow.HtmlFile,
                                 CompanyId = sectRow.CompanyId,
                                 CompanyName = sectRow.CompanyName,
                                 CompanyTypeId = sectRow.CompanyTypeId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.total
                             };

            List<DEIABusinessEntries.ROC.ProductByTextInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        #region Sections By Parent

        //Retrieves information from Section List by ParentId
        public List<DEIABusinessEntries.SectionInfo> rocGetSectionListByParentId(int parentId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionListByParentId(parentId)
                              select new DEIABusinessEntries.SectionInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  ParentId = (int)sectRow.ParentId,
                                  SectionName = sectRow.SectionName,
                                  Active = sectRow.Active
                              };

            List<DEIABusinessEntries.SectionInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentId(int editionId, int parentId, int numberByPage, int page)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsByParentId(editionId, parentId, numberByPage, page) 
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        //Retrieves information from all Sections by ParentId and FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByFulltext(int editionId, int parentId, int page, int numberByPage, string fullText)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsByParentIdByFulltext(editionId, parentId, page, numberByPage, fullText)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections by ParentId and Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByLetter(int editionId, int parentId, int page, int numberByPage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsByParentIdByLetter(editionId, parentId, page, numberByPage, letter)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections by ParentId and Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsByParentIdByText(int editionId, int parentId, int page, int numberByPage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsByParentIdByText(editionId, parentId, page, numberByPage, text)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        #endregion

        #region Sections with Companies

        //Retrieves information from all Sections with Companies by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompanies(int editionId, int parentId,  int page, int numberByPage)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsWithCompanies(editionId, parentId, page, numberByPage)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId =sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections with Companies by FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByFulltext(int parentId, int editionId, int page, int numberByPage, string fullText)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsWithCompaniesByFulltext(parentId, editionId, page, numberByPage, fullText)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections with Companies by Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByLetter(int parentId, int editionId, int page, int numberByPage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsWithCompaniesByLetter(parentId, editionId, page, numberByPage, letter)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections with Companies by Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSectionsWithCompaniesByText(int parentId, int editionId, int page, int numberByPage, string text)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSectionsWithCompaniesByText(parentId, editionId, page, numberByPage, text)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionsRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        #endregion

        #region SubSections

        //Retrieves information from all SubSections by ParentId
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsection(int parentId, int editionId,  int page, int numberByPage)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var subSectionsRow = from sectRow in db.roc_spGetSubsection(parentId, editionId, page, numberByPage)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> subSections = subSectionsRow.ToList();

            return subSections.Count() > 0 ? subSections : null;

        }

        //Retrieves information from all SubSections by ParentId and FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByFulltext(int parentId, int editionId, int page, int numberByPage, string fullText)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var subSectionsRow = from sectRow in db.roc_spGetSubsectionByFulltext(parentId, editionId, page, numberByPage, fullText)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> subSections = subSectionsRow.ToList();

            return subSections.Count() > 0 ? subSections : null;

        }

        //Retrieves information from all SubSections by ParentId and Letter.
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByLetter(int parentId, int ediionId, int page, int numberByPage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var subSectionsRow = from sectRow in db.roc_spGetSubsectionByLetter(parentId, ediionId, page, numberByPage, letter)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total= (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> subSections = subSectionsRow.ToList();

            return subSections.Count() > 0 ? subSections : null;

        }

        //Retrieves information from all SubSections by ParentId and Text.
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetSubsectionByText(int parentId, int editionId, int page, int numberByPage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var subSectionsRow = from sectRow in db.roc_spGetSubsectionByText(parentId, editionId, page, numberByPage, text)
                              select new DEIABusinessEntries.ROC.SectionByParentInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  SectionName = sectRow.SectionName,
                                  ParentId = sectRow.ParentId,
                                  RowNumber = (int)sectRow.RowNumber,
                                  Total = (int)sectRow.TOTAL
                              };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> subSections = subSectionsRow.ToList();

            return subSections.Count() > 0 ? subSections : null;

        }

        //Retrieves information from all SubSections by SectionId
        public List<DEIABusinessEntries.SectionInfo> rocGetSubsectionsBySectionId(int editionId, int parentId)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionsRow = from sectRow in db.roc_spGetSubsectionsBySectionId(editionId, parentId)
                              select new DEIABusinessEntries.SectionInfo
                              {
                                  SectionId = sectRow.SectionId,
                                  ParentId = (int)sectRow.ParentId,
                                  SectionName = sectRow.SectionName,
                                  Active = sectRow.Active

                              };

            List<DEIABusinessEntries.SectionInfo> subSections = sectionsRow.ToList();

            return subSections.Count() > 0 ? subSections : null;
        }


        #endregion

        #region QualityControl

        //Retrieves information from all Sections By Parent
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndex(int parentId, int page, int numberByPage)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlIndex(parentId, page, numberByPage)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Sections By Parent and letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByLetter(int parentId, int page, int numberByPage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlIndexByLetter(parentId, page, numberByPage, letter)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        //Retrieves information from all Sections By Parent and fulltext
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByFullText(int parentId, int page, int numberByPage, string fullText)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlIndexByFullText(parentId, page, numberByPage, fullText)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo 
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        //Retrieves information from all Sections By Parent and text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlIndexByText(int parentId, int page, int numberByPage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlIndexByText(parentId, page, numberByPage, text)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {
                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Section By Parent
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControl(int parentId, int page, int numberByPage)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControl(parentId, page, numberByPage)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {

                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }

        //Retrieves information from all Section By Parent And By Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByLetter(int parentId, int page, int numberByPage, string letter)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlByLetter(parentId, page, numberByPage, letter)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {

                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;

        }



        //Retrieves information from all Section By Parent and By Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByText(int parentId, int page, int numberByPage, string text)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlByText(parentId, page, numberByPage, text)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {

                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

        //Retrieves information from all Section By Parent and By FullText
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetQualityControlByFullText(int parentId, int page, int numberByPage, string text)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var sectionRow = from sectRow in db.roc_spGetQualityControlByFullText(parentId, page, numberByPage, text)
                             select new DEIABusinessEntries.ROC.SectionByParentInfo
                             {

                                 SectionId = sectRow.SectionId,
                                 SectionName = sectRow.SectionName,
                                 ParentId = sectRow.ParentId,
                                 RowNumber = (int)sectRow.RowNumber,
                                 Total = (int)sectRow.TOTAL
                             };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> sections = sectionRow.ToList();

            return sections.Count() > 0 ? sections : null;
        }

       


        #endregion

        #region Providers And Manufactures

        //Retrieves all Manufactues
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufactures(int parentId, int page, int numberByPage)
        {

            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var manufacturesRow = from manfRow in db.roc_spGetProvidersAndManufactures(parentId, page, numberByPage)
                                  select new DEIABusinessEntries.ROC.SectionByParentInfo
                                  {
                                      SectionId = manfRow.SectionId,
                                      ParentId = manfRow.ParentId,
                                      SectionName = manfRow.SectionName,
                                      RowNumber =(int)manfRow.RowNumber,
                                      Total = (int)manfRow.TOTAL
                                  };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> manufactures = manufacturesRow.ToList();

            return manufactures.Count() > 0 ? manufactures : null;
        }

        //Retrieves all Manufactues by Letter
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByLetter(int parentId, int page, int numberByPage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var manufByLetterRow = from manLetterRow in db.roc_spGetProvidersAndManufacturesByLetter(parentId, page, numberByPage, letter)
                                   select new DEIABusinessEntries.ROC.SectionByParentInfo
                                   {
                                       SectionId = manLetterRow.SectionId,
                                       ParentId = manLetterRow.ParentId,
                                       SectionName = manLetterRow.SectionName,
                                       RowNumber = (int)manLetterRow.RowNumber,
                                       Total = (int)manLetterRow.TOTAL
                                   };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> manufByLetter = manufByLetterRow.ToList();

            return manufByLetter.Count() > 0 ? manufByLetter : null;
        }

        //Retrieves all Manufactues by Fulltext
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByFulltext(int parentId, int page, int numberByPage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var manufByFullRow = from manByFullRow in db.roc_spGetProvidersAndManufacturesByFulltext(parentId, page, numberByPage, text)
                                 select new DEIABusinessEntries.ROC.SectionByParentInfo
                                 {
                                     SectionId = manByFullRow.SectionId,
                                     ParentId = manByFullRow.ParentId,
                                     SectionName = manByFullRow.SectionName,
                                     RowNumber = (int)manByFullRow.RowNumber,
                                     Total = (int)manByFullRow.TOTAL
                                 };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> manufByFullText = manufByFullRow.ToList();

            return manufByFullText.Count() > 0 ? manufByFullText : null;
        }

        //Retrieves all Manufactues by Text
        public List<DEIABusinessEntries.ROC.SectionByParentInfo> rocGetProvidersAndManufacturesByText(int parentId, int page, int numberByPage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var manufByTextRow = from manByTextRow in db.roc_spGetProvidersAndManufacturesByText(parentId, page, numberByPage, text)
                                 select new DEIABusinessEntries.ROC.SectionByParentInfo
                                 {
                                     SectionId = manByTextRow.SectionId,
                                     ParentId = manByTextRow.ParentId,
                                     SectionName = manByTextRow.SectionName,
                                     RowNumber = (int)manByTextRow.RowNumber,
                                     Total = (int)manByTextRow.TOTAL
                                 };

            List<DEIABusinessEntries.ROC.SectionByParentInfo> manufByText = manufByTextRow.ToList();

            return manufByText.Count() > 0 ? manufByText : null;

        }

        #endregion

        #endregion

        public static readonly SectionsDALC Instance = new SectionsDALC();

    }
}
