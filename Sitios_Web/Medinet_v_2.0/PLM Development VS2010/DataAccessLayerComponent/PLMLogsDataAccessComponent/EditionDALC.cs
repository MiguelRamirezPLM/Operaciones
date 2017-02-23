using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMLogsBusinessEntries;

namespace PLMLogsDataAccessComponent
{
    public class EditionDALC : PLMLogsDataAccessAdapter<EditionInfo>
    {

        #region Constructors

        private EditionDALC() { }

        #endregion

        #region Public Methods

        public EditionInfo getByISBN(string isbn) 
        {
            DbCommand dbCmd = EditionDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditions");

            // Add the parameters:
            EditionDALC.InstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override EditionInfo getFromDataReader(IDataReader current)
        {
            EditionInfo record = new EditionInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);

            if (current["EditionTypeId"] != System.DBNull.Value)
                record.EditionTypeId = Convert.ToByte(current["EditionTypeId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.CountryId = Convert.ToInt32(current["CountryId"]);
            record.BookId = Convert.ToInt32(current["BookId"]);
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            record.ISBN = current["ISBN"].ToString();
            record.BarCode = current["BarCode"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly EditionDALC Instance = new EditionDALC();

    }
}
