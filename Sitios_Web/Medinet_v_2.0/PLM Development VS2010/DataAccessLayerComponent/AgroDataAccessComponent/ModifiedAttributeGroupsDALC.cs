using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class ModifiedAttributeGroupsDALC : AgroDataAccessAdapter<ModifiedAttributeGroupsInfo>
    {

        #region Constructors

        private ModifiedAttributeGroupsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ModifiedAttributeGroupsInfo businessEntity)
        {
            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDModifiedAttributeGroups");

            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttributeGroupId);

            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ModifiedAttributeGroupsInfo businessEntity)
        {
            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDModifiedAttributeGroups");

            // Add the parameters:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttributeGroupId);

            //Delete record:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public AgroBusinessEntries.ModifiedAttributeGroupsInfo getOne(ModifiedAttributeGroupsInfo businessEntity)
        {
            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDModifiedAttributeGroups");

            // Add the parameters:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttributeGroupId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> getModifiedAttributeGroupsByProduct(int editionId, int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> BECollection = new List<ModifiedAttributeGroupsInfo>();

            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetModifiedAttributeGroupsByProduct");

            // Add the parameters:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<AgroBusinessEntries.AttributeGroupDetailInfo> getAttributeGroupsByProduct(int bookId, int countryId, int editionId,
            int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            List<AgroBusinessEntries.AttributeGroupDetailInfo> BECollection = new List<AttributeGroupDetailInfo>();

            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetModifiedAttributeGroupsByProduct");

            // Add the parameters:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            using (IDataReader dataReader = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                AgroBusinessEntries.AttributeGroupDetailInfo record;

                while (dataReader.Read())
                {
                    record = new AttributeGroupDetailInfo();

                    record.AttributeGroupId = Convert.ToInt32(dataReader["AttributeGroupId"]);
                    record.AttributeGroupName = dataReader["AttributeGroupName"].ToString();
                    record.ModifiedAttributeGroup = Convert.ToBoolean(dataReader["ModifiedAttributeGroup"]);

                    BECollection.Add(record);
                }
                return BECollection;
            }
        }

        public List<AgroBusinessEntries.AttributeGroupInfo> getModifiedAttributesByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<AgroBusinessEntries.AttributeGroupInfo> BECollection = new List<AttributeGroupInfo>();

            DbCommand dbCmd = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAttributeGroups");

            // Add the parameters:
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ModifiedAttributeGroupsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ModifiedAttributeGroupsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    AttributeGroupInfo record = new AttributeGroupInfo();

                    record.AttributeGroupId = Convert.ToInt32(dataReader["AttributeGroupId"]);
                    record.AttributeGroupName = dataReader["AttributeGroupName"].ToString();
                    record.AttributeGroupOrder = Convert.ToInt32(dataReader["AttributeGroupOrder"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override ModifiedAttributeGroupsInfo getFromDataReader(IDataReader current)
        {
            ModifiedAttributeGroupsInfo record = new ModifiedAttributeGroupsInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.AttributeGroupId = Convert.ToInt32(current["AttributeGroupId"]);
            
            return record;
        }

        #endregion

        public static readonly ModifiedAttributeGroupsDALC Instance = new ModifiedAttributeGroupsDALC();

    }
}
