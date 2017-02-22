using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
   public class ICDBLC
    {
       #region Constructors

       private ICDBLC() { }

       #endregion

       #region public

       public List<ICDInfo> getICDAssociatedByEncyclopediaContentsAttribute(string code, int encyclopediaId,int attributeId, int attributeGroupId)
       {
           if (encyclopediaId <= 0 || attributeGroupId <= 0 || attributeId <=0)
           {
               throw new ApplicationException("The encyclopediaId or attributes doesn´t valid");
           }
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.ICDDALC.Instance.getICDAssociatedByEncyclopediaContentAttribute(encyclopediaId,attributeId, attributeGroupId);

               else
                   throw new ApplicationException("The code is invalid or inactive");
           }
       }
       #endregion


       public static readonly ICDBLC Instance = new ICDBLC();
    }
}
