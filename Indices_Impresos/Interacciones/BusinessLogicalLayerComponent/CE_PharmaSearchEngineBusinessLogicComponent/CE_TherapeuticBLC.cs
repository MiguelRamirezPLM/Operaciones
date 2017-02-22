using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_TherapeuticBLC
    {
        #region Constructor

        private CE_TherapeuticBLC() { }

        #endregion

        #region Public Method
        
        public List<MedinetBusinessEntries.TherapeuticInfo> getBySpanishDescription(string code, byte countryId, int editionId, string spanishDescription, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getBySpanishDescription(spanishDescription);
            
            else
                return null;
        }

        public List<MedinetBusinessEntries.TherapeuticInfo> getByParent(string code, byte countryId, int editionId, int? therapeuticId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Terapéutico, "TherapeuticId=" + therapeuticId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByParent(therapeuticId);
            }
            else
                return null;   
        }

        public List<MedinetBusinessEntries.TherapeuticInfo> getByParent(int? therapeuticId)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByParent(therapeuticId);
        }

        public MedinetBusinessEntries.TherapeuticInfo getByTherapeutic(string code, byte countryId, int editionId, int? therapeuticId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                        (byte)PSELogsBusinessEntities.Catalogs.Entities.Terapéutico, "TherapeuticId=" + therapeuticId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByTherapeuticId(therapeuticId);
            }
            else
                return null;
        }

        public MedinetBusinessEntries.TherapeuticInfo getByTherapeutic(int? therapeuticId)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByTherapeuticId(therapeuticId);    
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByTherapeutic(string code, byte countryId, int editionId, int therapeuticId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.Med_ATC, "TherapeuticId=" + therapeuticId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.ProductByEditionInfo> getDrugsByTherapeutic(byte countryId, int editionId, int therapeuticId)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_ProductsDALC.Instance.getByTherapeutic(countryId, editionId, therapeuticId);
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getTherapeuticsByDrug(string code, byte countryId, int editionId, int drugId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med, "ProductId=" + drugId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByProduct(countryId, editionId, drugId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getTherapeuticsByDrugByPharmaForm(string code, byte countryId, int editionId, int drugId, int pharmaFormId, byte sourceId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
            {
                CE_PSELogsBusinessLogicComponent.CE_PSE_TrackingBLC.Instance.addLog(code, editionId, sourceId, (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada,
                    (byte)PSELogsBusinessEntities.Catalogs.Entities.ATC_Med, "ProductId=" + drugId.ToString() + ";PharmaFormId=" + pharmaFormId.ToString());

                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByProductByPharmaForm(countryId, editionId, drugId, pharmaFormId);
            }
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getTherpeuticsByDrugByTherapeutic(string code, byte countryId, int editionId, int drugId, int therapeuticId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByProductByTherapeutic(countryId, editionId, drugId, therapeuticId);
            else
                return null;
        }

        public List<PharmaSearchEngineBusinessEntries.TherapeuticByProductInfo> getTherapeutics(string code, byte countryId, int editionId)
        {
            if (CE_PLMClientsBusinessLogicComponent.CE_CodesBLC.Instance.validCode(code))
                return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getAll(countryId, editionId);
            else
                return null;
        }

        public int getMinTherapeuticValue()
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getMinTherapeuticValue();
        }

        public int getMaxTherapeuticValue()
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getMaxTherapeuticValue();
        }

        public MedinetBusinessEntries.TherapeuticInfo getByParams(string searchParams)
        {
            return CE_PharmaSearchEngineDataAccessComponent.CE_TherapeuticsDALC.Instance.getByParams(searchParams);
        }

        #endregion

        public static readonly CE_TherapeuticBLC Instance = new CE_TherapeuticBLC();
    }
}
