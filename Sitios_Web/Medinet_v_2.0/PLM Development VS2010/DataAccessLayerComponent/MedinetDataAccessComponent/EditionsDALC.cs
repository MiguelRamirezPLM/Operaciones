using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class EditionsDALC : MedinetDataAccessAdapter<EditionsInfo>
    {
        #region Constructors

        private EditionsDALC() { }

        #endregion

        #region Public methods

        public List<EditionsInfo> getAllByBook(int bookId, int countryId)
        {
            List<EditionsInfo> BECollection = new List<EditionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.MedInstanceDatabase.ExecuteReader(
                EditionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditionsByBook", bookId,countryId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;      
        }

        public List<EditionsInfo> getEncyclopediaEditionsByBook(int bookId, int countryId)
        {
            List<EditionsInfo> BECollection = new List<EditionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.MedInstanceDatabase.ExecuteReader(
                EditionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEncyclopediaEditionsByBook",countryId,bookId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public override EditionsInfo getOne(int pk)
        {
            DbCommand dbCmd = EditionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditions");

            // Add the parameters:
            EditionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EditionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }    
        }

        public EditionsInfo getByISBN(string isbn)
        {
            DbCommand dbCmd = EditionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditions");

            // Add the parameters:
            EditionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }    
        }

        #endregion

        #region Protected methods

        protected override EditionsInfo getFromDataReader(IDataReader current)
        {
            EditionsInfo record = new EditionsInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.CountryId = Convert.ToByte(current["CountryId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.BookId = Convert.ToInt32(current["BookId"]);
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);

            if (current["ISBN"] != DBNull.Value)
                record.ISBN = current["ISBN"].ToString();

            if (current["BarCode"] != DBNull.Value)
                record.BarCode = current["BarCode"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly EditionsDALC Instance = new EditionsDALC();
    }
}
