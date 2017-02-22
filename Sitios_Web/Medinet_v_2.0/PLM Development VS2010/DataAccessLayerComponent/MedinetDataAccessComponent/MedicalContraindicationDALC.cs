using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
namespace MedinetDataAccessComponent
{
    public class MedicalContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.MedicalContraindicationInfo>
    {
        #region Constructors

        private MedicalContraindicationDALC() { }

        #endregion
        public List<MedicalContraindicationInfo> getMedicalContraindications(string search)
        {
            DbCommand dbCmd = MedicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMedicalContraindicationsCatalog");
            MedicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            List<MedinetBusinessEntries.MedicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.MedicalContraindicationInfo>();
            using (IDataReader dataReader = MedicalContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.MedicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.MedicalContraindicationInfo();
                    record.MedicalContraindicationId = Convert.ToInt32(dataReader["MedicalContraindicationId"]);
                    record.MedicalContraindicationName  = dataReader["MedicalContraindicationName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public static readonly MedicalContraindicationDALC Instance = new MedicalContraindicationDALC();
    }
}
