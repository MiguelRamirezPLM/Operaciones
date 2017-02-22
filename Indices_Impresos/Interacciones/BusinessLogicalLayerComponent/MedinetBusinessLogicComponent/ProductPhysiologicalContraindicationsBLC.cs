using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
   public class ProductPhysiologicalContraindicationsBLC
    {
       #region Constructors

       private ProductPhysiologicalContraindicationsBLC() { }

        #endregion

        public void deleteProductPhysContraindication(MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo bEntity){
            MedinetDataAccessComponent.ProductPhysiologicalContraindicationsDALC.Instance.delete(bEntity);
        }


        public void deleteProductPhysContraindicationAll(MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductPhysiologicalContraindicationsDALC.Instance.deleteAll(bEntity);
        }

        public int addProductPhysContraintication(MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo bEntity)
        {
            bEntity.ProductId= MedinetDataAccessComponent.ProductPhysiologicalContraindicationsDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> getPhysicologicalContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductPhysiologicalContraindicationsDALC.Instance.getProductPhysContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

        public static readonly ProductPhysiologicalContraindicationsBLC Instance = new ProductPhysiologicalContraindicationsBLC();
    }
}
