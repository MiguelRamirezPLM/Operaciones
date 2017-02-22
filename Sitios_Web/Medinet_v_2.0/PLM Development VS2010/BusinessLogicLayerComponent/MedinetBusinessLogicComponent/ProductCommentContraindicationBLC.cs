using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
   public class ProductCommentContraindicationBLC
    {
       #region Constructors

       private ProductCommentContraindicationBLC() { }

        #endregion

        public void deleteProductCommentContraindication(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity)
        {
            MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.delete(bEntity);
        }

        public void deleteProductCommentContraindicationAll(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity)
        {
            MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.deleteAll(bEntity);
        }
        public int addProductCommentContraintication(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity)
        {
            bEntity.ProductId= MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductCommentContraindicationsInfo> getCommentsContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.getCommentsContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public static readonly ProductCommentContraindicationBLC Instance = new ProductCommentContraindicationBLC();
    }
}
 