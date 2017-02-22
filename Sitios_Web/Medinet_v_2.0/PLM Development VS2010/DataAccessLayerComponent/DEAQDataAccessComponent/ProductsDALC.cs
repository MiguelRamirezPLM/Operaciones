using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

using System.Linq;


namespace DEAQDataAccessComponent
{
    public sealed class ProductsDALC : DEAQEngineDataAccessAdapter<ProductDetailByEditionInfo>
    {
         
        #region Constructor

        private ProductsDALC() { }

        #endregion

        #region Public Methods

        #region ROC Methods
        //Retrieves information about MentionatedProduct by Edition and ProductId
        public DEAQBusinessEntries.MentionatedProductInfo rocGetMentionatedProduct(int editionId, int productId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetMentionatedProduct(editionId, productId)
                        select new DEAQBusinessEntries.MentionatedProductInfo
                        {
                            EditionId = productInfo.EditionId,
                            ProductId = productInfo.ProductId,
                            PharmaFormId = productInfo.PharmaFormId,
                            DivisionId = productInfo.DivisionId,
                            CategoryId = productInfo.CategoryId
                        };

            List<DEAQBusinessEntries.MentionatedProductInfo> products = prods.ToList();

            return products.Count() > 0 ? products[0] : null;
        }

        //Retrieves information about ParticipantProduct by Edition and ProductId
        public DEAQBusinessEntries.ParticipantProductInfo rocGetParticipantProduct(int editionId, int productId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetParticipantProduct(editionId, productId)
                        select new DEAQBusinessEntries.ParticipantProductInfo
                        {
                            EditionId = productInfo.EditionId,
                            DivisionId = productInfo.DivisionId,
                            CategoryId = productInfo.CategoryId,
                            ProductId = productInfo.ProductId,
                            PharmaFormId = productInfo.PharmaFormId,
                            HtmlContent = productInfo.HTMLContent,
                            Page = productInfo.Page
                        };

            List<DEAQBusinessEntries.ParticipantProductInfo> products = prods.ToList();

            return products.Count() > 0 ? products[0] : null;

        }

        //Retrieves information about Product by Edition and ProductId
        public DEAQBusinessEntries.ROC.ProductByEditionInfo rocGetProductById(int editionId, int productId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductById(editionId, productId)
                select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                {
                    ProductId = productInfo.ProductId,
                    ProductName = productInfo.ProductName,
                    Description = productInfo.Description,
                    Register = productInfo.Register,
                    PharmaFormId = productInfo.PharmaFormId,
                    LaboratoryId = productInfo.LaboratoryId,
                    LaboratoryName = productInfo.LaboratoryName,
                    DivisionId = productInfo.DivisionId,
                    DivisionName = productInfo.DivisionName,
                    CategoryId = productInfo.CategoryId
                };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products[0] : null;
        }

        //Retrieves all Products by Edition and Country
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProducts(int countryId, int editionId, int numberByPage, int page)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProducts(countryId, editionId, numberByPage, page)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            LaboratoryId = productInfo.LaboratoryId,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionId = productInfo.DivisionId,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId,
                            RowNumber = (int)productInfo.RowNumber,
                            Total = (int)productInfo.TOTAL
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;
        }

