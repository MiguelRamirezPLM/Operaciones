using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientsDALC : PLMClientsDataAccessAdapter<ClientInfo>
    {

        #region Constructors

        private ClientsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientInfo businessEntity)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@firstName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FirstName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@completeName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompleteName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@gender", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Gender);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (businessEntity.Birthday != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@birthday", DbType.Date,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Birthday);

            if (businessEntity.EntrySourceId > 0)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@entrySourceId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntrySourceId);

            if(businessEntity.Password != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);

            if(businessEntity.CountryId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);

            if(businessEntity.StateId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.Age != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@age", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Age);

            //Insert record:
            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ClientId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public int insertComplete(ClientInfo businessEntity)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients_with_ZipCode");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@firstName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FirstName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@completeName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompleteName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@gender", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Gender);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (businessEntity.Birthday != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@birthday", DbType.Date,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Birthday);

            if (businessEntity.EntrySourceId > 0)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@entrySourceId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntrySourceId);

            if (businessEntity.Password != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);

            if (businessEntity.CountryId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);

            if (businessEntity.StateId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.Age != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@age", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Age);

            if (businessEntity.LocationId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@locationId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LocationId);

            if (businessEntity.SuburbId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@suburbId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SuburbId);

            if (businessEntity.ZipCodeId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@zipCodeId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCodeId);

            //Insert record:
            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ClientId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ClientInfo businessEntity)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@firstName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FirstName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@completeName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompleteName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@gender", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Gender);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (!string.IsNullOrEmpty(businessEntity.Birthday.ToString()))
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@birthday", DbType.Date,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Birthday);

            if (businessEntity.Password != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);

            if (businessEntity.CountryId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);

            if (businessEntity.StateId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.Age != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@age", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Age);

            //Update record:
            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
                        
        }

        public void updateComplete(ClientInfo businessEntity)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients_with_ZipCode");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@firstName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FirstName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@completeName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompleteName);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@gender", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Gender);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            if (!string.IsNullOrEmpty(businessEntity.Birthday.ToString()))
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@birthday", DbType.Date,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Birthday);

            if (businessEntity.Password != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);

            if (businessEntity.CountryId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);

            if (businessEntity.StateId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StateId);

            if (businessEntity.Age != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@age", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Age);
            if (businessEntity.LocationId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@locationId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LocationId);

            if (businessEntity.SuburbId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@suburbId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SuburbId);

            if (businessEntity.ZipCodeId != null)
                ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@zipCodeId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ZipCodeId);

            //Update record:
            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public bool checkEmail(string email)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckMail");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);

            ClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override ClientInfo getOne(int pk)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClients");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public ClientInfo getByEmail(string email)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEmail");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public ClientInfo getCompleteByEmail(string email)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEmail");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            ClientInfo record = new ClientInfo();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {


                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.FirstName = dataReader["FirstName"].ToString();
                    record.LastName = dataReader["LastName"].ToString();
                    record.SecondLastName = dataReader["SecondLastName"].ToString();
                    record.CompleteName = dataReader["CompleteName"].ToString();
                    record.Gender = Convert.ToChar(dataReader["Gender"]);

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    if (dataReader["Birthday"] != System.DBNull.Value)
                        record.Birthday = Convert.ToDateTime(dataReader["Birthday"]);

                    if (dataReader["Password"] != System.DBNull.Value)
                        record.Password = dataReader["Password"].ToString();

                    if (dataReader["CountryId"] != System.DBNull.Value)
                        record.CountryId = Convert.ToInt32(dataReader["CountryId"]);

                    if (dataReader["StateId"] != System.DBNull.Value)
                        record.StateId = Convert.ToInt32(dataReader["StateId"]);

                    if (dataReader["Age"] != System.DBNull.Value)
                        record.Age = dataReader["Age"].ToString();

                    if (dataReader["LocationId"] != System.DBNull.Value)
                        record.LocationId = Convert.ToInt32(dataReader["LocationId"]);

                    if (dataReader["SuburbId"] != System.DBNull.Value)
                        record.SuburbId = Convert.ToInt32(dataReader["SuburbId"]);

                    if (dataReader["ZipCodeId"] != System.DBNull.Value)
                        record.ZipCodeId = Convert.ToInt32(dataReader["ZipCodeId"]);


                    record.AddedDate = Convert.ToDateTime(dataReader["AddedDate"]);
                    record.LastUpdate = Convert.ToDateTime(dataReader["LastUpdate"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    return record;
                }
                else {
                    return null;
                }
            }
        }

        public  List<ClientInfo> getByEntrySource(int entrySource)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAdvisors_ByEntrySource");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@entrySourceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, entrySource);
            ClientInfo record;
            List<ClientInfo> listRecord = new List<ClientInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read()){
                    record = new ClientInfo();
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.FirstName = dataReader["FirstName"].ToString();
                    record.LastName = dataReader["LastName"].ToString();
                    record.SecondLastName = dataReader["SecondLastName"].ToString();
                    record.CompleteName = dataReader["CompleteName"].ToString();
                    record.Email = dataReader["Email"].ToString();
                    record.PhoneNumber = dataReader["Phone"].ToString();
                    listRecord.Add(record);
                }
                   
            }
            return listRecord;
        }

        public ClientInfo getByName(string completeName)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClients");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@completeName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, completeName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public MobileClientInfo getByIMEI(string IMEI)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByIMEI");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    MobileClientInfo record = new MobileClientInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.FirstName = dataReader["FirstName"].ToString();
                    record.LastName = dataReader["LastName"].ToString();
                    record.SecondLastName = dataReader["SecondLastName"].ToString();
                    record.CompleteName = dataReader["CompleteName"].ToString();
                    record.Gender = Convert.ToChar(dataReader["Gender"]);

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    if (dataReader["Password"] != System.DBNull.Value)
                        record.Password = dataReader["Password"].ToString();

                    if (dataReader["Birthday"] != System.DBNull.Value)
                        record.Birthday = Convert.ToDateTime(dataReader["Birthday"]);

                    record.AddedDate = Convert.ToDateTime(dataReader["AddedDate"]);
                    record.LastUpdate = Convert.ToDateTime(dataReader["LastUpdate"]);

                    if (dataReader["CountryId"] != System.DBNull.Value)
                        record.CountryId = Convert.ToInt32(dataReader["CountryId"]);

                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    if (dataReader["Profession"] != System.DBNull.Value)
                        record.Profession = (Catalogs.Professions)Convert.ToInt16(dataReader["Profession"]);

                    if (dataReader["OtherProfession"] != System.DBNull.Value)
                        record.OtherProfession = dataReader["OtherProfession"].ToString();

                    if (dataReader["ProfessionalLicense"] != System.DBNull.Value)
                        record.ProfessionalLicense = dataReader["ProfessionalLicense"].ToString();

                    if (dataReader["StateId"] != System.DBNull.Value)
                        record.StateId = Convert.ToInt32(dataReader["StateId"]);

                    if (dataReader["StateName"] != System.DBNull.Value)
                        record.StateName = dataReader["StateName"].ToString();

                    if (dataReader["StateShortName"] != System.DBNull.Value)
                        record.StateShortName = dataReader["StateShortName"].ToString();

                    return record;
                }
                else
                    return null;
            }    
        }

        public MobileClientInfo getByIMEI(string IMEI, string prefix)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByIMEI");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    MobileClientInfo record = new MobileClientInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.FirstName = dataReader["FirstName"].ToString();
                    record.LastName = dataReader["LastName"].ToString();
                    record.SecondLastName = dataReader["SecondLastName"].ToString();
                    record.CompleteName = dataReader["CompleteName"].ToString();
                    record.Gender = Convert.ToChar(dataReader["Gender"]);

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    if (dataReader["Password"] != System.DBNull.Value)
                        record.Password = dataReader["Password"].ToString();

                    if (dataReader["Birthday"] != System.DBNull.Value)
                        record.Birthday = Convert.ToDateTime(dataReader["Birthday"]);

                    record.AddedDate = Convert.ToDateTime(dataReader["AddedDate"]);
                    record.LastUpdate = Convert.ToDateTime(dataReader["LastUpdate"]);

                    if (dataReader["CountryId"] != System.DBNull.Value)
                        record.CountryId = Convert.ToInt32(dataReader["CountryId"]);

                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    if (dataReader["Profession"] != System.DBNull.Value)
                        record.Profession = (Catalogs.Professions)Convert.ToInt16(dataReader["Profession"]);

                    if (dataReader["OtherProfession"] != System.DBNull.Value)
                        record.OtherProfession = dataReader["OtherProfession"].ToString();

                    if (dataReader["ProfessionalLicense"] != System.DBNull.Value)
                        record.ProfessionalLicense = dataReader["ProfessionalLicense"].ToString();

                    if (dataReader["StateId"] != System.DBNull.Value)
                        record.StateId = Convert.ToInt32(dataReader["StateId"]);

                    if (dataReader["StateName"] != System.DBNull.Value)
                        record.StateName = dataReader["StateName"].ToString();

                    if (dataReader["StateShortName"] != System.DBNull.Value)
                        record.StateShortName = dataReader["StateShortName"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

        public ClientInfo getByCode(string codeString)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByCode");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CodeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);    

             using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public WebClientInfo getByEmailByPassword(string email, string password)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEmailByPassword");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, password);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    PLMClientsBusinessEntities.WebClientInfo record = new WebClientInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);

                    if (dataReader["CodeString"] != System.DBNull.Value)
                        record.CodeString = dataReader["CodeString"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

        public ClientDetailCodeInfo getByEmailByPasswordByPrefix(string email, string password, string prefix)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetClientByEmailByPasswordByPrefix");

            // Add the parameters:
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, password);
            ClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    PLMClientsBusinessEntities.ClientDetailCodeInfo record = new ClientDetailCodeInfo();

                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);

                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();

                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    record.PrefixName = dataReader["PrefixName"].ToString();
                    if (dataReader["CodeString"] != System.DBNull.Value)
                        record.Codestring = dataReader["CodeString"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ClientInfo getFromDataReader(IDataReader current)
        {
            ClientInfo record = new ClientInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.FirstName = current["FirstName"].ToString();
            record.LastName = current["LastName"].ToString();
            record.SecondLastName = current["SecondLastName"].ToString();
            record.CompleteName = current["CompleteName"].ToString();
            record.Gender = Convert.ToChar(current["Gender"]);
            
            if(current["Email"] != System.DBNull.Value)
                record.Email = current["Email"].ToString();
            
            if (current["Birthday"] != System.DBNull.Value)
                record.Birthday = Convert.ToDateTime(current["Birthday"]);

            if (current["Password"] != System.DBNull.Value)
                record.Password = current["Password"].ToString();

            if (current["CountryId"] != System.DBNull.Value)
                record.CountryId = Convert.ToInt32(current["CountryId"]);

            if (current["StateId"] != System.DBNull.Value)
                record.StateId = Convert.ToInt32(current["StateId"]);

            if (current["Age"] != System.DBNull.Value)
                record.Age = current["Age"].ToString();

            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.LastUpdate = Convert.ToDateTime(current["LastUpdate"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly ClientsDALC Instance = new ClientsDALC();

    }
}
