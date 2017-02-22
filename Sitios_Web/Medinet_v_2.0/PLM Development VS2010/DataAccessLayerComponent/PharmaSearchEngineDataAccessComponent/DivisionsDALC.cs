using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public class DivisionsDALC : PharmaSearchEngineDataAccessAdapter<DivisionByEditionInfo>
    {
        #region Constructors

        private DivisionsDALC() { }

        #endregion

        #region Public methods

        public List<DivisionByEditionInfo> getAll(byte countryId, int editionId, string description, byte typeInEdition)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEDivisions");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Participante ? "P" :
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Mencionado ? "M" : "%");

            if (!string.IsNullOrEmpty(description))
                DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<DivisionByEditionInfo> BECollection = new List<DivisionByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public List<DivisionByEditionInfo> getByActiveSubstance(byte countryId, int editionId, int activeSubstanceId, byte typeInEdition)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEDivisions");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Participante ? "P" :
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<DivisionByEditionInfo> BECollection = new List<DivisionByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<DivisionByEditionInfo> getByIndication(byte countryId, int editionId, int indicationId, byte typeInEdition)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEDivisions");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Participante ? "P" :
                (DivisionsDALC.TypeInEdition)typeInEdition == DivisionsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<DivisionByEditionInfo> BECollection = new List<DivisionByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;    
        }

        public int getAll(int editionId, byte countryId)
        {
            DbCommand dbCmd = DivisionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEDivisions");

            // Add the parameters:
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);

            List<DivisionByEditionInfo> BECollection = new List<DivisionByEditionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection.Count;
        }

        #endregion

        #region Protected methods

        protected override DivisionByEditionInfo getFromDataReader(IDataReader current)
        {
            DivisionByEditionInfo record = new DivisionByEditionInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.Description = current["DivisionName"].ToString();
            record.Active = Convert.ToBoolean(current["DivisionActive"]);

            if (current["DivisionShortName"] != System.DBNull.Value)
                record.ShortName = current["DivisionShortName"].ToString();
            else
                record.ShortName = null;

            
            record.CountryId = Convert.ToInt32(current["CountryId"]);
            

            return record;
        }

        #endregion

        public static readonly DivisionsDALC Instace = new DivisionsDALC();
    }
}
