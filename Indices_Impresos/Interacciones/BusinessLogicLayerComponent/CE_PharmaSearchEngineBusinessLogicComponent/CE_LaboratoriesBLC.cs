using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_LaboratoriesBLC
    {
        #region Constructor

        private CE_LaboratoriesBLC() { }

        #endregion

        #region Public Method

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getLabs(string code, byte countryId, int editionId, string labName, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getAll(countryId, editionId, labName);
            
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByLab(string code, byte countryId, int editionId, int divisionId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Lab, "DivisionId=" + divisionId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByDivision(countryId, editionId, divisionId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getLabsBySubstance(string code, byte countryId, int editionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Lab_Sust, "ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getByActiveSubstance(countryId, editionId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByLabBySubstance(string code, byte countryId, int editionId, int divisionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Lab_Sust, "DivisionId=" + divisionId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByDivisionByActiveSubstance(countryId, editionId, divisionId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneDrugsByLabBySubstance(string code, byte countryId, int editionId, int divisionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Lab_Sust, "DivisionId=" + divisionId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getAloneByDivisionByActiveSubstance(countryId, editionId, divisionId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedDrugsByLabBySubstance(string code, byte countryId, int editionId, int divisionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Lab_Sust, "DivisionId=" + divisionId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getCombinedByDivisionByActiveSubstance(countryId, editionId, divisionId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.DivisionByEditionInfo> getLabsByIndication(string code, byte countryId, int editionId, int indicationId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Lab_Ind, "IndicationId=" + indicationId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getByIndication(countryId, editionId, indicationId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByLabByIndication(string code, byte countryId, int editionId, int divisionId, int indicationId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Lab_Ind, "DivisionId=" + divisionId.ToString() + ";IndicationId=" + indicationId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByDivisionByIndication(countryId, editionId, divisionId, indicationId);
            }
            else
                return null;
        }

        public MedinetBusinessEntries.DivisionInformationInfo getLabInformation(string code, int divisionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getDivisionInformation(divisionId);
            else
                return null;
        }

        public MedinetBusinessEntries.DivisionsInfo getLaboratory(string code, int divisionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getById(divisionId);
            else
                return null;
        }

        public int? getNumberLabs(string code, byte countryId, int editionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getAll(editionId, countryId);
            else
                return null;
        }

        public PharmaSearchEngineBusinessEntries.DivisionByEditionInfo getByParams(string searchParam)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_DivisionsDALC.Instance.getByParams(searchParam);
        }

        #endregion

        public static readonly CE_LaboratoriesBLC Instance = new CE_LaboratoriesBLC();

    }
}
