using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroDataAccessComponent
{
    public class WeedsDALC
    {
        #region Constructor

        private WeedsDALC() { }

        #endregion

        #region Public Methods

        #region Select

        public List<AgroEntityFramework.WeedTypes> getTypesWeedsByName(string weedTypeName)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.WeedTypes
                        where w.TypeName.Contains(weedTypeName) 
                        select w;

            List<AgroEntityFramework.WeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }
        
        
        public List<AgroEntityFramework.Weeds> getWeedsById(int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.Weeds
                        where w.WeedId == weedId
                        select w;

            List<AgroEntityFramework.Weeds> listWeeds = query.ToList();

            return listWeeds;
        }


        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeeds(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.plm_vwWeedsWithWeedTypes
                         join c in db.CropWeeds
                                on d.WeedId equals c.WeedId
                        where c.CropId == cropId && c.StateId == stateId
                        select d).Distinct();

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getGridWeedAdd(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from dRow in db.plm_vwWeedsWithWeedTypes
                         where !(from o in db.CropWeeds
                                 where o.CropId == cropId && o.StateId == stateId
                                 select o.WeedId).Contains(dRow.WeedId)
                         select dRow);

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> listDiseases = query.ToList();

            return listDiseases;
        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsList()
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwWeedsWithWeedTypes
                        select w;

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsByNameByScientificNameView(string name)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwWeedsWithWeedTypes
                        where w.WeedName.Contains(name) || w.ScientificName.Contains(name)
                        select w;

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }
        public List<AgroEntityFramework.Weeds> getWeedsByNameByScientificName(string name)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.Weeds
                        where w.WeedName.Contains(name) || w.ScientificName.Contains(name)
                        select w;

            List<AgroEntityFramework.Weeds> listWeeds = query.ToList();

            return listWeeds;
        }

        public List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> getWeedsByCropId(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.plm_vwWeedsWithWeedTypes
                        join cw in db.CropWeeds
                               on w.WeedId equals cw.WeedId
                        where cw.CropId == cropId && cw.StateId == stateId
                        select w;

            List<AgroEntityFramework.plm_vwWeedsWithWeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }

        public List<AgroEntityFramework.WeedTypes> getWeedsTypes()
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.WeedTypes
                        select w;

            List<AgroEntityFramework.WeedTypes> listWeeds = query.ToList();

            return listWeeds;
        }


        public List<AgroEntityFramework.CropFightWeeds> getCropFightWeeds(int cropId, int stateId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = from w in db.Weeds
                        join cw in db.CropFightWeeds
                               on w.WeedId equals cw.WeedId
                        where cw.CropId == cropId && cw.StateId == stateId
                        select cw;

            List<AgroEntityFramework.CropFightWeeds> listWeeds = query.ToList();

            return listWeeds;
        }

        public List<AgroEntityFramework.CropFightWeeds> getCropFightWeed(int weedId, int cropId, int stateId)
        {
            List<AgroEntityFramework.CropFightWeeds> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropFightWeeds
                           where p.CropId == cropId && p.StateId == stateId && p.WeedId == weedId
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }
        //*************************************************************************************++ELIMINAR WEDDS CATALOG*****************************************+
        public List<AgroEntityFramework.CropFightWeeds> getCropFightWeed(int weedId)
        {
            List<AgroEntityFramework.CropFightWeeds> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropFightWeeds
                           where p.WeedId == weedId 
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }
        
        public List<AgroEntityFramework.CropWeeds> getCropWeed(int weedId)
        {
            List<AgroEntityFramework.CropWeeds> cfp;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from p in context.CropWeeds
                           where p.WeedId == weedId
                           select p);
                cfp = pes.ToList();
            }

            return cfp;

        }

        #endregion

        #region Insert

        public void InsertWeeds(AgroEntityFramework.Weeds Weeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToWeeds(Weeds);

            db.SaveChanges();
        }

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

        public void InsertTypesWeeds(AgroEntityFramework.WeedTypes typeWeeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            db.AddToWeedTypes(typeWeeds);

            db.SaveChanges();
        }

        #endregion

        #region Update

        public void updateWeedName(string weedName, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from w in db.Weeds

                         where w.WeedId == weedId

                         select w).Single();

            query.WeedName = weedName;

            db.SaveChanges();
        }

        public void updateScientificName(string scientificName, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from w in db.Weeds

                         where w.WeedId == weedId

                         select w).Single();

            query.ScientificName = scientificName;

            db.SaveChanges();
        }

        public void updateWeedTypeId(int WeedTypeId, int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from w in db.Weeds

                         where w.WeedId == weedId

                         select w).Single();

            query.WeedTypeId = WeedTypeId;

            db.SaveChanges();
        }

        #endregion

        #region Delete

        public void deleteWeeds(int weedId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.Weeds
                where w.WeedId == weedId
                select w;

            db.DeleteObject(delete);

            db.SaveChanges();
        }

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

        public void deleteCropFightWeeds(int weedId, int stateId, int cropId, int fightTypeId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.CropFightWeeds
                where w.WeedId == weedId && w.StateId == stateId && w.CropId == cropId && w.FightTypeId == fightTypeId
                select w;

            foreach (AgroEntityFramework.CropFightWeeds item in delete.ToList())
            {
                db.DeleteObject(item);
                db.SaveChanges(); 
            }
        }
        public void deleteCropFightWeeds(int weedId, int stateId, int cropId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var delete =
                from w in db.CropFightWeeds
                where w.WeedId == weedId && w.StateId == stateId && w.CropId == cropId 
                select w;

            foreach (AgroEntityFramework.CropFightWeeds item in delete.ToList())
            {
                db.DeleteObject(item);
                db.SaveChanges();
            }
        }

        public void deleteCropFightWeeds(AgroEntityFramework.CropFightWeeds cropFightWeed)
        {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightWeeds
                           where d.CropId == cropFightWeed.CropId && d.StateId == cropFightWeed.StateId && d.WeedId == cropFightWeed.WeedId && d.ActiveSubstanceId == cropFightWeed.ActiveSubstanceId && d.FightTypeId == cropFightWeed.FightTypeId
                           select d).Single();
                context.CropFightWeeds.DeleteObject(pes);
                context.SaveChanges();
            }
        }

        public void deleteCropWeedss(AgroEntityFramework.CropWeeds cropWeeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropWeeds
                           where d.WeedId == cropWeeds.WeedId && d.StateId == cropWeeds.StateId && d.CropId == cropWeeds.CropId
                           select d).Single();
                context.CropWeeds.DeleteObject(pes);
                context.SaveChanges();
            }
        }

        //**********************************************************************+ELIMINAR WEEDS CATALOG****************************
        public void deleteCropFightWeedsCat(AgroEntityFramework.CropFightWeeds cropFightWeed)
        {
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropFightWeeds
                           where d.WeedId == cropFightWeed.WeedId 
                           select d);
                foreach (AgroEntityFramework.CropFightWeeds item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }

        public void deleteCropWeedssCat(AgroEntityFramework.CropWeeds cropWeeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.CropWeeds
                           where d.WeedId == cropWeeds.WeedId 
                           select d);
                foreach (AgroEntityFramework.CropWeeds item in pes.ToList())
                {
                    context.DeleteObject(item);
                    context.SaveChanges();
                }
            }
        }
        
        public void deleteWeedss(AgroEntityFramework.Weeds Weeds)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Weeds
                           where d.WeedId == Weeds.WeedId 
                           select d).Single();
                context.Weeds.DeleteObject(pes);
                context.SaveChanges();
            }
        }

        #endregion

        #endregion
        public static readonly WeedsDALC Instace = new WeedsDALC();
    }

}
