using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class SymptomsBLC
    {

        #region Constructors

        private SymptomsBLC() { }

        #endregion

        #region Public methods

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getByEdition(string code, int editionId, string symptom)
        {
            if (editionId <= 0)
                throw new ArgumentException("The book edition does not exist");

            else if (string.IsNullOrEmpty(symptom))
                throw new ArgumentException("The parameter symptom cannot be null or empty");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> symptomList =
                        MedinetDataAccessComponent.SymptomsDALC.Instance.getByEdition(editionId, symptom);

                    PSELogsBusinessEntities.PSE_TrackingParentInfo BEntity = new PSELogsBusinessEntities.PSE_TrackingParentInfo();
                    BEntity.CodeString = code;
                    BEntity.EditionId = editionId;
                    BEntity.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                    BEntity.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));                    
                    
                    BEntity.SearchDate = null;
                    BEntity.SearchParameters = "Text=" + symptom;

                    if (symptomList.Count > 0)
                    {
                        BEntity.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sintomas;
                        BEntity.ChildrenTrackingInfo = new List<PSELogsBusinessEntities.PSE_TrackingListInfo>();
                        PSELogsBusinessEntities.PSE_TrackingListInfo sonTracking;

                        foreach (PharmaSearchEngineBusinessEntries.SymptomByEditionInfo sypmtom in symptomList)
                        {
                            sonTracking = new PSELogsBusinessEntities.PSE_TrackingListInfo();
                            sonTracking.CodeString = code;
                            sonTracking.EditionId = editionId;
                            sonTracking.SourceId =Convert.ToByte(Catalogs.Instance.getSourceIdByTargetName(PLMClientsBusinessLogicComponent.CodesBLC.Instance.getTargetByCodeString(code).TargetId));
                            sonTracking.SearchTypeId = (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto;
                            sonTracking.EntityId = (int)PSELogsBusinessEntities.Catalogs.Entities.Sintomas;
                            sonTracking.SearchDate = null;
                            sonTracking.SearchParameters = "SymptomId=" + sypmtom.SymptomId.ToString();
                            BEntity.ChildrenTrackingInfo.Add(sonTracking);
                        }
                    }
                    else
                    {
                        BEntity.EntityId = (byte)PSELogsBusinessEntities.Catalogs.Entities.Busqueda_no_encontrada;
                    }

                    this._PLMLogs.addLogParentActivity(BEntity);

                    //PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]), (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Texto,
                    //    (byte)PSELogsBusinessEntities.Catalogs.Entities.Medicamentos_por_Sintoma, "Text=" + symptom);
                    //List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> symptoms = MedinetDataAccessComponent.SymptomsDALC.Instance.getByEdition(editionId, symptom);
                    
                    this.addUrl(symptomList, System.Configuration.ConfigurationManager.AppSettings["hashkey"], editionId);

                    return symptomList;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getSymptomsDetailByEdition(string code, int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The Edition does not exist.");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> symptoms = MedinetDataAccessComponent.SymptomsDALC.Instance.getSymptomsDetailByEdition(editionId);
                    this.addUrl(symptoms, System.Configuration.ConfigurationManager.AppSettings["hashkey"], editionId);
                    return symptoms;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> getSymptomsByEditionByParent(string code, int editionId, int parentId)
        {
            if (editionId <= 0 || parentId <= 0)
                throw new ArgumentException("The Edition or Parent does not exist.");

            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> symptoms = MedinetDataAccessComponent.SymptomsDALC.Instance.getSymptomsByEditionByParent(editionId, parentId);
                    this.addUrl(symptoms, System.Configuration.ConfigurationManager.AppSettings["hashkey"], editionId);
                    return symptoms;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #region Private Methods

        private void addUrl(List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> symptoms, string applicationKey, int editionId)
        {
            foreach (PharmaSearchEngineBusinessEntries.SymptomByEditionInfo symptom in symptoms)
            {
                if (!string.IsNullOrEmpty(symptom.HeaderImage))
                    symptom.HeaderImage = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getOne(editionId).CountryId).CountryName +
                         "/" + System.Configuration.ConfigurationManager.AppSettings["OtcUrl"] + "/" + symptom.HeaderImage;

                if (symptom.SymptomChildrens.Count > 0)
                    this.addUrl(symptom.SymptomChildrens, applicationKey, editionId);
            }
        }

        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        public static readonly SymptomsBLC Instance = new SymptomsBLC();
    }
}
