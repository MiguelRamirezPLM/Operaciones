using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductOtherContraindicationsBLC
    {
         #region Constructors

        private ProductOtherContraindicationsBLC() { }

        #endregion

        public void deleteProductOtherContraindication(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.delete(bEntity);
        }

        public void deleteProductOtherContraindicationAll(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.deleteAll(bEntity);
        }

        public int addProductOtherContraintication(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity)
        {
            bEntity.ProductId= MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductOtherContraindicationInfo> getOtherContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.getProductOtherContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }
        public static readonly ProductOtherContraindicationsBLC Instance = new ProductOtherContraindicationsBLC();
    }
}
