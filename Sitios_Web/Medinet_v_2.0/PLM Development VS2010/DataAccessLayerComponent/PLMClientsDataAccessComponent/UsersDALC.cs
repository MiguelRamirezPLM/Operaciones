using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class UsersDALC : PLMClientsDataAccessAdapter<UserInfo>
    {
        #region Constructors

        private UsersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(UserInfo businessEntity)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickPrefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickPrefixId);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.UserId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void update(UserInfo businessEntity)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickPrefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.NickPrefixId);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Password);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);
            
            //Delete record:
            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);   
        }

        public override UserInfo getOne(int pk)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public bool checkNickName(string nickName)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckNickName");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nickName);

            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        public UserInfo getByNickName(string nickname)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nickname);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public UserByClient getByNickNameByPass(string nickName, string password)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@password", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, password);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByClient record = new UserByClient();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();
                    record.UserActive = Convert.ToBoolean(dataReader["UserActive"]);

                    return record;
                }
                else
                    return null;
            }

        }

        public UserByClient getByEmail(string email)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByClient record = new UserByClient();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();
                    record.UserActive = Convert.ToBoolean(dataReader["UserActive"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public UserByClient getByNickNameByCode(string nickName, string codeString)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@nickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, nickName);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByClient record = new UserByClient();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();
                    record.UserActive = Convert.ToBoolean(dataReader["UserActive"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public UserByClient getByUser(int userId)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsers");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByClient record = new UserByClient();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    if (dataReader["Email"] != System.DBNull.Value)
                        record.Email = dataReader["Email"].ToString();
                    record.UserActive = Convert.ToBoolean(dataReader["UserActive"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public UserByCode getByCode(string codeString)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsersByCode");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByCode record = new UserByCode();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    record.CodeStatusId = Convert.ToByte(dataReader["CodeStatusId"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public UserByCode getByCode(string codeString, string nickName)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsersByCode");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByCode record = new UserByCode();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    record.CodeStatusId = Convert.ToByte(dataReader["CodeStatusId"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public UserByCode getByCode(int userId)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsersByCode");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    UserByCode record = new UserByCode();

                    record.UserId = Convert.ToInt32(dataReader["UserId"]);
                    record.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    record.NickName = dataReader["NickName"].ToString();
                    record.Password = dataReader["Password"].ToString();
                    record.CodeString = dataReader["CodeString"].ToString();
                    record.CodeStatusId = Convert.ToByte(dataReader["CodeStatusId"]);

                    return record;
                }
                else
                    return null;
            }
        }

        public bool checkUserByCode(string codeString)
        {
            DbCommand dbCmd = UsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckUsersByCode");

            // Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            UsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        #endregion

        #region Protected Methods

        protected override UserInfo getFromDataReader(IDataReader current)
        {
            UserInfo record = new UserInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.NickName = current["NickName"].ToString();
            record.NickPrefixId = Convert.ToInt32(current["NickPrefixId"]);
            record.Password = current["Password"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly UsersDALC Instance = new UsersDALC();

    }
}
