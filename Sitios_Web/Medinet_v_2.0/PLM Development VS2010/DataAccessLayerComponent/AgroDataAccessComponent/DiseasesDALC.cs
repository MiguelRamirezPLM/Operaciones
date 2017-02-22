using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroDataAccessComponent
{
    public class DiseasesDALC
    {
        #region Constructor

        private DiseasesDALC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.Diseases> getDiseasesById(int diseaseId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from d in db.Diseases
                        where d.DiseaseId == diseaseId
                        select d;

            List<AgroEntityFramework.Diseases> listDiseases = query.ToList();

            return listDiseases;
       }

        public List<AgroEntityFramework.Diseases> getDiseases( int cropId,  int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

           var query = (from dRow in db.Diseases
                        where  !(from o in db.CropDiseases
                            where o.CropId == cropId && o.StateId == stateId
                            select o.DiseaseId).Contains(dRow.DiseaseId)                 
                        select dRow);

           List<AgroEntityFramework.Diseases> listDiseases = query.ToList();

            return listDiseases;
        }

        public List<AgroEntityFramework.Diseases> getDiseasesByNameByScientificName(string name)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.Diseases
                         where d.DiseaseName.Contains(name) || d.ScientificName.Contains(name)
                        //where d.DiseaseName.StartsWith(name) || d.ScientificName.StartsWith(name) ||
                        //d.DiseaseName.EndsWith(name) || d.ScientificName.EndsWith(name)
                        select d);

            List<AgroEntityFramework.Diseases> listDiseases = query.ToList();
           
            return listDiseases;
        }

        public List<AgroEntityFramework.Diseases> getDiseasesByCropByState(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from  d in db.Diseases
                        join c in db.CropDiseases
                               on d.DiseaseId equals c.DiseaseId
                        where c.CropId == cropId && c.StateId == stateId
                        select d).Distinct();

            List<AgroEntityFramework.Diseases> listDiseases = query.ToList();

            return listDiseases;

          
        }

        public List<AgroEntityFramework.CropFightDiseases> getCropFightDisease(int diseaseId, int cropId, int stateId)
        {
            List<AgroEntityFramework.CropFightDiseases> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var dis = (from p in context.CropFightDiseases
                           where p.CropId == cropId && p.StateId == stateId && p.DiseaseId == diseaseId

                           select p);
                cfp = dis.ToList();
            }

            return cfp;

        }

        //*************************************************************************************++ELIMINAR DISEASES CATALOG*****************************************+
        public List<AgroEntityFramework.CropFightDiseases> getCropFightDisease(int diseaseId)
        {
            List<AgroEntityFramework.CropFightDiseases> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropFightDiseases
                           where p.DiseaseId == diseaseId
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }

        public List<AgroEntityFramework.CropDiseases> getCropDisease(int diseaseId)
        {
            List<AgroEntityFramework.CropDiseases> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropDiseases
                           where p.DiseaseId == diseaseId
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }

        #endregion

        #region Insert

        public void addDiseases(AgroEntityFramework.Diseases Diseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToDiseases(Diseases);

            db.SaveChanges();
        }

        public void addCropDiseases(AgroEntityFramework.CropDiseases CropDiseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropDiseases(CropDiseases);

            db.SaveChanges();
        }

        public void addCropFightDiseases(AgroEntityFramework.CropFightDiseases CropFightDiseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToCropFightDiseases(CropFightDiseases);

            db.SaveChanges();
        }

        #endregion

        #region Update

        public void updateDisease(string diseaseName, int diseaseId,string scientificName)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.Diseases

                         where d.DiseaseId == diseaseId

                         select d).Single();

            query.DiseaseName = diseaseName;

            query.ScientificName = scientificName;

            db.SaveChanges();
       }


        #endregion

        #region Delete

        public void deleteCropFightDiseasess(AgroEntityFramework.CropFightDiseases cropFightDiseases)
        {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightDiseases
                           where d.CropId == cropFightDiseases.CropId && d.StateId == cropFightDiseases.StateId && d.DiseaseId == cropFightDiseases.DiseaseId && d.ActiveSubstanceId == cropFightDiseases.ActiveSubstanceId && d.FightTypeId == cropFightDiseases.FightTypeId
                           select d).Single();

                context.CropFightDiseases.DeleteObject(pes);
                context.SaveChanges();
            }
        }
        
        public void deleteDiseases(int diseaseId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from d in db.Diseases
                where d.DiseaseId == diseaseId
                select d;

                 db.DeleteObject(delete);

                 db.SaveChanges();
        }

        public void deleteCropDiseases(int diseaseId, int stateId, int cropId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from d in db.CropDiseases
                where d.DiseaseId == diseaseId  && d.StateId == stateId &&  d.CropId == cropId
                select d).Single();

            db.CropDiseases.DeleteObject(delete);
          
            db.SaveChanges();
          
        }

        public void deleteCropDiseasess(AgroEntityFramework.CropDiseases cropDisease)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropDiseases
                           where d.DiseaseId == cropDisease.DiseaseId && d.StateId == cropDisease.StateId && d.CropId == cropDisease.CropId
                           select d).Single();
                context.CropDiseases.DeleteObject(pes);
                context.SaveChanges();
            }

        }

        public void deleteCropFightDiseases(int diseaseId, int stateId, int cropId ,int activeSubstanceId , int fightTypeId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                (from d in db.CropFightDiseases
                where d.DiseaseId == diseaseId && d.StateId == stateId && d.CropId == cropId && d.ActiveSubstanceId == activeSubstanceId && d.FightTypeId == fightTypeId
                select d).Single();
          
            db.CropFightDiseases.DeleteObject(delete);
        }

        public void deleteCropFightDiseases(int diseaseId, int stateId, int cropId, int fightTypeId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.CropFightDiseases
                where w.DiseaseId == diseaseId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId
                select w;

            foreach (AgroEntityFramework.CropFightDiseases item in delete.ToList())
            {
                db.DeleteObject(item);
                db.SaveChanges();
            }
        }

        //**********************************************************************+ELIMINAR WEEDS CATALOG****************************
        public void deleteCropFightDiseasesCat(AgroEntityFramework.CropFightDiseases cropFightDisease)
        {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightDiseases
                           where d.DiseaseId == cropFightDisease.DiseaseId
                           select d);
                foreach (AgroEntityFramework.CropFightDiseases item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }

        public void deleteCropDiseasessCat(AgroEntityFramework.CropDiseases cropDiseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropDiseases
                           where d.DiseaseId == cropDiseases.DiseaseId
                           select d);
                foreach (AgroEntityFramework.CropDiseases item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }

        public void deleteDiseasess(AgroEntityFramework.Diseases Diseases)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Diseases
                           where d.DiseaseId == Diseases.DiseaseId
                           select d).Single();
                context.Diseases.DeleteObject(pes);
                context.SaveChanges();
            }
        }

        #endregion

        #endregion
        public static readonly DiseasesDALC Instance = new DiseasesDALC();
    }

}
