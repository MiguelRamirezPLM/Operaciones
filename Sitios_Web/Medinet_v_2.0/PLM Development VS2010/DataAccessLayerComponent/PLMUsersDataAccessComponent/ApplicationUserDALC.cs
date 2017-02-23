using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class ApplicationUserDALC : PLMUsersDataAccesAdapter<ApplicationUserInfo>
    {
        #region Constructors

        private ApplicationUserDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ApplicationUserInfo businessEntity)
        {
            DbCommand dbCmd = ApplicationUserDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplicationUsers");
            //Add the parameters: 
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Create);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
                        
            //Insert Record:
            ApplicationUserDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void  delete(ApplicationUserInfo businessEntity)
       {
            DbCommand dbCmd = ApplicationUserDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplicationUsers");
            
            //Add parameter to Stored Procedure
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Delete);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            
            //Delete record:
            ApplicationUserDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public override void update(ApplicationUserInfo businessEntity)
        {
            DbCommand dbCmd = ApplicationUserDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplicationUsers");
            
            //Add the parameters:
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Update);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
          
            //Update record:
            ApplicationUserDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public ApplicationUserInfo getOne(int appId, int usId)
        {
            DbCommand dbCmd = ApplicationUserDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDApplicationUsers");

            // Add the parameters:
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationUserDALC.CRUD.Read);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, appId);
            ApplicationUserDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, usId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ApplicationUserDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }
               
        #endregion

        #region Protected

        protected override ApplicationUserInfo getFromDataReader(IDataReader current)
        {
            ApplicationUserInfo record = new ApplicationUserInfo();

            record.ApplicationId = Convert.ToInt32(current["ApplicationId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);
            record.RoleId = Convert.ToInt32(current["RoleId"]);
                                   
            return record;
        }

        #endregion

        public static readonly ApplicationUserDALC Instance = new ApplicationUserDALC();
    }
}
