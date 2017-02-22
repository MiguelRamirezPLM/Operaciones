using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;


namespace MedinetBusinessLogicComponent
{
    public class ICDBLC
    {

      #region Constructors

        private ICDBLC() {}

      #endregion


      #region Methods

         public MedinetBusinessEntries.ICDInfo getICD(int icdId)
        {

            return MedinetDataAccessComponent.ICDDALC.Instance.getOne(icdId);
        }

         public List<ICDInfo> getICDByDrugs(int editionId, int productId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.ICDDALC.Instance.getICDByProduct(editionId, productId, pharmaFormId);
        }

         public List<ICDInfo> getICDByText(int editionId, string search)
        {
            return MedinetDataAccessComponent.ICDDALC.Instance.getICDByText(editionId, search);
        }

         public List<ProductByEditionInfo> getDrugsByICD(byte countryId, int editionId, int icdId)
         {
             return PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.getByICD(countryId, editionId, icdId, (byte)CatalogsBLC.TypeInEdition.Participante);
         }
         public List<ICDInfo> getAllICD(int? parentId)
         {
             return MedinetDataAccessComponent.ICDDALC.Instance.getAll(parentId);
         }

         public int getDifferenceICD(int productId, int icdIdParent) {
             return MedinetDataAccessComponent.ICDDALC.Instance.getDifferenceICD(productId, icdIdParent);
         }

         public List<ICDInfo> getICDWithOutProduct(int productId, string search, int pharmaFormId)
         {
             return MedinetDataAccessComponent.ICDDALC.Instance.getICDWithOutProduct(productId, search, pharmaFormId);
         }

      #endregion

     public static readonly ICDBLC Instance = new ICDBLC();


    }
}
