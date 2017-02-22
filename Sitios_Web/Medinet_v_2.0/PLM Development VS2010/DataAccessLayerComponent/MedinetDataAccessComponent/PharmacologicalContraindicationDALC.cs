using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
   public class PharmacologicalContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.PharmacologicalContraindicationInfo>
    {
       #region Constructors

       private PharmacologicalContraindicationDALC() { }

        #endregion
        public List<PharmacologicalContraindicationInfo> getPharmaContraindications(string search)
        {
            DbCommand dbCmd = PharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaContraindicationsCatalog");
            PharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            List<MedinetBusinessEntries.PharmacologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.PharmacologicalContraindicationInfo>();
            using (IDataReader dataReader = PharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.PharmacologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.PharmacologicalContraindicationInfo();
                    record.PharmaContraindicationId = Convert.ToInt32(dataReader["PharmaContraindicationId"]);
                    record.PharmaContraindicationName = dataReader["PharmaContraindicationName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public static readonly PharmacologicalContraindicationDALC Instance = new PharmacologicalContraindicationDALC();
    }
}
