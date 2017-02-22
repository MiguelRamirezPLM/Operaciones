using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_EditionsDALC : CE_SyncDatabasesDataAccessAdapter<EditionInfo>
    {
         #region Constructors

        private CE_EditionsDALC() { }

        #endregion

        #region Public Methods

        public EditionInfo getByISBN(string isbn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT EditionId, CountryId, ParentId, BookId, NumberEdition, ISBN, BarCode, Active");
            sb.Append("\n FROM Editions");
            sb.Append("\n Where ISBN = '" + isbn + "'");

            CE_EditionsDALC.SyncInstanceDatabase.CreateConnection();
   
            using (IDataReader dataReader = CE_EditionsDALC.SyncInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                EditionInfo record = null;

                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);


                CE_EditionsDALC.SyncInstanceDatabase.CloseSharedConnection();

                return record;
            }
        }

        public EditionInfo getByAssignCode(string isbn, string codeString)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\nSELECT e.EditionId, e.ParentId, e.BookId, e.CountryId, e.NumberEdition, e.ISBN, e.BarCode, e.Active");
            sb.Append("\nFROM AssignCodes ac");
            sb.Append("\nINNER JOIN EditionAssignCodes eac ON (ac.AssignId = eac.AssignId)");
            sb.Append("\nINNER JOIN Editions e ON (eac.EditionId = e.EditionId)");
            sb.Append("\nWHERE e.ISBN = '" + isbn + "' AND");
            sb.Append("\nac.CodeString = '" + codeString + "'");

            CE_EditionsDALC.SyncInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_EditionsDALC.SyncInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                EditionInfo record = null;

                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);

                CE_EditionsDALC.SyncInstanceDatabase.CloseSharedConnection();

                return record;
            }    
        }

        #endregion

        #region Protected Methods

        protected override SyncDatabaseBusinessEntries.EditionInfo getFromDataReader(IDataReader current)
        {

            SyncDatabaseBusinessEntries.EditionInfo record = new SyncDatabaseBusinessEntries.EditionInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            int countryId = Convert.ToInt32(current["CountryId"]);
            record.CountryId = (Byte)countryId;
            if (current["ParentId"] != System.DBNull.Value)
                { record.ParentId = Convert.ToInt32(current["ParentId"]); }
            record.BookId = Convert.ToInt32(current["BookId"]);
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            record.ISBN = current["ISBN"].ToString();
            if (current["BarCode"] != System.DBNull.Value)
                { record.BarCode = current["BarCode"].ToString(); }
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_EditionsDALC Instance = new CE_EditionsDALC();
    }
}
