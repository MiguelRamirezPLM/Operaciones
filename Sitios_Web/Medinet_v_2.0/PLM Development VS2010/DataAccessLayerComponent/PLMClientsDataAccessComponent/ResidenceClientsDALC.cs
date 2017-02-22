using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ResidenceClientsDALC : PLMClientsDataAccessAdapter<ResidenceClientsInfo>
    {

        #region Constructors

        private ResidenceClientsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.ResidenceClientsInfo BEntity)
        {
            DbCommand dbCmd = ResidenceClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDResidenceClients");

            // Add the parameters:
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.SpecialityId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@residenceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ResidenceId);
            
            //Insert record:
            ResidenceClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(int clientId)
        {
            DbCommand dbCmd = ResidenceClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDResidenceClients");

            // Add the parameters:
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            //Delete record:
            ResidenceClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(ResidenceClientsInfo BEntity)
        {
            DbCommand dbCmd = ResidenceClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDResidenceClients");

            // Add the parameters:
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.SpecialityId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@residenceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ResidenceId);

            //Delete record:
            ResidenceClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override ResidenceClientsInfo getOne(ResidenceClientsInfo BEntity)
        {
            DbCommand dbCmd = ResidenceClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDResidenceClients");

            // Add the parameters:
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.SpecialityId);
            ResidenceClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@residenceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ResidenceId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResidenceClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ResidenceClientsInfo getFromDataReader(IDataReader current)
        {
            ResidenceClientsInfo record = new ResidenceClientsInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.SpecialityId = Convert.ToInt16(current["SpecialityId"]);
            record.ResidenceId = Convert.ToByte(current["ResidenceId"]);

            return record;
        }

        #endregion

        public static readonly ResidenceClientsDALC Instance = new ResidenceClientsDALC();

    }
}
