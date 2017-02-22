using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class UnitsDALC : MedinetDataAccessAdapter<UnitInfo>
    {
        #region Constructors

        private UnitsDALC() { }

        #endregion

        #region Public methods

        public override List<UnitInfo> getAll()
        {
            List<UnitInfo> BECollection = new List<UnitInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = UnitsDALC.MedInstanceDatabase.ExecuteReader(
                UnitsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUnits")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override UnitInfo getFromDataReader(IDataReader current)
        {
            UnitInfo record = new UnitInfo();

            record.UnitId = Convert.ToInt32(current["UnitId"]);
            record.Name = current["Name"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly UnitsDALC Instance = new UnitsDALC();
    }
}
