using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_IndicationsBLC
    {
        #region Constructor

        private CE_IndicationsBLC() { }

        #endregion

        #region Public Method

        public MedinetBusinessEntries.IndicationInfo getIndication(string code, int indicationId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_IndicationsDALC.Instance.getOne(indicationId);
            else
                return null;
        }

        public List<MedinetBusinessEntries.IndicationInfo> getIndications(string code, byte countryId, int editionId, string indication, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_IndicationsDALC.Instance.getAll(countryId, editionId, indication);
            
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByIndication(string code, byte countryId, int editionId, int indicationId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Ind, "IndicationId=" + indicationId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByIndication(countryId, editionId, indicationId);
            }
            else
                return null;
            
        }

        public List<MedinetBusinessEntries.IndicationInfo> getIndicationsByDrug(string code, byte countryId, int editionId, int drugId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Ind_Med, "ProductId=" + drugId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_IndicationsDALC.Instance.getByProduct(countryId, editionId, drugId);
            }
            else
                return null;
        }

        public MedinetBusinessEntries.IndicationInfo getByParams(string paramSearch)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_IndicationsDALC.Instance.getByParams(paramSearch);
        }

        #endregion

        public static readonly CE_IndicationsBLC Instance = new CE_IndicationsBLC();
    }
}
