using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class OperationRoleDALC : PLMUsersDataAccesAdapter<OperationRolesInfo>
    {
         #region Constructors

        private OperationRoleDALC() { }

        #endregion

        #region Public Methods

        public override int insert(OperationRolesInfo businessEntity)
        {
            DbCommand dbCom = OperationRoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperationRoles");

            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Create);
            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@operationId", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OperationId);
            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
        
            OperationRoleDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

            return Convert.ToInt32(dbCom.Parameters["@Return"].Value);
        }

        public override void delete(OperationRolesInfo businessEntity)
        {
            DbCommand dbCom = OperationRoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperationRoles");

            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Delete);
            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@operationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OperationId);
            OperationRoleDALC.InstanceDatabase.AddParameter(dbCom, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
            
            OperationDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

       

        #endregion

        #region Protected

        protected override OperationRolesInfo getFromDataReader(IDataReader current)
        {
            OperationRolesInfo record = new OperationRolesInfo();

            record.OperationId = Convert.ToInt32(current["OperationId"]);
            record.RoleId = Convert.ToInt32(current["RoleId"]);

            return record;
        }

        #endregion

        public static readonly OperationRoleDALC Instance = new OperationRoleDALC();

    }
}
