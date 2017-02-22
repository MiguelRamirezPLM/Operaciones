using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_VersionsDALC : CE_SyncDatabasesDataAccessAdapter<SyncDatabaseBusinessEntries.VersionInfo>
    {
        #region Constructors

        private CE_VersionsDALC() { }

        #endregion

        #region Public Methods

        public VersionInfo getByCode(string code)
        {
            CE_VersionsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect v.VersionId, v.EditionId, v.DbId, v.PackCount, v.VersionNumber, v.LastUpdate, v.Active ");
            sb.Append("\nFrom Versions v Inner Join EditionAssignCodes e On(v.EditionId = e.EditionId) ");
            sb.Append("\nInner Join AssignCodes a On(e.AssignId = a.AssignId) ");
            sb.Append("\nWhere a.CodeString = '" + code + "' ");

            using (IDataReader dataReader = CE_VersionsDALC.SyncInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    CE_VersionsDALC.SyncInstanceDatabase.CloseSharedConnection();

                    return this.getFromDataReader(dataReader);
                }
                else
                {
                    CE_VersionsDALC.SyncInstanceDatabase.CloseSharedConnection();

                    return null;
                }
            }

        }

        public override void update(VersionInfo businessEntity)
        {
            CE_VersionsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nUdpate Versions ");
            sb.Append("\nSet PackCount = " + businessEntity.PackCount + ", ");
            sb.Append("\nVersionNumber = " + businessEntity.VersionNumber + ", ");
            sb.Append("\nLastUpdate = '" + System.DateTime.Now.ToShortDateString() + "' ");
            sb.Append("\nWhere VersionId = " + businessEntity.VersionId);

            CE_VersionsDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

        }

        #endregion

        #region Protected Methods

        protected override SyncDatabaseBusinessEntries.VersionInfo getFromDataReader(IDataReader current)
        {
            VersionInfo record = new VersionInfo();

            record.VersionId = Convert.ToInt32(current["VersionId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DbId = Convert.ToInt32(current["DbId"]);
            record.PackCount = Convert.ToInt32(current["PackCount"]);
            record.VersionNumber = current["VersionNumber"].ToString();
            record.LastUpdate = Convert.ToDateTime(current["LastUpdate"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_VersionsDALC Instance = new CE_VersionsDALC();
    }
}
