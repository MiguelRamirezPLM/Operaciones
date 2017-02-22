using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class DistributionsDALC : PLMClientsDataAccessAdapter<DistributionsInfo>
    {

        #region Constructors

        private DistributionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(DistributionsInfo businessEntity)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDistributions");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DistributionName);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@version", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Version);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@lastUpdate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastUpdate);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            DistributionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.DistributionId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }



        public override void update(DistributionsInfo businessEntity)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDistributions");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DistributionId);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DistributionName);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@version", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Version);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            DistributionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }


        public List<DistributionsInfo> getDistributions()
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDistributions");

            List<DistributionsInfo> BECollection = new List<DistributionsInfo>();

                // Get the result set from the stored procedure:
                using (IDataReader dataReader = DistributionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
                {
                    // Iterates through records:
                    while (dataReader.Read())
                    {
                        BECollection.Add(this.getFromDataReader(dataReader));
                    }
                }

                return BECollection;
         }   
        


       


        public DistributionsInfo getByCodeString(string codeString)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
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

        public DistributionsInfo getByPrefix(int prefixId)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.AnsiString,
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

        public DistributionsInfo getByIMEI(string IMEI)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
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

        public DistributionsInfo getByIMEIByISBN(string IMEI, string isbn)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@ISBN", DbType.AnsiString,
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

        public DistributionsInfo getByIMEIByPrefix(string IMEI, string prefix)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByIMEI");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@IMEI", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, IMEI);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
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

        public DistributionsInfo getByClientByPrefix(int clientId, string codePrefix, string hwIdentifier)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByClientByPrefix");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hwIdentifier);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public bool checkClientCode(string code)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckClientCodes");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            DistributionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;
 
        }

        public DistributionsInfo getByEmailByPrefix(string email, string prefix)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodes");

            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, email);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
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

        public DistributionsInfo getByLicenseKey(string license)
        {
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByLicenseKey");
            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.String,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@licensekey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, license);


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
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
            DbCommand dbCmd = DistributionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetByCode");
            TargetOutputsInfo record = new TargetOutputsInfo();
            // Add the parameters:
            DistributionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
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

        protected override DistributionsInfo getFromDataReader(IDataReader current)
        {
            DistributionsInfo record = new DistributionsInfo();

            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.DistributionName = current["DistributionName"].ToString();
            record.Description = current["Description"].ToString();
            record.Version = current["Version"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly DistributionsDALC Instance = new DistributionsDALC();

    }
}
