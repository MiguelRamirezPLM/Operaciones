using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PSELogsBusinessEntities;

namespace PSELogsDataAccessComponent
{
    public class PSE_TrackingAttributeGroupsDALC : PSELogsDataAccessAdapter<PSE_TrackingAttributeGroupsInfo>
    {

        #region Constructors

        private PSE_TrackingAttributeGroupsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PSE_TrackingAttributeGroupsInfo businessEntity)
        {
            DbCommand dbCmd = PSE_TrackingAttributeGroupsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPSE_TrackingAttributeGroups");
            
            //Add the parameters: 
            PSE_TrackingAttributeGroupsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PSE_TrackingAttributeGroupsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PSE_TrackingAttributeGroupsDALC.InstanceDatabase.AddParameter(dbCmd, "@trackId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TrackId);
            PSE_TrackingAttributeGroupsDALC.InstanceDatabase.AddParameter(dbCmd, "@attributeGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttributeGroupId);

            PSE_TrackingAttributeGroupsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        #region Protected Methods

        protected override PSE_TrackingAttributeGroupsInfo getFromDataReader(IDataReader current)
        {
            PSE_TrackingAttributeGroupsInfo record = new PSE_TrackingAttributeGroupsInfo();

            record.TrackId = Convert.ToInt32(current["TrackId"]);
            record.AttributeGroupId = Convert.ToInt32(current["AttributeGroupId"]);

            return record;
        }

        #endregion

        public static readonly PSE_TrackingAttributeGroupsDALC Instance = new PSE_TrackingAttributeGroupsDALC();

    }
}
