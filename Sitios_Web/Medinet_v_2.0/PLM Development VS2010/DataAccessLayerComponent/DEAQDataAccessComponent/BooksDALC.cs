using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace DEAQDataAccessComponent
{
    public sealed class BooksDALC : DEAQEngineDataAccessAdapter <AgroBusinessEntries.BooksInfo>
    {
        #region Constructors

        private BooksDALC() { }

        #endregion

        #region Public Methods

       
        public override BooksInfo getOne(int pk)
        {

            DbCommand dbCmd = BooksDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBooks");

            // Add the parameters:
            BooksDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            BooksDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BooksDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

            protected override BooksInfo getFromDataReader(IDataReader current)
            {
                BooksInfo record = new BooksInfo();

                record.BookId = Convert.ToInt32(current["BookId"]);
                
                if (current["BookName"] != DBNull.Value)
                    record.Description = current["BookName"].ToString();

                if(current["ShortName"] != DBNull.Value)
                    record.ShortName = current["ShortName"].ToString();

                record.Active = Convert.ToBoolean(current["Active"]);

                return record;
            }

        #endregion

        public static readonly BooksDALC Instance = new BooksDALC();
    }
}
