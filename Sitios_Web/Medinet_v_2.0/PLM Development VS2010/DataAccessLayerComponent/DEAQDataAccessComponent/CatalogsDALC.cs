using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using System.Data;
using System.Data.Common;
namespace DEAQDataAccessComponent
{
    public class CatalogsDALC : DEAQDataAccessComponent.DEAQEngineDataAccessAdapter<AgroBusinessEntries.CategoriesInfo>
    {
        #region Constructor

        private CatalogsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves Information From Books
        public BookInfo rocGetBook(int bookId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var book = from bookRow in db.roc_spGetBook(bookId)
                       select new BookInfo
                       {
                           BookId = bookRow.BookId,
                           BookName = bookRow.BookName,
                           ShortName = bookRow.ShortName,
                           Active = bookRow.Active
                       };

            List<BookInfo> bookInfo = book.ToList();

            return bookInfo.Count > 0 ? bookInfo[0] : null;
        }


        //Retrieves Information From Categories
        public CategoryInfo rocGetCategory(int categoryId)
           {
           
               DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

               var category = from categoryRow in db.roc_spGetCategory(categoryId)
                              select new CategoryInfo
                             {
                                 CategoryId = categoryRow.CategoryId,
                                 CategoryName = categoryRow.CategoryName,
                                 Active = categoryRow.Active
                             };

               List<CategoryInfo> categoryInfo = category.ToList();

               return categoryInfo.Count() > 0 ? categoryInfo[0] : null;

           }
                
        //Retrieves Information From Countries By ID
        public CountryInfo rocGetCountry(string country)
           {
           
               DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

               var count = from countryRow in db.roc_spGetCountry(country)
                             select new CountryInfo
                             {

                                 CountryId = countryRow.CountryId,
                                 CountryName = countryRow.CountryName,
                                 CountryCode = countryRow.CountryCode,
                                 ID = countryRow.ID,
                                 Active = countryRow.Active
                             };

               List<CountryInfo> countryInfo = count.ToList();

               return countryInfo.Count > 0 ? countryInfo[0] : null;
           }
                    
       //Retrieves Information From Countries By CountryId
        public CountryInfo rocGetCountryById(int countryId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var country = from countryRow in db.roc_spGetCountryById(countryId)
                          select new CountryInfo
                          {
                              CountryId = countryRow.CountryId,
                              CountryName = countryRow.CountryName,
                              ID = countryRow.ID,
                              Active = countryRow.Active
                          };

            List<CountryInfo> countryInfo = country.ToList();

            return countryInfo.Count > 0 ? countryInfo[0] : null;
        }
            
        //Retrieves Information From Countries By Edition
        public CountryByEditionInfo rocGetCountryByEdition(int editionId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var country = from countryRow in db.roc_spGetCountryByEdition(editionId)
                          select new CountryByEditionInfo
                          {
                              EditionId = countryRow.EditionId,
                              CountryId = (int)countryRow.CountryId,
                              CountryName = countryRow.CountryName,
                          };

            List<CountryByEditionInfo> countryInfo = country.ToList();

            return countryInfo.Count > 0 ? countryInfo[0] : null;
        }


       //Retrieves Information From Editions
        public EditionInfo rocGetEdition(int countryId, int bookId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var edition = from editionRow in db.roc_spGetEdition(countryId, bookId)
                          select new EditionInfo
                          {
                              EditionId = editionRow.EditionId,
                              CountryId = editionRow.CountryId,
                              ParentId = editionRow.ParentId,
                              BookId = editionRow.BookId,
                              NumberEdition = editionRow.NumberEdition,
                              ISBN = editionRow.ISBN,
                              BarCode = editionRow.BarCode,
                              Active = editionRow.Active

                          };

            List<EditionInfo> editionInfo = edition.ToList();

            return editionInfo.Count > 0 ? editionInfo[0] : null;
        }

        //Retrieves Information From Laboratories
        public LaboratoryInfo rocGetLaboratory(int laboratoryId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var laboratory = from laboratoryRow in db.roc_spGetLaboratory(laboratoryId)
                             select new LaboratoryInfo
                             {
                                 LaboratoryId = laboratoryRow.LaboratoryId,
                                 LaboratoryName = laboratoryRow.LaboratoryName,
                                 Active = laboratoryRow.Active
                             };
          
            List<LaboratoryInfo> laboratoryInfo = laboratory.ToList();

            return laboratoryInfo.Count > 0 ? laboratoryInfo[0] : null;
        }

        //Retrieves Information From PharmaForms
        public PharmaFormInfo rocGetPharmaForm(int pharmaFormId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var pharma = from pharmaRow in db.roc_spGetPharmaForm(pharmaFormId)
                         select new PharmaFormInfo
                         {
                             PharmaFormId = pharmaRow.PharmaFormId,
                             PharmaForm = pharmaRow.PharmaForm,
                             Active = pharmaRow.Active
                         };

            List<PharmaFormInfo> pharmaInfo = pharma.ToList();

            return pharmaInfo.Count > 0 ? pharmaInfo[0] : null;

        }

