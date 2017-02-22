using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class EditionDivisionsDALC : MedinetDataAccessAdapter<EditionDivisionInfo>
    {
        #region Constructors

        private EditionDivisionsDALC() { }

        #endregion

        #region Public Methods

        public List<EditionDivisionInfo> getByEdition(int editionId)
        {
            DbCommand dbCmd = EditionDivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spEditionDivisions");

            EditionDivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            List<EditionDivisionInfo> BECollection = new List<EditionDivisionInfo>();

            using (IDataReader dataReader = EditionDivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<EditionDivisionInfo> getByDivision(int divisionId)
        {
            DbCommand dbCmd = EditionDivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spEditionDivisions");

            EditionDivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            List<EditionDivisionInfo> BECollection = new List<EditionDivisionInfo>();

            using (IDataReader dataReader = EditionDivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override EditionDivisionInfo getFromDataReader(IDataReader current)
        {
            EditionDivisionInfo record = new EditionDivisionInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);

            return record;

        }

        #endregion

        public static readonly EditionDivisionsDALC Instance = new EditionDivisionsDALC();

    }
}
