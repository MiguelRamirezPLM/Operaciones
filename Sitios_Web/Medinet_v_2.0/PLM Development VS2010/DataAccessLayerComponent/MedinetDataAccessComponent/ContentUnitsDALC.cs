using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ContentUnitsDALC : MedinetDataAccessAdapter<ContentUnitsInfo>
    {

        #region Constructors

        private ContentUnitsDALC() { }

        #endregion

        #region Public Methods

        public override List<ContentUnitsInfo> getAll()
        {
            List<ContentUnitsInfo> BECollection = new List<ContentUnitsInfo>();

            DbCommand dbCmd = ContentUnitsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetContentUnits");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ContentUnitsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override ContentUnitsInfo getFromDataReader(IDataReader current)
        {
            ContentUnitsInfo record = new ContentUnitsInfo();

            record.ContentUnitId = Convert.ToInt32(current["ContentUnitId"]);
            record.UnitName = current["UnitName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ContentUnitsDALC Instance = new ContentUnitsDALC();

    }
}
