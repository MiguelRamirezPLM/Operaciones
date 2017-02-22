using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ExternalPacksDALC : MedinetDataAccessAdapter<ExternalPacksInfo>
    {

        #region Constructors

        private ExternalPacksDALC() { }

        #endregion

        #region Public Methods

        public override List<ExternalPacksInfo> getAll()
        {
            List<ExternalPacksInfo> BECollection = new List<ExternalPacksInfo>();

            DbCommand dbCmd = ExternalPacksDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetExternalPacks");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ExternalPacksDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override ExternalPacksInfo getFromDataReader(IDataReader current)
        {
            ExternalPacksInfo record = new ExternalPacksInfo();

            record.ExternalPackId = Convert.ToInt32(current["ExternalPackId"]);
            record.ExternalPackName = current["ExternalPackName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ExternalPacksDALC Instance = new ExternalPacksDALC();

    }
}
