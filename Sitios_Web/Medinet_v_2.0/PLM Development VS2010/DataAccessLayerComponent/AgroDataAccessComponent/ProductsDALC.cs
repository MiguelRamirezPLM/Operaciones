using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

using System.Linq;


namespace AgroDataAccessComponent
{
    public sealed class ProductsDALC : AgroDataAccessAdapter<ProductDetailByEditionInfo>
    {
         
        #region Constructor

        private ProductsDALC() { }

        #endregion

        #region Public Methods


        #region PLM Methods
        public void update(ProductsInfo businessEntity)
        {
            ProductsDALC.AgroInstanceDatabase.ExecuteNonQuery("dbo.plm_spCRUDProducts",
                    CRUD.Update,
                    businessEntity.ProductId,
                    businessEntity.CountryId,
                    businessEntity.LaboratoryId,
                    businessEntity.ProductName,
                    businessEntity.Description,
                    businessEntity.Active,
                    businessEntity.Register,
                    0,0);
        }
        public ProductsInfo getOne(int pk)
        {
            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProducts");

            // Add the parameters:
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<ProductsToEditInfo> getEditableProductsByDivision(int divisionId, int countryId, int editionId, int bookId, string brand)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsToEditByDivision");

            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            if (brand != null)
                ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);

                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);

                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    if (dataReader["PharmaActive"] != System.DBNull.Value)
                        record.PharmaActive = Convert.ToBoolean(dataReader["PharmaActive"]);

                    if (dataReader["DivisionId"] != System.DBNull.Value)
                        record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);

                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    if (dataReader["CategoryId"] != System.DBNull.Value)
                        record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);

                    record.CategoryName = dataReader["CategoryName"].ToString();
                    record.Editions = dataReader["Editions"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.NewProduct = Convert.ToBoolean(dataReader["New"]);
                    record.ModifiedContent= Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.Register = dataReader["Reg"].ToString();
                   
                 
                    

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }
  

        public ProductsInfo getById(int productId) {
            ProductsInfo record = new ProductsInfo();
            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductById");

            // Add the parameters:
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.ProductName = dataReader["ProductName"].ToString();
                    record.SanitaryRegistry = dataReader["Register"].ToString();
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.LaboratoryId = Convert.ToInt32(dataReader["LaboratoryId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }
            return record;
        }

        public ProductDetailInfo getProductAttributes(string isbn, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductAttributes");

            // Add the parameters:
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ProductDetailInfo record = new ProductDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.DivisionName = dataReader["DivisionName"].ToString();

                    if (dataReader["ProductShot"] != DBNull.Value)
                        record.ProductShot = dataReader["ProductShot"].ToString();

                    if (dataReader["DivisionImage"] != DBNull.Value)
                        record.DivisionImage = dataReader["DivisionImage"].ToString();

                    if (dataReader["BaseUrl"] != DBNull.Value)
                        record.BaseUrl = dataReader["BaseUrl"].ToString();

                    if (dataReader["ReferenceUrl"] != DBNull.Value)
                        record.ReferenceUrl = dataReader["ReferenceUrl"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

    
        public List<ProductsInfo> getByDivision(int divisionId, int countryId, int bookId)
        {
            List<ProductsInfo> BECollection = new List<ProductsInfo>();

            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByDivisionWithOutEdition");

            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public int insert(ProductsInfo businessEntity)
        {
            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProducts");

            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@accessId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LaboratoryId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductName);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            if (businessEntity.Description != null)
                ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);

        
                ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Register", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Register);

            ProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ProductId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ProductId;
        }
        public List<ProductsToEditInfo> getProductsByCountryAndDivision(int divisionId, int countryId)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByCountryAndDivision");

            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);


            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);

                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    if (dataReader["PharmaActive"] != System.DBNull.Value)
                        record.PharmaActive = Convert.ToBoolean(dataReader["PharmaActive"]);

                    if (dataReader["DivisionId"] != System.DBNull.Value)
                        record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    if (dataReader["CategoryId"] != System.DBNull.Value)
                        record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.Editions = dataReader["Editions"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                   

                    record.Register = dataReader["Register"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<AssignedProduct> getNoAssignedProducts(int editionId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetNoAssignedProducts");


            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<AssignedProduct> recordCollection = new List<AssignedProduct>();
            dbCmd.CommandTimeout = 600;
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {

                AssignedProduct record;

                while (dataReader.Read())
                {
                    record = new AssignedProduct();
                    
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.ProductType = dataReader["ProductType"].ToString();
                    //record.ProductTypeId = Convert.ToInt32(dataReader["ProductTypeId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.Substances= Convert.ToBoolean(dataReader["Substances"]);
                    record.Agrochemicals= Convert.ToBoolean(dataReader["Agrochemicals"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Croops = Convert.ToBoolean(dataReader["Crops"]);

                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.Seeds = Convert.ToBoolean(dataReader["Seeds"]);
                    recordCollection.Add(record);
                }

                return recordCollection;


            }
        }
        public List<DivisionProductsToEditInfo> getEditableDivisionProducts(int divisionId, int countryId, int? divisionSearch, string brand)
        {
            List<DivisionProductsToEditInfo> BECollection = new List<DivisionProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionProductsToEdit");

            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            if (divisionSearch != null)
                ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionSearch", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionSearch);

            if (brand != null)
                ProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                DivisionProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new DivisionProductsToEditInfo();

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }



        #endregion


        #endregion

        #region Protected Metods

   
        protected ProductsInfo getFromDataReader(IDataReader current)
        {
            ProductsInfo record = new ProductsInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
           
            record.LaboratoryId = Convert.ToInt32(current["LaboratoryId"]);
            record.CountryId = Convert.ToInt32(current["CountryId"]);

            record.ProductName= current["ProductName"].ToString();

            if (current["Description"] != DBNull.Value)
                record.Description = current["Description"].ToString();

            if (current["Register"] != DBNull.Value)
                record.Register= current["Register"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }


        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();
    }
}
