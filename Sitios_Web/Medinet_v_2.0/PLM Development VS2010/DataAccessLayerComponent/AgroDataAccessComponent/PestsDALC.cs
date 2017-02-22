using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroEntityFramework;
using System.Data;
using System.Data.Objects;

namespace AgroDataAccessComponent
{
    public class PestsDALC
    {
        
         #region Constructor

        private PestsDALC() { }

        #endregion
        #region Public Methods Insert

        public void addPest(AgroEntityFramework.Pests pest) {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            db.AddToPests(pest);
            db.SaveChanges();
            
         }
        public void addCropPest(AgroEntityFramework.CropPests cropPest) {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            db.AddToCropPests(cropPest);
            db.SaveChanges();
        }
        public void addCropFightPest(AgroEntityFramework.CropFightPests cropFightPest)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            db.AddToCropFightPests(cropFightPest);
            db.SaveChanges();
        }
       
        #endregion
        
        #region Public Methods Update
        public void updatePest(AgroEntityFramework.Pests pest) {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
            var pes = (from d in context.Pests
            where d.PestId == pest.PestId
            select d).Single();
            pes.PestName = pest.PestName;
            pes.ScientificName = pest.ScientificName;
            pes.Image = pest.Image;
            context.SaveChanges();
            }
            
        }
        #endregion

        #region Public Methods Delete
        public void deletePest(int pestId) {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
           using (var context = new AgroEntityFramework.DEAQEntites())
            {
            var pes = (from d in context.Pests
            where d.PestId == pestId
            select d).Single();
            context.Pests.DeleteObject(pes);
            context.SaveChanges();
            }
            
        }
        public void deleteCropPest(AgroEntityFramework.CropPests cropPest)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropPests
                           where d.PestId == cropPest.PestId && d.StateId==cropPest.StateId && d.CropId==cropPest.CropId
                           select d).Single();
                context.CropPests.DeleteObject(pes);
                context.SaveChanges();
            }

        }
        public void deleteCropFightPest(AgroEntityFramework.CropFightPests cropFightPest) {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightPests
                           where d.CropId==cropFightPest.CropId && d.StateId == cropFightPest.StateId && d.PestId == cropFightPest.PestId && d.ActiveSubstanceId == cropFightPest.ActiveSubstanceId && d.FightTypeId==cropFightPest.FightTypeId
                           select d).Single();
                
                context.CropFightPests.DeleteObject(pes);
                context.SaveChanges();
            }
        }

        public void deleteCropFightPests(int pestId, int stateId, int cropId, int fightTypeId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.CropFightPests
                where w.PestId == pestId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId
                select w;

            foreach (AgroEntityFramework.CropFightPests item in delete.ToList())
            {
                db.DeleteObject(item);
                db.SaveChanges();
            }
        }

        //**********************************************************************+ELIMINAR PESTS CATALOG****************************
        public void deleteCropFightPestsCat(AgroEntityFramework.CropFightPests cropFightPests)
        {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightPests
                           where d.PestId == cropFightPests.PestId
                           select d);
                foreach (AgroEntityFramework.CropFightPests item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }

        public void deleteCropPestsCat(AgroEntityFramework.CropPests cropPests)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropPests
                           where d.PestId == cropPests.PestId
                           select d);
                foreach (AgroEntityFramework.CropPests item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }

        public void deletePestss(AgroEntityFramework.Pests Pests)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Pests
                           where d.PestId == Pests.PestId
                           select d).Single();
                context.Pests.DeleteObject(pes);
                context.SaveChanges();
            }
        }
        #endregion

        #region Public Methods Select
        public List<AgroEntityFramework.Pests> getPests()
        {
            List<AgroEntityFramework.Pests> listPest;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Pests
                           select d);

                listPest =pes.ToList();
            }
            return listPest;
        }
        public List<AgroEntityFramework.Pests> getPestsNotRelation(int cropId,int stateId)
        {
            List<AgroEntityFramework.Pests> listPest;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.Pests
                         where !(from cp in context.CropPests
                                            where cp.CropId == cropId && cp.StateId == stateId
                                            select cp.PestId).Contains(p.PestId)
                         select p); 
              
                listPest = pes.ToList();
            }
            return listPest;
        }
        public AgroEntityFramework.Pests getPestsById(int pestId)
        {
            AgroEntityFramework.Pests pest;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Pests
                           where d.PestId==pestId
                           select d ).Single();

                pest = pes;
            }
            return pest;
        }
        public List<AgroEntityFramework.Pests> getPestsByCropId(int cropId,int stateId)
        {
            List<AgroEntityFramework.Pests> pest = new List<Pests>(); ;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Pests
                           join cp in context.CropPests on d.PestId equals cp.PestId
                           where cp.CropId==cropId && cp.StateId==stateId
                           select d);

                pest = pes.ToList();
            }
            return pest;
        }
       
        public List<AgroEntityFramework.ActiveSubstances> getSubstancesByCropByStateByPestsByTrateament(int cropId, int stateId, int fightTypeId, int pestsId)//NUEVO
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
        public List<AgroEntityFramework.Pests> getPestsByName(String pestName, int cropId, int stateId)
        {
            List<AgroEntityFramework.Pests> listPest;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.Pests
                           where !(from cp in context.CropPests
                                   where cp.CropId == cropId && cp.StateId == stateId
                                   select cp.PestId).Contains(p.PestId) && p.PestName.Contains(pestName)
                           select p);
                listPest = pes.ToList();
            }
            return listPest;
        }
        public List<AgroEntityFramework.Pests> getPestsByName(String pestName, int cropId, int stateId, String ScientificName)
        {
            List<AgroEntityFramework.Pests> listPest;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.Pests
                           where (!(from cp in context.CropPests
                                    where cp.CropId == cropId && cp.StateId == stateId
                                    select cp.PestId).Contains(p.PestId)) && (p.PestName.Contains(pestName) || p.ScientificName.Contains(ScientificName))
                           select p);
                listPest = pes.ToList();
            }
            return listPest;
        }

        public List<AgroEntityFramework.CropFightPests> getCropFightPest(int pestId, int cropId, int stateId) {
            List<AgroEntityFramework.CropFightPests >cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropFightPests
                           where p.CropId==cropId && p.StateId==stateId && p.PestId==pestId
                                   
                           select p);
                cfp = pes.ToList();
            }

            return cfp;
        
        }

        //*************************************************************************************++ELIMINAR WEDDS CATALOG*****************************************+
        public List<AgroEntityFramework.CropFightPests> getCropFightPest(int pestId)
        {
            List<AgroEntityFramework.CropFightPests> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropFightPests
                           where p.PestId == pestId 
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }

        public List<AgroEntityFramework.CropPests> getCropPest(int pestId)
        {
            List<AgroEntityFramework.CropPests> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropPests
                           where p.PestId == pestId 
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }
        #endregion
        //public static List<T> NewList<T>(T witness)
        //{
        //    return new List<T>();
        //}

        public static readonly PestsDALC Instance = new PestsDALC();
    }
}
