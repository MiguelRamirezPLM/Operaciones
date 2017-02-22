using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class SpecialityClientsDALC : PLMClientsDataAccessAdapter<SpecialityClientInfo>
    {
        #region Constructors

        private SpecialityClientsDALC() { }

        #endregion
        
        #region Public Methods

        public SpecialityClientInfo getByClient(int clientId)
        {
            DbCommand dbCmd = SpecialityClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialityClients");

            // Add the parameters:
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SpecialityClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }

        }

        public override int insert(SpecialityClientInfo businessEntity)
        {
            DbCommand dbCmd = SpecialityClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialityClients");

            // Add the parameters:
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SpecialityId);

            if (businessEntity.OtherSpeciality != null)
                SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@otherSpeciality", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OtherSpeciality);

            //Insert record:
            SpecialityClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public override void delete(SpecialityClientInfo businessEntity)
        {
            DbCommand dbCmd = SpecialityClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialityClients");

            // Add the parameters:
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SpecialityId);

            //Delete record:
            SpecialityClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        
        }

        public override void update(SpecialityClientInfo businessEntity)
        {
            DbCommand dbCmd = SpecialityClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialityClients");

            // Add the parameters:
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SpecialityId);

            if (!string.IsNullOrEmpty(businessEntity.OtherSpeciality))
                SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@otherSpeciality", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OtherSpeciality);

            //Update record:
            SpecialityClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        
        }

        public override void delete(int clientId)
        {
            DbCommand dbCmd = SpecialityClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSpecialityClients");

            // Add the parameters:
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            SpecialityClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            //Delete record:
            SpecialityClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override SpecialityClientInfo getFromDataReader(IDataReader current)
        {
            SpecialityClientInfo record = new SpecialityClientInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.SpecialityId = Convert.ToInt16(current["SpecialityId"]);

            if (current["OtherSpeciality"] != System.DBNull.Value)
                record.OtherSpeciality = current["OtherSpeciality"].ToString();

            return record;

        }

        #endregion

        public static readonly SpecialityClientsDALC Instance = new SpecialityClientsDALC();

    }
}
