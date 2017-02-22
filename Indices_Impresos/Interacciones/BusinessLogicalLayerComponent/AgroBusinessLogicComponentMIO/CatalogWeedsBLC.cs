using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
    public class CatalogWeedsBLC
    {
        #region Contructors

        private CatalogWeedsBLC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.WeedTypes> getTypesWeedsByName(string weedTypeName)
        {

            if (string.IsNullOrEmpty(weedTypeName))
                throw new ArgumentException("The Type Weed is null");
            else
            {
                List<AgroEntityFramework.WeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getTypesWeedsByName(weedTypeName);

                return weeds;
            }

        }


        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeeds(int cropId, int stateId)
        {
            if (cropId <= 0 )
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeeds(cropId, stateId);

                return weeds;
            }
        }


        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getGridWeedAdd(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getGridWeedAdd(cropId, stateId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsList()
        {
            //if (cropId <= 0 && stateId <= 0)
            //    throw new ArgumentException("The country, book edition or drug does not exist");
            //else
            //{
                List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsList();

                return weeds;
            //}
        }
        public List<AgroEntityFramework.Weeds> getWeedsById(int weedId)
        {
            if (weedId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.Weeds> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsById(weedId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.Weeds> getWeedsByNameByScientificName(string name)
        {

            List<AgroEntityFramework.Weeds> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsByNameByScientificName(name);

            return weeds;

        }
        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsByNameByScientificNameView(string name)
        {

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsByNameByScientificNameView(name);

            return weeds;

        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsByCropId(int cropId, int stateId)
        {
            if (cropId <= 0)
                throw new ArgumentException("The Disease is null");
            else
            {
                List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsByCropId(cropId, stateId);

                return weeds;
            }
        }

        public List<AgroEntityFramework.WeedTypes> getWeedsTypes()
        {         
            List<AgroEntityFramework.WeedTypes> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getWeedsTypes();
            return weeds;

        }

        public List<AgroEntityFramework.CropFightWeeds> getCropFightWeeds(int cropId, int stateId)
        {
            if (cropId <= 0 && stateId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                List<AgroEntityFramework.CropFightWeeds> weeds = AgroDataAccessComponent.WeedsDALC.Instace.getCropFightWeeds(cropId, stateId);

                return weeds;
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

        public void InsertWeeds(AgroEntityFramework.Weeds Weeds)
        {
            if (Weeds == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.InsertWeeds(Weeds);
            }
        }

        public void InsertTypesWeeds(AgroEntityFramework.WeedTypes typeWeeds)
        {
            if (typeWeeds == null)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.InsertTypesWeeds(typeWeeds);
            }
        }

        #endregion

        #region Update

        public void updateWeedName(string weedName, int weedId)
        {
            if (string.IsNullOrEmpty(weedName))
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.updateWeedName(weedName, weedId);
            }
        }

        public void updateScientificName(string scientificName, int weedId)
        {
            if (string.IsNullOrEmpty(scientificName))
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.updateScientificName(scientificName, weedId);
            }
        }

        public void updateWeedTypeId(int weedTypeId, int weedId)
        {
            if (weedTypeId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.updateWeedTypeId(weedTypeId, weedId);
            }
        }

        #endregion

        #region Delete

        public void deleteCropWeeds(int weedId, int stateId, int cropId)
        {
            if (weedId <= 0 || stateId <= 0 || cropId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.deleteCropWeeds(weedId, stateId, cropId);
            }
        }

      

        public void deleteWeeds(int weedId)
        {
            if (weedId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.deleteWeeds(weedId);
            }
        }

        public void deleteCropWeedsAll(int cropId, int stateID, int weedId)
        {

            AgroEntityFramework.CropWeeds pest = new AgroEntityFramework.CropWeeds();
            pest.CropId = cropId;
            pest.StateId = stateID;
            pest.WeedId = weedId;
            foreach (AgroEntityFramework.CropFightWeeds it in AgroDataAccessComponent.WeedsDALC.Instace.getCropFightWeed(weedId, cropId, stateID))
            {
                this.deleteCropFightWeeds(it.CropId, it.StateId, it.WeedId, it.ActiveSubstanceId, it.FightTypeId);
            }

            AgroDataAccessComponent.WeedsDALC.Instace.deleteCropWeedss(pest);
        }
       
        public void deleteCropFightWeeds(int cropId, int stateID, int weedId, int activeSubstanceId, int fightTypeId)
        {

            AgroEntityFramework.CropFightWeeds cfp = new AgroEntityFramework.CropFightWeeds();
            cfp.CropId = cropId;
            cfp.StateId = stateID;
            cfp.WeedId = weedId;
            cfp.ActiveSubstanceId = activeSubstanceId;
            cfp.FightTypeId = byte.Parse(fightTypeId.ToString());
            AgroDataAccessComponent.WeedsDALC.Instace.deleteCropFightWeeds(cfp);

        }

        //*****************************************************************************DELETE OF CATALOG WEEDS*******************************************************+
        public void deleteCropWeedsAll(int weedId)
        {

            AgroEntityFramework.CropWeeds weeds = new AgroEntityFramework.CropWeeds();
            weeds.WeedId = weedId;
            foreach (AgroEntityFramework.CropFightWeeds it in AgroDataAccessComponent.WeedsDALC.Instace.getCropFightWeed(weedId))
            {
                this.deleteCropFightWeeds(it.WeedId);
            }

            AgroDataAccessComponent.WeedsDALC.Instace.deleteCropWeedssCat(weeds);
        }

        public void deleteCropFightWeeds(int weedId)
        {

            AgroEntityFramework.CropFightWeeds cfp = new AgroEntityFramework.CropFightWeeds();
            cfp.WeedId = weedId;
            AgroDataAccessComponent.WeedsDALC.Instace.deleteCropFightWeedsCat(cfp);

        }
        public void deleteWeedsAll(int weedId)
        {
            AgroEntityFramework.Weeds weeds = new AgroEntityFramework.Weeds();
            weeds.WeedId = weedId;
            foreach (AgroEntityFramework.CropWeeds it in AgroDataAccessComponent.WeedsDALC.Instace.getCropWeed(weedId))
            {
                this.deleteCropWeedsAll(it.WeedId);
            }

            AgroDataAccessComponent.WeedsDALC.Instace.deleteWeedss(weeds);
        }

//aqui acaba nuevo metodo
        public void deleteCropFightWeeds(int weedId, int stateId, int cropId, int fightTypeId)
        {
            if (weedId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
                throw new ArgumentException("The Weed is null");
            else
            {
                AgroDataAccessComponent.WeedsDALC.Instace.deleteCropFightWeeds(weedId, stateId, cropId, fightTypeId);
            }
        }

        #endregion

        #endregion

        public static readonly CatalogWeedsBLC Instanse = new CatalogWeedsBLC();
    }

}


