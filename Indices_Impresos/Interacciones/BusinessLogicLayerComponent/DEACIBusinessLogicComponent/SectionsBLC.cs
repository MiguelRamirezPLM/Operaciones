using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEACIBusinessLogicComponent
{
    public class SectionsBLC
    {

        #region Constructor

        private SectionsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves all Sections By SectionId
        public DEACIBusinessEntries.ROC.IndexProductInfo rocGetIndexProduct(string code, int editionId, int indexId, int sectionId)
        {
            if (editionId < 1 || indexId <1 || sectionId < 1)
            {
                throw new ArgumentException("The Edition or Index or Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetIndexProduct(editionId, indexId, sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By Index
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProducts(string code, int page, int numberByPage, int editionId, int indexId)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || indexId <= 0 )
            {
                throw new ArgumentException("The Page or Edition or Index does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetIndexProducts(page, numberByPage, editionId, indexId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By Index and By FullText
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByFulltext(string code, int page, int numberByPage, int editionId, int indexId, string fullText)
        {
            if (page < 0 || numberByPage < 0 || editionId < 1 || indexId < 1)
            {
                throw new ArgumentException("The Page or Edition or Index does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetIndexProductsByFulltext(page, numberByPage, editionId, indexId, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By Index and By Letter
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByLetter(string code, int page, int numberByPage, int editionId, int indexId, string letter)
        {
            if (page < 0 || numberByPage < 0 || editionId < 1 || indexId < 1)
            {
                throw new ArgumentException("The Page or Edition or Index does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetIndexProductsByLetter(page, numberByPage, editionId, indexId, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By Index and By Text
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetIndexProductsByText(string code, int page, int numberByPage, int editionId, int indexId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId < 1 || indexId < 1)
            {
                throw new ArgumentException("The Page or Edition or Index does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetIndexProductsByText(page, numberByPage, editionId, indexId, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionIdParent
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSections(string code, int page, int numberByPage, int sectionIdParent)
        {
            if (page < 0 || numberByPage < 0 || sectionIdParent < 1)
            {
                throw new ArgumentException("The Page or SectionParent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if(validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSections(page, numberByPage, sectionIdParent);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionIdParent and FullText
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByFulltext(string code, int page, int numberByPage, int sectionIdParent, string fullText)
        {
            if (page < 0 || numberByPage < 0 || sectionIdParent < 1)
            {
                throw new ArgumentException("The Page or SectionParent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSectionsByFulltext(page, numberByPage, sectionIdParent, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionIdParent and Letter
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByLetter(string code, int page, int numberByPage, int sectionIdParent, string letter)
        {
            if (page < 0 || numberByPage < 0 || sectionIdParent < 1)
            {
                throw new ArgumentException("The Page or SectionParent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSectionsByLetter(page, numberByPage, sectionIdParent, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionIdParent and Text
        public List<DEACIBusinessEntries.ROC.IndexProductInfo> rocGetSectionsByText(string code, int page, int numberByPage, int sectionIdParent, string text)
        {
            if (page < 0 || numberByPage < 0 || sectionIdParent < 1)
            {
                throw new ArgumentException("The Page or SectionParent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSectionsByText(page, numberByPage, sectionIdParent, text);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By FullText
        public List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> rocGetMultipleSectionsByFulltext(string code, int page, int numberByPage, string fullText, int sectionId)
        {
            if (page < 0 || numberByPage < 0 || sectionId < 1)
            {
                throw new ArgumentException("The Page or Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetMultipleSectionsByFulltext(page, numberByPage, fullText, sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By Text
        public List<DEACIBusinessEntries.ROC.MultipleSectionByTextInfo> rocGetMultipleSectionsByText(string code, int page, int numberByPage, string text, int sectionId)
        {
            if (page < 0 || numberByPage < 0 || sectionId < 1)
            {
                throw new ArgumentException("The Page or Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetMultipleSectionsByText(page, numberByPage, text, sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionParent and SectionId
        public DEACIBusinessEntries.SectionInfo rocGetSection(string code, int sectionIdParent, int sectionId)
        {
            if (sectionIdParent < 1 || sectionId < 1)
            {
                throw new ArgumentException("The SectionParent or Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSection(sectionIdParent, sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Sections By SectionParent.
        public List<DEACIBusinessEntries.SectionInfo> rocGetSectionsByParentId(string code, int sectionIdParent)
        {
            if (sectionIdParent < 1)
            {
                throw new ArgumentException("The SectionParent does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.SectionsDALC.Instance.rocGetSectionsByParentId(sectionIdParent);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly SectionsBLC Instance = new SectionsBLC();

    }
}
