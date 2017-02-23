using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class CountryDALC : PLMUsersDataAccesAdapter<CountryInfo>
    {
        #region Constructors

        private CountryDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CountryInfo businessEntity)
        {
            DbCommand dbCom = CountryDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CountryDALC.CRUD.Create);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@countryName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryName);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@id", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ID);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@regionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RegionId);

            CountryDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

            businessEntity.CountryId = Convert.ToInt32(dbCom.Parameters["@Return"].Value);

            return businessEntity.CountryId;
        }

        public override void delete(int pk)
        {
            DbCommand dbCom = CountryDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CountryDALC.CRUD.Delete);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            CountryDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override void update(CountryInfo businessEntity)
        {
            DbCommand dbCom = CountryDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CountryDALC.CRUD.Update);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@countryName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryName);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@id", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ID);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@regionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RegionId);
            

            CountryDALC.InstanceDatabase.ExecuteNonQuery(dbCom);

        }

        public override CountryInfo getOne(int pk)
        {
            DbCommand dbCom = CountryDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCountries");

            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CountryDALC.CRUD.Read);
            CountryDALC.InstanceDatabase.AddParameter(dbCom, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = CountryDALC.InstanceDatabase.ExecuteReader(dbCom))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return new CountryInfo();
            }
        }

        public List<CountryInfo> getCountries()
        {
            DbCommand dbCmd = CountryDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCountries");

            List<CountryInfo> BECollection = new List<CountryInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }
        #endregion

        #region Protected

        protected override CountryInfo getFromDataReader(IDataReader current)
        {
            CountryInfo record = new CountryInfo();

            record.CountryId = Convert.ToInt32(current["CountryId"]);
            record.CountryName = current["CountryName"].ToString();
            record.ID = current["ID"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.RegionId = Convert.ToInt32(current["RegionId"]);

            return record;
        }

        #endregion

        public static readonly CountryDALC Instance = new CountryDALC();

    }
}
