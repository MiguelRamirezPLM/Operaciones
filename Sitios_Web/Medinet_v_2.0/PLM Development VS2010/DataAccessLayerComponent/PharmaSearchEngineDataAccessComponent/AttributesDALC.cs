using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class AttributesDALC : PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.AttributesInfo>
    {
        #region Constructors

        private AttributesDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.AttributesInfo> getAll(int editionId)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEAttibutes");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            List<MedinetBusinessEntries.AttributesInfo> BECollection = new List<MedinetBusinessEntries.AttributesInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEAttibutes");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeByProductInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.AttributeByProductInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeByProductInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);
                    record.AttributeName = dataReader["AttributeName"].ToString();

                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        public MedinetBusinessEntries.ProductContentsInfo getAttribute(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, int attributeId)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductContents");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@attributeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    MedinetBusinessEntries.ProductContentsInfo record = new MedinetBusinessEntries.ProductContentsInfo();

                    record.ProductContentId = Convert.ToInt32(dataReader["ProductContentId"]);
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);

                    if (dataReader["Content"] != System.DBNull.Value)
                        record.Content = dataReader["Content"].ToString();

                    if (dataReader["PlainContent"] != System.DBNull.Value)
                        record.PlainContent = dataReader["PlainContent"].ToString();

                    return record;
                }
                else
                    return null;
            }

        }

        public List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> getAttributes(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, MedinetBusinessEntries.Catalogs.TargetOutputs targetId)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEAttibuteDetails");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)targetId);

            List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.AttributeDetailInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeDetailInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);
                    record.AttributeName = dataReader["AttributeName"].ToString();
                    record.AttributeContent = dataReader["AttributeContent"].ToString();
                    record.HTMLContent = dataReader["AttributeHTMLContent"].ToString();

                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo> getAttributesBySymptom(int editionId, int symptomId)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAttributesBySymptom");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, symptomId);

            List<PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeBySymptomInfo();

                    record.AttributeId = Convert.ToInt32(dataReader["AttributeId"]);
                    record.Description = dataReader["Description"].ToString();

                    if (dataReader["Content"] != DBNull.Value)
                        record.Content = dataReader["Content"].ToString();
                    
                    if (dataReader["PlainContent"] != DBNull.Value)
                        record.PlainContent = dataReader["PlainContent"].ToString();

                    if (dataReader["AttributeOrder"] != DBNull.Value)
                        record.AttributeOrder = (int)dataReader["AttributeOrder"];

                    if (dataReader["HeaderImage"] != DBNull.Value)
                        record.HeaderImage = dataReader["HeaderImage"].ToString();

                    BECollection.Add(record);
                }
            }

            return BECollection;
        }

        public List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> getCompleteAttributes(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, String prefixName)
        {
            DbCommand dbCmd = AttributesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEGetCompleteAttributes");

            // Add the parameters:
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            AttributesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixName);

            List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.AttributeDetailInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeDetailInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeGroupId"]);
                    record.AttributeName = dataReader["AttributeGroupName"].ToString();
                    record.AttributeContent = dataReader["AttributePlainContent"].ToString();
                    record.HTMLContent = dataReader["AttributeHTMLContent"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.AttributesInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.AttributesInfo record = new MedinetBusinessEntries.AttributesInfo();

            record.AttributeId = Convert.ToInt32(current["AttributeId"]);
            record.Description = current["Description"].ToString();
            record.TechnicalSheet = Convert.ToBoolean(current["TechnicalSheet"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly AttributesDALC Instance = new AttributesDALC();
    }
}
