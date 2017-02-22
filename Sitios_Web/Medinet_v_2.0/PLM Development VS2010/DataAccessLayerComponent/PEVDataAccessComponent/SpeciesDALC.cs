using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class SpeciesDALC
    {
        #region Constructor

        private SpeciesDALC() { }

        #endregion

        #region Public Methods

        //Retrieves information about an Specie by SpecieId
        public SpeciesBySpecieIdInfo rocGetSpecieBySpecieId(int specieId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var specieInformation = from specieInfo in db.roc_spGetSpecie(specieId)
                                    select new SpeciesBySpecieIdInfo
                                    {
                                        SpecieId = specieInfo.SpecieId,
                                        SpecieName = specieInfo.SpecieName,
                                        Active = specieInfo.Active
                                    };

            List<SpeciesBySpecieIdInfo> specie = specieInformation.ToList();

            return specie.Count() > 0 ? specie[0] : null;

        }

        //Retrieves all Species
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpecies(int page, int numberByPage) 
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var specieInformation = from specieInfo in db.roc_spGetSpecies(page, numberByPage)
                                    select new PEVBusinessEntries.ROC.SpeciesInfo
                                    {
                                        SpecieId = specieInfo.SpecieId,
                                        SpecieName = specieInfo.SpecieName,
                                        RowNumber = (int)specieInfo.RowNumber,
                                        Total = (int)specieInfo.TOTAL
                                    };

            List<PEVBusinessEntries.ROC.SpeciesInfo> specie = specieInformation.ToList();

            return specie.Count() > 0 ? specie : null;

        }

        //Retrieves all Species by Letter
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByLetter(string letter, int page, int numberByPage , int editionid )
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var specieInformation = from specieInfo in db.roc_spGetSpeciesByLetter(letter, page, numberByPage, editionid)
                                    select new PEVBusinessEntries.ROC.SpeciesInfo
                                    {
                                        SpecieId = specieInfo.SpecieId,
                                        SpecieName = specieInfo.SpecieName,
                                        RowNumber = (int)specieInfo.RowNumber,
                                        Total = (int)specieInfo.TOTAL
                                    };

            List<PEVBusinessEntries.ROC.SpeciesInfo> specie = specieInformation.ToList();

            return specie.Count() > 0 ? specie : null;

        }

        //Retrieves all Species by Text
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByText(string text, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var specieInformation = from specieInfo in db.roc_spGetSpeciesByText(text, page, numberByPage)
                                    select new PEVBusinessEntries.ROC.SpeciesInfo
                                    {
                                        SpecieId = specieInfo.SpecieId,
                                        SpecieName = specieInfo.SpecieName,
                                        RowNumber = (int)specieInfo.RowNumber,
                                        Total = (int)specieInfo.TOTAL
                                    };

            List<PEVBusinessEntries.ROC.SpeciesInfo> specie = specieInformation.ToList();

            return specie.Count() > 0 ? specie : null;

        }

        //Retrieves all Species by FullText
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByFullText(string fullText, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var specieInformation = from specieInfo in db.roc_spGetSpeciesByFullText(fullText, page, numberByPage)
                                    select new PEVBusinessEntries.ROC.SpeciesInfo
                                    {
                                        SpecieId = specieInfo.SpecieId,
                                        SpecieName = specieInfo.SpecieName,
                                        RowNumber = (int)specieInfo.RowNumber,
                                        Total = (int)specieInfo.TOTAL
                                    };

            List<PEVBusinessEntries.ROC.SpeciesInfo> specie = specieInformation.ToList();

            return specie.Count() > 0 ? specie : null;

        }

        #endregion

        public static readonly SpeciesDALC Instance = new SpeciesDALC();

    }
}
