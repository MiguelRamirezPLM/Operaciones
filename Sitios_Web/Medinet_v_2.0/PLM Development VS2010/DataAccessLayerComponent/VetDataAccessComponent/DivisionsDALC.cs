using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetDataAccessComponent
{
    public sealed class DivisionsDALC: VetDataAccessAdapter<DivisionInfo>
    {
       #region Constructors

        private DivisionsDALC() { }

        #endregion

        #region Public Methods

        public List<DivisionInfo> getDivisionsByText(int editionId, string searchText)
        {
            List<DivisionInfo> BeCollection = new List<DivisionInfo>();
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETDivision");

            // Add the parameters
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);


            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }


        }





        #endregion

        #region Protected Methods

        protected override DivisionInfo getFromDataReader(IDataReader current)
        {
            DivisionInfo record = new DivisionInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
           
            record.CountryId = Convert.ToInt32(current["CountryId"]);
            

            record.ShortName = current["ShortName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.DivisionName = current["DivisionName"].ToString();

            return record;
        }



        #endregion

        public static readonly DivisionsDALC Instance = new DivisionsDALC();

    }
}
