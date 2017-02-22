using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class OSMobileUserCodesDALC : PLMClientsDataAccessAdapter<OSMobileUserCodesInfo>
    {
        #region Constructors

        private OSMobileUserCodesDALC() { }

        #endregion

        #region Public methods

        public override int insert(OSMobileUserCodesInfo businessEntity)
        {
            DbCommand dbCmd = OSMobileUserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOSMobileUserCodes");

            // Add the parameters:
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OSMobileId);
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            OSMobileUserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IMEI);

            //Insert record:
            OSMobileUserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 

        }

        #endregion

        #region Protected Methods

        protected override OSMobileUserCodesInfo getFromDataReader(IDataReader current)
        {
            OSMobileUserCodesInfo record = new OSMobileUserCodesInfo();

            record.OSMobileId = Convert.ToByte(current["OSMobileId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.IMEI = current["IMEI"].ToString();            

            return record;
        }

        #endregion

        public static readonly OSMobileUserCodesDALC Instance = new OSMobileUserCodesDALC();
    }
}
