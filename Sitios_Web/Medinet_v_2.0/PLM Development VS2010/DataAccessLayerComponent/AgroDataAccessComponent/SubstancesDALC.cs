using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroDataAccessComponent
{
    public class SubstancesDALC
    {
        #region Constructor

        private SubstancesDALC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesById(int activeSubstanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from a in db.ActiveSubstances
                        where a.ActiveSubstanceId == activeSubstanceId
                        select a;

            List<AgroEntityFramework.ActiveSubstances> listSubstance = query.ToList();

            return listSubstance;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstances(int activeSubstanceId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from wRow in db.ActiveSubstances
                        join cw in db.CropFightWeeds
                                on wRow.ActiveSubstanceId equals cw.WeedId into JoinedEmpDept
                        from cw in JoinedEmpDept.DefaultIfEmpty()
                        where !(from o in db.CropFightWeeds
                                where o.ActiveSubstanceId == activeSubstanceId && o.StateId == stateId
                                select o.ActiveSubstanceId)
                                .Contains(wRow.ActiveSubstanceId)
                        select wRow;

            List<AgroEntityFramework.ActiveSubstances> listSubstances = query.ToList();

            return listSubstances;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByName(string name)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from a in db.ActiveSubstances
                        where a.ActiveSubstanceName.Contains(name)
                        select a;

            List<AgroEntityFramework.ActiveSubstances> listSusbtances = query.ToList();

            return listSusbtances;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropId(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.ActiveSubstances
                        join cw in db.CropFightWeeds
                               on w.ActiveSubstanceId equals cw.ActiveSubstanceId
                        where cw.CropId == cropId && cw.StateId == stateId
                        select w;

            List<AgroEntityFramework.ActiveSubstances> listSusbtances = query.ToList();

            return listSusbtances;
        }

        public List<AgroEntityFramework.CropFightPests> getCropFightPests(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.Pests
                        join cw in db.CropFightPests
                               on w.PestId equals cw.PestId
                        where cw.CropId == cropId && cw.StateId == stateId
                        select cw;

            List<AgroEntityFramework.CropFightPests> listPests = query.ToList();

            return listPests;
        }

        public List<AgroEntityFramework.CropFightDiseases> getCropFightDiseases(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.Diseases
                        join cw in db.CropFightDiseases
                               on w.DiseaseId equals cw.DiseaseId
                        where cw.CropId == cropId && cw.StateId == stateId
                        select cw;

            List<AgroEntityFramework.CropFightDiseases> listDiaseases = query.ToList();

            return listDiaseases;
        }

        public List<AgroEntityFramework.FightTypes> getFightTypes()
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.FightTypes
                        select w;

            List<AgroEntityFramework.FightTypes> listFight = query.ToList();

            return listFight;
        }

        public List<AgroEntityFramework.plm_vwAllInformationPests> getAllInfoPests(int cropId, int stateId, int pestId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwAllInformationPests
                        where w.CropId == cropId && w.StateId == stateId && w.PestId == pestId
                        select w;
            List<AgroEntityFramework.plm_vwAllInformationPests> ListPests = query.ToList();

            return ListPests;
        }

        public List<AgroEntityFramework.plm_vwAllInformationDiseases> getAllInfoDiseases(int cropId, int stateId, int diseaseId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwAllInformationDiseases
                        where w.CropId == cropId && w.StateId == stateId && w.DiseaseId == diseaseId
                        select w;
            List<AgroEntityFramework.plm_vwAllInformationDiseases> ListDiseases = query.ToList();

            return ListDiseases;
        }

        public List<AgroEntityFramework.plm_vwAllInformationWeeds> getAllInfoWeeds(int cropId, int stateId, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwAllInformationWeeds
                        where w.CropId == cropId && w.StateId == stateId && w.WeedId == weedId
                        select w;
            List<AgroEntityFramework.plm_vwAllInformationWeeds> ListWeeds = query.ToList();

            return ListWeeds;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByDiseasesByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int diseasesId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightDiseases
                        join sus in db.ActiveSubstances
                                on d.ActiveSubstanceId equals sus.ActiveSubstanceId
                        where d.CropId == cropId && d.StateId == stateId && d.DiseaseId == diseasesId && d.FightTypeId == fightTypeId 
                        select sus;
            List<AgroEntityFramework.ActiveSubstances> Listsusbstances = query.ToList();

            return Listsusbstances;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByPestsByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int pestsId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightPests
                        join sus in db.ActiveSubstances
                                on d.ActiveSubstanceId equals sus.ActiveSubstanceId
                        where d.CropId == cropId && d.StateId == stateId && d.PestId == pestsId && d.FightTypeId == fightTypeId 
                        select sus;
            List<AgroEntityFramework.ActiveSubstances> Listsusbstances = query.ToList();

            return Listsusbstances;
        }

        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByWeedsByFightIdByTrateament(int cropId, int stateId, int fightTypeId, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightWeeds
                        join sus in db.ActiveSubstances
                                on d.ActiveSubstanceId equals sus.ActiveSubstanceId
                        where d.CropId == cropId && d.StateId == stateId && d.WeedId == weedId && d.FightTypeId == fightTypeId 
                        select sus;
            List<AgroEntityFramework.ActiveSubstances> Listsusbstances = query.ToList();

            return Listsusbstances;
        }

        public List<AgroEntityFramework.CropFightWeeds> getTreatmentByCropByStateByWeedsByFightId(int cropId, int stateId, int fightTypeId, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightWeeds
                        where d.CropId == cropId && d.StateId == stateId && d.WeedId == weedId && d.FightTypeId == fightTypeId
                        select d;
            List<AgroEntityFramework.CropFightWeeds> Listsusbstances = query.ToList();

            return Listsusbstances;
        }

        public List<AgroEntityFramework.CropFightPests> getTreatmentByCropByStateByPestsByFightId(int cropId, int stateId, int fightTypeId, int pestId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightPests
                        where d.CropId == cropId && d.StateId == stateId && d.PestId == pestId && d.FightTypeId == fightTypeId
                        select d;
            List<AgroEntityFramework.CropFightPests> Listsusbstances = query.ToList();

            return Listsusbstances;
        }

        public List<AgroEntityFramework.CropFightDiseases> getTreatmentByCropByStateByDiseasesByFightId(int cropId, int stateId, int fightTypeId, int diseaseId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightDiseases
                        where d.CropId == cropId && d.StateId == stateId && d.DiseaseId == diseaseId && d.FightTypeId == fightTypeId
                        select d;
            List<AgroEntityFramework.CropFightDiseases> Listsusbstances = query.ToList();

            return Listsusbstances;
        }


        #endregion

        #region Insert

        public void InsertSubstances(AgroEntityFramework.ActiveSubstances Substances)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToActiveSubstances(Substances);

            db.SaveChanges();
        }
        //por checar
        public void InsertCropWeeds(AgroEntityFramework.CropWeeds CropWeeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropWeeds(CropWeeds);

            db.SaveChanges();
        }

        public void InsertCropFightWeeds(AgroEntityFramework.CropFightWeeds CropFightWeeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropFightWeeds(CropFightWeeds);

            db.SaveChanges();
        }

        public void InsertCropFightPest(AgroEntityFramework.CropFightPests CropFightPests)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropFightPests(CropFightPests);

            db.SaveChanges();
        }

        public void InsertCropFightDisease(AgroEntityFramework.CropFightDiseases CropFightDiseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropFightDiseases(CropFightDiseases);

            db.SaveChanges();
        }

        #endregion

        #region Update

        public void updateDescription(int cropId, int stateId, int fightTypeId, int weedId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.CropFightWeeds

                         where w.WeedId == weedId && w.CropId == cropId && w.StateId == stateId && w.FightTypeId == fightTypeId //&& w.Description == description

                         select w;

            foreach (AgroEntityFramework.CropFightWeeds item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        public void updateDescriptionDisease(int cropId, int stateId, int fightTypeId, int diseaseId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.CropFightDiseases

                        where w.DiseaseId == diseaseId && w.CropId == cropId && w.StateId == stateId && w.FightTypeId == fightTypeId //&& w.Description == description

                        select w;

            foreach (AgroEntityFramework.CropFightDiseases item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        public void updateDescriptionPest(int cropId, int stateId, int fightTypeId, int pestId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.CropFightPests

                        where w.PestId == pestId && w.CropId == cropId && w.StateId == stateId && w.FightTypeId == fightTypeId //&& w.Description == description

                        select w;

            foreach (AgroEntityFramework.CropFightPests item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        public void updateDescriptionByCropByStateByWeedsByFightId(int cropId, int stateId, int fightTypeId, int weedId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.CropFightWeeds
                         where d.CropId == cropId && d.StateId == stateId && d.WeedId == weedId && d.FightTypeId == fightTypeId// && description.Equals(d.Description.ToString())
                         select d);

            foreach (AgroEntityFramework.CropFightWeeds item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

        }

        public void updateDescriptionByCropByStateByPestsByFightId(int cropId, int stateId, int fightTypeId, int pestId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.CropFightPests
                        where w.PestId == pestId && w.CropId == cropId && w.StateId == stateId && w.FightTypeId == fightTypeId //&& w.Description == description
                        select w;

            foreach (AgroEntityFramework.CropFightPests item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        public void updateDescriptionByCropByStateByDiseasesByFightId(int cropId, int stateId, int fightTypeId, int diseaseId, string description)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.CropFightDiseases
                        where d.CropId == cropId && d.StateId == stateId && d.DiseaseId == diseaseId && d.FightTypeId == fightTypeId// && description.Equals(d.Description.ToString())
                        select d;

            foreach (AgroEntityFramework.CropFightDiseases item in query.ToList())
            {
                item.Description = description;
                db.SaveChanges();
            }

            db.SaveChanges();
        }

        public void updateSubstanceName(string substanceName, int substanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from w in db.ActiveSubstances

                         where w.ActiveSubstanceId == substanceId

                         select w).Single();

            query.ActiveSubstanceName = substanceName;

            db.SaveChanges();
        }

        #endregion

        #region Delete

        public void deleteSubstances(int substanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.ActiveSubstances
                where w.ActiveSubstanceId == substanceId
                select w;

            db.DeleteObject(delete);

            db.SaveChanges();
        }
        //pendientes
        public void deleteCropWeeds(int weedId, int stateId, int cropId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from w in db.CropWeeds
                 where w.WeedId == weedId && w.StateId == stateId && w.CropId == cropId
                 select w).Single();

            db.CropWeeds.DeleteObject(delete);

            db.SaveChanges();

        }

        public void deleteCropFightWeeds(int cropId, int stateId, int fightTypeId, int weedId, int substanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from w in db.CropFightWeeds
                where w.WeedId == weedId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId && w.ActiveSubstanceId == substanceId
                select w).Single();;

            db.CropFightWeeds.DeleteObject(delete);

            db.SaveChanges();
        }

        public void deleteCropFightPests(int cropId, int stateId, int fightTypeId, int pestId, int substanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from w in db.CropFightPests
                 where w.PestId == pestId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId && w.ActiveSubstanceId == substanceId
                 select w).Single(); ;

            db.CropFightPests.DeleteObject(delete);

            db.SaveChanges();
        }

        public void deleteCropFightDiseases(int cropId, int stateId, int fightTypeId, int diseaseId, int substanceId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from w in db.CropFightDiseases
                 where w.DiseaseId == diseaseId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId && w.ActiveSubstanceId == substanceId
                 select w).Single(); ;

            db.CropFightDiseases.DeleteObject(delete);

            db.SaveChanges();
        }


        #endregion

        #endregion
        public static readonly SubstancesDALC Instace = new SubstancesDALC();
    }

}
