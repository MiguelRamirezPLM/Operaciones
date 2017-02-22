using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using AgroBusinessEntries;
using System.Linq;

namespace DEAQDataAccessComponent
{
    public class DivisionsDALC : DEAQEngineDataAccessAdapter<ProductDetailByEditionInfo>
   {
       #region Constructor

       private DivisionsDALC() { }

       #endregion

       #region Public Methods

       #region ROC Methods
       //Retrieves All Division By DivisionId
       public DivisionByCountryInfo rocGetDivision(int countryId, int divisionId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var division = from divisionRow in db.roc_spGetDivision(countryId, divisionId)
                          select new DivisionByCountryInfo
                          {
                              DivisionId = divisionRow.DivisionId,
                              ParentId = divisionRow.ParentId,
                              DivisionName = divisionRow.DivisionName,
                              ShortName = divisionRow.ShortName,
                              LaboratoryId = divisionRow.LaboratoryId,
                              CountryId = divisionRow.CountryId,
                              Active = divisionRow.Active,
                              DivisionInformationId = divisionRow.DivisionInformationId,
                              Image = divisionRow.Image,
                              Address = divisionRow.Address,
                              Suburb = divisionRow.Suburb,
                              Location = divisionRow.Location,
                              ZipCode = divisionRow.Zipcode,
                              Telephone = divisionRow.Telephone,
                              Lada = divisionRow.Lada,
                              Fax = divisionRow.Fax,
                              Web = divisionRow.Web,
                              City = divisionRow.City,
                              State = divisionRow.State,
                              Email = divisionRow.Email

                          };

           List<DivisionByCountryInfo> divisionInfo = division.ToList();

           return divisionInfo.Count > 0 ? divisionInfo[0] : null;

       }

       //Retrieves All Division By LaboratoryId
       public DivisionByCountryInfo rocGetDivisionByLaboratoryId(int laboratoryId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divLab = from divRow in db.roc_spGetDivisionByLaboratoryId(laboratoryId)
                        select new DivisionByCountryInfo
                        {
                            DivisionId = divRow.DivisionId,
                            ParentId = divRow.ParentId,
                            DivisionName = divRow.DivisionName,
                            ShortName = divRow.ShortName,
                            LaboratoryId = divRow.LaboratoryId,
                            CountryId = divRow.CountryId,
                            Active = divRow.Active,
                            DivisionInformationId = divRow.DivisionInformationId,
                            Image = divRow.Image,
                            Address = divRow.Address,
                            Suburb = divRow.Suburb,
                            Location = divRow.Location,
                            ZipCode = divRow.Zipcode,
                            Telephone = divRow.Telephone,
                            Lada = divRow.Lada,
                            Fax = divRow.Fax,
                            Web = divRow.Web,
                            City = divRow.City,
                            State = divRow.State,
                            Email = divRow.Email
                        };

           List<DivisionByCountryInfo> divInfo = divLab.ToList();

           return divInfo.Count > 0 ? divInfo[0] : null;
       }


       //Retrieves ALL Category by Divisions
       public CategoryInfo rocGetDivisionCategories(int divisionId, int editionId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divCategory = from divRow in db.roc_spGetDivisionCategories(divisionId, editionId)
                             select new CategoryInfo
                             {
                                 CategoryId = divRow.CategoryId,
                                 CategoryName = divRow.CategoryName,
                                 Active = divRow.Active
                             };

           List<CategoryInfo> divCategoryInfo = divCategory.ToList();

           return divCategoryInfo.Count > 0 ? divCategoryInfo[0] : null;
       }


       //Retrieves All Division By EditionId
       public List<DivisionByEditionInfo> rocGetDivisions(int numberByPage, int page, int editionId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divisions = from divRow in db.roc_spGetDivisions(numberByPage, page, editionId)
                           select new DivisionByEditionInfo
                           {
                               Total = (int)divRow.TOTAL,
                               DivisionId = divRow.DivisionId,
                               DivisionName = divRow.DivisionName,
                               ShortName = divRow.ShortName,
                               RowNumber = (int)divRow.RowNumber
                           };

           List<DivisionByEditionInfo> divisionsInfo = divisions.ToList();

           return divisionsInfo.Count > 0 ? divisionsInfo : null;
       }

       //Retrieves All Division By Fulltext
       public List<DivisionByEditionInfo> rocGetDivisionsByFullText(int numberByPage, int page, int editionId, string text)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divFull = from divRow in db.roc_spGetDivisionsByFullText(numberByPage, page, editionId, text)
                         select new DivisionByEditionInfo
                         {
                             Total = (int)divRow.TOTAL,
                             DivisionId = divRow.DivisionId,
                             DivisionName = divRow.DivisionName,
                             ShortName = divRow.ShortName,
                             RowNumber = (int)divRow.RowNumber
                         };

           List<DivisionByEditionInfo> divFullInfo = divFull.ToList();

           return divFullInfo.Count > 0 ? divFullInfo : null;
       }


       //Retrieves All Division By ParentId
       public List<DivisionByCountryInfo> rocGetDivisionsByLaboratoryIdAndParentId(int laboratoryId, int parentId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divParent = from divRow in db.roc_spGetDivisionsByLaboratoryIdAndParentId(laboratoryId, parentId)
                           select new DivisionByCountryInfo
                           {
                               DivisionId = divRow.DivisionId,
                               ParentId = divRow.ParentId,
                               DivisionName = divRow.DivisionName,
                               ShortName = divRow.ShortName,
                               LaboratoryId = divRow.LaboratoryId,
                               CountryId = divRow.CountryId,
                               Active = divRow.Active,
                               DivisionInformationId = divRow.DivisionInformationId,
                               Image = divRow.Image,
                               Address = divRow.Address,
                               Suburb = divRow.Suburb,
                               Location = divRow.Location,
                               ZipCode = divRow.Zipcode,
                               Telephone = divRow.Telephone,
                               Lada = divRow.Lada,
                               Fax = divRow.Fax,
                               Web = divRow.Web,
                               City = divRow.City,
                               State = divRow.State,
                               Email = divRow.Email
                           };

           List<DivisionByCountryInfo> divParentInfo = divParent.ToList();

           return divParentInfo.Count > 0 ? divParentInfo : null;
       }

