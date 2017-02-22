using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientCodesDALC : PLMClientsDataAccessAdapter<ClientCodesInfo>
    {

        #region Constructors

        private ClientCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientCodes");

            // Add the parameters:
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            if (businessEntity.PrescriptionFolio != null)
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionFolio", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrescriptionFolio);

            ClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientCodes");

            // Add the parameters:
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            //Delete record:
            ClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientCodes");

            // Add the parameters:
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            if (businessEntity.PrescriptionFolio != null)
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prescriptionFolio", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrescriptionFolio);
            
            //Update record:
            ClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override ClientCodesInfo getOne(ClientCodesInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientCodes");

            // Add the parameters:
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ClientCodesInfo getFromDataReader(IDataReader current)
        {
            ClientCodesInfo record = new ClientCodesInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);

            if (current["PrescriptionFolio"] != System.DBNull.Value)
                record.PrescriptionFolio = Convert.ToInt32(current["PrescriptionFolio"]);

            return record;
        }

        #endregion

        public static readonly ClientCodesDALC Instance = new ClientCodesDALC();

    }
}
