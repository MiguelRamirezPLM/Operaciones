using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using VetBusinessEntries;

namespace VetDataAccessComponent
{
    public sealed class SpeciesDALC: VetDataAccessAdapter<SpecieInfo>
    {
        #region Constructors

        private SpeciesDALC() { }

        #endregion

        #region Public Methods

        public List<SpecieInfo> getSpeciesByText(int editionId, string searchText)
        {
            List<SpecieInfo> BeCollection = new List<SpecieInfo>();
            DbCommand dbCmd = SpeciesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETSpecies");

            // Add the parameters
            SpeciesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            SpeciesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);


            using (IDataReader dataReader = SpeciesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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

        protected override SpecieInfo getFromDataReader(IDataReader current)
        {

            SpecieInfo record = new SpecieInfo();

            record.SpecieId = Convert.ToInt32(current["SpecieId"]);
            record.SpecieName = current["SpecieName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;


        }


        #endregion

        public static readonly SpeciesDALC Instance = new SpeciesDALC();




    }
}
