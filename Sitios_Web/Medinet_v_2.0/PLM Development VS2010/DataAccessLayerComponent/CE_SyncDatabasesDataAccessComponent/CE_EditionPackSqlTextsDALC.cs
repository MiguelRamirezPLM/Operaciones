using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_EditionPackSqlTextsDALC:CE_SyncDatabasesDataAccessAdapter<EditionPackSQLTextInfo>
    {
        #region Constructors

        private CE_EditionPackSqlTextsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionPackSQLTextInfo businessEntity)
        {
            CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nINSERT INTO [EditionPackSQLTexts] ([PackSqlTextId],[PackUpdId],[AddedDate],[SqlText],[PackOrder],[Active])");
            sb.Append("\nVALUES(" + businessEntity.PackSqlTextId + "," + businessEntity.PackUpdId);
            sb.Append(",'" + businessEntity.AddedDate.ToShortDateString() + "','" + businessEntity.SqlText.Replace("'","''") + "'," + businessEntity.PackOrder + ",1)");

            CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.CloseSharedConnection();

            return businessEntity.PackSqlTextId;
        }
              
        public bool checkPack(int packSqlTextId)
        {
            CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect * From EditionPackSQLTexts Where PackSqlTextId = " + packSqlTextId);

            using (IDataReader dataReader = CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                {
                    CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.CloseSharedConnection();

                    return true;
                }
                else
                {
                    CE_EditionPackSqlTextsDALC.SyncInstanceDatabase.CloseSharedConnection();

                    return false;
                }
            }
            
            
        }

        #endregion

        #region Protected Methods

        protected override EditionPackSQLTextInfo getFromDataReader(IDataReader current)
        {
            EditionPackSQLTextInfo record = new EditionPackSQLTextInfo();

            record.PackSqlTextId = Convert.ToInt32(current["PackSqlTextId"]);
            record.PackUpdId = Convert.ToInt32(current["PackUpdId"]);
            record.SqlText = current["SqlText"].ToString();
            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.PackOrder = Convert.ToInt32(current["PackOrder"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_EditionPackSqlTextsDALC Instance = new CE_EditionPackSqlTextsDALC();

    }
}
