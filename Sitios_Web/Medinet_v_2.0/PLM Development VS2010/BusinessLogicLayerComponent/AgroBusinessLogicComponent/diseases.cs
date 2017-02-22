using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
    public class diseases
    {
        #region Contructors

            private diseases(){}

        #endregion

        #region Public Methods

            #region Select 

            public List<AgroEntityFramework.Diseases> getDiseases(int cropId, int stateId)
            {
                if (cropId <= 0 && stateId <= 0)
                    throw new ArgumentException("The country, book edition or drug does not exist");
                else
                {
                    List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instace.getDiseases(cropId, stateId);

                    return diseases;
                }
            }

            public List<AgroEntityFramework.Diseases> getDiseasesById(int diseaseId)
            {
                if (diseaseId <= 0)
                    throw new ArgumentException("The country, book edition or drug does not exist");
                else
                {
                    List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instace.getDiseasesById(diseaseId);

                    return diseases;
                }
            }

            public List<AgroEntityFramework.Diseases> getDiseasesByNameByScientificName(string name)
            {

                    List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instace.getDiseasesByNameByScientificName(name);

                    return diseases;
                
            }

            public List<AgroEntityFramework.Diseases> getDiseasesByCropId(int cropId, int stateId)
            {
                if (cropId <= 0)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instace.getDiseasesByCropId(cropId, stateId);

                    return diseases;
                }
            }

            #endregion

            #region Insert

            public void insertCropDiseases(AgroEntityFramework.CropDiseases cropDiseases)
            {
                if (cropDiseases == null)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.InsertCropDiseases(cropDiseases);
                }
            }

            public void insertCropFightDiseases(AgroEntityFramework.CropFightDiseases cropFightDiseases)
            {
                if (cropFightDiseases == null)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.InsertCropFightDiseases(cropFightDiseases);
                }
            }

            public void InsertDiseases(AgroEntityFramework.Diseases Diseases)
            {
                if (Diseases == null)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.InsertDiseases(Diseases);
                }
            }

            #endregion

            #region Update

            public void updateDiseaseName(string diseaseName , int diseaseId)
            {
                if (string.IsNullOrEmpty(diseaseName))
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.updateDiseaseName(diseaseName, diseaseId);
                }
            }

            public void updateScientificName(string scientificName ,int diseaseId)
            {
                if (string.IsNullOrEmpty(scientificName))
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.updateScientificName(scientificName, diseaseId);
                }
            }

            #endregion

            #region Delete

            public void deleteCropDiseases(int diseaseId, int stateId, int cropId)
            {
                if (diseaseId <= 0 || stateId <= 0 || cropId <= 0)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.deleteCropDiseases(diseaseId, stateId, cropId);
                }
            }

            public void deleteCropFightDiseases(int diseaseId, int stateId, int cropId, int activeSubstanceId, int fightTypeId)
            {
                if (diseaseId <= 0 || stateId <= 0 || cropId <= 0 || activeSubstanceId <= 0 || fightTypeId <= 0)
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.deleteCropFightDiseases(diseaseId, stateId, cropId, activeSubstanceId, fightTypeId);
                }
            }

            public void deleteDiseases(int diseaseId)
            {
                if (diseaseId <= 0 )
                    throw new ArgumentException("The Disease is null");
                else
                {
                    AgroDataAccessComponent.DiseasesDALC.Instace.deleteDiseases(diseaseId);
                }
            }

            #endregion

        #endregion

            public static readonly diseases Instanse = new diseases(); 
    }

}


