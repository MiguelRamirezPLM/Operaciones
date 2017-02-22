using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class OSMobileClientCodesDALC : PLMClientsDataAccessAdapter<OSMobileClientCodesInfo>
    {

        #region Constructors

        private OSMobileClientCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(OSMobileClientCodesInfo businessEntity)
        {

            DbCommand dbCmd = OSMobileClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOSMobileClientCodes");

            // Add the parameters:
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IMEI);
            
            //Insert record:
            OSMobileClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public override void delete(OSMobileClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = OSMobileClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOSMobileClientCodes");

            // Add the parameters:
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            
            //Delete record:
            OSMobileClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public override OSMobileClientCodesInfo getOne(OSMobileClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = OSMobileClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOSMobileClientCodes");

            // Add the parameters:
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            OSMobileClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = OSMobileClientCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override OSMobileClientCodesInfo getFromDataReader(IDataReader current)
        {
            OSMobileClientCodesInfo record = new OSMobileClientCodesInfo();

            record.OSMobileId = Convert.ToByte(current["OSMobileId"]);
            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            
            if (current["IMEI"] != System.DBNull.Value)
                record.IMEI = current["IMEI"].ToString();

            return record;
        }

        #endregion

        public static readonly OSMobileClientCodesDALC Instance = new OSMobileClientCodesDALC();

    }
}
