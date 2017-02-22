using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using System.Data;
using System.Data.Common;
namespace AgroDataAccessComponent
{
    public class CountriesDALC : AgroDataAccessAdapter<CountriesInfo>
    {
        #region Constructor

        private CountriesDALC() { }

        #endregion
        #region Public Methods
        #region Select Methods
        public List<AgroEntityFramework.Countries> SelectCountries(string countriess){
            List<AgroEntityFramework.Countries> countries;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Countries
                           where countriess.Contains(d.ID)
                           orderby d.CountryName
                           select d);

                countries = pes.ToList();
            }
            return countries;
        }

        public List<AgroEntityFramework.Countries> getCountries()
        {
            List<AgroEntityFramework.Countries> countries;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = (from d in context.Countries
                           select d);

                countries = pes.ToList();
            }
            return countries;
        }



        #endregion
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
        public List<CountryCodeInfo> getCountryCodesByProduct(int productId, int pharmaFormId, int countryId)
        {
            List<CountryCodeInfo> BECollection = new List<CountryCodeInfo>();
            CountryCodeInfo record;
            DbCommand dbCmd = CountriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesCodes");

            //// Add the parameters:
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
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
        public List<CountriesInfo> getCountries(string countries)
        {
            List<CountriesInfo> BECollection = new List<CountriesInfo>();

            DbCommand dbCmd = CountriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountriesByCodeId");

            //// Add the parameters:
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countries", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countries);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }
        public override CountriesInfo getOne(int pk)
        {
            DbCommand dbCmd = CountriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            // Add the parameters:
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CountriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CountriesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public static readonly CountriesDALC Instance = new CountriesDALC();
    }
}