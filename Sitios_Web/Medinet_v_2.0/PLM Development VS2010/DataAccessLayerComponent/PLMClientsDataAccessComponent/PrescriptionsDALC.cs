using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class PrescriptionsDALC : PLMClientsDataAccessAdapter<PrescriptionsInfo>
    {

        #region Constructors

        private PrescriptionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PrescriptionsInfo businessEntity)
        {
            DbCommand dbCmd = PrescriptionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPrescriptions");

            // Add the parameters:
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@patient", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Patient);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@folio", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Folio);

            if (businessEntity.Comments != null)
                PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@comments", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comments);

            PrescriptionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.PrescriptionId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = PrescriptionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPrescriptions");

            // Add the parameters:
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            PrescriptionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(PrescriptionsInfo businessEntity)
        {
            DbCommand dbCmd = PrescriptionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPrescriptions");

            // Add the parameters:
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrescriptionId);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@patient", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Patient);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@folio", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Folio);
            
            if (businessEntity.Comments != null)
                PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@comments", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comments);

            //Update record:
            PrescriptionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override PrescriptionsInfo getOne(int pk)
        {
            DbCommand dbCmd = PrescriptionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPrescriptions");

            // Add the parameters:
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            PrescriptionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PrescriptionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override PrescriptionsInfo getFromDataReader(IDataReader current)
        {
            PrescriptionsInfo record = new PrescriptionsInfo();

            record.PrescriptionId = Convert.ToInt32(current["PrescriptionId"]);
            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.Patient = current["Patient"].ToString();
            record.Folio = current["Folio"].ToString();
            
            if (current["Comments"] != System.DBNull.Value)
                record.Comments = current["Comments"].ToString();

            return record;
        }

        #endregion

        public static readonly PrescriptionsDALC Instance = new PrescriptionsDALC();

    }
}
