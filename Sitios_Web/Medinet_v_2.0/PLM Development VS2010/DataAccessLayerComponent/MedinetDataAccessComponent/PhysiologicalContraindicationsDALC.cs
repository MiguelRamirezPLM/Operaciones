using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class PhysiologicalContraindicationsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.PhysiologicalContraindicationInfo>
    {
        #region Constructors

        private PhysiologicalContraindicationsDALC() { }

        #endregion
        public List<PhysiologicalContraindicationInfo> getPhysContraindications(string search)
        {
            DbCommand dbCmd = PhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPhysContraindicationsCatalog");
            PhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            List<MedinetBusinessEntries.PhysiologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.PhysiologicalContraindicationInfo>();
            using (IDataReader dataReader = PhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.PhysiologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.PhysiologicalContraindicationInfo();
                    record.PhysContraindicationId = Convert.ToInt32(dataReader["PhysContraindicationId"]);
                    record.PhysContraindicationName = dataReader["PhysContraindicationName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public static readonly PhysiologicalContraindicationsDALC Instance = new PhysiologicalContraindicationsDALC();
    }
}
