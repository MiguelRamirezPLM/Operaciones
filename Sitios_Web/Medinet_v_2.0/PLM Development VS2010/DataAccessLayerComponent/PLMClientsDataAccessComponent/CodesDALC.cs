using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CodesDALC : PLMClientsDataAccessAdapter<CodeInfo>
    {

        #region Constructors

        private CodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CodeInfo businessEntity)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeStatusId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeStatusId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@assign", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Assign);

            //Insert record:
            CodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.CodeId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void update(CodeInfo businessEntity)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeStatusId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeStatusId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@assign", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Assign);

            //Update record:
            CodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            CodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);   
        }

        public override CodeInfo getOne(int pk)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
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

        public CodeInfo getByCodeString(string codeString)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }   
        }

        public CodeInfo getByPrefix(int prefixId)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public CodeInfo getByIMEI(string IMEI)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public CodeInfo getByIMEIByISBN(string IMEI, string isbn)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@ISBN", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public CodeInfo getByIMEIByPrefix(string IMEI, string prefix)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public CodeInfo getByClientByPrefix(int clientId, string codePrefix, string hwIdentifier)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByClientByPrefix");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hwIdentifier);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public bool checkClientCode(string code)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckClientCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            CodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;
 
        }

        public CodeInfo getByEmailByPrefix(string email, string prefix)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public CodeInfo getByLicenseKey(string license)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByLicenseKey");
            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.String,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@licensekey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, license);


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }
        
        //GetTargetByCode
        public TargetOutputsInfo getTargetByCodeString(string codeString)
        {
            DbCommand dbCmd = CodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetByCode");
            TargetOutputsInfo record = new TargetOutputsInfo();
            // Add the parameters:
            CodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    //return this.getFromDataReaderT(dataReader);                 
                    record.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    record.TargetName = dataReader["TargetName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                }         
            }
            return record;
        }
 
        #endregion

        #region Protected Methods

        protected override CodeInfo getFromDataReader(IDataReader current)
        {
            CodeInfo record = new CodeInfo();

            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.CodeStatusId = Convert.ToByte(current["CodeStatusId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.CodeString = current["CodeString"].ToString();
            record.Assign = Convert.ToBoolean(current["Assign"]);

            return record;
        }

        //protected override TargetOutputsInfo getFromDataReader(IDataReader current)
        //{
        //    TargetOutputsInfo record = new TargetOutputsInfo();

        //    record.TargetId = Convert.ToByte(current["TargetId"]);
        //    record.TargetName = current["TargetName"].ToString();
        //    record.Active = Convert.ToBoolean(current["Active"]);

        //    return record;
        //}


        #endregion

        public static readonly CodesDALC Instance = new CodesDALC();

    }
}
