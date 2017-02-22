using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductPharmacologicalContraindicationsBLC
    {
        #region Constructors

        private ProductPharmacologicalContraindicationsBLC() { }

        #endregion

        public void deleteProductPharmaContraindication(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity){
            MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.delete(bEntity);
        }

        public void deleteProductPharmaContraindicationAll(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.deleteAll(bEntity);
        }
        public int addProductPharmaContraintication(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity)
        {
            bEntity.ProductId =  MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo> getPharmacologicalContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.getProductPharmacologicalContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

        public static readonly ProductPharmacologicalContraindicationsBLC Instance = new ProductPharmacologicalContraindicationsBLC();
    }
}
