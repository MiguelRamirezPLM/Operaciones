using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ParticipantProductsDALC : MedinetDataAccessAdapter<ParticipantProductsInfo>
    {

        #region Constructors

        private ParticipantProductsDALC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spProductEdition");

            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(ParticipantProductsInfo businessEntity)
        {

            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
           
            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public ParticipantProductsInfo getOne(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ParticipantProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public override void delete(ParticipantProductsInfo businessEntity)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);


            //Delete record:
            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ParticipantProductsInfo businessEntity)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDParticipantProducts");

            // Add the parameters:
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@page", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Page);

            if(!string.IsNullOrEmpty(businessEntity.HTMLContent))
                ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@htmlContent", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HTMLContent);

            if (!string.IsNullOrEmpty(businessEntity.XMLContent))
                ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@xmlContent", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.XMLContent);

            if (businessEntity.ModifiedContent != null)
                ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@modifiedContent", DbType.Boolean,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ModifiedContent);

            //Update record:
            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<PagedProductInfo> getPagedProducts(int editionId, string brand, byte typeInEdition)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPagedProducts");

            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Participante ? "P" :
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<PagedProductInfo> recordCollection = new List<PagedProductInfo>();

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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
        public List<PagedProductInfo> getPagedProductsSymptoms(int editionId, string brand, byte typeInEdition)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPagedProductsSymptom");

            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Participante ? "P" :
                (ParticipantProductsDALC.TypeInEdition)typeInEdition == ParticipantProductsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<PagedProductInfo> recordCollection = new List<PagedProductInfo>();

            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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
