using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class WeightUnitsDALC : MedinetDataAccessAdapter<WeightUnitsInfo>
    {

        #region Constructors

        private WeightUnitsDALC() { }

        #endregion

        #region Public Methods

        public override List<WeightUnitsInfo> getAll()
        {
            List<WeightUnitsInfo> BECollection = new List<WeightUnitsInfo>();

            DbCommand dbCmd = WeightUnitsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetWeightUnits");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = WeightUnitsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override WeightUnitsInfo getFromDataReader(IDataReader current)
        {
            WeightUnitsInfo record = new WeightUnitsInfo();

            record.WeightUnitId = Convert.ToInt32(current["WeightUnitId"]);
            record.UnitName = current["UnitName"].ToString();
            record.ShortName = current["ShortName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly WeightUnitsDALC Instance = new WeightUnitsDALC();

    }
}
