using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class BooksDALC : MedinetDataAccessAdapter<BooksInfo>
    {
        #region Constructors

        private BooksDALC() { }

        #endregion

        #region Public Methods

        public List<BooksInfo> getAllByCountry(int countryId)
        {
            List<BooksInfo> BECollection = new List<BooksInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = BooksDALC.MedInstanceDatabase.ExecuteReader(
                BooksDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBooksByCountry", countryId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;      
            
        }
        public List<BooksInfo> getEncyclopediaBooksByCountry(int countryId)
        {
            List<BooksInfo> BECollection = new List<BooksInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = BooksDALC.MedInstanceDatabase.ExecuteReader(
                BooksDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaBooksByCountry", countryId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public override BooksInfo getOne(int pk)
        {

            DbCommand dbCmd = BooksDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBooks");

            // Add the parameters:
            BooksDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            BooksDALC.MedInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BooksDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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
                
                if (current["Description"] != DBNull.Value)
                    record.Description = current["Description"].ToString();

                if(current["ShortName"] != DBNull.Value)
                    record.ShortName = current["ShortName"].ToString();

                record.Active = Convert.ToBoolean(current["Active"]);

                return record;
            }

        #endregion

        public static readonly BooksDALC Instance = new BooksDALC();
    }
}
