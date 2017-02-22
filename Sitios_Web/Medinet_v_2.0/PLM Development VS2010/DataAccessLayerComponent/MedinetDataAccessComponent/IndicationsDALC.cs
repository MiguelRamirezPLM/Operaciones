using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class IndicationsDALC : MedinetDataAccessAdapter<IndicationInfo>
    {
        #region Constructors

        private IndicationsDALC() { }

        #endregion

        #region Public methods

        public override int insert(IndicationInfo businessEntity)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDIndications");

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);

            if(businessEntity.ParentId != null)
                IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ParentId);
    
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            IndicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(IndicationInfo businessEntity)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDIndications");

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IndicationId);
            
            if(businessEntity.ParentId != null)
                IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ParentId);

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            IndicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override IndicationInfo getOne(int pk)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDIndications");

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<IndicationInfo> getAllByFilter(string description)
        {
            List<IndicationInfo> BECollection = new List<IndicationInfo>();

            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllIndications");

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<IndicationInfo> getAllByFilter(int indicationId, string description)
        {
            List<IndicationInfo> BECollection = new List<IndicationInfo>();

            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllIndications");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public IndicationInfo getOneByName(string description)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetIndicationsByName");

            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<IndicationInfo> getAllByProduct(int productId)
        {
            List<IndicationInfo> BECollection = new List<IndicationInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(
                IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetIndicationsByProduct", productId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public string getIndicationsByProduct(int productId)
        {
                DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetSqlStringCommand("SELECT dbo.plm_dfGetIndicationsByProduct (" + productId + ")");

            return IndicationsDALC.MedInstanceDatabase.ExecuteScalar(dbCmd).ToString();
        }

        public List<IndicationInfo> getAllByParent(int parentId)
        {
            List<IndicationInfo> BECollection = new List<IndicationInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(
                IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetIndicationsByParentId", parentId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;        
        }

        public List<IndicationInfo> getAllWithoutProduct(int productId, string indication)
        {
            List<IndicationInfo> BECollection = new List<IndicationInfo>();

            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetIndicationsWithoutProduct");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indication", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indication);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override IndicationInfo getFromDataReader(IDataReader current)
        {
            IndicationInfo record = new IndicationInfo();

            record.IndicationId = Convert.ToInt32(current["IndicationId"]);
            if (current["ParentId"] != DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly IndicationsDALC Instance = new IndicationsDALC();
     
    }
}
