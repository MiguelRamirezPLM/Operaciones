using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
    public class DiseasesBLC
    {
        #region Contructors

        private DiseasesBLC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.Diseases> getDiseases(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instance.getDiseases(cropId, stateId);

                return diseases;
            }
        }

        public List<AgroEntityFramework.Diseases> getDiseasesById(int diseaseId)
        {
            if (diseaseId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instance.getDiseasesById(diseaseId);

                return diseases;
            }
        }

        public List<AgroEntityFramework.Diseases> getDiseasesByNameByScientificName(string name)
        {

            List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instance.getDiseasesByNameByScientificName(name);

            return diseases;

        }

        public List<AgroEntityFramework.Diseases> getDiseasesByCropByState(int cropId, int stateId)
        {
            if (cropId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                List<AgroEntityFramework.Diseases> diseases = AgroDataAccessComponent.DiseasesDALC.Instance.getDiseasesByCropByState(cropId, stateId);

                return diseases;
            }
        }

        #endregion

        #region Insert

        public void addCropDiseases(AgroEntityFramework.CropDiseases cropDiseases)
        {
            if (cropDiseases == null)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.addCropDiseases(cropDiseases);
            }
        }

        public void addCropFightDiseases(AgroEntityFramework.CropFightDiseases cropFightDiseases)
        {
            if (cropFightDiseases == null)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.addCropFightDiseases(cropFightDiseases);
            }
        }

        public void addDiseases(AgroEntityFramework.Diseases Diseases)
        {
            if (Diseases == null)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.addDiseases(Diseases);
            }
        }

        #endregion

        #region Update

        public void updateDiseaseName(string diseaseName, int diseaseId, string scientificName)
        {
            if (string.IsNullOrEmpty(diseaseName))
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.updateDisease(diseaseName, diseaseId, scientificName);
            }
        }


        #endregion

        #region Delete


        public void deleteCropDiseases(int diseaseId, int stateId, int cropId)
        {

            AgroEntityFramework.CropDiseases disease = new AgroEntityFramework.CropDiseases();
            disease.CropId = cropId;
            disease.StateId = stateId;
            disease.DiseaseId = diseaseId;
            foreach (AgroEntityFramework.CropFightDiseases it in AgroDataAccessComponent.DiseasesDALC.Instance.getCropFightDisease(diseaseId, cropId, stateId))
            {
                this.deleteCropFightDisease(it.CropId, it.StateId, it.DiseaseId, it.ActiveSubstanceId, it.FightTypeId);
            }

            AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropDiseasess(disease);

        }

        public void deleteCropFightDisease(int cropId, int stateId, int diseaseId, int activeSubstanceId, int fightTypeId)
        {

            AgroEntityFramework.CropFightDiseases cfp = new AgroEntityFramework.CropFightDiseases();
            cfp.CropId = cropId;
            cfp.StateId = stateId;
            cfp.DiseaseId = diseaseId;
            cfp.ActiveSubstanceId = activeSubstanceId;
            cfp.FightTypeId = byte.Parse(fightTypeId.ToString());
            AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropFightDiseasess(cfp);

        }

        public void deleteCropFightDiseases(int diseaseId, int stateId, int cropId, int activeSubstanceId, int fightTypeId)
        {
            if (diseaseId <= 0 || stateId <= 0 || cropId <= 0 || activeSubstanceId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropFightDiseases(diseaseId, stateId, cropId, activeSubstanceId, fightTypeId);
            }
        }

        public void deleteDiseases(int diseaseId)
        {
            if (diseaseId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.deleteDiseases(diseaseId);
            }
        }

        public void deleteCropFightDiseases(int diseaseId, int stateId, int cropId, int fightTypeId)
        {
            if (diseaseId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropFightDiseases(diseaseId, stateId, cropId, fightTypeId);
            }
        }

        //*****************************************************************************DELETE OF CATALOG DISEASES*******************************************************+
        public void deleteCropDiseasesAll(int diseaseId)
        {

            AgroEntityFramework.CropDiseases diseases = new AgroEntityFramework.CropDiseases();
            diseases.DiseaseId = diseaseId;
            foreach (AgroEntityFramework.CropFightDiseases it in AgroDataAccessComponent.DiseasesDALC.Instance.getCropFightDisease(diseaseId))
            {
                this.deleteCropFightDiseases(it.DiseaseId);
            }

            AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropDiseasessCat(diseases);
        }

        public void deleteCropFightDiseases(int diseaseId)
        {

            AgroEntityFramework.CropFightDiseases cfp = new AgroEntityFramework.CropFightDiseases();
            cfp.DiseaseId = diseaseId;
            AgroDataAccessComponent.DiseasesDALC.Instance.deleteCropFightDiseasesCat(cfp);

        }
        public void deleteDiseasesAll(int diseaseId)
        {
            AgroEntityFramework.Diseases diseases = new AgroEntityFramework.Diseases();
            diseases.DiseaseId = diseaseId;
            foreach (AgroEntityFramework.CropDiseases it in AgroDataAccessComponent.DiseasesDALC.Instance.getCropDisease(diseaseId))
            {
                this.deleteCropDiseasesAll(it.DiseaseId);
            }

            AgroDataAccessComponent.DiseasesDALC.Instance.deleteDiseasess(diseases);
        }

        //aqui acaba nuevo metodo

        #endregion

        #endregion

        public static readonly DiseasesBLC Instanse = new DiseasesBLC();
    }

}


