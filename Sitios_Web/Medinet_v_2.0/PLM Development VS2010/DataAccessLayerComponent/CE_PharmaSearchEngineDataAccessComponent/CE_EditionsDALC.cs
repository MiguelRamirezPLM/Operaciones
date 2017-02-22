using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CE_PharmaSearchEngineDataAccessComponent
{
    public class CE_EditionsDALC : CE_PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.EditionsInfo>
    {
        #region Constructors

        private CE_EditionsDALC() { }

        #endregion

        public override MedinetBusinessEntries.EditionsInfo getOne(int pk)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT EditionId,CountryId,ParentId,BookId,NumberEdition,ISBN,Barcode,Active");
            sb.Append(" FROM Editions");
            sb.Append(" WHERE EditionId =  " + pk);

            MedinetBusinessEntries.EditionsInfo record = null;

            CE_EditionsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_EditionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if(dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_EditionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;
        }

        public override List<MedinetBusinessEntries.EditionsInfo> getAll()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n SELECT EditionId, CountryId, ParentId, BookId, NumberEdition, ISBN, BarCode, Active");
            sb.Append("\n FROM Editions");

            List<MedinetBusinessEntries.EditionsInfo> BECollection = new List<MedinetBusinessEntries.EditionsInfo>();

            CE_EditionsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_EditionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            CE_EditionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public MedinetBusinessEntries.EditionsInfo getByISBN(string isbn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT EditionId,CountryId,ParentId,BookId,NumberEdition,ISBN,Barcode,Active");
            sb.Append(" FROM Editions");
            sb.Append(" WHERE ISBN =  '" + isbn + "'");

            MedinetBusinessEntries.EditionsInfo record = null;

            CE_EditionsDALC.PharmaInstanceDatabase.CreateConnection();

            using (IDataReader dataReader = CE_EditionsDALC.PharmaInstanceDatabase.ExecuteReader(System.Data.CommandType.Text, sb.ToString()))
            {
                if (dataReader.Read())
                    record = this.getFromDataReader(dataReader);
            }

            CE_EditionsDALC.PharmaInstanceDatabase.CloseSharedConnection();

            return record;   
        }

        #region Protected methods

        protected override MedinetBusinessEntries.EditionsInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.EditionsInfo record = new MedinetBusinessEntries.EditionsInfo();
            
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.CountryId = Convert.ToByte(current["CountryId"]);

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);

            record.BookId = Convert.ToInt32(current["BookId"]);
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            record.ISBN = current["ISBN"].ToString();

            if (current["BarCode"] != System.DBNull.Value)
                record.BarCode = current["BarCode"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_EditionsDALC Instance = new CE_EditionsDALC();

    }
}
