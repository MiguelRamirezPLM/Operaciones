using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
   public class AttributeContentBLC
    {
       #region Constructors

       private AttributeContentBLC() { }

       #endregion

       #region Public

       public List<AttributeContentInfo> getEncyclopediaContents(string code, int encyclopediaId)
       {
           if (encyclopediaId <= 0)
           {
               throw new ApplicationException("The encyclopediaId doesn´t valid");
           }
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.AttributeContentDALC.Instance.getEncyclopediaContents(encyclopediaId);

               else
                   throw new ApplicationException("The code is invalid or inactive");
           }
       }

       public List<AttributeContentInfo> getEncyclopediaContentsByAttributeType(string code, int encyclopediaId,int attributeTypeId)
       {
           if (encyclopediaId <= 0 || attributeTypeId <=0)
           {
               throw new ApplicationException("The encyclopediaId or attributeTypeId doesn´t valid");
           }
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.AttributeContentDALC.Instance.getEncyclopediaContentsByAttributeType(encyclopediaId,attributeTypeId);

               else
                   throw new ApplicationException("The code is invalid or inactive");
           }
       }

       public List<AttributeContentInfo> getEncyclopediaContentsByAttribute(string code, int encyclopediaId, int attributeGroupId)
       {
           if (encyclopediaId <= 0 || attributeGroupId <= 0)
           {
               throw new ApplicationException("The encyclopediaId or attributeTypeId doesn´t valid");
           }
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   return EncyclopediasDataAccessComponent.AttributeContentDALC.Instance.getEncyclopediaContentsByAttribute(encyclopediaId,attributeGroupId);

               else
                   throw new ApplicationException("The code is invalid or inactive");
           }
       }

       

       #endregion

       public static readonly AttributeContentBLC Instance = new AttributeContentBLC();
    }
}
