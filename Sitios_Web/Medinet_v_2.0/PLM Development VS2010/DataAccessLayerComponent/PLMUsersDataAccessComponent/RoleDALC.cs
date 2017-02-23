using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class RoleDALC : PLMUsersDataAccesAdapter<RoleInfo>
    {
        #region Constructors

        private RoleDALC() { }

        #endregion

        #region Public Methods

        public override int insert(RoleInfo businessEntity)
        {
            DbCommand dbCom = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRoles");

            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RoleDALC.CRUD.Create);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@RoleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
         
            RoleDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

            businessEntity.RoleId = Convert.ToInt32(dbCom.Parameters["@Return"].Value);

            return businessEntity.RoleId;
        }

        public override void delete(int pk)
        {
            DbCommand dbCom = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRoles");

            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RoleDALC.CRUD.Delete);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            RoleDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override void update(RoleInfo businessEntity)
        {
            DbCommand dbCom = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRoles");

            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RoleDALC.CRUD.Update);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
        
            RoleDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override RoleInfo getOne(int pk)
        {
            DbCommand dbCom = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDRoles");

            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, RoleDALC.CRUD.Read);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = RoleDALC.InstanceDatabase.ExecuteReader(dbCom))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public RoleInfo getByUser(int userId, string hashKey)
        {
            DbCommand dbCom = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetRoleByUser");

            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            RoleDALC.InstanceDatabase.AddParameter(dbCom, "@hashKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hashKey);

            using (IDataReader dataReader = RoleDALC.InstanceDatabase.ExecuteReader(dbCom))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<RoleInfo> getRoles()
        {
            DbCommand dbCmd = RoleDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetRoles");

            List<RoleInfo> BECollection = new List<RoleInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = RoleDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        } 

        #endregion

        #region Protected

        protected override RoleInfo getFromDataReader(IDataReader current)
        {
            RoleInfo record = new RoleInfo();

            record.RoleId = Convert.ToInt32(current["RoleId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion

        public static readonly RoleDALC Instance = new RoleDALC();
    }
}
