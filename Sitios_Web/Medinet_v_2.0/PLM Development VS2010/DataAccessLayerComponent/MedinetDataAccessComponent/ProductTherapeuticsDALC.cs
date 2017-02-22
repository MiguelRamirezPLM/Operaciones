using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ProductTherapeuticsDALC : MedinetDataAccessAdapter<ProductTherapeuticInfo>
    {
        #region Constructors

        private ProductTherapeuticsDALC() { }

        #endregion

        #region Public methods

        public override int insert(ProductTherapeuticInfo businessEntity)
        {
            DbCommand dbCmd = ProductTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductTherapeutics");

            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductTherapeuticsDALC.CRUD.Create);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TherapeuticId);

            ProductTherapeuticsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(ProductTherapeuticInfo businessEntity)
        {
            DbCommand dbCmd = ProductTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductTherapeutics");

            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductTherapeuticsDALC.CRUD.Delete);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TherapeuticId);

            ProductTherapeuticsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public MedinetBusinessEntries.ProductTherapeuticInfo getTherapeuticByProduct(int therapeuticId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductTherapeutics");

            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductTherapeuticsDALC.CRUD.Read);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductTherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override ProductTherapeuticInfo getFromDataReader(IDataReader current)
        {
            ProductTherapeuticInfo record = new ProductTherapeuticInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.TherapeuticId = Convert.ToInt32(current["TherapeuticId"]);
           
            return record;
        }

        #endregion

        public static readonly ProductTherapeuticsDALC Instance = new ProductTherapeuticsDALC();
    }
}
