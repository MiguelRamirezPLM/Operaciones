using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class TherapeuticUsesBLC
    {
        
        #region Constructor

        private TherapeuticUsesBLC() { }

        #endregion

        #region Public Methods

        //Retrieves all TherapeuticUses by level and ParentId
        public List<PEVBusinessEntries.TherapeuticUsesInfo> rocGetTherapeuticUsesByLevel(string code, int level, int parentId)
        {
            if (level < 1 || parentId < 1)
            {
                throw new ArgumentException("The Level or ParentId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUsesByLevel(level, parentId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves information about an TherapeuticUse by therapeuticId
        public PEVBusinessEntries.TherapeuticUsesInfo rocGetTherapeuticUse(string code, int therapeuticId)
        {
            if (therapeuticId < 1)
            {
                throw new ArgumentException("The TherapeuticId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUse(therapeuticId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all TherapeuticUses
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUses(string code, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUses(page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all TherapeuticUses by letter
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByLetter(string code, string letter, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUsesByLetter(letter, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all TherapeuticUses by text
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByText(string code, string text, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUsesByText(text, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all TherapeuticUses by fullText
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByFullText(string code, string fullText, int page, int numberBypage)
        {
            if (page < 0 || numberBypage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.TherapeuticsDALC.Instance.rocGetTherapeuticUsesByFullText(fullText, page, numberBypage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly TherapeuticUsesBLC Instance = new TherapeuticUsesBLC();
    }
}