        //Retrieves all Products by Edition and ActiveSubstance
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByActiveSubstance(int activeSubstanceId, int editionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByActiveSubstance(activeSubstanceId, editionId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            PharmaForm = productInfo.PharmaForm,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;

        }

        //Retrieves all Products by Edition and ActiveSubstance
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByCombinedActiveSubstance(int activeSubstanceId, int editionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByCombinedActiveSubstance(activeSubstanceId, editionId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            PharmaForm = productInfo.PharmaForm,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;
        }

        //Retrieves all Products by Edition and Crop
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByCrop(int editionId, int cropId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByCrop(editionId, cropId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            LaboratoryId = productInfo.LaboratoryId,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;
        }

        //Retrieves all Products by Edition, Category and Division
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByDivisionAndCategory(int editionId, int categoryId, int divisionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByDivisionAndCategory(editionId, categoryId, divisionId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            PharmaForm = productInfo.PharmaForm,
                            LaboratoryId = productInfo.LaboratoryId,
                            DivisionId = productInfo.DivisionId,
                            CategoryId = productInfo.CategoryId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;

        }

        //Retrieves all Products by Country, Edition, and FullText
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByFullText(int countryId, int editionId, int numberByPage, int page, string fullText)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByFullText(countryId, editionId, numberByPage, page, fullText)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            LaboratoryId = productInfo.LaboratoryId,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionId = productInfo.DivisionId,
                            DivisionName = productInfo.DivisionName,
                            RowNumber = (int)productInfo.RowNumber,
                            Total = (int)productInfo.TOTAL
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;

        }

        //Retrieves all Products by Country, Edition, and letter
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByLetter(int countryId, int editionId, int numberByPage, int page, string letter)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByLetter(countryId, editionId, numberByPage, page, letter)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            LaboratoryId = productInfo.LaboratoryId,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionId = productInfo.DivisionId,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId,
                            RowNumber = (int)productInfo.RowNumber,
                            Total = (int)productInfo.TOTAL
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;


        }

        //Retrieves all Products by Country, Edition and Text
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsByText(int countryId, int editionId, int numberByPage, int page, string text)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByText(countryId, editionId, numberByPage, page, text)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            PharmaFormId = productInfo.PharmaFormId,
                            LaboratoryId = productInfo.LaboratoryId,
                            LaboratoryName = productInfo.LaboratoryName,
                            DivisionId = productInfo.DivisionId,
                            DivisionName = productInfo.DivisionName,
                            CategoryId = productInfo.CategoryId,
                            RowNumber = (int)productInfo.RowNumber,
                            Total = (int)productInfo.TOTAL
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;
        }

        //Retrieves all Products by Country and Seed
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsBySeed(int countryId, int seedId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsBySeed(countryId, seedId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            LaboratoryId = productInfo.LaboratoryId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;

        }

        //Retrieves all Products by Edition, Seed and Laboratory
        public List<DEAQBusinessEntries.ROC.ProductByEditionInfo> rocGetProductsBySeedAndLaboratory(int editionId, int seedId, int laboratoryId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsBySeedAndLaboratory(editionId, seedId, laboratoryId)
                        select new DEAQBusinessEntries.ROC.ProductByEditionInfo
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            LaboratoryId = productInfo.LaboratoryId,
                            EditionId = productInfo.EditionId
                        };

            List<DEAQBusinessEntries.ROC.ProductByEditionInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;

        }

       //Retrieves all Products by Use
        public List<DEAQBusinessEntries.ROC.ProductByUseInfo> rocGetProductsByUseId(int useId, int countryId, int editionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var prods = from productInfo in db.roc_spGetProductsByUseId(useId, countryId, editionId)
                        select new DEAQBusinessEntries.ROC.ProductByUseInfo
                        {
                            AgrochemicalUseId = productInfo.AgrochemicalUseId,
                            AgrochemicalUseName = productInfo.AgrochemicalUseName,
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Description = productInfo.Description,
                            Register = productInfo.Register,
                            LaboratoryId =(int)productInfo.LaboratoryId,
                            DivisionId = productInfo.DivisionId,
                            CategoryId = productInfo.CategoryId
                        };
            List<DEAQBusinessEntries.ROC.ProductByUseInfo> products = prods.ToList();

            return products.Count() > 0 ? products : null;
        }

        #endregion

        #region PLM Methods

        public List<ProductDetailByEditionInfo> getAll(string isbn)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByEdition");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductDetailByEditionInfo> getByActiveSubstance(string isbn, int activeSubstaceId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByActiveSubstance");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@activeSubstance", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstaceId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductDetailByEditionInfo> getByCrop(string isbn, int cropId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByCrop");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@cropId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, cropId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        public List<ProductsToEditInfo> getEditableProductsByDivision(int divisionId, int countryId, int editionId, int bookId, string brand)
        {
            List<ProductsToEditInfo> BECollection = new List<ProductsToEditInfo>();

            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsToEditByDivision");

            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);

            if (brand != null)
                ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);

            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
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
                    record.AdDescription = dataReader["AdDescription"].ToString();
                    record.Participant = Convert.ToBoolean(dataReader["Participant"]);
                    record.Mentionated = Convert.ToBoolean(dataReader["Mentionated"]);
                    
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<ProductDetailByEditionInfo> getByDivision(string isbn, int divisionId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByDivision");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductDetailByEditionInfo> getBySeed(string isbn, int seedId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductBySeed");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@seedId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, seedId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductDetailByEditionInfo> getByUse(string isbn, int useId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByUse");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@useId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, useId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ProductDetailByEditionInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByText");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text ", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public ProductsInfo getById(int productId) {
            ProductsInfo record = new ProductsInfo();
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductById");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
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
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductAttributes");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ProductDetailInfo record = new ProductDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
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

        public List<ProductDetailByEditionInfo> getByDivisionCategory(string isbn, int divisionId,int categoryId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByDivisionCategory");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        public List<ProductDetailByEditionInfo> getByCategory(string isbn, int categoryId)
        {
            DbCommand dbCmd = ProductsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByCategory");

            // Add the parameters:
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ProductsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId ", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            List<ProductDetailByEditionInfo> BECollection = new List<ProductDetailByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        #endregion


        #endregion

        #region Protected Metods

        protected ProductDetailByEditionInfo getFromDataReader(IDataReader current)
        {
            ProductDetailByEditionInfo record = new ProductDetailByEditionInfo();

                record.CategoryId = Convert.ToInt32(current["CategoryId"]);
                record.Description = current["ProductDescription"].ToString();
                record.DivisionId = Convert.ToInt32(current["DivisionId"]);
                record.DivisionName = current["DivisionName"].ToString();
                record.PharmaForm = current["PharmaForm"].ToString();
                record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
                record.ProductId = Convert.ToInt32(current["ProductId"]);
                record.ProductName = current["ProductName"].ToString();
                record.CategoryName = current["CategoryName"].ToString();
                record.DivisionShortName = current["DivisionShortName"].ToString();
            return record;
        }

        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();
    }
}
