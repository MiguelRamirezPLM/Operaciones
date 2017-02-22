using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class AddressesDALC : PLMClientsDataAccessAdapter<AddressInfo>
    {
        #region Constructors

        private AddressesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(AddressInfo businessEntity)
        {
            DbCommand dbCmd = AddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAddresses");

            // Add the parameters:
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@street", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Street);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (businessEntity.ZipCode != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);

            if (businessEntity.Location != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@location", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Location);

            if (businessEntity.InternalNumber != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@internalNumber", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InternalNumber);

            if (businessEntity.StateId != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.StateName != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateName);

            //Insert record:
            AddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);  
        }

        public override void update(AddressInfo businessEntity)
        {
            DbCommand dbCmd = AddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAddresses");

            // Add the parameters:
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddressId);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@street", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Street);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@suburb", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Suburb);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (businessEntity.ZipCode != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@zipCode", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCode);

            if (businessEntity.Location != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@location", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Location);

            if (businessEntity.InternalNumber != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@internalNumber", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InternalNumber);

            if (businessEntity.StateId != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.StateName != null)
                AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateName", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateName);

            //Update record:
            AddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAddresses");

            // Add the parameters:
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            AddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override AddressInfo getOne(int pk)
        {
            DbCommand dbCmd = AddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAddresses");

            // Add the parameters:
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AddressesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public AddressByClient getByClient(int clientId)
        {
            DbCommand dbCmd = AddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAddressByClient");

            // Add the parameters:
            AddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AddressesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                AddressByClient record = new AddressByClient();

                // Iterates through records:
                if (dataReader.Read())
                {
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.AddressId = Convert.ToInt32(dataReader["AddressId"]);
                    record.Street = dataReader["Street"].ToString();
                    record.Suburb = dataReader["Suburb"].ToString();
                    record.ZipCode = dataReader["ZipCode"].ToString();
                    record.Location = dataReader["Location"].ToString();
                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);

                    if (dataReader["InternalNumber"] != System.DBNull.Value)
                        record.InternalNumber = dataReader["InternalNumber"].ToString();

                    if (dataReader["StateId"] != System.DBNull.Value)
                        record.StateId = Convert.ToInt32(dataReader["StateId"]);

                    if (dataReader["StateName"] != System.DBNull.Value)
                        record.StateName = dataReader["StateName"].ToString();


                    return record;
                }
                else
                    return null;

            }

        }

        #endregion

        #region Protected Methods

        protected override AddressInfo getFromDataReader(IDataReader current)
        {
            AddressInfo record = new AddressInfo();

            record.AddressId = Convert.ToInt32(current["AddressId"]);
            record.Street = current["Street"].ToString();
            record.Suburb = current["Suburb"].ToString();
            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            if (current["ZipCode"] != System.DBNull.Value)
                record.ZipCode = current["ZipCode"].ToString();

            if (current["Location"] != System.DBNull.Value)
                record.Location = current["Location"].ToString();

            if (current["InternalNumber"] != System.DBNull.Value)
                record.InternalNumber = current["InternalNumber"].ToString();

            if (current["StateId"] != System.DBNull.Value)
                record.StateId = Convert.ToInt32(current["StateId"]);

            if (current["StateName"] != System.DBNull.Value)
                record.StateName = current["StateName"].ToString();

            return record;

        }

        #endregion

        public static readonly AddressesDALC Instance = new AddressesDALC();

    }
}
