using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class BinnaclesStatusNameDALC : PLMUsersDataAccesAdapter<BinnaclesStatusNameInfo>
    {
        #region Constructors

        private BinnaclesStatusNameDALC() { }

        #endregion

        #region Public Methods

        public List<BinnaclesStatusNameInfo> getBinnaclesByUser(int userId)
        {
            DbCommand dbCmd = BinnaclesStatusNameDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBinnaclesNameByUser");

            BinnaclesStatusNameDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            List<BinnaclesStatusNameInfo> BECollection = new List<BinnaclesStatusNameInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BinnaclesStatusNameDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

         #endregion

        #region Protected Methods

        protected override BinnaclesStatusNameInfo getFromDataReader(IDataReader current)
        {
            BinnaclesStatusNameInfo record = new BinnaclesStatusNameInfo();

            record.BinnacleId = Convert.ToInt32(current["BinnacleId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);
            record.StatusId = Convert.ToByte(current["StatusId"]);
            record.BinnacleDescription = current["BinnacleDescription"].ToString();
            record.Comment = current["Comment"].ToString();
            record.InitialDateD = Convert.ToDateTime(current["InitialDateD"]);
            record.FinalDateD = Convert.ToDateTime(current["FinalDateD"]);
            record.StatusName = current["StatusName"].ToString();

            return record;
        }

        #endregion

        public static readonly BinnaclesStatusNameDALC Instance = new BinnaclesStatusNameDALC();
    }
}