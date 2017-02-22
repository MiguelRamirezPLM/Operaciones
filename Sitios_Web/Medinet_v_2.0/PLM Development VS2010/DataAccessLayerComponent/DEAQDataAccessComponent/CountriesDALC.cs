using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using AgroBusinessEntries;
using System.Data.Common;
using System.Data;
namespace DEAQDataAccessComponent
{
    public class CountriesDALC : DEAQEngineDataAccessAdapter<AgroBusinessEntries.CountriesInfo>
    {

        #region Constructor

        private CountriesDALC() { }

        #endregion

        #region Public Methods

        //Retrieves All Countries
        public DEAQBusinessEntries.CountryInfo getCountries()
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();
            var countries = from countryInfo in db.roc_spGetCountries()
                            select new DEAQBusinessEntries.CountryInfo
                            {
                                CountryId = countryInfo.CountryId,
                                CountryName = countryInfo.CountryName,
                                CountryCode = countryInfo.CountryCode,
                                ID = countryInfo.ID,
                                Active = countryInfo.Active
                            };
            List<DEAQBusinessEntries.CountryInfo> products = countries.ToList();
            //return countries.Count() > 0 ? countries[0] : null;
            return products.Count() > 0 ? products[0] : null;
        }

        public override AgroBusinessEntries.CountriesInfo getOne(int pk)
        {
            DbCommand dbCmd = CountriesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            // Add the parameters:
            CountriesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CountriesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }
        public List<DEAQBusinessEntries.CountryCodeInfo> getCountryCodesByProduct(int productId, int pharmaFormId, int countryId)
        {
            List<DEAQBusinessEntries.CountryCodeInfo> BECollection = new List<DEAQBusinessEntries.CountryCodeInfo>();
            DEAQBusinessEntries.CountryCodeInfo record;
            DbCommand dbCmd = CountriesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesCodes");

            //// Add the parameters:
            CountriesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CountriesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            CountriesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new DEAQBusinessEntries.CountryCodeInfo();

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

        #region Protected Methods

        protected override AgroBusinessEntries.CountriesInfo getFromDataReader(IDataReader current)
        {
            AgroBusinessEntries.CountriesInfo record = new AgroBusinessEntries.CountriesInfo();

            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.CountryName = current["CountryName"].ToString();
            record.ID = current["ID"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion


        public static readonly CountriesDALC Instance = new CountriesDALC();
        
    }
}
