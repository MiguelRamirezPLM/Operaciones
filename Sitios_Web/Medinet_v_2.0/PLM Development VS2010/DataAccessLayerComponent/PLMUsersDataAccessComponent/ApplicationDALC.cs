using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class ApplicationDALC : PLMUsersDataAccesAdapter<ApplicationInfo>
    {
        #region Constructors

        private ApplicationDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ApplicationInfo businessEntity)
        {
            DbCommand dbCmd = ApplicationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplication");
            //Add the parameters: 
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0); 
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Create);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@hashKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HashKey);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@url", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Url);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@version", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Version);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@lastUpdate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastUpdate);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd,"@documenFile", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DocumentFile);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
                        
            //Insert Record:
            ApplicationDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ApplicationId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ApplicationId;
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = ApplicationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplication");
            
            //Add parameter to Stored Procedure
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Delete);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            ApplicationDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ApplicationInfo businessEntity)
        {
            DbCommand dbCmd = ApplicationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplication");
            
            //Add the parameters:
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Update);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@hashKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HashKey);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@version", DbType.AnsiString,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Version);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@lastUpdate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastUpdate);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@url", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Url);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@documentFile", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DocumentFile);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            ApplicationDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override ApplicationInfo getOne(int pk)
        {
            DbCommand dbCmd = ApplicationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplication");

            // Add the parameters:
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Read);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ApplicationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public ApplicationInfo getOne(string pk)
        {
            DbCommand dbCmd = ApplicationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spSearchApplications");

            // Add the parameters:
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@hashKey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ApplicationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        
        
        

        #endregion

        #region Protected

        protected override ApplicationInfo getFromDataReader(IDataReader current)
        {
            ApplicationInfo record = new ApplicationInfo();

            record.ApplicationId = Convert.ToInt32(current["ApplicationId"]);
            record.Description = current["Description"].ToString();
            record.HashKey = current["HashKey"].ToString();
            record.Url = current["URL"].ToString();
            record.RootUrl = current["RootURL"].ToString();
            record.Version = current["Version"].ToString();
            record.DocumentFile = current["DocumentFile"].ToString();            
            record.Active = Convert.ToBoolean(current["Active"]);
                       
            return record;
        }

        

        #endregion

        public static readonly ApplicationDALC Instance = new ApplicationDALC();

    }
}
