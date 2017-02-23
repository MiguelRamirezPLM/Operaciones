using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class OperationDALC : PLMUsersDataAccesAdapter<OperationInfo>
    {
        #region Constructors

        private OperationDALC() { }

        #endregion

        #region Public Methods

        public override int insert(OperationInfo businessEntity)
        {
            DbCommand dbCom = OperationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperations");

            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Create);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
        
            OperationDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

            businessEntity.OperationId = Convert.ToInt32(dbCom.Parameters["@Return"].Value);

            return businessEntity.OperationId;
        }

        public override void delete(int pk)
        {
            DbCommand dbCom = OperationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperations");

            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Delete);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@operationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            OperationDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override void update(OperationInfo businessEntity)
        {
            DbCommand dbCom = OperationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperations");

            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Update);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@operationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OperationId);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@description", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            OperationDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override OperationInfo getOne(int pk)
        {
            DbCommand dbCom = OperationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOperations");

            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, OperationDALC.CRUD.Read);
            OperationDALC.InstanceDatabase.AddParameter(dbCom, "@operationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = OperationDALC.InstanceDatabase.ExecuteReader(dbCom))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected

        protected override OperationInfo getFromDataReader(IDataReader current)
        {
            OperationInfo record = new OperationInfo();

            record.OperationId = Convert.ToInt32(current["OperationId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly OperationDALC Instance = new OperationDALC();
    }
}
