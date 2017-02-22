using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class TherapeuticsDALC
    {

        #region Constructor

        private TherapeuticsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves all TherapeuticUses by level and ParentId
        public List<PEVBusinessEntries.TherapeuticUsesInfo> rocGetTherapeuticUsesByLevel(int level, int parentId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticInfo = from theraInfo in db.roc_spGetTherapeuticUseByLevel(level, parentId)
                                  select new PEVBusinessEntries.TherapeuticUsesInfo
                                  {
                                      TherapeuticId = theraInfo.TherapeuticId,
                                      TherapeuticName = theraInfo.TherapeuticName,
                                      Active = theraInfo.Active,
                                      ParentId = theraInfo.ParentId,
                                      Llave = theraInfo.Llave,
                                      CountryId = theraInfo.CountryId,
                                      IdUso = theraInfo.IDUso,
                                      Id = theraInfo.ID,
                                      Nivel = theraInfo.Nivel
                                  };

            List<PEVBusinessEntries.TherapeuticUsesInfo> therapeutic = therapeuticInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic : null;

        }

        //Retrieves information about an TherapeuticUse by therapeuticId
        public PEVBusinessEntries.TherapeuticUsesInfo rocGetTherapeuticUse(int therapeuticId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticsInfo = from theraInfo in db.roc_spGetTherapeuticUse(therapeuticId)
                                   select new PEVBusinessEntries.TherapeuticUsesInfo
                                   {
                                       TherapeuticId = theraInfo.TherapeuticId,
                                       TherapeuticName = theraInfo.TherapeuticName,
                                       Active = theraInfo.Active,
                                       ParentId = theraInfo.ParentId,
                                       Llave = theraInfo.Llave,
                                       CountryId = theraInfo.CountryId,
                                       IdUso = theraInfo.IDUso,
                                       Id = theraInfo.ID,
                                       Nivel = theraInfo.Nivel
                                   };
                                   

            List<PEVBusinessEntries.TherapeuticUsesInfo> therapeutic = therapeuticsInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic[0] : null;

        }

        //Retrieves all TherapeuticUses
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUses(int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticInfo = from theraInfo in db.roc_spGetTherapeuticUses(page, numberByPage)
                                  select new PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo
                                  {
                                      TherapeuticId = theraInfo.TherapeuticId,
                                      TherapeuticName = theraInfo.TherapeuticName,
                                      Nivel = theraInfo.Nivel,
                                      ParentId = theraInfo.ParentId,
                                      RowNumber = (int)theraInfo.RowNumber,
                                      Total = theraInfo.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> therapeutic = therapeuticInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic : null;


        }

        //Retrieves all TherapeuticUses by letter
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByLetter(string letter, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticsInfo = from theraInfo in db.roc_spGetTherapeuticUsesByLetter(letter, page, numberByPage)
                                   select new PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo
                               {
                                   TherapeuticId = theraInfo.TherapeuticId,
                                   TherapeuticName = theraInfo.TherapeuticName,
                                   Nivel = theraInfo.Nivel,
                                   ParentId = theraInfo.ParentId,
                                   RowNumber = (int)theraInfo.RowNumber,
                                   Total = theraInfo.TOTAL
                               };

            List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> therapeutic = therapeuticsInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic : null;

        }

        //Retrieves all TherapeuticUses by text
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByText(string text, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticInfo = from theraInfo in db.roc_spGetTherapeuticUsesByText(text, page, numberByPage)
                                  select new PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo
                                  {
                                      TherapeuticId = theraInfo.TherapeuticId,
                                      TherapeuticName = theraInfo.TherapeuticName,
                                      Nivel = theraInfo.Nivel,
                                      ParentId = theraInfo.ParentId,
                                      RowNumber = (int)theraInfo.RowNumber,
                                      Total = theraInfo.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> therapeutic = therapeuticInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic : null;
        }

        //Retrieves all TherapeuticUses by fullText
        public List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> rocGetTherapeuticUsesByFullText(string fullText, int page, int numberBypage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var therapeuticInfo = from theraInfo in db.roc_spGetTherapeuticUsesByFullText(fullText, page, numberBypage)
                                  select new PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo
                                  {
                                      TherapeuticId = theraInfo.TherapeuticId,
                                      TherapeuticName = theraInfo.TherapeuticName,
                                      Nivel = theraInfo.Nivel,
                                      ParentId = theraInfo.ParentId,
                                      RowNumber = (int)theraInfo.RowNumber,
                                      Total = theraInfo.TOTAL
                                  };

            List<PEVBusinessEntries.ROC.TherapeuticUsesByTextInfo> therapeutic = therapeuticInfo.ToList();

            return therapeutic.Count() > 0 ? therapeutic : null;

        }

        #endregion

        public static readonly TherapeuticsDALC Instance = new TherapeuticsDALC();

    }
}
