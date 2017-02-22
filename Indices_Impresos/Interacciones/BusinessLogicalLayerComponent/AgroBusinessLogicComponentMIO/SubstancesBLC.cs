using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
    public class SubstancesBLC
    {
        #region Contructors

        private SubstancesBLC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.ActiveSubstances> getSubstances(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> substances = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstances(cropId, stateId);

                return substances;
            }
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesById(int susbtanceId)
        {
            if (susbtanceId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> substances = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesById(susbtanceId);

                return substances;
            }
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByName(string name)
        {

            List<AgroEntityFramework.ActiveSubstances> substances = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesByName(name);

            return substances;

        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropId(int cropId, int stateId)
        {
            if (cropId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> substances = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesByCropId(cropId, stateId);

                return substances;
            }
        }

        public List<AgroEntityFramework.CropFightPests> getCropFightPests(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightPests> pests = AgroDataAccessComponent.SubstancesDALC.Instace.getCropFightPests(cropId, stateId);

                return pests;
            }
        }

        public List<AgroEntityFramework.CropFightDiseases> getCropFightDiseases(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightDiseases> diaseases = AgroDataAccessComponent.SubstancesDALC.Instace.getCropFightDiseases(cropId, stateId);

                return diaseases;
            }
        }

        public List<AgroEntityFramework.FightTypes> getFightTypes()
        {
            List<AgroEntityFramework.FightTypes> fightTypes = AgroDataAccessComponent.SubstancesDALC.Instace.getFightTypes();
            return fightTypes;

        }

        public List<AgroEntityFramework.plm_vwAllInformationPests> getAllInfoPests(int cropId, int stateId, int pestId)
        {
            if (cropId <= 0 && stateId <= 0 && pestId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.plm_vwAllInformationPests> pests = AgroDataAccessComponent.SubstancesDALC.Instace.getAllInfoPests(cropId, stateId, pestId);

                return pests;
            }
        }

        public List<AgroEntityFramework.plm_vwAllInformationDiseases> getAllInfoDiseases(int cropId, int stateId, int diseasesId)
        {
            if (cropId <= 0 && stateId <= 0 && diseasesId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.plm_vwAllInformationDiseases> diseases = AgroDataAccessComponent.SubstancesDALC.Instace.getAllInfoDiseases(cropId, stateId, diseasesId);

                return diseases;
            }
        }

        public List<AgroEntityFramework.plm_vwAllInformationWeeds> getAllInfoWeeds(int cropId, int stateId,int weedId)
        {
            if (cropId <= 0 && stateId <= 0 && weedId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.plm_vwAllInformationWeeds> weeeds = AgroDataAccessComponent.SubstancesDALC.Instace.getAllInfoWeeds(cropId, stateId, weedId);

                return weeeds;
            }
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByDiseasesByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int diseasesId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> weeds = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesByCropByStateByDiseasesByFightIdByTrateament(cropId, stateId, fightTypeId, diseasesId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByPestsByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int pestsId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> weeds = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesByCropByStateByPestsByFightIdByTrateament(cropId, stateId, fightTypeId, pestsId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByWeedsByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int weedId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.ActiveSubstances> weeds = AgroDataAccessComponent.SubstancesDALC.Instace.getSubstancesByCropByStateByWeedsByFightIdByTrateament(cropId, stateId, fightTypeId, weedId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.CropFightWeeds> getTreatmentByCropByStateByWeedsByFightId(int cropId, int stateId, int fightTypeId, int weedId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightWeeds> weeds = AgroDataAccessComponent.SubstancesDALC.Instace.getTreatmentByCropByStateByWeedsByFightId(cropId, stateId, fightTypeId, weedId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.CropFightPests> getTreatmentByCropByStateByPestsByFightId(int cropId, int stateId, int fightTypeId, int pestId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightPests> pests = AgroDataAccessComponent.SubstancesDALC.Instace.getTreatmentByCropByStateByPestsByFightId(cropId, stateId, fightTypeId, pestId);

                return pests;
            }
        }

        public List<AgroEntityFramework.CropFightDiseases> getTreatmentByCropByStateByDiseasesByFightId(int cropId, int stateId, int fightTypeId, int diseaseId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightDiseases> diseases = AgroDataAccessComponent.SubstancesDALC.Instace.getTreatmentByCropByStateByDiseasesByFightId(cropId, stateId, fightTypeId, diseaseId);

                return diseases;
            }
        }


        #endregion

        #region Insert

        public void insertCropWeeds(AgroEntityFramework.CropWeeds cropWeeds)
        {
            if (cropWeeds == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.InsertCropWeeds(cropWeeds);
            }
        }

        public void insertCropFightWeeds(AgroEntityFramework.CropFightWeeds cropFightWeeds)
        {
            if (cropFightWeeds == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.InsertCropFightWeeds(cropFightWeeds);
            }
        }

        public void InsertCropFightPest(AgroEntityFramework.CropFightPests CropFightPests)
        {
            if (CropFightPests == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.InsertCropFightPest(CropFightPests);
            }
        }
        public void InsertCropFightDisease(AgroEntityFramework.CropFightDiseases CropFightDiseases)
        {
            if (CropFightDiseases == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.InsertCropFightDisease(CropFightDiseases);
            }
        }

        public void InsertSubstances(AgroEntityFramework.ActiveSubstances Substances)
        {
            if (Substances == null)
                throw new ArgumentException("The Substances is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.InsertSubstances(Substances);
            }
        }

       

        #endregion

        #region Update

        public void updateDescription(int cropId, int stateId, int fightTypeId, int weedId, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescription(cropId, stateId, fightTypeId, weedId, description);
            }
        }

        public void updateDescriptionPest(int cropId, int stateId, int fightTypeId, int pestId, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescriptionPest(cropId, stateId, fightTypeId, pestId, description);
            }
        }

        public void updateDescriptionDisease(int cropId, int stateId, int fightTypeId, int diseaseId, string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescriptionDisease(cropId, stateId, fightTypeId, diseaseId, description);
            }
        }

        public void updateDescriptionByCropByStateByWeedsByFightId(int cropId, int stateId, int fightTypeId, int weedId, string description)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescriptionByCropByStateByWeedsByFightId(cropId, stateId, fightTypeId, weedId, description);
            }
        }

        public void updateDescriptionByCropByStateByDiseasesByFightId(int cropId, int stateId, int fightTypeId, int diseaseId, string description)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescriptionByCropByStateByDiseasesByFightId(cropId, stateId, fightTypeId, diseaseId, description);
            }
        }

        public void updateDescriptionByCropByStateByPestsByFightId(int cropId, int stateId, int fightTypeId, int pestId, string description)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateDescriptionByCropByStateByPestsByFightId(cropId, stateId, fightTypeId, pestId, description);
            }
        }

        public void updateSubstanceName(string substanceName, int substanceId)
        {
            if (string.IsNullOrEmpty(substanceName))
                throw new ArgumentException("The Substance is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.updateSubstanceName(substanceName, substanceId);
            }
        }

        #endregion

        #region Delete

        public void deleteCropSubstances(int substanceId, int stateId, int cropId)
        {
            if (substanceId <= 0 || stateId <= 0 || cropId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.deleteCropWeeds(substanceId, stateId, cropId);
            }
        }

        public void deleteCropFightWeeds(int cropId, int stateId, int fightTypeId, int weedId, int substanceId)
        {
            if (weedId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.deleteCropFightWeeds(cropId, stateId, fightTypeId, weedId, substanceId);
            }
        }

        public void deleteCropFightPests(int cropId, int stateId, int fightTypeId, int pestId, int substanceId)
        {
            if (pestId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.deleteCropFightPests(cropId, stateId, fightTypeId, pestId, substanceId);
            }
        }

        public void deleteCropFightDiseases(int cropId, int stateId, int fightTypeId, int diseaseId, int substanceId)
        {
            if (diseaseId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.deleteCropFightDiseases(cropId, stateId, fightTypeId, diseaseId, substanceId);
            }
        }

        public void deleteSubstances(int substanceId)
        {
            if (substanceId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.SubstancesDALC.Instace.deleteSubstances(substanceId);
            }
        }

        #endregion

        #endregion

        public static readonly SubstancesBLC Instanse = new SubstancesBLC();
    }

}


