using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductMedicalContraindicationBLC
    {
        #region Constructors

        private ProductMedicalContraindicationBLC() { }

        #endregion

        public void deleteProductMedicalContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity){
            MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.delete(bEntity);
        }

        public void deleteAllProductMedicalContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.deleteAll(bEntity);
        }
        public int insertContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity)
        {
             bEntity.ProductId =MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.insert(bEntity);
             return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> getMedicalContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.getProductMedicalContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

      

        public static readonly ProductMedicalContraindicationBLC Instance = new ProductMedicalContraindicationBLC();
    }
}
