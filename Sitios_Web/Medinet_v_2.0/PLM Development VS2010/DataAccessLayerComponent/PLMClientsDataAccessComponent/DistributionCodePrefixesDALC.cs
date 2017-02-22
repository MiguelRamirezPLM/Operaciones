using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionCodePrefixesDALC : PLMClientsDataAccessAdapter<DistributionCodePrefixesInfo>
    {

        #region Constructors

        private DistributionCodePrefixesDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.DistributionCodePrefixesInfo getDistributionCodePrefix(int prefixId)
        {
            PLMClientsBusinessEntities.DistributionCodePrefixesInfo BEntity = new DistributionCodePrefixesInfo();

            DbCommand dbCmd = DistributionCodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDistributionCodePrefix");

            // Add the parameters:
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionCodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    BEntity.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    BEntity.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    BEntity.TargetId = Convert.ToByte(dataReader["TargetId"]);

                    if (dataReader["InitialDate"] != System.DBNull.Value)
                        BEntity.InicialDate = Convert.ToDateTime(dataReader["InitialDate"]);

                    if (dataReader["FinalDate"] != System.DBNull.Value)
                        BEntity.FinalDate = Convert.ToDateTime(dataReader["FinalDate"]);

                    if (dataReader["AllowedInstallations"] != System.DBNull.Value)
                        BEntity.AllowedInstallations = Convert.ToInt16(dataReader["AllowedInstallations"]);
                }
            }
            return BEntity;
        }

        public PLMClientsBusinessEntities.DistributionCodePrefixesInfo getDistributionCodePrefix(int distributionId, int prefixId, byte targetId)
        {
            PLMClientsBusinessEntities.DistributionCodePrefixesInfo BEntity = new DistributionCodePrefixesInfo();

            DbCommand dbCmd = DistributionCodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDistributionCodePrefix");

            // Add the parameters:
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, distributionId);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionCodePrefixesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    BEntity.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    BEntity.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    BEntity.TargetId = Convert.ToByte(dataReader["TargetId"]);

                    if (dataReader["InitialDate"] != System.DBNull.Value)
                        BEntity.InicialDate = Convert.ToDateTime(dataReader["InitialDate"]);

                    if (dataReader["FinalDate"] != System.DBNull.Value)
                        BEntity.FinalDate = Convert.ToDateTime(dataReader["FinalDate"]);

                    if (dataReader["AllowedInstallations"] != System.DBNull.Value)
                        BEntity.AllowedInstallations = Convert.ToInt16(dataReader["AllowedInstallations"]);
                }
            }
            return BEntity;
        }

        public override int insert(DistributionCodePrefixesInfo businessEntity)
        {
            DbCommand dbCmd = DistributionCodePrefixesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDistributionCodePrefixes");

            // Add the parameters:
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DistributionId);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrefixId);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TargetId);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@initialDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InicialDate);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);
            DistributionCodePrefixesDALC.InstanceDatabase.AddParameter(dbCmd, "@allowedInstallations", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AllowedInstallations);   

            //Insert record:
            DistributionCodePrefixesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.DistributionId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        public static readonly DistributionCodePrefixesDALC Instance = new DistributionCodePrefixesDALC();

    }
}
