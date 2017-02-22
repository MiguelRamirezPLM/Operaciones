using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ActiveSubstancesDALC : MedinetDataAccessAdapter<ActiveSubstanceInfo>
    {
        #region Contructors

        private ActiveSubstancesDALC() { }

        #endregion

        #region Public methods

        public override int insert(MedinetBusinessEntries.ActiveSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstances");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@englishDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EnglishDescription);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@enunciative", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Enunciative);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            ActiveSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(ActiveSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstances");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@englishDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EnglishDescription);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@enunciative", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Enunciative);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            ActiveSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ActiveSubstanceInfo> getAllByFilter(string description)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(
                ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstanceByName", description)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public ActiveSubstanceInfo getOneByName(string description)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstanceByName");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<ActiveSubstanceInfo> getAllByProduct(int productId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstanceByProduct", productId);

            List<ActiveSubstanceInfo> recordCollection = new List<ActiveSubstanceInfo>();

            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    recordCollection.Add(getFromDataReader(dataReader));
                }

                return recordCollection;
            }
        }

        public string getActiveSustByProduct(int productId)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetSqlStringCommand("SELECT dbo.plm_dfGetActiveSubstancesByProduct (" + productId + ")");

            return IndicationsDALC.MedInstanceDatabase.ExecuteScalar(dbCmd).ToString();
        }

        public List<ActiveSubstanceInfo> getAllWithoutProduct(int productId, string substance)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesWithoutProduct");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substance", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, substance);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }
        
        public List<ActiveSubstanceInfo> getAllWithoutProductInteractions(int productId, string substance,int pharmaFormId,int categoryId,int divisionId)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesWithoutInteractions");
            
            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaForm", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substance", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, substance);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public ActiveSubstanceInfo checkByName(string description)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstances");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override ActiveSubstanceInfo getOne(int pk)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstances");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<ActiveSubstanceInfo> getAllByFilter(int activeSubstanceId)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<ActiveSubstanceInfo> getAllByFilter(int activeSubstanceId, string description)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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

        protected override ActiveSubstanceInfo getFromDataReader(IDataReader current)
        {
            ActiveSubstanceInfo record = new ActiveSubstanceInfo();

            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.Description = current["Description"].ToString();
            if (current["EnglishDescription"] != System.DBNull.Value)
                record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ActiveSubstancesDALC Instance = new ActiveSubstancesDALC();
    }
}