       //Retrieves All Division By Letter
       public List<DivisionByEditionInfo> rocGetDivisionsByLetter(int numberByPage, int page, int editionId, string letter)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divLet = from divRow in db.roc_spGetDivisionsByLetter(numberByPage, page, editionId, letter)
                        select new DivisionByEditionInfo
                        {
                            Total = (int)divRow.TOTAL,
                            DivisionId = divRow.DivisionId,
                            DivisionName = divRow.DivisionName,
                            ShortName = divRow.ShortName,
                            RowNumber = (int)divRow.RowNumber
                        };

           List<DivisionByEditionInfo> divLetInfo = divLet.ToList();

           return divLetInfo.Count > 0 ? divLetInfo : null;

       }

        //Retrieves Information From Division By ParentId
       public List<DivisionByCountryInfo> rocGetDivisionsByParentId(int parentId)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divParent = from divRow in db.roc_spGetDivisionsByParentId(parentId)
                           select new DivisionByCountryInfo
                           {
                               DivisionId = divRow.DivisionId,
                               ParentId = divRow.ParentId,
                               DivisionName = divRow.DivisionName,
                               ShortName = divRow.ShortName,
                               LaboratoryId = divRow.LaboratoryId,
                               CountryId = divRow.CountryId,
                               Active = divRow.Active,
                               DivisionInformationId = divRow.DivisionInformationId,
                               Image = divRow.Image,
                               Address = divRow.Address,
                               Suburb = divRow.Suburb,
                               Location = divRow.Location,
                               ZipCode = divRow.Zipcode,
                               Telephone = divRow.Telephone,
                               Lada = divRow.Lada,
                               Fax = divRow.Fax,
                               Web = divRow.Web,
                               City = divRow.City,
                               State = divRow.State,
                               Email = divRow.Email                           
                         };

           List<DivisionByCountryInfo> divParentInfo = divParent.ToList();

           return divParentInfo.Count > 0 ? divParentInfo : null;
       }

       //Retrieves ALL Division By Text
       public List<DivisionByEditionInfo> rocGetDivisionsByText(int numberByPage, int page, int editionId, string text)
       {

           DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

           var divText = from divRow in db.roc_spGetDivisionsByText(numberByPage, page, editionId, text)
                         select new DivisionByEditionInfo
                         {
                             Total = (int)divRow.TOTAL,
                             DivisionId = divRow.DivisionId,
                             DivisionName = divRow.DivisionName,
                             ShortName = divRow.ShortName,
                             RowNumber = (int)divRow.RowNumber

                         };

           List<DivisionByEditionInfo> divTextInfo = divText.ToList();

           return divTextInfo.Count > 0 ? divTextInfo : null;

       }


       #endregion
       #region PLM Methods

       public List<DivisionInfo> getAll(string isbn)
       {
           DbCommand dbCmd = DivisionsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisions");

           // Add the parameters:
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
           List<DivisionInfo> BECollection = new List<DivisionInfo>();

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
                   BECollection.Add(this.getFromDataReader(dataReader));
           }

           return BECollection;
       }

       public DivisionInfo getById(string isbn, int divisionId)
       {
           DbCommand dbCmd = DivisionsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisions");

           // Add the parameters:
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           DivisionInfo BECollection = new DivisionInfo();

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
                   BECollection=(this.getFromDataReader(dataReader));
           }

           return BECollection;
       }

       public List<DivisionInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId)
       {
           DbCommand dbCmd = DivisionsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByProduct");

           // Add the parameters:
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

           List<DivisionInfo> BECollection = new List<DivisionInfo>();

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
                   BECollection.Add(this.getFromDataReader(dataReader));
           }

           return BECollection;
       }

       public List<DivisionInfo> getByText(string isbn, string text)
       {
           DbCommand dbCmd = DivisionsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByText");

           // Add the parameters:
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
           List<DivisionInfo> BECollection = new List<DivisionInfo>();

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = DivisionsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
                   BECollection.Add(this.getFromDataReader(dataReader));
           }

           return BECollection;
       }

       public List<DivisionInfo> getByCategory(string isbn, int categoryId)
       {
           DbCommand dbCmd = DivisionsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionsByCategory");

           // Add the parameters:
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
           DivisionsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           List<DivisionInfo> BECollection = new List<DivisionInfo>();

           // Get the result set from the stored procedure:
           using (IDataReader dataReader = DivisionsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
                   BECollection.Add(this.getFromDataReader(dataReader));
           }

           return BECollection;
       }
       #endregion

       #endregion

       #region Pretected Methods

       protected DivisionInfo getFromDataReader(IDataReader current)
       {
           DivisionInfo record = new DivisionInfo();

           record.DivisionId = Convert.ToInt32(current["DivisionId"]);
           record.ShortName = current["DivisionShortName"].ToString();
           record.DivisionName = current["DivisionName"].ToString();
           return record;
       }

       #endregion
       public static readonly DivisionsDALC Instance = new DivisionsDALC();


   }
}

