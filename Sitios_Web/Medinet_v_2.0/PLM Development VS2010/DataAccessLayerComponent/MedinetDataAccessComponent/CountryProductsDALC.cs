using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class CountryProductsDALC : MedinetDataAccessAdapter<CountryProductInfo>
    {
        #region Constructors

        private CountryProductsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CountryProductInfo businessEntity)
        {
            DbCommand dbCmd = CountryProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountryProducts");

            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryCodeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryCodeId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            CountryProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(CountryProductInfo businessEntity)
        {
            DbCommand dbCmd = DivisionProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountryProducts");

            // Add the parameters:
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryCodeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryCodeId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            //Delete record:
            CountryProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public bool participate(byte countryCodeId, int pharmaFormId, int productId)
        {
            DbCommand dbCmd = CountryProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountryProduct");

            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryCodeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryCodeId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            CountryProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }


        #endregion




        public static readonly CountryProductsDALC Instance = new CountryProductsDALC();
    }
}
