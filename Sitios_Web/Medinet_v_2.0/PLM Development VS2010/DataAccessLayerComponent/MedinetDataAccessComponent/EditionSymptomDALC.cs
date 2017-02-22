using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class EditionSymptomDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.EditionSymptomInfo>
    {
         #region Constructors

        private EditionSymptomDALC() { }

        #endregion
        public override int insert(EditionSymptomInfo businessEntity)
        {
            DbCommand dbCmd = EditionSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionSymptom");

            //Add the parameters:
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SymptomId);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);

            EditionSymptomDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return 0;
        }
        public override void delete(EditionSymptomInfo businessEntity)
        {
            DbCommand dbCmd = EditionSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionSymptom");

            //Add the parameters:
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SymptomId);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);

            EditionSymptomDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public override void update(EditionSymptomInfo businessEntity)
        {
            DbCommand dbCmd = EditionSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionSymptom");

            //Add the parameters:
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SymptomId);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@page", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Page);

            ProductSymptomDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }
        public List<EditionSymptomInfo> getEditionSymptoms(int editionId,string search) {
            DbCommand dbCmd = EditionSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionSymptom");

            //Add the parameters:
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);

            List<MedinetBusinessEntries.EditionSymptomInfo> BECollection = new List<MedinetBusinessEntries.EditionSymptomInfo>();
            using (IDataReader dataReader = EditionSymptomDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.EditionSymptomInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.EditionSymptomInfo();

                    record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);
                    record.SymptomName = dataReader["SymptomName"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["editionId"]);
                    record.Page= dataReader["page"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public static readonly EditionSymptomDALC Instance = new EditionSymptomDALC();
    }
}
