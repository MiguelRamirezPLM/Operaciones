using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_ProductsBLC
    {
        #region Constructor

        private CE_ProductsBLC() { }

        #endregion

        #region Public Method

        public PharmaSearchEngineBusinessEntries.DrugInfo getDrug(string code, byte countryId, int editionId, int drugId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getById(countryId, editionId, drugId);
            else
                return null;
        }

        public PharmaSearchEngineBusinessEntries.DrugPharmaFormInfo getDrugByPharmaForm(string code, int drugId, int pharmaFormId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByIdByPharmaForm(drugId, pharmaFormId);
            else
                return null;
        }

        public PharmaSearchEngineBusinessEntries.ProductByEditionInfo getByParams(string paramSearch)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByParams(paramSearch);  
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugs(string code, byte countryId, int editionId, string drug, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getAll(countryId, editionId, drug);

            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsBySubstance(string code, byte countryId, int editionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Sust, "ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByActiveSubstance(countryId, editionId, substanceId);
            }
            else
                return null;
        }

        public MedinetBusinessEntries.ParticipantProductsInfo getContent(string code, int editionId, int drugId, int pharmaFormId, int labId, int categoryId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido, "DivisionId=" + labId.ToString() + ";CategoryId=" + categoryId.ToString() + ";ProductId=" + drugId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getContent(editionId, drugId, pharmaFormId, labId, categoryId); ;
            }
            else
                return null;
        }

        public bool? checkContent(string code, int editionId, int drugId, int pharmaFormId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.checkContent(editionId, drugId, pharmaFormId);
            else
                return null;
        }

        public int? getNumberDrugs(string code, byte countryId, int editionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getAll(countryId, editionId);
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> searchText(string code, byte countryId, int editionId, string attributes, string prefix, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addAttributeLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos, "Text=" + prefix, attributes);

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.searchText(countryId, editionId, attributes, prefix);
            }
            else
                return null;
        }

        #endregion

        public static readonly CE_ProductsBLC Instance = new CE_ProductsBLC();       
    }
}
