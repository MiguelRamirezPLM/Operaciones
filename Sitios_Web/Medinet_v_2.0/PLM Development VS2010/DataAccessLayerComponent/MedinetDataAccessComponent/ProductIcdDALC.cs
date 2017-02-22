using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
     public sealed class ProductIcdDALC : MedinetDataAccessAdapter<ProductICDInfo>
    {
         #region Constructors

         private ProductIcdDALC() { }

        #endregion

         #region Public methods

         public override int insert(ProductICDInfo businessEntity)
         {
             DbCommand dbCmd = ProductIcdDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductICD");

             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                 ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductIcdDALC.CRUD.Create);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ICDId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaformID", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
             ProductIcdDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

             return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
         }

         public override void delete(ProductICDInfo businessEntity)
         {
             DbCommand dbCmd = ProductIcdDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductICD");

             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                 ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductIcdDALC.CRUD.Delete);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ICDId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaformID", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

             ProductIcdDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
         }

         public MedinetBusinessEntries.ProductICDInfo getICDByIDByProduct(int icdId, int productId, int pharmaFormId)
         {
             DbCommand dbCmd = ProductIcdDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductICD");

             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                 ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductIcdDALC.CRUD.Read);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@icdId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, icdId);
             ProductIcdDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaformID", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

             // Get the result set from the stored procedure:
             using (IDataReader dataReader = ProductIcdDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
             {
                 if (dataReader.Read())
                     return this.getFromDataReader(dataReader);
                 else
                     return null;
             }
         }

         #endregion

         #region Protected methods

         protected override ProductICDInfo getFromDataReader(IDataReader current)
         {
             ProductICDInfo record = new ProductICDInfo();

             record.ProductId = Convert.ToInt32(current["ProductId"]);
             record.ICDId = Convert.ToInt32(current["ICDId"]);
             
             return record;
         }

         #endregion
         public static readonly ProductIcdDALC Instance = new ProductIcdDALC();
     }
}
