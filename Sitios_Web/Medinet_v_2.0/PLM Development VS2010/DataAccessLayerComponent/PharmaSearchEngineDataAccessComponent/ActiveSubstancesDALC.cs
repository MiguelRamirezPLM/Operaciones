using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace PharmaSearchEngineDataAccessComponent
{
    public class ActiveSubstancesDALC : PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.ActiveSubstanceInfo>
    {
        #region Constructors

        private ActiveSubstancesDALC() { }

        #endregion

        #region Public methods

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getAll(int countryId, int editionId, string description, byte typeInEdition)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Participante ? "P" :
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Mencionado ? "M" : "%");

            if (!string.IsNullOrEmpty(description))
                ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByDivision(int countryId, int editionId, int divisionId, byte typeInEdition)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Participante ? "P" :
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;   
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByProduct(int countryId, int editionId, int productId, byte typeInEdition)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Participante ? "P" :
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByProductWithoutActiveSubstance(int countryId, int editionId, int productId, int activeSubstanceId, byte typeInEdition)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@exclusiveActSubs", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Participante ? "P" :
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getByTherapeutic(int countryId, int editionId, int therapeuticId, byte typeInEdition)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Participante ? "P" :
                (ActiveSubstancesDALC.TypeInEdition)typeInEdition == ActiveSubstancesDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public override MedinetBusinessEntries.ActiveSubstanceInfo getOne(int pk)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<MedinetBusinessEntries.ActiveSubstanceInfo> getInteractionSubstances(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEInteractionSubstances");

            // Add the parameters:
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            
            List<MedinetBusinessEntries.ActiveSubstanceInfo> BECollection = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override MedinetBusinessEntries.ActiveSubstanceInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo record = new MedinetBusinessEntries.ActiveSubstanceInfo();

            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.Enunciative = Convert.ToBoolean(current["Enunciative"]);

            return record;
        }

        #endregion

        public static readonly ActiveSubstancesDALC Instance = new ActiveSubstancesDALC();
    }
}
