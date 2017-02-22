using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ElectronicInformationDALC : PLMClientsDataAccessAdapter<ElectronicInformationByTargetInfo>
    {

        #region Constructors

        private ElectronicInformationDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByType(byte informationTypeId, string country)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@infoTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, informationTypeId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefix(string prefix, string country)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTarget(string prefix, byte targetId, string country)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByType(string prefix, byte targetId, byte informationTypeId, string country)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@infoTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, informationTypeId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationBySection(string prefix, byte targetId, string country, int sectionId, string resolutionKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBannersBySection");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@sectionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sectionId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@resolutionKey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, resolutionKey);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTypeBySpeciality(string prefix, byte targetId, byte informationTypeId, string country, int specialityId)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@infoTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, informationTypeId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country); 
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTypeByICD(string prefix, byte targetId, byte informationTypeId, string country, int icdId)
        {

            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformationByICD");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@infoTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, informationTypeId);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int16,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, icdId);

            // Get the result set from the stored procedure:

            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));

            }
            return BECollection;
        }


        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getMedicalGuidelinesByText(string prefix, string text)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> BECollection = new List<ElectronicInformationByTargetInfo>();

            DbCommand dbCmd = ElectronicInformationDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSearchElectronicInformation");

            // Add the parameters:
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ElectronicInformationDALC.InstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicInformationDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }


      

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ElectronicInformationByTargetInfo getFromDataReader(IDataReader current)
        {
            ElectronicInformationByTargetInfo record = new ElectronicInformationByTargetInfo();

            record.ElectronicId = Convert.ToInt32(current["ElectronicId"]);
            record.InfoTypeId = Convert.ToByte(current["InfoTypeId"]);
            record.InfDescription = current["InfoDescription"].ToString();
            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.ElectronicTitle = current["ElectronicTitle"].ToString();
            record.ElectronicDescription = current["ElectronicDescription"].ToString();
            record.PublishedDate = Convert.ToDateTime(current["PublishedDate"]);
            record.FileName = current["FileName"].ToString();
            record.HTMLFileName = current["HTMLFileName"].ToString();
            record.Link = current["Link"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            record.ResolutionBaseUrl = current["ResolutionBaseUrl"].ToString();
            record.Order = Convert.ToByte(current["Order"]);

            return record;
        }

        #endregion

        public static readonly ElectronicInformationDALC Instance = new ElectronicInformationDALC();

    }
}
