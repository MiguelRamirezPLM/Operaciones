using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class IppaProductContraindicationsBLC
    {
        public Boolean checkProductContraindications(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstance)
        {
            if (MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.checkProductInteractions(categoryId, pharmaFormId, productId, divisionId, activeSubstance) > 0)
                return true;
            else
                return false;
        }
        public string getHtmlContraindicationByProduct(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.getHtmlContentContraindicationByProduct(categoryId, pharmaFormId, productId, divisionId);
        }

        public List<MedinetBusinessEntries.IppaProductContraindicationsInfo> getProductContraindications(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstance)
        {
            return MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.getIppaProductsContraindications(categoryId, pharmaFormId, productId, divisionId, activeSubstance);
        }
        public List<MedinetBusinessEntries.IppaProductContraindicationsInfo> getProductContraindications(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            return MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.getIppaProductsContraindications(categoryId, pharmaFormId, productId, divisionId);
        }

        public void addProductContraindications(MedinetBusinessEntries.IppaProductContraindicationsInfo BEntity,bool deleteClasification)
        {
            MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.insert(BEntity,deleteClasification);
        }
        public void updateProductContraindications(MedinetBusinessEntries.IppaProductContraindicationsInfo BEntity)
        {
            MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.update(BEntity);
        }

        public void checkStatusCMProduct(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.checkStatusCMProductChanges(categoryId, pharmaFormId, productId, divisionId);
        }
        public void deleteProductContraindication(MedinetBusinessEntries.IppaProductContraindicationsInfo BEntity)
        {
        
            MedinetDataAccessComponent.IppaProductContraindicationsDALC.Instance.delete(BEntity);


        }

        public static readonly IppaProductContraindicationsBLC Instance = new IppaProductContraindicationsBLC();
    }
}
