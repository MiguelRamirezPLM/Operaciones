using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class CatalogsDALC
    {
        #region Constructors

        private CatalogsDALC() { }
        
        #endregion

        #region Public Methods

        //Retrieves information about a Country by ID
        public CountryInfo rocGetCountryById(string id)
        {

            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var countriesName = from nameCountry in db.roc_spGetCountry(id)
                                select new CountryInfo
                                {
                                    CountryId = nameCountry.CountryId,
                                    CountryName = nameCountry.CountryName,
                                    CountryCode = nameCountry.CountryCode,
                                    ID = nameCountry.ID,
                                    Active = nameCountry.Active
                                };

            List<CountryInfo> country = countriesName.ToList();

            return country.Count() > 0 ? country[0] : null;
            
        }

        //Retrieves information from Edition By Country
        public EditionInfo rocGetEdition(int countryId) 
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var editions = from numberEdition in db.roc_spGetEdition(countryId)
                           select new EditionInfo
                           {
                               EditionId = numberEdition.EditionId,
                               NumberEdition = numberEdition.NumberEdition,
                               Isbn = numberEdition.ISBN,
                               BarCode = numberEdition.BarCode,
                               CountryId = numberEdition.CountryId,
                               BookId = numberEdition.BookId,
                               ParentId = numberEdition.ParentId,
                               Active = numberEdition.Active
                           };

            List<EditionInfo> editionInfo = editions.ToList();
            
            return editionInfo.Count() > 0 ? editionInfo[0] : null;

        }

        //Retrieves all Categories by DivisionId
        public List<CategoryInfo> rocGetCategoriesByDivisionId(int divisionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var categories = from categoriesInfo in db.roc_spGetCategoriesByDivision(divisionId)
                            select new CategoryInfo
                            {
                                CategoryId = categoriesInfo.CategoryId,
                                CategoryName = categoriesInfo.CategoryName,
                                Active = categoriesInfo.Active
                            };
            List<CategoryInfo> category = categories.ToList();

            return category.Count() > 0 ? category : null;

        }

        #endregion

        public static readonly CatalogsDALC Instance = new CatalogsDALC();
    }
}
