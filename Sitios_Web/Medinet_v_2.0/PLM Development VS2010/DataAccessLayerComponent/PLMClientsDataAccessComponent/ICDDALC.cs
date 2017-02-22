using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using PLMClientsBusinessEntities;


namespace PLMClientsDataAccessComponent
{
    public class ICDDALC : PLMClientsDataAccessAdapter<ICDInfo>
    {
        
        #region Constructors
        private ICDDALC() { }
        #endregion
        
        
        #region Public Methods


        public List<PLMClientsBusinessEntities.ICDInfo> getInformationByPrefixByTypeBySpecialityByICD(string prefix, byte targetId, byte informationTypeId, string country, int specialityId, string keyICD)
        {

            List<PLMClientsBusinessEntities.ICDInfo> BECollection = new List<ICDInfo>();
            DbCommand dbCmd = ICDDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetElectronicInformationByICD");

            // Add the parameters:
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@infoTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, informationTypeId);
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@country", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);
            ICDDALC.InstanceDatabase.AddParameter(dbCmd, "@keyICD", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, keyICD);

            // Get the result set from the stored procedure:

            using (IDataReader dataReader = ICDDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
                
            }

            return BECollection;



        }


        public List<ICDGuidesInfo> getByGuides(string prefix, string icdKey)
        {
            List<ICDGuidesInfo> BECollection = new List<ICDGuidesInfo>();

            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetICDByGuides");

            // Add the parameters:
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            SpecialitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@icdKey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, icdKey);

            using (IDataReader dataReader = ICDDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReaderGuides(dataReader));
            }

            return BECollection;
        }

        #endregion
        
        
        #region Protected Methods

        protected override ICDInfo getFromDataReader(IDataReader current)
        {
            ICDInfo record = new ICDInfo();

            record.ICDId = Convert.ToInt32(current["ICDId"]);
            record.ICDKey = current["ICDKey"].ToString();
            record.SpanishDescription = current["SpanishDescription"].ToString();
            record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }


        protected ICDGuidesInfo getFromDataReaderGuides(IDataReader current)
        {
            ICDGuidesInfo record = new ICDGuidesInfo();

            record.ICDId = Convert.ToInt32(current["ICDId"]);
            record.ICDKey = current["ICDKey"].ToString();
            record.SpanishDescription = current["SpanishDescription"].ToString();
            record.EnglishDescription = current["EnglishDescription"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }


        #endregion


        public static readonly ICDDALC Instance = new ICDDALC();

    }
}
