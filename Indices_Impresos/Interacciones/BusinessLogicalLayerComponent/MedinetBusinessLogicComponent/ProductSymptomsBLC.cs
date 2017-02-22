using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductSymptomsBLC
    {
         #region Constructors

        private ProductSymptomsBLC() { }

        #endregion

        public void addProductSymptom(MedinetBusinessEntries.ProductSymptomInfo bEntity) {
            MedinetDataAccessComponent.ProductSymptomDALC.Instance.insert(bEntity);
        }
        public void deleteProductSymptom(MedinetBusinessEntries.ProductSymptomInfo bEntity)
        {
            MedinetDataAccessComponent.ProductSymptomDALC.Instance.delete(bEntity);
        }
        public List<MedinetBusinessEntries.ProductSymptomInfo> getSymptomsByParticipantProducts(int editionId,int productId,int pharmaFormId) {
            return MedinetDataAccessComponent.ProductSymptomDALC.Instance.getSymptomsByParticipantProducts(editionId, productId, pharmaFormId);
        }

        public static readonly ProductSymptomsBLC Instance = new ProductSymptomsBLC();
    }
       
}
