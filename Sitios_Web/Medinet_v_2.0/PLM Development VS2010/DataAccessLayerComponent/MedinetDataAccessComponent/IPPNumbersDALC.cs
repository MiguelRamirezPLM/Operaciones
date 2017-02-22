using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class IPPNumbersDALC : MedinetDataAccessAdapter<IPPNumbersInfo>
    {
        #region Constructors

        private IPPNumbersDALC() { }

        #endregion

        #region Public Methods

        public List<IPPNumbersInfo> getAll(int productId)
        {
            DbCommand dbCmd = IPPNumbersDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetIppByProduct");

            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            List<IPPNumbersInfo> recordCollection = new List<IPPNumbersInfo>();

            using (IDataReader dataReader = IPPNumbersDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                IPPNumbersInfo record;

                while (dataReader.Read())
                {
                    record = new IPPNumbersInfo();

                    record.IppId = Convert.ToInt32(dataReader["IppId"]);
                    record.Description = dataReader["Description"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    recordCollection.Add(record);
                }

                return recordCollection;


            }
        }

        public override IPPNumbersInfo getOne(int pk)
        {
            DbCommand dbCmd = IPPNumbersDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppNumbers");

            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@ippId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = IPPNumbersDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override void  delete(int pk)
        {
            DbCommand dbCmd = IPPNumbersDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppNumbers");

            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@ippId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            IPPNumbersDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override int insert(IPPNumbersInfo businessEntity)
        {
            DbCommand dbCmd = IPPNumbersDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppNumbers");

            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@ippId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CountryId);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            IPPNumbersDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            IPPNumbersDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.IppId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.IppId; 

        }

        public override void update(IPPNumbersInfo businessEntity)
        {
            IPPNumbersDALC.MedInstanceDatabase.ExecuteNonQuery("dbo.plm_spCRUDIppNumbers",
                    CRUD.Update,
                    businessEntity.IppId,
                    businessEntity.ProductId,
                    businessEntity.CountryId,
                    businessEntity.Description,
                    businessEntity.Active);

        }

        #endregion

        #region Protected Methods

        protected override IPPNumbersInfo getFromDataReader(IDataReader current)
        {
            IPPNumbersInfo record = new IPPNumbersInfo();

            record.IppId = Convert.ToInt32(current["IppId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.CountryId = Convert.ToInt32(current["CountryId"]);
            
            if (current["Description"] != DBNull.Value)
                record.Description = current["Description"].ToString();

            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion

        public static readonly IPPNumbersDALC Instance = new IPPNumbersDALC();

    }
}
