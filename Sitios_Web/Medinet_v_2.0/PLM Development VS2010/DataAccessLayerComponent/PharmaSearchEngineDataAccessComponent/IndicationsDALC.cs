using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class IndicationsDALC : PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.IndicationInfo>
    {
        #region Constructors

        private IndicationsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.IndicationInfo> getAll(byte countryId, int editionId, string description, byte typeInEdition)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEIndications");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Participante ? "P" :
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Mencionado ? "M" : "%");
            
            if(!string.IsNullOrEmpty(description))
                IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<MedinetBusinessEntries.IndicationInfo> BECollection = new List<MedinetBusinessEntries.IndicationInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        public List<MedinetBusinessEntries.IndicationInfo> getByProduct(byte countryId, int editionId, int productId, byte typeInEdition)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEIndications");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Participante ? "P" :
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Mencionado ? "M" : "%");
           
            List<MedinetBusinessEntries.IndicationInfo> BECollection = new List<MedinetBusinessEntries.IndicationInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        public override MedinetBusinessEntries.IndicationInfo getOne(int pk)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDIndications");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<MedinetBusinessEntries.IndicationInfo> getByDivision(int countryId, int editionId, int divisionId, byte typeInEdition)
        {
            DbCommand dbCmd = IndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEIndicationsByDivision");

            // Add the parameters:
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Participante ? "P" :
                (IndicationsDALC.TypeInEdition)typeInEdition == IndicationsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.IndicationInfo> BECollection = new List<MedinetBusinessEntries.IndicationInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = IndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override MedinetBusinessEntries.IndicationInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.IndicationInfo record = new MedinetBusinessEntries.IndicationInfo();

            record.IndicationId = Convert.ToInt32(current["IndicationId"]);
            record.Description = current["Description"].ToString();

            if (current["ParentId"] != System.DBNull.Value)
                record.ParentId = Convert.ToInt32(current["ParentId"]);
            else
                record.ParentId = null;            
            
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly IndicationsDALC Instance = new IndicationsDALC();
    }
}
