using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class CountriesDALC : MedinetDataAccessAdapter<CountriesInfo>
    {
        #region Constructors

        private CountriesDALC() { }

        #endregion

        #region Public methods

        public override CountriesInfo getOne(int pk)
        {
            DbCommand dbCmd = CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            // Add the parameters:
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);
          
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<CountriesInfo> getCountries(string countries)
        {
            List<CountriesInfo> BECollection = new List<CountriesInfo>();

            DbCommand dbCmd = CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesByCodeId");

            //// Add the parameters:
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countries", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countries);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;  
        }

        public List<CountryCodeInfo> getCountryCodesByProduct(int productId,int pharmaFormId,int countryId)
        {
            List<CountryCodeInfo> BECollection = new List<CountryCodeInfo>();
            CountryCodeInfo record;
            DbCommand dbCmd = CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesCodes");

            //// Add the parameters:
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new CountryCodeInfo();

                    record.CountryCodeId = Convert.ToByte(dataReader["CountryCodeId"]);
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.CountryCode = Convert.ToByte(dataReader["CountryCode"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<CountryCodeInfo> getCountryCodes(int countryId)
        {
            List<CountryCodeInfo> BECollection = new List<CountryCodeInfo>();
            CountryCodeInfo record;
            DbCommand dbCmd = CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesCodes");

            //// Add the parameters:
            CountriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new CountryCodeInfo();

                    record.CountryCodeId = Convert.ToByte(dataReader["CountryCodeId"]);
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.CountryCode = Convert.ToByte(dataReader["CountryCode"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public override List<CountriesInfo> getAll()
        {
            List<CountriesInfo> BECollection = new List<CountriesInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(
                CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountries")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;  
        }

        public List<CountryCodeInfo> getSpecialCountries(int countryId)
        {
            List<CountryCodeInfo> BECollection = new List<CountryCodeInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.MedInstanceDatabase.ExecuteReader(
                CountriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountryCodes", countryId)))
            {
                CountryCodeInfo record;
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new CountryCodeInfo();

                    record.CountryCodeId = Convert.ToByte(dataReader["CountryCodeId"]);
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();
                    record.CountryCode = Convert.ToByte(dataReader["CountryCode"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    
                    BECollection.Add(record);
                }
            }
            return BECollection;  

        }

        #endregion

        #region Protected methods

        protected override CountriesInfo getFromDataReader(IDataReader current)
        {
            CountriesInfo record = new CountriesInfo();

            record.CountryId = Convert.ToInt32(current["CountryId"]);
            record.CountryName = current["CountryName"].ToString();
            record.ID = current["ID"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CountriesDALC Instance = new CountriesDALC();
    }
}
