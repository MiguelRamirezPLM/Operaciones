using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ProductOMSTherapeuticsDALC : MedinetDataAccessAdapter<ProductTherapeuticOMSInfo>
    {
    #region Constructors

        private ProductOMSTherapeuticsDALC() { }

        #endregion

        #region Public methods

        public override int insert(ProductTherapeuticOMSInfo businessEntity)
        {
            DbCommand dbCmd = ProductOMSTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOMSTherapeutics");

            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductOMSTherapeuticsDALC.CRUD.Create);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticOMSId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TherapeuticOMSId);

            ProductOMSTherapeuticsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(ProductTherapeuticOMSInfo businessEntity)
        {
            DbCommand dbCmd = ProductOMSTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOMSTherapeutics");

            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductOMSTherapeuticsDALC.CRUD.Delete);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticOMSId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TherapeuticOMSId);

            ProductOMSTherapeuticsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public MedinetBusinessEntries.ProductTherapeuticOMSInfo getTherapeuticByProduct(int therapeuticOMSId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductOMSTherapeuticsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOMSTherapeutics");

            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductOMSTherapeuticsDALC.CRUD.Read);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticOMSId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticOMSId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductOMSTherapeuticsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductOMSTherapeuticsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override ProductTherapeuticOMSInfo getFromDataReader(IDataReader current)
        {
            ProductTherapeuticOMSInfo record = new ProductTherapeuticOMSInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.TherapeuticOMSId = Convert.ToInt32(current["TherapeuticOMSId"]);
           
            return record;
        }

        #endregion

        public static readonly ProductOMSTherapeuticsDALC Instance = new ProductOMSTherapeuticsDALC();
    }
}
