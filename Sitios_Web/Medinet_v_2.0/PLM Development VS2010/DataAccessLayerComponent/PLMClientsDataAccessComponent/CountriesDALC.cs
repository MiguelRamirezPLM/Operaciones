using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CountriesDALC : PLMClientsDataAccessAdapter<CountryInfo>
    {
        #region Constructors

        private CountriesDALC() { }

        #endregion

        #region Public Methods

        public override List<CountryInfo> getAll()
        {
            List<CountryInfo> BECollection = new List<CountryInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.InstanceDatabase.ExecuteReader(
                CountriesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountries")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public override CountryInfo getOne(int pk)
        {
            DbCommand dbCmd = CountriesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            // Add the parameters:
            CountriesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CountriesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public CountryInfo getByCode(string country)
        {
            DbCommand dbCmd = CountriesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesByCodeId");

            //// Add the parameters:
            CountriesDALC.InstanceDatabase.AddParameter(dbCmd, "@countries", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, country);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            } 
        }

        public List<CountryMobileInfo> getCountriesByTarget(byte targetId)
        {
            DbCommand dbCmd = CountriesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetCountries");

            List<CountryMobileInfo> BECollection = new List<CountryMobileInfo>();

            // Add the parameters:
            CountriesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)targetId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = CountriesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    CountryMobileInfo record = new CountryMobileInfo();

                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.BaseUrl = dataReader["BaseUrl"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.ID = dataReader["ID"].ToString();
                    record.FileName = dataReader["FileName"].ToString();
                    record.ISBN = dataReader["FileName"].ToString();
                    
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override CountryInfo getFromDataReader(IDataReader current)
        {
            CountryInfo record = new CountryInfo();

            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.CountryName = current["CountryName"].ToString();
            
            if (current["CountryCode"] != System.DBNull.Value)
                record.CountryCode = Convert.ToByte(current["CountryCode"]);
            
            record.ID = current["ID"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly CountriesDALC Instance = new CountriesDALC();

    }
}
