using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class StateOSMobileUserCodesDALC : PLMClientsDataAccessAdapter<StateOSMobileUserCodeInfo>
    {
        #region Constructors

        private StateOSMobileUserCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(StateOSMobileUserCodeInfo businessEntity)
        {
            DbCommand dbCmd = StateOSMobileUserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDStateOsMobileUserCodes");

            // Add the parameters:
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            //Insert record:
            StateOSMobileUserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(StateOSMobileUserCodeInfo businessEntity)
        {
            DbCommand dbCmd = StateOSMobileUserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDStateOsMobileUserCodes");

            // Add the parameters:
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            //Insert record:
            StateOSMobileUserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public StateOSMobileUserCodeInfo getOne(StateOSMobileUserCodeInfo businessEntity)
        {
            DbCommand dbCmd = StateOSMobileUserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDStateOsMobileUserCodes");

            // Add the parameters:
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            //Read record:
            using (IDataReader dataReader = StateOSMobileUserCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    return this.getFromDataReader(dataReader);
                }
                else
                {
                    return null;
                }
            }
        }

        public StateOSMobileUserCodeInfo getByMobile(byte osMobileId, int codeId, int userId)
        {
            DbCommand dbCmd = StateOSMobileUserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStateOsMobileUserCodes");

            // Add the parameters:
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, osMobileId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            StateOSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeId);

            //Read record:
            using (IDataReader dataReader = StateOSMobileUserCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    return this.getFromDataReader(dataReader);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Private Methods

        protected override StateOSMobileUserCodeInfo getFromDataReader(IDataReader current)
        {
            StateOSMobileUserCodeInfo record = new StateOSMobileUserCodeInfo();

            record.StateId = Convert.ToInt32(current["StateId"]); ;
            record.OSMobileId = Convert.ToByte(current["OSMobileId"]); ;
            record.UserId = Convert.ToInt32(current["UserId"]); ;
            record.CodeId = Convert.ToInt32(current["CodeId"]);

            return record;
        }

        #endregion
            
        public static readonly StateOSMobileUserCodesDALC Instance = new StateOSMobileUserCodesDALC();

    }
}
