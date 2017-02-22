using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using InfoLogsBusinessEntries;

namespace InfoLogsDataAccessComponent
{
    public class Info_TrackingDALC : InfoLogsDataAccessAdapter<Info_TrackingInfo>
    {

        #region Constructors

        private Info_TrackingDALC() { }

        #endregion

        #region Public Methods

        public override int insert(Info_TrackingInfo businessEntity)
        {
            DbCommand dbCmd = Info_TrackingDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDInfo_Tracking");

            //Add the parameters: 
            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);

            if(businessEntity.ParentId != null)
                Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ParentId);

            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            
            if (businessEntity.SearchDate != null)
                Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchDate);

            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@sourceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SourceId);
            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchTypeId);
            Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@entityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntityId);

            if(businessEntity.SearchParameters != null)
                Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchParameters", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchParameters);

            if(businessEntity.JSONFormat != null)
                Info_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@JSONFormat", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JSONFormat);

            //Insert record:
            Info_TrackingDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            int result = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return result;
        }

        #endregion

        #region Protected Methods

        protected override Info_TrackingInfo getFromDataReader(IDataReader current)
        {
            Info_TrackingInfo record = new Info_TrackingInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);

            if (record.ParentId != null)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.CodeString = current["CodeString"].ToString();
            record.SearchDate = Convert.ToDateTime(current["SearchDate"]);
            record.SourceId = Convert.ToByte(current["SourceId"]);
            record.SearchTypeId = Convert.ToByte(current["SearchTypeId"]);
            record.EntityId = Convert.ToInt32(current["EntityId"]);
            record.SearchParameters = current["SearchParameters"].ToString();

            if(record.JSONFormat != null)
                record.JSONFormat = current["JSONFormat"].ToString();

            return record;
        }

        #endregion

        public static readonly Info_TrackingDALC Instance = new Info_TrackingDALC();

    }
}
