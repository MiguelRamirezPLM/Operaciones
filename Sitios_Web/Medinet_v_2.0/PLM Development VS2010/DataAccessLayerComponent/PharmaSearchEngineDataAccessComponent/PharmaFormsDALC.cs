using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;


namespace PharmaSearchEngineDataAccessComponent
{
    public class PharmaFormsDALC:PharmaSearchEngineDataAccessAdapter<MedinetBusinessEntries.PharmaceuticalFormInfo>
    {
        #region Constructors

        private PharmaFormsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getAll(int countryId, int editionId, string description, byte typeInEdition)
        {
            DbCommand dbCmd = PharmaFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEPharmaForms");

            // Add the parameters:
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (PharmaFormsDALC.TypeInEdition)typeInEdition == PharmaFormsDALC.TypeInEdition.Participante ? "P" :
                (PharmaFormsDALC.TypeInEdition)typeInEdition == PharmaFormsDALC.TypeInEdition.Mencionado ? "M" : "%");

            if(!string.IsNullOrEmpty(description))
                PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.AnsiString,
                   ParameterDirection.Input, string.Empty, DataRowVersion.Current, description);

            List<MedinetBusinessEntries.PharmaceuticalFormInfo> BECollection = new List<MedinetBusinessEntries.PharmaceuticalFormInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PharmaFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;

        }

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getByProduct(int countryId, int editionId, int productId, byte typeInEdition)
        {
            DbCommand dbCmd = PharmaFormsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEPharmaForms");

            // Add the parameters:
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PharmaFormsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@typeInEdition", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current,
                (PharmaFormsDALC.TypeInEdition)typeInEdition == PharmaFormsDALC.TypeInEdition.Participante ? "P" :
                (PharmaFormsDALC.TypeInEdition)typeInEdition == PharmaFormsDALC.TypeInEdition.Mencionado ? "M" : "%");

            List<MedinetBusinessEntries.PharmaceuticalFormInfo> BECollection = new List<MedinetBusinessEntries.PharmaceuticalFormInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PharmaFormsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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

        protected override MedinetBusinessEntries.PharmaceuticalFormInfo getFromDataReader(IDataReader current)
        {
            MedinetBusinessEntries.PharmaceuticalFormInfo record = new MedinetBusinessEntries.PharmaceuticalFormInfo();

            record.PharmaFormId = Convert.ToInt32(current["pharmaFormId"]);
            record.Description = current["pharmaForm"].ToString();
            record.Active = Convert.ToBoolean(current["pharmaActive"]);

            return record;
        }

        #endregion

        public static readonly PharmaFormsDALC Instance = new PharmaFormsDALC();
    }
}
