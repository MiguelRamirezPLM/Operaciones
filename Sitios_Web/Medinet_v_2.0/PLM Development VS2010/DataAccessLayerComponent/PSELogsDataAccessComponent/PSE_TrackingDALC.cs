using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PSELogsBusinessEntities;

namespace PSELogsDataAccessComponent
{
    public class PSE_TrackingDALC : PSELogsDataAccessAdapter<PSE_TrackingInfo>
    {
        #region Constructors

        private PSE_TrackingDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PSE_TrackingInfo businessEntity)
        {
            return this.insert(businessEntity, string.Empty);
        }

        public int insert(PSE_TrackingInfo businessEntity, string attributes)
        {
            DbCommand dbCmd = this.prepareDBCommand(businessEntity, attributes, null);

            return this.executeInsert(dbCmd, businessEntity);
        }

        public int insert(PSE_TrackingInfo businessEntity, int parentId)
        {
            DbCommand dbCmd = this.prepareDBCommand(businessEntity, string.Empty, parentId);

            return this.executeInsert(dbCmd, businessEntity);
        }

        #endregion

        #region Private method

        private DbCommand prepareDBCommand(PSE_TrackingInfo businessEntity, string attributes, int? parentId)
        {
            DbCommand dbCmd = PSE_TrackingDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPSE_Tracking");

            //Add the parameters: 
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@JsonFormat", DbType.AnsiString,
    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JsonFormat);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);

            if (businessEntity.SearchDate != null)
                PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchDate);

            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@sourceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SourceId);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchTypeId);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@entityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntityId);
            PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchParameters", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchParameters);

            if (businessEntity.ClientTrackId != null)
                PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@ClientTrackId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientTrackId);

            if (!string.IsNullOrEmpty(attributes))
                PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@AttributeIds", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributes);

            if (parentId != null)
                PSE_TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@ParentId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            return dbCmd;
        }

        private int executeInsert(DbCommand dbCmd, PSE_TrackingInfo businessEntity)
        {
            //Insert Record:
            PSE_TrackingDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            
            businessEntity.TrackId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.TrackId;        
        }

        #endregion

        #region Protected Methods

        protected override PSE_TrackingInfo getFromDataReader(IDataReader current)
        {
            PSE_TrackingInfo record = new PSE_TrackingInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);
            record.CodeString = current["CodeString"].ToString();

            if(record.SearchDate != null)
                record.SearchDate = Convert.ToDateTime(current["SearchDate"]);

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.SourceId = Convert.ToByte(current["SourceId"]);
            record.SearchTypeId = Convert.ToByte(current["SearchTypeId"]);
            record.EntityId = Convert.ToInt32(current["EntityId"]);
            record.SearchParameters = current["SearchParameters"].ToString();

            return record;
        }

        #endregion

        public static readonly PSE_TrackingDALC Instance = new PSE_TrackingDALC();

    }
}
