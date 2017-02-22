using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductHypersensibilityContraindicationsBLC
    {
         #region Constructors

        private ProductHypersensibilityContraindicationsBLC() { }

        #endregion

        public void deleteProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity){
            MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.delete(bEntity);
        }

        public void deleteProductHypersensibilityAll(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity)
        {
            MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.deleteAll(bEntity);
        }
        public int addProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity)
        {
            bEntity.ProductId= MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.insert(bEntity);
            return bEntity.ProductId;
        }

        public List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo> getHypersensibilityContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.getProductHypersensibilities(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public static readonly ProductHypersensibilityContraindicationsBLC Instance = new ProductHypersensibilityContraindicationsBLC();
    }
}
