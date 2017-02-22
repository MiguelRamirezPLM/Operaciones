using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class BlockingCodesDALC : PLMClientsDataAccessAdapter<BlockingCodeInfo>
    {
        #region Constructors

        private BlockingCodesDALC() { }

        #endregion

        #region Public Methods

        public BlockingCodeInfo getByBlockString(string blockString)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBlockingCodes");

            // Add the parameters:
            BlockingCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@blockString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, blockString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BlockingCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }   
        }

        #endregion

        #region Protected Methods

        protected override BlockingCodeInfo getFromDataReader(IDataReader current)
        {
            BlockingCodeInfo record = new BlockingCodeInfo();

            record.BlockingCodeId = Convert.ToInt32(current["BlockingCodeId"]);
            record.BlockString = current["BlockString"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly BlockingCodesDALC Instance = new BlockingCodesDALC();
    }
}
