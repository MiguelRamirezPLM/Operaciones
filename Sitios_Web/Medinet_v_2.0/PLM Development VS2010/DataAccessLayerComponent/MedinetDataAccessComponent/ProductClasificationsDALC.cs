using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;


namespace MedinetDataAccessComponent
{
    public sealed class ProductClasificationsDALC : MedinetDataAccessAdapter<ProductClasificationInfo>
    {

        #region Constructors

        private ProductClasificationsDALC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductClasificationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductClasification");

            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            ProductClasificationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(ProductClasificationInfo businessEntity)
        {

            DbCommand dbCmd = ProductClasificationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductClasifications");

            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

            ProductClasificationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }


        public ProductClasificationInfo getOne(int productId, int editionId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductClasificationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductClasifications");

            // Add the parameters:
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductClasificationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ParticipantProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }


        public override void delete(ProductClasificationInfo businessEntity)
        {
            DbCommand dbCmd = ParticipantProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductClasifications");

            // Add the parameters:
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            ParticipantProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

            //Delete record:
            ParticipantProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }


        #endregion

        #region Protected Methods

        protected override ProductClasificationInfo getFromDataReader(IDataReader current)
        {
            ProductClasificationInfo record = new ProductClasificationInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);

            return record;
        }

        #endregion

        public static readonly ProductClasificationsDALC Instance = new ProductClasificationsDALC();

    }
}
