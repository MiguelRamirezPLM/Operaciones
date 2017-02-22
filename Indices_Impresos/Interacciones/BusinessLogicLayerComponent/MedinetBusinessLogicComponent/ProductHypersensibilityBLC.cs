using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductHypersensibilityBLC
    {
         #region Constructors

        private ProductHypersensibilityBLC() { }

        #endregion

        public void deleteProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesInfo bEntity){
            MedinetDataAccessComponent.ProductHypersensibilityDALC.Instance.delete(bEntity);
        }
        public void addProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesInfo bEntity) {
            MedinetDataAccessComponent.ProductHypersensibilityDALC.Instance.insert(bEntity);
        }

        public static readonly ProductHypersensibilityBLC Instance = new ProductHypersensibilityBLC();
    }
}
