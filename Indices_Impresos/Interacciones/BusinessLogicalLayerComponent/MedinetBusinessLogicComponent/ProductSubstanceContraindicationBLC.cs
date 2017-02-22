using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductSubstanceContraindicationBLC
    {
          #region Constructors

        private ProductSubstanceContraindicationBLC() { }

        #endregion

        public void deleteProductSubstanceContraindication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.delete(bEntity);
        }

        public void deleteProductSubstanceContraindicationAll(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.deleteAll(bEntity);
        }
        public void addProductSubstanceContraintication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.insert(bEntity);
        }

        public List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> getContraindicationSubstances(int productId,int categoryId,int divisionId,int pharmaFormId,int activeSubstanceId) {
        
        return MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.getContraindicationSubstances(productId,
        pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

        public int insertContraindication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bussinesEntity)
        {
            
            bussinesEntity.ProductId = MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.insert(bussinesEntity);
            return bussinesEntity.ProductId;

        }


        public static readonly ProductSubstanceContraindicationBLC Instance = new ProductSubstanceContraindicationBLC();
    }
}
