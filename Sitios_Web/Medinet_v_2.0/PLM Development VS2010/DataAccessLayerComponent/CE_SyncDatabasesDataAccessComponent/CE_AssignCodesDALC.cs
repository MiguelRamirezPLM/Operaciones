using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_AssignCodesDALC : CE_SyncDatabasesDataAccessAdapter<SyncDatabaseBusinessEntries.AssignCodeInfo>
    {
        #region Constructors

        private CE_AssignCodesDALC() { }

        #endregion

        #region Public Methods

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCode(string codeString)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n Select AssignId, CodeString, AddedDate, Active, AllowUpdates ");
            sb.Append("\n From AssignCodes ");
            sb.Append("\n WHERE CodeString = '" + codeString.Trim() + "'");

            SyncDatabaseBusinessEntries.AssignCodeInfo record = null;

            CE_AssignCodesDALC.SyncInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCodeByEdition(int editionId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT ac.AssignId, ac.CodeString, ac.AddedDate, ac.Active, ac.AllowUpdates");
            sb.Append("\nFROM AssignCodes ac");
            sb.Append("\nINNER JOIN EditionAssignCodes eac ON (ac.AssignId = eac.AssignId)");
            sb.Append("\nWHERE	ac.Active = 1 AND");
            sb.Append("\neac.EditionId = " + editionId.ToString());

            SyncDatabaseBusinessEntries.AssignCodeInfo record = null;

            CE_AssignCodesDALC.SyncInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCodeByIsbn(string isbn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT ac.AssignId, ac.CodeString, ac.AddedDate, ac.Active, ac.AllowUpdates");
            sb.Append("\n FROM AssignCodes ac");
            sb.Append("\n INNER JOIN EditionAssignCodes eac ON (ac.AssignId = eac.AssignId)");
            sb.Append("\n INNER JOIN Editions e ON (eac.EditionId = e.EditionId)");
            sb.Append("\n WHERE	ac.Active = 1 AND");
            sb.Append("\n e.ISBN = '" + isbn + "'");

            SyncDatabaseBusinessEntries.AssignCodeInfo record = null;

            CE_AssignCodesDALC.SyncInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public override int insert(SyncDatabaseBusinessEntries.AssignCodeInfo businessEntity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n INSERT INTO AssignCodes (CodeString, AddedDate, Active, AllowUpdates)");
            sb.Append("\n VALUES(");
            sb.Append("\n '" + businessEntity.CodeString + "',");
            sb.Append("\n GETDATE(),");
            sb.Append("\n " + (businessEntity.Active ? "1" : "0") + ",");
            sb.Append("\n " + (businessEntity.AllowUpdates ? "1" : "0") + ")");

            CE_AssignCodesDALC.SyncInstanceDatabase.CreateConnection();

            int pk = 0;
            CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteNonQuerySql(sb.ToString(), out pk);
            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

            businessEntity.AssignId = pk;

            return pk;
        }

        public override void update(SyncDatabaseBusinessEntries.AssignCodeInfo businessEntity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nUPDATE AssignCodes");
            sb.Append("\nSET CodeString = '" + businessEntity.CodeString + "', Active = " + (businessEntity.Active ? "1" : "0"));
            sb.Append("\nWHERE CodeString = '" + businessEntity.CodeString + "'");

            CE_AssignCodesDALC.SyncInstanceDatabase.CreateConnection();
            CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());
            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();
        }

        #endregion 

        #region Protected Methods

        protected override SyncDatabaseBusinessEntries.AssignCodeInfo getFromDataReader(IDataReader current)
        {
            SyncDatabaseBusinessEntries.AssignCodeInfo record = new SyncDatabaseBusinessEntries.AssignCodeInfo();

            record.AssignId = Convert.ToInt32(current["AssignId"]);
            record.CodeString = current["CodeString"].ToString();
            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            record.AllowUpdates = Convert.ToBoolean(current["AllowUpdates"]);

            return record;
        }

        #endregion

        public static readonly CE_AssignCodesDALC Instance = new CE_AssignCodesDALC();

    }
}
