using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetDataAccessComponent
{
     public sealed class ActiveSubstancesDALC : VetDataAccessAdapter<ActiveSubstanceInfo>
    {
        #region Constructors

        private ActiveSubstancesDALC() { }

        #endregion

        #region Public Methods

        public List<ActiveSubstanceInfo> getSubstancesByText(int editionId, string searchText)
        {
            List<ActiveSubstanceInfo> BeCollection = new List<ActiveSubstanceInfo>();
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETActiveSubstances");

            // Add the parameters
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);


            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }


        }


        #endregion


        #region Protected methods

        protected override ActiveSubstanceInfo getFromDataReader(IDataReader current)
        {
            ActiveSubstanceInfo record = new ActiveSubstanceInfo();

            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.ActiveSubstanceName = current["ActiveSubstanceName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            return record;
        }

        #endregion



        public static readonly ActiveSubstancesDALC Instance = new ActiveSubstancesDALC();





    }
}
