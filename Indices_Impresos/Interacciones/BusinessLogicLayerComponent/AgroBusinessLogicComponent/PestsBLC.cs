using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
   public class PestsBLC
    {
        #region Contructors

       private PestsBLC() { 
       }


        #endregion

       #region Inserts

       public void addPest(String pestName,String scientificName,String image)
       {
           AgroEntityFramework.Pests pest = new AgroEntityFramework.Pests();
           pest.PestName = pestName.ToUpper() ;
           pest.ScientificName = scientificName.ToUpper();
           pest.Image = image;
           pest.Active = true;
           AgroDataAccessComponent.PestsDALC.Instance.addPest(pest);
       }
       public void addCropPest(AgroEntityFramework.CropPests cropPest)
       {
           AgroDataAccessComponent.PestsDALC.Instance.addCropPest(cropPest);
       }
       public void addCropFightPest(AgroEntityFramework.CropFightPests cropFightPest)
       {
           AgroDataAccessComponent.PestsDALC.Instance.addCropFightPest(cropFightPest);
       }

       #endregion

       #region Select

       public List<AgroEntityFramework.Pests> getPest() {
           return AgroDataAccessComponent.PestsDALC.Instance.getPests();
       }
       public List<AgroEntityFramework.Pests> getPestNonRelation(int cropId,int stateId)
       {
           return AgroDataAccessComponent.PestsDALC.Instance.getPestsNotRelation(cropId, stateId);
       }
       public List<AgroEntityFramework.Pests> getPestByCropId(int cropId,int stateId)
       {
           return AgroDataAccessComponent.PestsDALC.Instance.getPestsByCropId(cropId, stateId);
       }
      
       public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByPestsByTrateament(int cropId, int stateId, int fightTypeId, int pestsId)//NUEVO
       {
           if (cropId <= 0 && stateId <= 0)
               throw new ArgumentException("The country, book edition or drug does not exist");
           else
           {
               List<AgroEntityFramework.ActiveSubstances> weeds = AgroDataAccessComponent.PestsDALC.Instance.getSubstancesByCropByStateByPestsByTrateament(cropId, stateId, fightTypeId, pestsId);

               return weeds;
           }
       }
       public List<AgroEntityFramework.Pests> getPestByName(String pestName, int cropId, int stateId)
       {
           return AgroDataAccessComponent.PestsDALC.Instance.getPestsByName(pestName, cropId, stateId);
       }
       public List<AgroEntityFramework.Pests> getPestByName(String pestName, int cropId, int stateId, String ScientificName)
       {
           return AgroDataAccessComponent.PestsDALC.Instance.getPestsByName(pestName, cropId, stateId, ScientificName);
       }
       public AgroEntityFramework.Pests getPestById(int pestId)
       {
           if (pestId <= 0)
               throw new ArgumentException("Value null");
           else
           {
               return AgroDataAccessComponent.PestsDALC.Instance.getPestsById(pestId);
           }
       }
             #endregion


       #region delete
       public void deleteCropPests(int cropId,int stateID,int pestId) { 

           AgroEntityFramework.CropPests pest= new AgroEntityFramework.CropPests();
           pest.CropId=cropId;
           pest.StateId=stateID;
           pest.PestId=pestId;
           foreach (AgroEntityFramework.CropFightPests it in  AgroDataAccessComponent.PestsDALC.Instance.getCropFightPest(pestId, cropId, stateID))
           {
               this.deleteCropFightPests(it.CropId, it.StateId, it.PestId, it.ActiveSubstanceId, it.FightTypeId);
           }

           AgroDataAccessComponent.PestsDALC.Instance.deleteCropPest(pest);
       
       }
       public void deleteCropFightPests(int cropId, int stateID, int pestId, int activeSubstanceId, int fightTypeId)
       {

           AgroEntityFramework.CropFightPests cfp = new AgroEntityFramework.CropFightPests();
           cfp.CropId = cropId;
           cfp.StateId = stateID;
           cfp.PestId = pestId;
           cfp.ActiveSubstanceId = activeSubstanceId;
           cfp.FightTypeId = byte.Parse(fightTypeId.ToString());
           AgroDataAccessComponent.PestsDALC.Instance.deleteCropFightPest(cfp);

       }

       public void deleteCropFightPests(int pestId, int stateId, int cropId, int fightTypeId)
       {
           if (pestId <= 0 || stateId <= 0 || cropId <= 0 || fightTypeId <= 0)
               throw new ArgumentException("The Pest is null");
           else
           {
               AgroDataAccessComponent.PestsDALC.Instance.deleteCropFightPests(pestId, stateId, cropId, fightTypeId);
           }
       }

       //*****************************************************************************DELETE OF CATALOG PESTS*******************************************************+
       public void deleteCropPestsAll(int pestId)
       {

           AgroEntityFramework.CropPests pests = new AgroEntityFramework.CropPests();
           pests.PestId = pestId;
           foreach (AgroEntityFramework.CropFightPests it in AgroDataAccessComponent.PestsDALC.Instance.getCropFightPest(pestId))
           {
               this.deleteCropFightPests(it.PestId);
           }

           AgroDataAccessComponent.PestsDALC.Instance.deleteCropPestsCat(pests);
       }

       public void deleteCropFightPests(int pestId)
       {

           AgroEntityFramework.CropFightPests cfp = new AgroEntityFramework.CropFightPests();
           cfp.PestId = pestId;
           AgroDataAccessComponent.PestsDALC.Instance.deleteCropFightPestsCat(cfp);

       }
       public void deletePestsAll(int pestId)
       {
           AgroEntityFramework.Pests pests = new AgroEntityFramework.Pests();
           pests.PestId = pestId;
           foreach (AgroEntityFramework.CropPests it in AgroDataAccessComponent.PestsDALC.Instance.getCropPest(pestId))
           {
               this.deleteCropPestsAll(it.PestId);
           }

           AgroDataAccessComponent.PestsDALC.Instance.deletePestss(pests);
       }

       //aqui acaba nuevo metodo

       #endregion

       #region update
       public void updatePest(AgroEntityFramework.Pests pest) {
           pest.PestName = pest.PestName.ToUpper();
           pest.ScientificName = pest.ScientificName.ToUpper();
           if (pest.Image==null)
           {
               pest.Image=AgroDataAccessComponent.PestsDALC.Instance.getPestsById(pest.PestId).Image;
           }

           AgroDataAccessComponent.PestsDALC.Instance.updatePest(pest);
       }

  
            #endregion

       public static readonly PestsBLC Instance = new PestsBLC(); 
    }
}
