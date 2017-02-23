using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PSELogsBusinessEntities;

namespace PSELogsDataAccessComponent
{
    public class PSE_TrackingAttributesDALC : PSELogsDataAccessAdapter<PSE_TrackingAttributeInfo>
    {
        #region Constructors

        private PSE_TrackingAttributesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PSE_TrackingAttributeInfo businessEntity)
        {

            DbCommand dbCmd = PSE_TrackingAttributesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPSE_TrackingAttributes");
            //Add the parameters: 
            PSE_TrackingAttributesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PSE_TrackingAttributesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PSE_TrackingAttributesDALC.InstanceDatabase.AddParameter(dbCmd, "@trackId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TrackId);
            PSE_TrackingAttributesDALC.InstanceDatabase.AddParameter(dbCmd, "@attributeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttributeId);

            PSE_TrackingAttributesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

        }

        #endregion

        #region Protected Methods

        protected override PSE_TrackingAttributeInfo getFromDataReader(IDataReader current)
        {
            PSE_TrackingAttributeInfo record = new PSE_TrackingAttributeInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);
            record.AttributeId = Convert.ToInt32(current["AttributeId"]);

            return record;

        }

        #endregion

        public static readonly PSE_TrackingAttributesDALC Instance = new PSE_TrackingAttributesDALC();

    }
}
