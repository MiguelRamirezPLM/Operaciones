using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_GeneralSearchBLC
    {
        #region Constructors

        private CE_GeneralSearchBLC() { }

        #endregion

        #region Public methods

        public PharmaSearchEngineBusinessEntries.SearchResultsInfo getResults(string code, byte countryId, int editionId, byte sourceId, string searchString)
        { 
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                PharmaSearchEngineBusinessEntries.SearchResultsInfo searchInfo = new PharmaSearchEngineBusinessEntries.SearchResultsInfo();

                // Search into Drugs catalog:
                searchInfo.Products = CE_ProductsBLC.Instance.getDrugs(code, countryId, editionId, searchString, sourceId);

                // Add Products log:
                if (searchInfo.Products.Count > 0)
                    CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos, "Text=" + searchString);

                // Search into Substances catalog:
                searchInfo.Substances = CE_SubstancesBLC.Instance.getSubstances(code, countryId, editionId, searchString, sourceId);

                // Add Substances log:
                if (searchInfo.Substances.Count > 0)
                    CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Sustancias, "Text=" + searchString);

                // Search into Indications catalog:
                searchInfo.Indications = CE_IndicationsBLC.Instance.getIndications(code, countryId, editionId, searchString, sourceId);

                // Add Indications log:
                if (searchInfo.Indications.Count > 0)
                    CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Indicaciones, "Text=" + searchString);

                // Search into Therapeutics catalog:
                searchInfo.Therapeutics = CE_TherapeuticBLC.Instance.getBySpanishDescription(code, countryId, editionId, searchString, sourceId);

                // Add Therapeutics log:
                if (searchInfo.Therapeutics.Count > 0)
                    CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Terapéutico, "Text=" + searchString);

                // Search into Labs catalog:
                searchInfo.Labs = CE_LaboratoriesBLC.Instance.getLabs(code, countryId, editionId, searchString, sourceId);

                // Add Labs log:
                if (searchInfo.Labs.Count > 0)
                    CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Laboratorios, "Text=" + searchString);

                return searchInfo;
            }
            else
                return null;
        }

        #endregion

        public static readonly CE_GeneralSearchBLC Instance = new CE_GeneralSearchBLC();
    }
}
