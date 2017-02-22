using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class ActiveSubstancesDALC
    {
        #region Constructors

        private ActiveSubstancesDALC() { }
        
        #endregion

        #region Public Methods

        //Retrieves an ActiveSubstance by an ActiveSubstanceId
        public ActiveSubstanceInfo rocGetActiveSubstanceBySubstanceId(int activeSubstanceId){

            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstance(activeSubstanceId)
                               select new ActiveSubstanceInfo
                               {
                                   ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                   ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                   Active = actSubstRow.Active
                               };

            List<ActiveSubstanceInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance[0] : null;

        }

        //Retrieves all ActiveSubstances by an ProductId
        public List<ActiveSubstanceInfo> rocGetActiveSubstancesByProductId(int productId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstancesByProduct(productId)
                                  select new ActiveSubstanceInfo
                                  {
                                      ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                      ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                      Active = actSubstRow.Active
                                  };

            List<ActiveSubstanceInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance : null;

        }

        //Retrieves all ActiveSubstances by an EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByEditionId(int editionId, int page, int numberByPage)
        {

            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstances(editionId, page, numberByPage)
                                  select new PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo
                                  {
                                      ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                      ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                      RowNumber = (int)actSubstRow.RowNumber,
                                      Total = (int)actSubstRow.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance : null;

        }

        //Retrieves all ActiveSubstances by letter and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByLetter(int editionId, string letter, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstancesByLetter(editionId, letter, page, numberByPage)
                                  select new PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo
                                  {
                                      ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                      ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                      RowNumber = (int)actSubstRow.RowNumber,
                                      Total = (int)actSubstRow.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance : null;
        }

        //Retrieves all ActiveSubstances by an Text and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByText(int editionId, string text, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstancesByText(editionId, text, page, numberByPage)
                                  select new PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo
                                  {
                                      ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                      ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                      RowNumber = (int)actSubstRow.RowNumber,
                                      Total = (int)actSubstRow.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance : null;

        }

        //Retrieves all ActiveSubstances by an FullText and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByFullText(int editionId, string fullText, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var actSubstanceRow = from actSubstRow in db.roc_spGetActiveSubstancesByFullText(editionId, fullText, page, numberByPage)
                                  select new PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo
                                  {
                                      ActiveSubstanceId = actSubstRow.ActiveSubstanceId,
                                      ActiveSubstanceName = actSubstRow.ActiveSubstanceName,
                                      RowNumber = (int)actSubstRow.RowNumber,
                                      Total = (int)actSubstRow.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> substance = actSubstanceRow.ToList();

            return substance.Count() > 0 ? substance : null;

        }

        #endregion

        public static readonly ActiveSubstancesDALC Instance = new ActiveSubstancesDALC();
    }
}
