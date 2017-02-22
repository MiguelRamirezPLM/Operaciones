using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class TargetOutputDALC : PLMClientsDataAccessAdapter<TargetOutputsInfo>
    {

        #region Constructors

        private TargetOutputDALC() { }

        #endregion

        #region Public Methods

        public override int insert(TargetOutputsInfo businessEntity)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetOutputs");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TargetId);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@targetName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TargetName);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            TargetOutputDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.TargetId = Convert.ToByte(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(TargetOutputsInfo businessEntity)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetOutputs");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TargetId);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@targetName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TargetName);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            TargetOutputDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        /*
        public override void delete(int pk)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetOutputs");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            TargetOutputDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);   
        } */

        public override TargetOutputsInfo getOne(int pk)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetOutputs");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
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


        public List<TargetOutputsInfo> getTargets()
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetOutputs");

            List<TargetOutputsInfo> BECollection = new List<TargetOutputsInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetOutputDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }
/*
        public TargetOutputsInfo getByCodeString(string codeString)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
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

        public TargetOutputsInfo getByPrefix(int prefixId)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.AnsiString,
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

        public TargetOutputsInfo getByIMEI(string IMEI)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
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

        public TargetOutputsInfo getByIMEIByISBN(string IMEI, string isbn)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@ISBN", DbType.AnsiString,
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

        public TargetOutputsInfo getByIMEIByPrefix(string IMEI, string prefix)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
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

        public TargetOutputsInfo getByClientByPrefix(int clientId, string codePrefix, string hwIdentifier)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByClientByPrefix");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hwIdentifier);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetOutputDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public bool checkClientCode(string code)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckClientCodes");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            TargetOutputDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;
 
        }

        public TargetOutputsInfo getByEmailByPrefix(string email, string prefix)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
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

        public TargetOutputsInfo getByLicenseKey(string license)
        {
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByLicenseKey");
            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.String,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@licensekey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, license);


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetOutputDALC.InstanceDatabase.ExecuteReader(dbCmd))
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
            DbCommand dbCmd = TargetOutputDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetByCode");
            TargetOutputsInfo record = new TargetOutputsInfo();
            // Add the parameters:
            TargetOutputDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetOutputDALC.InstanceDatabase.ExecuteReader(dbCmd))
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
 */
        #endregion

        #region Protected Methods

        protected override TargetOutputsInfo getFromDataReader(IDataReader current)
        {
            TargetOutputsInfo record = new TargetOutputsInfo();

            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.TargetName = current["TargetName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

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

        public static readonly TargetOutputDALC Instance = new TargetOutputDALC();

    }
}
