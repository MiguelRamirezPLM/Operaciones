using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class InternalPacksDALC : MedinetDataAccessAdapter<InternalPacksInfo>
    {

        #region Constructors

        private InternalPacksDALC() { }

        #endregion

        #region Public Methods

        public override List<InternalPacksInfo> getAll()
        {
            List<InternalPacksInfo> BECollection = new List<InternalPacksInfo>();

            DbCommand dbCmd = InternalPacksDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInternalPacks");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = InternalPacksDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override InternalPacksInfo getFromDataReader(IDataReader current)
        {
            InternalPacksInfo record = new InternalPacksInfo();

            record.InternalPackId = Convert.ToInt32(current["InternalPackId"]);
            record.InternalPackName = current["InternalPackName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly InternalPacksDALC Instance = new InternalPacksDALC();

    }
}
