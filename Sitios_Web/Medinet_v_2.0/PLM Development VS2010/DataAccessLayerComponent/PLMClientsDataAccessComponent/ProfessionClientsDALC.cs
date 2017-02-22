using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ProfessionClientsDALC : PLMClientsDataAccessAdapter<ProfessionClientInfo>
    {
        #region Constructors

        private ProfessionClientsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ProfessionClientInfo businessEntity)
        {
            DbCommand dbCmd = ProfessionClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProfessionClients");

            // Add the parameters:
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProfessionId);

            if (businessEntity.OtherProfession != null)
                ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@otherProfession", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OtherProfession);

            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionalLicense", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProfessionalLicense);


            //Insert record:
            ProfessionClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public override void delete(ProfessionClientInfo businessEntity)
        {
            DbCommand dbCmd = ProfessionClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProfessionClients");

            // Add the parameters:
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProfessionId);

            //Delete record:
            ProfessionClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public override void update(ProfessionClientInfo businessEntity)
        {
            DbCommand dbCmd = ProfessionClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProfessionClients");

            // Add the parameters:
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProfessionId);

            if (!string.IsNullOrEmpty(businessEntity.OtherProfession))
                ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@otheProfession", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OtherProfession);

            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@professionalLicense", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProfessionalLicense);

            //Update record:
            ProfessionClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public ProfessionClientInfo getByClient(int clientId)
        {
            DbCommand dbCmd = ProfessionClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProfessionClients");

            // Add the parameters:
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProfessionClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProfessionClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ProfessionClientInfo getFromDataReader(IDataReader current)
        {
            ProfessionClientInfo record = new ProfessionClientInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.ProfessionId = Convert.ToInt16(current["ProfessionId"]);

            if (current["OtherProfession"] != System.DBNull.Value)
                record.OtherProfession = current["OtherProfession"].ToString();

            record.ProfessionalLicense = current["ProfessionalLicense"].ToString();

            return record;

        }

        #endregion

        public static readonly ProfessionClientsDALC Instance = new ProfessionClientsDALC();

    }
}
