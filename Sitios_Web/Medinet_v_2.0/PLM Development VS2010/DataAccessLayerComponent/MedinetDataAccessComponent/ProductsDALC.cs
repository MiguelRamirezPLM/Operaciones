using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductsDALC : MedinetDataAccessAdapter<ProductsInfo>
    {
        #region Constructors

        private ProductsDALC() { }

        #endregion

        #region Public methods

        public ProductContentsInfo getProductContent(int productId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductContent");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            ProductContentsInfo record = new ProductContentsInfo();
            
            using(IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.ProductId = Convert.ToInt32(dataReader["productId"]);
                    //record.Div_ID = Convert.ToInt32(dataReader["div_Id"]);
                    //record.Edi_ID = Convert.ToInt32(dataReader["edi_Id"]);
                    //record.Atr_ID = Convert.ToInt32(dataReader["atr_Id"]);
                    //record.Atr_Nombre = dataReader["atr_Nombre"].ToString();
                    //record.Tex_TextoPlano = dataReader["tex_TextoPlano"].ToString();

                    return record;
                }
                else
                    return null;
            }
            
        }

        public List<ProductsToEditInfo> getEditableProducts(int laboratoryId, string brand, int countryId, int editionId, int bookId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsToEdit");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, laboratoryId);

            if (brand != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            List<ProductsToEditInfo> recordCollection = new List<ProductsToEditInfo>();
            
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {   
                ProductsToEditInfo record; 
                
                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();
                    
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.ProductActive = Convert.ToBoolean(dataReader["ProductActive"]);

                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                    {
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                        record.PharmaForm = dataReader["PharmaForm"].ToString();
                        record.PharmaActive = Convert.ToBoolean(dataReader["PharmaActive"]);
                    }
                    if (dataReader["DivisionId"] != System.DBNull.Value)
                        record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);

                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    if (dataReader["CategoryId"] != System.DBNull.Value)
                        record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);

                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.Editions = dataReader["Editions"].ToString();

                    record.ProductDescription = dataReader["ProductDescription"].ToString();

                    record.AdDescription = dataReader["AdDescription"].ToString();

                    recordCollection.Add(record);
                }
                return recordCollection;
            }
        }

        public List<ProductsToEditInfo> getEditableProductsByDivision(int divisionId, int countryId, int editionId, int bookId, string brand)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsToEditByDivision");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            if (brand != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();
                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    record.ProductType = dataReader["ProductType"].ToString();
                    if (dataReader["ContentFamilyId"] != System.DBNull.Value)
                        record.ContentFamilyId = Convert.ToInt32(dataReader["ContentFamilyId"]);

                    if (dataReader["ContentFamilyString"] != System.DBNull.Value)
                        record.ContentFamilyString = dataReader["ContentFamilyString"].ToString();

                    if (dataReader["PSFamilyId"] != System.DBNull.Value)
                        record.PSFamilyId = Convert.ToInt32(dataReader["PSFamilyId"]);

                    if (dataReader["PSFamilyString"] != System.DBNull.Value)
                        record.PSFamilyString = dataReader["PSFamilyString"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<ProductsToEditInfo> getProductsByCountryAndDivision(int divisionId, int countryId)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByCountryAndDivision");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);


            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();

                      record.ProductType = dataReader["ProductType"].ToString();
  
                    BECollection.Add(record);
                }
                return BECollection;
            }
        }


        public List<ProductsToEditInfo> getParticipantProductsByDivision(int divisionId, int countryId, int editionId, int bookId)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetParticipantProductsByDivision");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();
                    
                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    record.ProductType = dataReader["ProductType"].ToString();
                    record.Symptoms = dataReader["Symptoms"].ToString();

                    if (dataReader["ContentFamilyId"] != System.DBNull.Value)
                        record.ContentFamilyId = Convert.ToInt32(dataReader["ContentFamilyId"]);
                    if (dataReader["ContentFamilyString"] != System.DBNull.Value)
                        record.ContentFamilyString = dataReader["ContentFamilyString"].ToString();
                    if (dataReader["PSFamilyId"] != System.DBNull.Value)
                        record.PSFamilyId = Convert.ToInt32(dataReader["PSFamilyId"]);
                    if (dataReader["PSFamilyString"] != System.DBNull.Value)
                        record.PSFamilyString = dataReader["PSFamilyString"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }
        public List<ProductsToEditInfo> getParticipantProductImages(int divisionId, int countryId,int bookId,int editionId)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetParticipantProductImages");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);


            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();

                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    record.ProductType = dataReader["ProductType"].ToString();
                    record.Symptoms = dataReader["Symptoms"].ToString();
                    record.Image = dataReader["Imagen"].ToString();
                    if (dataReader["ContentFamilyId"] != System.DBNull.Value)
                        record.ContentFamilyId = Convert.ToInt32(dataReader["ContentFamilyId"]);
                    if (dataReader["ContentFamilyString"] != System.DBNull.Value)
                        record.ContentFamilyString = dataReader["ContentFamilyString"].ToString();
                    if (dataReader["PSFamilyId"] != System.DBNull.Value)
                        record.PSFamilyId = Convert.ToInt32(dataReader["PSFamilyId"]);
                    if (dataReader["PSFamilyString"] != System.DBNull.Value)
                        record.PSFamilyString = dataReader["PSFamilyString"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<ProductsToEditInfo> getParticipantProductsByDivisionSymptoms(int divisionId, int countryId, int editionId, int bookId)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();
            
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetParticipantProductsByDivisionSymptoms");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();

                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    record.ProductType = dataReader["ProductType"].ToString();
                    record.Symptoms = dataReader["Symptoms"].ToString();

                    if (dataReader["ContentFamilyId"] != System.DBNull.Value)
                        record.ContentFamilyId = Convert.ToInt32(dataReader["ContentFamilyId"]);
                    if (dataReader["ContentFamilyString"] != System.DBNull.Value)
                        record.ContentFamilyString = dataReader["ContentFamilyString"].ToString();
                    if (dataReader["PSFamilyId"] != System.DBNull.Value)
                        record.PSFamilyId = Convert.ToInt32(dataReader["PSFamilyId"]);
                    if (dataReader["PSFamilyString"] != System.DBNull.Value)
                        record.PSFamilyString = dataReader["PSFamilyString"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<ProductsToEditInfo> getParticipantProductsByEdition(int countryId, int editionId, int bookId, string brand)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetParticipantProductsByEdition");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                ProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new ProductsToEditInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
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
                    record.AdDescription = dataReader["AdDescription"].ToString();

                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);

                    if (dataReader["ContentFamilyId"] != System.DBNull.Value)
                        record.ContentFamilyId = Convert.ToInt32(dataReader["ContentFamilyId"]);
                    if (dataReader["ContentFamilyString"] != System.DBNull.Value)
                        record.ContentFamilyString = dataReader["ContentFamilyString"].ToString();
                    if (dataReader["PSFamilyId"] != System.DBNull.Value)
                        record.PSFamilyId = Convert.ToInt32(dataReader["PSFamilyId"]);
                    if (dataReader["PSFamilyString"] != System.DBNull.Value)
                        record.PSFamilyString = dataReader["PSFamilyString"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public override ProductsInfo getOne(int pk)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProducts");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert(ProductsInfo businessEntity)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProducts");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@alphabetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AlphabetId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LaboratoryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Brand);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            if (businessEntity.Description != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            
            if(businessEntity.SanitaryRegistry != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@sanitaryRegistry", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SanitaryRegistry);

            ProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ProductId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ProductId;       
        }

        public override void update(ProductsInfo businessEntity)    
        {
            ProductsDALC.MedInstanceDatabase.ExecuteNonQuery("dbo.plm_spCRUDProducts",
                    CRUD.Update,
                    businessEntity.ProductId,
                    businessEntity.AlphabetId,
                    businessEntity.CountryId,
                    businessEntity.LaboratoryId,
                    businessEntity.Brand,
                    businessEntity.Description,
                    businessEntity.Active,
                    businessEntity.SanitaryRegistry,
                    businessEntity.ProductTypeId);   
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProducts");

            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            ProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<AssignedProduct> getNoAssignedProducts(int editionId, byte typeInEdition)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetNoAssignedProducts");


            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Participante ? "P" :
                (ProductsDALC.TypeInEdition)typeInEdition == ProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<AssignedProduct> recordCollection = new List<AssignedProduct>();
            dbCmd.CommandTimeout = 600;
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                AssignedProduct record;

                while (dataReader.Read())
                {
                    record = new AssignedProduct();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    if (dataReader["PharmaFormId"] != System.DBNull.Value)
                        record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();

                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();

                    record.ATC = Convert.ToBoolean(dataReader["ATC"]);
                    record.Substance = Convert.ToBoolean(dataReader["Substance"]);

                    record.Indication = Convert.ToBoolean(dataReader["Indication"]);
                    
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.Interaction = dataReader["Interaction"].ToString();
                    record.ProductType = dataReader["ProductType"].ToString();
                    record.ProductTypeId = Convert.ToInt32(dataReader["ProductTypeId"]);
                    record.Symptom = Convert.ToBoolean(dataReader["Symptom"]);
                    record.Contraindication = dataReader["Contraindication"].ToString();
                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    record.ICD = Convert.ToBoolean(dataReader["ICD"]);
                    record.Encyclopedia = Convert.ToBoolean(dataReader["ENCYCLOPEDIA"]);
                    record.ATCOMS = Convert.ToBoolean(dataReader["ATCOMS"]);
                    //
                    recordCollection.Add(record);
                }

                return recordCollection;


            }
        }

        public bool hasContent(int productId)
        {
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spProductHasContent");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pro_ID", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            ProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public List<ProductsInfo> getByDivision(int divisionId, int countryId, int bookId)
        {
            List<ProductsInfo> BECollection = new List<ProductsInfo>(); 

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByDivision");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<DivisionProductsToEditInfo> getEditableDivisionProducts(int divisionId, int countryId, int? divisionSearch, string brand)
        {
            List<DivisionProductsToEditInfo> BECollection = new List<DivisionProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionProductsToEdit");

            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            
            if(divisionSearch != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionSearch", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionSearch);

            if (brand != null)
                ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                DivisionProductsToEditInfo record;

                while (dataReader.Read())
                {
                    record = new DivisionProductsToEditInfo();

                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        #endregion

        #region Protected methods

        protected override ProductsInfo getFromDataReader(IDataReader current)
        {
            ProductsInfo record = new ProductsInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.AlphabetId = Convert.ToInt32(current["AlphabetId"]);
            record.LaboratoryId = Convert.ToInt32(current["LaboratoryId"]);
            record.CountryId = Convert.ToInt32(current["CountryId"]);

            record.Brand = current["Brand"].ToString();

            if (current["Description"] != DBNull.Value)
                record.Description = current["Description"].ToString();

            if (current["SanitaryRegistry"] != DBNull.Value)
                record.SanitaryRegistry = current["SanitaryRegistry"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);
            record.ProductTypeId = Convert.ToInt32(current["ProductTypeId"]);

            return record;
        }

        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();
    }
}
