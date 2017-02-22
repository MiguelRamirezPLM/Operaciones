using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_SubstancesBLC
    {
        #region Constructor

        private CE_SubstancesBLC() { }

        #endregion

        #region Public Method

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstances(string code, byte countryId, int editionId, string substance, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getAll(countryId, editionId, substance);
            
            else
                return null;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstanceByTherapeutic(string code, byte countryId, int editionId, int therapeuticId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_ATC, "TherapeuticId=" + therapeuticId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneDrugsBySubstance(string code, byte countryId, int editionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_Sust, "ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getAloneByActiveSubstance(countryId, editionId, substanceId); ;
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedDrugsBySubstance(string code, byte countryId, int editionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Sust, "ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getCombinedByActiveSubstance(countryId, editionId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionBySubstanceInfo> getCombinedDrugsBySubstanceDetail(string code, byte countryId, int editionId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_Sust, "ActiveSubstanceId=" + substanceId.ToString());

                List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> productByEditionList = 
                    CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getCombinedByActiveSubstance(countryId, editionId, substanceId);

                // Declare an actual return reference:
                List<PharmaSearchEngineBusinessEntries.ProductByEditionBySubstanceInfo> productByEditionBySubstanceList = 
                    new List<PharmaSearchEngineBusinessEntries.ProductByEditionBySubstanceInfo>();

                foreach (PharmaSearchEngineBusinessEntries.ProductByEditionInfo currentProductByEditionInfo in productByEditionList)
                {
                    PharmaSearchEngineBusinessEntries.ProductByEditionBySubstanceInfo productByEditionBySubstanceInfo = 
                        new PharmaSearchEngineBusinessEntries.ProductByEditionBySubstanceInfo();

                    // Fill properties which come from Super class ProductByEditionInfo:
                    productByEditionBySubstanceInfo.ProductId = currentProductByEditionInfo.ProductId;
                    productByEditionBySubstanceInfo.Brand = currentProductByEditionInfo.Brand;
                    productByEditionBySubstanceInfo.DivisionId = currentProductByEditionInfo.DivisionId;
                    productByEditionBySubstanceInfo.DivisionName = currentProductByEditionInfo.DivisionName;
                    productByEditionBySubstanceInfo.DivisionShortName = currentProductByEditionInfo.DivisionShortName;
                    productByEditionBySubstanceInfo.PharmaFormId = currentProductByEditionInfo.PharmaFormId;
                    productByEditionBySubstanceInfo.PharmaForm = currentProductByEditionInfo.PharmaForm;
                    productByEditionBySubstanceInfo.CategotyId = currentProductByEditionInfo.CategotyId;
                    productByEditionBySubstanceInfo.CategoryName = currentProductByEditionInfo.CategoryName;
                    productByEditionBySubstanceInfo.CountryCodes = currentProductByEditionInfo.CountryCodes;

                    // Then fill all the substances except the current substance given by substanceId parameter:
                    productByEditionBySubstanceInfo.Substances = this.getSubstancesByDrugBySubs(code, countryId, editionId, currentProductByEditionInfo.ProductId, substanceId, sourceId);

                    // Add to th final list:
                    productByEditionBySubstanceList.Add(productByEditionBySubstanceInfo);
                }

                return productByEditionBySubstanceList;
            }
            else
                return null;    
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getAloneDrugsByTheraBySubstance(string code, byte countryId, int editionId, int therapeuticId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Monoing_ATC_Sust, "TherapeuticId=" + therapeuticId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getAloneByTherapeuticByActiveSubstance(countryId, editionId, therapeuticId, substanceId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getCombinedDrugsByTheraBySubstance(string code, byte countryId, int editionId, int therapeuticId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_Multiing_ATC_Sust, "TherapeuticId=" + therapeuticId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getCombinedByTherapeuticByActiveSubstance(countryId, editionId, therapeuticId, substanceId);
            }
            else
                return null;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesByLab(string code, byte countryId, int editionId, int labId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Lab, "DivisionId=" + labId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getByDivision(countryId, editionId, labId);
            }
            else
                return null;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesByDrug(string code, byte countryId, int editionId, int drugId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {

                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med, "ProductId=" + drugId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getByProduct(countryId, editionId, drugId);
            }
            else
                return null;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getSubstancesByDrugBySubs(string code, byte countryId, int editionId, int drugId, int substanceId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Sust_Med_Sust, "ProductId=" + drugId.ToString() + ";ActiveSubstanceId=" + substanceId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getByProductWithoutActiveSubstance(countryId, editionId, drugId, substanceId);
            }
            else
                return null;
        }

        public MedinetBusinessEntries.ActiveSubstanceInfo getSubstance(string code, int substanceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getOne(substanceId);
            else
                return null;
        }

        public MedinetBusinessEntries.ActiveSubstanceInfo getByParams(string paramSearch)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_ActiveSubstancesDALC.Instance.getByParams(paramSearch);
        }

        #endregion

        public static readonly CE_SubstancesBLC Instance = new CE_SubstancesBLC();
           

    }
}
