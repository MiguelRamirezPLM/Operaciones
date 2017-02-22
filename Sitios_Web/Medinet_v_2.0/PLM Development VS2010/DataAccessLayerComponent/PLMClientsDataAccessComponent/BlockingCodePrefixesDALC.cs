using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class BlockingCodePrefixesDALC : PLMClientsDataAccessAdapter<BlockingCodePrefixInfo>
    {
        #region Constructors

        private BlockingCodePrefixesDALC() { }

        #endregion

        #region Public Methods

        public bool getByBlockCode(string prefixName, byte targetId, string blockString)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBlockingCodes");

            // Add the parameters:
            BlockingCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            BlockingCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixName);
            BlockingCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            BlockingCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@blockString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, blockString);

            BlockingCodePrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;
        }

        #endregion

        #region Protected Methods

        protected override BlockingCodePrefixInfo getFromDataReader(IDataReader current)
        {
            BlockingCodePrefixInfo record = new BlockingCodePrefixInfo();

            record.BlockingCodeId = Convert.ToInt32(current["BlockingCodeId"]);
            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);

            return base.getFromDataReader(current);
        }

        #endregion

        public static readonly BlockingCodePrefixesDALC Instance = new BlockingCodePrefixesDALC();
    }
}