        //Retrieves Information From PharmaForms By EditionId
         public List<PharmaFormInfo> rocGetPharmaFormsByProductId(int editionId, int productId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var pharmaForm = from pharmaRow in db.roc_spGetPharmaFormsByProductId(editionId, productId)
                             select new PharmaFormInfo
                             {
                                 PharmaFormId = pharmaRow.PharmaFormId,
                                 PharmaForm = pharmaRow.PharmaForm,
                                 Active = pharmaRow.Active
                             };

            List<PharmaFormInfo> pharmaFormInfo = pharmaForm.ToList();

            return pharmaFormInfo.Count > 0 ? pharmaFormInfo : null;
        }

         //Retrieves information all Advertisements by Edition and Division
         public List<DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo> rocGetAdvertisementsByDivision(int divisionId, int editionId)
         {
             DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

             var adv = from advertisementInfo in db.roc_spGetAdvertisementsByDivision(divisionId, editionId)
                       select new DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo
                       {
                           AdId = advertisementInfo.AdId,
                           AdFile = advertisementInfo.AdFile,
                           EditionId = advertisementInfo.EditionId,
                           DivisionId = advertisementInfo.DivisionId,
                           BaseUrl = advertisementInfo.BaseUrl
                       };

             List<DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo> advertisements = adv.ToList();

             return advertisements.Count() > 0 ? advertisements : null;

         }

        #endregion

         #region PLM Methods
         public List<AgroBusinessEntries.CategoriesInfo> GetCategoriesByISBN(string isbn,int divisionId)
         {
             DbCommand dbCmd = CatalogsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionCategories");
             List<AgroBusinessEntries.CategoriesInfo> recordList = new List<AgroBusinessEntries.CategoriesInfo>();
             // Add the parameters:
             CatalogsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
             CatalogsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

             // Get the result set from the stored procedure:
             using (IDataReader dataReader = CatalogsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
             {
                 while (dataReader.Read())
                 {
                     AgroBusinessEntries.CategoriesInfo record = new AgroBusinessEntries.CategoriesInfo();

                     record.Active = Convert.ToBoolean(dataReader["CategoryActive"]);
                     record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                     record.Description = dataReader["CategoryName"].ToString();
                     recordList.Add(record);
                 }
             }
             return recordList;
         }

         public AgroBusinessEntries.CategoriesInfo GetCategoriesById(int categoryId)
         {
             DbCommand dbCmd = CatalogsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategoryById");
             AgroBusinessEntries.CategoriesInfo record = new AgroBusinessEntries.CategoriesInfo();
             // Add the parameters:
             CatalogsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

             // Get the result set from the stored procedure:
             using (IDataReader dataReader = CatalogsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
             {
                 if (dataReader.Read())
                 {
                     record.Active = Convert.ToBoolean(dataReader["Active"]);
                     record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                     record.Description = dataReader["CategoryName"].ToString();
                     
                 }
             }
             return record;
         }

         public AgroBusinessEntries.DivisionInfo GetDivisionById(int divisionId)
         {
             DbCommand dbCmd = CatalogsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionById");
             AgroBusinessEntries.DivisionInfo record = new AgroBusinessEntries.DivisionInfo();
             // Add the parameters:
             CatalogsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

             // Get the result set from the stored procedure:
             using (IDataReader dataReader = CatalogsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
             {
                 if (dataReader.Read())
                 {
                     record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                     record.DivisionName = dataReader["DivisionName"].ToString();
                     record.ShortName= dataReader["ShortName"].ToString();

                 }
             }
             return record;
         }
         public List<AgroBusinessEntries.CategoriesInfo> GetCategoriesByDivision( int divisionId)
         {
             DbCommand dbCmd = CatalogsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionCategories");
             List<AgroBusinessEntries.CategoriesInfo> recordList = new List<AgroBusinessEntries.CategoriesInfo>();
             // Add the parameters:
             CatalogsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

             // Get the result set from the stored procedure:
             using (IDataReader dataReader = CatalogsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
             {
                 while (dataReader.Read())
                 {
                     AgroBusinessEntries.CategoriesInfo record = new AgroBusinessEntries.CategoriesInfo();

                     record.Active = Convert.ToBoolean(dataReader["CategoryActive"]);
                     record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                     record.Description = dataReader["CategoryName"].ToString();
                     recordList.Add(record);
                 }
             }
             return recordList;
         }
         #endregion

         public static readonly CatalogsDALC Instance = new CatalogsDALC();
    }
}
