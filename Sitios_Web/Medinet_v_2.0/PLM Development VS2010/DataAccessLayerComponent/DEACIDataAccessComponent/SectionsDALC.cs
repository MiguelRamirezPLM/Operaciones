using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEACIBusinessEntries;

namespace DEACIDataAccessComponent
{
    public class SectionsDALC
    {

        #region Constructor

        private SectionsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves all Sections By SectionId
        public DEACIBusinessEntries.ROC.IndexProductInfo rocGetIndexProduct(int editionId, int indexId, int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetIndexProduct(editionId, indexId, sectionId)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = 1,
                              Total = 1
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation[0] : null;

        }

        //Retrieves all Sections By Index
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProducts(int page, int numberByPage, int editionId, int indexId)
        {
        
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetIndexProducts(page, numberByPage, editionId, indexId)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                            SectionId = sectionRow.SectionId,
                            Description = sectionRow.Description,
                            RowNumber =(int)sectionRow.RowNumber,
                            Total =(int)sectionRow.TOTAL
   
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        //Retrieves all Sections By Index and By FullText
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByFulltext(int page, int numberByPage, int editionId, int indexId, string fullText)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetIndexProductsByFulltext(page, numberByPage, editionId, indexId, fullText)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        //Retrieves all Sections By Index and By Letter
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByLetter(int page, int numberByPage, int editionId, int indexId, string letter)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetIndexProductsByLetter(page, numberByPage, editionId, indexId, letter)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        //Retrieves all Sections By Index and By Text
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByText(int page, int numberByPage, int editionId, int indexId, string text)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetIndexProductsByText(page, numberByPage, editionId, indexId, text)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        //Retrieves all Sections By SectionIdParent
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSections(int page, int numberByPage, int sectionIdParent)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSections(page, numberByPage, sectionIdParent)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;

        }

        //Retrieves all Sections By SectionIdParent and FullText
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByFulltext(int page, int numberByPage, int sectionIdParent, string fullText)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSectionsByFulltext(page, numberByPage, sectionIdParent, fullText)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;

        }

        //Retrieves all Sections By SectionIdParent and Letter
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByLetter(int page, int numberByPage, int sectionIdParent, string letter)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSectionsByLetter(page, numberByPage, sectionIdParent, letter)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;

        }

        //Retrieves all Sections By SectionIdParent and Text
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByText(int page, int numberByPage, int sectionIdParent, string text)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSectionsByText(page, numberByPage, sectionIdParent, text)
                          select new DEACIBusinessEntries.ROC.IndexProductInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.IndexProductInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;

        }

        //Retrieves all Sections By FullText
        public List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> rocGetMultipleSectionsByFulltext(int page, int numberByPage, string fullText, int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetMultipleSectionsByFulltext(page, numberByPage, fullText, sectionId)
                          select new DEACIBusinessEntries.ROC.MultipleSectionByTextInfo
                          {
                              SubSectionId = sectionRow.SubSectionId,
                              SubSectionDescription = sectionRow.SubSectionDescription,
                              SectionId = sectionRow.SectionId,
                              SectionIdParent = sectionRow.SectionIdParent,
                              SectionDescription = sectionRow.SectionDescription,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;

        }

        //Retrieves all Sections By Text
        public List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> rocGetMultipleSectionsByText(int page, int numberByPage, string text, int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetMultipleSectionsByText(page, numberByPage, text, sectionId)
                          select new DEACIBusinessEntries.ROC.MultipleSectionByTextInfo
                          {
                              SubSectionId = sectionRow.SubSectionId,
                              SubSectionDescription = sectionRow.SubSectionDescription,
                              SectionId = sectionRow.SectionId,
                              SectionIdParent = sectionRow.SectionIdParent,
                              SectionDescription = sectionRow.SectionDescription,
                              RowNumber = (int)sectionRow.RowNumber,
                              Total = (int)sectionRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        //Retrieves all Sections By SectionParent and SectionId
        public DEACIBusinessEntries.SectionInfo rocGetSection(int sectionIdParent, int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSection(sectionIdParent, sectionId)
                          select new DEACIBusinessEntries.SectionInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              SectionIdParent = sectionRow.SectionIdParent,
                              Active = sectionRow.Active
                          };

            List<DEACIBusinessEntries.SectionInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation[0] : null;
        }

        //Retrieves all Sections By SectionParent.
        public List<DEACIBusinessEntries.SectionInfo> rocGetSectionsByParentId(int sectionIdParent)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var section = from sectionRow in db.roc_spGetSectionsByParentId(sectionIdParent)
                          select new DEACIBusinessEntries.SectionInfo
                          {
                              SectionId = sectionRow.SectionId,
                              Description = sectionRow.Description,
                              SectionIdParent = sectionRow.SectionIdParent,
                              Active = sectionRow.Active
                          };

            List<DEACIBusinessEntries.SectionInfo> sectionInformation = section.ToList();

            return sectionInformation.Count() > 0 ? sectionInformation : null;
        }

        #endregion

        public static readonly SectionsDALC Instance = new SectionsDALC();

    }
}
