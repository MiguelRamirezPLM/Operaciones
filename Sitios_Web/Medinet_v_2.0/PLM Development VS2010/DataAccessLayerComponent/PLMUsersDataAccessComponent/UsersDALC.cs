using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class UsersDALC : PLMUsersDataAccesAdapter<UserInfo>
    {
        #region Constructors

        private UsersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(UserInfo businessEntity)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");
            //Add the parameters: 
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0); 
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Create);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@UserId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@name", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Name);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@AddedDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddedDate);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@ExpirationDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ExpirationDate);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@LastUpdate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastUpdate);

            ////Insert Record:
            //UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            //businessEntity.UserId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
            //return businessEntity.UserId;

            //Insert record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            businessEntity.UserId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");
            
            //Add parameter to Stored Procedure
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Delete);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public override void update(UserInfo businessEntity)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");
            
            //Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Update);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@name", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Name);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@secondLastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SecondLastName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);

            //Update record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override UserInfo getOne(int pk)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");

            //Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Read);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }
        
        //Retrieves the User associated with the given NickName and Password.
        public UserInfo getUser(string nick, string pass)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spSearchUsers");

            // Add the parameters:
            
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nick);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pass);
                       
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        //Retrieves the User associated with the given Email.
        public UserInfo getUser(string mail)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spSearchUsers");

            // Add the parameters:

            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, mail);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public CountriesByUserInfo getUserContries(string nick, string pass)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spSearchUsersCountries");

            // Add the parameters:

            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nick);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pass);

            CountriesByUserInfo record = new CountriesByUserInfo();
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Countries = dataReader["Countries"].ToString().Replace("\"", "'");
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    return record;
                }
                else
                    return null;
            }

        }

        public List<UserInfo> getUsers()
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            List<UserInfo> BECollection = new List<UserInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        public List<UserInfo> getUsersByApplication()
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsersByApp");

            List<UserInfo> BECollection = new List<UserInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        } 

        #endregion

        #region Protected

        protected override UserInfo getFromDataReader(IDataReader current)
        {
            UserInfo record = new UserInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.Name = current["Name"].ToString();
            record.LastName = current["LastName"].ToString();
            record.SecondLastName = current["SecondLastName"].ToString();
            record.NickName = current["NickName"].ToString();
            record.Password = current["Password"].ToString();
            record.Email = current["Email"].ToString();
            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.ExpirationDate = Convert.ToDateTime(current["ExpirationDate"]);
            record.LastUpdate = Convert.ToDateTime(current["LastUpdate"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            record.CountryId = Convert.ToInt32(current["CountryId"]);
           
            return record;
        }

        #endregion

        public static readonly UsersDALC Instance = new UsersDALC();

    }
}
