using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMLogsBusinessEntries;

namespace PLMLogsDataAccessComponent
{
    public class TrackingDALC : PLMLogsDataAccessAdapter<TrackingInfo>
    {

        #region Constructors

        private TrackingDALC() { }

        #endregion

        #region Public Methods

        public override int insert(TrackingInfo businessEntity)
        {
            return this.insert(businessEntity, string.Empty);
        }

        public int insert(TrackingInfo businessEntity, string attributeGroupKey)
        {
            DbCommand dbCmd = this.prepareDBCommand(businessEntity, attributeGroupKey, null);

            return this.executeInsert(dbCmd, businessEntity);
        }

        public int insert(TrackingInfo businessEntity, int parentId)
        {
            DbCommand dbCmd = this.prepareDBCommand(businessEntity, string.Empty, parentId);

            return this.executeInsert(dbCmd, businessEntity);
        }

        #endregion

        #region Private Methods

        private DbCommand prepareDBCommand(TrackingInfo businessEntity, string attributeGroupKey, int? parentId)
        {
            DbCommand dbCmd = TrackingDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTracking");

            //Add the parameters: 
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@JsonFormat", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.JsonFormat);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);

            if (businessEntity.SearchDate != null)
                TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchDate);

            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@sourceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SourceId);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchTypeId);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@entityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntityId);
            TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@searchParameters", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SearchParameters);

            if (businessEntity.ClientTrackId != null)
                TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@clientTrackId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientTrackId);

            if (!string.IsNullOrEmpty(attributeGroupKey))
                TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@attributeGroupKey", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, attributeGroupKey);

            if (parentId != null)
                TrackingDALC.InstanceDatabase.AddParameter(dbCmd, "@parentId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentId);

            return dbCmd;
        }

        private int executeInsert(DbCommand dbCmd, TrackingInfo businessEntity)
        {
            //Insert Record:
            TrackingDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.TrackId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.TrackId;
        }

        #endregion

        public static readonly TrackingDALC Instance = new TrackingDALC();

    }
}
