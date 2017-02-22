using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class LaboratoriesDALC : MedinetDataAccessAdapter<LaboratoriesInfo>
    {
        #region Constructors

        private LaboratoriesDALC() { }

        #endregion

        #region Public Methods

        public override List<LaboratoriesInfo> getAll()
        {
            List<LaboratoriesInfo> BECollection = new List<LaboratoriesInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = LaboratoriesDALC.MedInstanceDatabase.ExecuteReader(
                LaboratoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLaboratories")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;    
        }

        public override LaboratoriesInfo getOne(int pk)
        {
            DbCommand dbCmd = LaboratoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLaboratories");

            // Add the parameters:
            LaboratoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            LaboratoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LaboratoriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override LaboratoriesInfo getFromDataReader(IDataReader current)
        {
            LaboratoriesInfo record = new LaboratoriesInfo();

            record.LaboratoryId = Convert.ToInt32(current["LaboratoryId"]);

            if (current["LaboratoryName"] != DBNull.Value)
                record.LaboratoryName = Convert.ToString(current["LaboratoryName"]);

            if (current["ImageName"] != DBNull.Value)
                record.ImageName = Convert.ToString(current["ImageName"]);

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly LaboratoriesDALC Instance = new LaboratoriesDALC();

    }
}
