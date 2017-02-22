using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_PacksBLC
    {
        #region Constructor

        private CE_PacksBLC() { }

        #endregion

        #region Public Method

        public List<MedinetBusinessEntries.PackInfo> getPacksByProduct(string code, int editionId, int productId, int pharmaFormId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_PacksDALC.Instance.getByProductByPF(editionId, productId, pharmaFormId);
            else
                return null;
        }

        #endregion

        public static readonly CE_PacksBLC Instance = new CE_PacksBLC();

    }
}
