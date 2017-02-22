using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductPharmaGroupContraindicationBLC
    {
          #region Constructors

        private ProductPharmaGroupContraindicationBLC() { }

        #endregion

        public void deleteProductPharmaGroupContraindication(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity){
            MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.delete(bEntity);
        }

        public void deleteProductPharmaGroupContraindicationAll(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.deleteAll(bEntity);
        }
        public int addProductPharmaGroupContraintication(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity)
        {
            bEntity.ProductId= MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo> getPharmaGroupsContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.getProductPharmaGroupContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }
        public static readonly ProductPharmaGroupContraindicationBLC Instance = new ProductPharmaGroupContraindicationBLC();
    } 
}
