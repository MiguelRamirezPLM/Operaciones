using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class MetadataVersionDALC : PLMClientsDataAccessAdapter<MetadataVersionInfo>
    {

        #region Constructors

        private MetadataVersionDALC() { }

        #endregion

        #region Public Methods

        public string getUrlVersion(string codeString)
        {
            PLMClientsBusinessEntities.MetadataVersionInfo metadataVersion = new MetadataVersionInfo();

            DbCommand dbCmd = MetadataVersionDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUrlVersion");

            // Add the parameters:
            MetadataVersionDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeString);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MedicalCalculatorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    if (dataReader["VersionId"] != System.DBNull.Value)
                        metadataVersion.VersionId = Convert.ToInt32(dataReader["VersionId"]);

                    if (dataReader["DistributionId"] != System.DBNull.Value)
                        metadataVersion.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);

                    if (dataReader["PrefixId"] != System.DBNull.Value)
                        metadataVersion.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);

                    if (dataReader["TargetId"] != System.DBNull.Value)
                        metadataVersion.TargetId = Convert.ToByte(dataReader["TargetId"]);

                    if (dataReader["UrlPackage"] != System.DBNull.Value)
                        metadataVersion.UrlPackage = dataReader["UrlPackage"].ToString();

                    if (dataReader["AddedDate"] != System.DBNull.Value)
                        metadataVersion.AddedDate = Convert.ToDateTime(dataReader["AddedDate"]);

                    if (dataReader["Active"] != System.DBNull.Value)
                        metadataVersion.Active = Convert.ToBoolean(dataReader["Active"]);
                }
            }
            return metadataVersion.UrlPackage;
        }

        #endregion

        public static readonly MetadataVersionDALC Instance = new MetadataVersionDALC();

    }
}
