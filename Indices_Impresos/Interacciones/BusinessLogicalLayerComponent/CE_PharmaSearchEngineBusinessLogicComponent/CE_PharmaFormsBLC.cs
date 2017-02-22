using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_PharmaFormsBLC
    {
        #region Constructor

        private CE_PharmaFormsBLC() { }

        #endregion

        #region Public Method

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getPharmaFormsByDrug(string code, byte countryId, int editionId, int drugId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_PharmaFormsDALC.Instance.getByProduct(countryId, editionId, drugId);
            else
                return null;
        }

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getPharmaForms(string code, byte countryId, int editionId, string pharmaForm)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_PharmaFormsDALC.Instance.getAll(countryId, editionId, pharmaForm);
            else
                return null;
        }
        
        #endregion

        public static readonly CE_PharmaFormsBLC Instance = new CE_PharmaFormsBLC();
    }
}
