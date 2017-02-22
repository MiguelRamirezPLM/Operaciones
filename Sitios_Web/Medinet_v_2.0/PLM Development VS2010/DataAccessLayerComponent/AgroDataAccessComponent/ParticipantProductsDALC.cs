using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class ParticipantProductsDALC : AgroDataAccessAdapter<ParticipantProductsInfo>
    {

        #region Constructors

        private ParticipantProductsDALC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spProductEdition");

            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ParticipantProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(ParticipantProductsInfo businessEntity)
        {

            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
           
            ParticipantProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public ParticipantProductsInfo getOne(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ParticipantProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public override void delete(ParticipantProductsInfo businessEntity)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);


            //Delete record:
            ParticipantProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ParticipantProductsInfo businessEntity)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@page", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Page);

            if(!string.IsNullOrEmpty(businessEntity.HTMLContent))
                ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@htmlContent", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HTMLContent);

            if (!string.IsNullOrEmpty(businessEntity.XMLContent))
                ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@xmlContent", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.XMLContent);

            if (businessEntity.ModifiedContent != null)
                ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@modifiedContent", DbType.Boolean,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ModifiedContent);

            //Update record:
            ParticipantProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<PagedProductInfo> getPagedProducts(int editionId, string brand, byte typeInEdition)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPagedProducts");

            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Participante ? "P" :
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<PagedProductInfo> recordCollection = new List<PagedProductInfo>();

            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {

                PagedProductInfo record;

                while (dataReader.Read())
                {
                    record = new PagedProductInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Brand = dataReader["ProductName"].ToString();
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ProductType = dataReader["TypeName"].ToString();
                   
                    record.Register = dataReader["Register"].ToString();
                    if (dataReader["Page"] != System.DBNull.Value)
                        record.Page = dataReader["Page"].ToString();

                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);
                    
                    recordCollection.Add(record);
                }

            }

            return recordCollection;

        }
        public List<PagedProductInfo> getPagedProductsSymptoms(int editionId, string brand, byte typeInEdition)
        {
            DbCommand dbCmd = ParticipantProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPagedProductsSymptom");

            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            ParticipantProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Participante ? "P" :
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<PagedProductInfo> recordCollection = new List<PagedProductInfo>();

            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {

                PagedProductInfo record;

                while (dataReader.Read())
                {
                    record = new PagedProductInfo();

                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.CategoryName = dataReader["CategoryName"].ToString();
                    record.ProductDescription = dataReader["ProductDescription"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ProductType = dataReader["productType"].ToString();
                    record.Symptoms = dataReader["Symptoms"].ToString();
                    if (dataReader["Page"] != System.DBNull.Value)
                        record.Page = dataReader["Page"].ToString();

                    record.NewProduct = Convert.ToBoolean(dataReader["NewProduct"]);
                    record.ModifiedContent = Convert.ToBoolean(dataReader["ModifiedContent"]);
                    record.Sidef = Convert.ToBoolean(dataReader["Sidef"]);

                    recordCollection.Add(record);
                }

            }

            return recordCollection;

        }
        #endregion 

        #region Protected Methods

        protected override ParticipantProductsInfo getFromDataReader(IDataReader current)
        {
            ParticipantProductsInfo record = new ParticipantProductsInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);

            if (current["Page"] != System.DBNull.Value)
                record.Page = current["Page"].ToString();

            if (current["HTMLContent"] != System.DBNull.Value)
                record.HTMLContent = current["HTMLContent"].ToString();

            if (current["XMLContent"] != System.DBNull.Value)
                record.XMLContent = current["XMLContent"].ToString();

            return record;
        }

        #endregion

        public static readonly ParticipantProductsDALC Instance = new ParticipantProductsDALC();

    }
}
