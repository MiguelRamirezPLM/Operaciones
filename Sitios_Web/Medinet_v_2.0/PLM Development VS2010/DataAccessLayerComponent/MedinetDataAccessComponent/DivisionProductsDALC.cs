using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class DivisionProductsDALC : MedinetDataAccessAdapter<DivisionProductInfo>
    {
        #region Constructors

        private DivisionProductsDALC() { }

        #endregion

        #region Public Methods


        public DivisionProductInfo assigned(int productId, int pharmaFormId, int countryId, int laboratoryId)
        {
            DbCommand dbCmd = DivisionProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spDivisionProducts");

            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@laboratoryId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, laboratoryId);

            using (IDataReader dataReader = DivisionProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert(DivisionProductInfo businessEntity)
        {
            DbCommand dbCmd = DivisionProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionProducts");

            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            
            DivisionProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(DivisionProductInfo businessEntity)
        {
            DbCommand dbCmd = DivisionProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionProducts");

            // Add the parameters:
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);

            //Delete record:
            DivisionProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public DivisionProductInfo getOne(int productId, int pharmaFormId, int divisionId)
        {
            DbCommand dbCmd = DivisionProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionProducts");

            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            DivisionProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        #endregion

        #region Protected Methods

        protected override DivisionProductInfo getFromDataReader(IDataReader current)
        {
            DivisionProductInfo record = new DivisionProductInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);

            return record;
        }

        #endregion

        public static readonly DivisionProductsDALC Instance = new DivisionProductsDALC();


    }
}
