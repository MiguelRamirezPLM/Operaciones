using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class AlphabetDALC : MedinetDataAccessAdapter<AlphabetInfo>
    {
        #region Constructors

        private AlphabetDALC() { }

        #endregion

        #region Public methods

        public AlphabetInfo getOne(char letter)
        {
            DbCommand dbCmd = AlphabetDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAlphabetByLetter");

            // Add the parameters:
            AlphabetDALC.MedInstanceDatabase.AddParameter(dbCmd, "@letter", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, letter);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AlphabetDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override List<AlphabetInfo> getAll()
        {
            List<AlphabetInfo> BECollection = new List<AlphabetInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AlphabetDALC.MedInstanceDatabase.ExecuteReader(
                AlphabetDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAlphabet")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;      
        }

        #endregion

        #region Protected methods

        protected override AlphabetInfo getFromDataReader(IDataReader current)
        {
            AlphabetInfo record = new AlphabetInfo();

            record.AlphabetId = Convert.ToByte(current["AlphabetId"]);

            if (current["Letter"] != DBNull.Value)
                record.Letter = Convert.ToChar(current["Letter"]);

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly AlphabetDALC Instance = new AlphabetDALC();
    }
}
