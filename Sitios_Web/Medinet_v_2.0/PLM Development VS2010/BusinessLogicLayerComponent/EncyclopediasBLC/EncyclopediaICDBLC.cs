using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
    public class EncyclopediaICDBLC
    {
        #region Constructors

        private EncyclopediaICDBLC() { }

        #endregion

        #region Public
        public List<EncyclopediaICDInfo> getEncyclopediaICDByICDAssociated(string code, int ICDAssociatedId)
        {
            if (ICDAssociatedId <= 0)
                throw new ArgumentException("The ICD does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaICDDALC.Instance.getEncyclopediasICDByICDAssociated(ICDAssociatedId);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        public List<EncyclopediaICDInfo> getEncyclopediaICDByICDAssociatedByEdition(string code, int ICDAssociatedId,string isbn)
        {
            if (ICDAssociatedId <= 0)
                throw new ArgumentException("The ICD does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaICDDALC.Instance.getEncyclopediasICDByICDAssociatedByEdition(ICDAssociatedId,isbn);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        public List<EncyclopediaICDInfo> getEncyclopediaICDByICDAssociatedByType(string code, int ICDAssociatedId,int encyclopediaTypeId)
        {
            if (ICDAssociatedId <= 0||encyclopediaTypeId<=0)
                throw new ArgumentException("The ICD or the encyclopedia type does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaICDDALC.Instance.getEncyclopediasICDByICDAssociatedByType(ICDAssociatedId,encyclopediaTypeId);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        public List<EncyclopediaICDInfo> getEncyclopediaICDByICDAssociatedByTypeByEdition(string code, int ICDAssociatedId, int encyclopediaTypeId ,string isbn)
        {
            if (ICDAssociatedId <= 0||encyclopediaTypeId<=0)
                throw new ArgumentException("The ICD or the encyclopedia type does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.EncyclopediaICDDALC.Instance.getEncyclopediasICDByICDAssociatedByTypeByEdition(ICDAssociatedId,encyclopediaTypeId, isbn);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        #endregion

        public static readonly EncyclopediaICDBLC Instance = new EncyclopediaICDBLC();
    }
}
