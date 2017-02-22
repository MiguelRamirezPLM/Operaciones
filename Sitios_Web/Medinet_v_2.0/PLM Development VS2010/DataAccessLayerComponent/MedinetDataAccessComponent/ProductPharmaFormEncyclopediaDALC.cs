using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductPharmaFormEncyclopediaDALC : MedinetDataAccessAdapter<ProductPharmaFormEncyclopediaInfo>
    {
        #region Constructors

        private ProductPharmaFormEncyclopediaDALC() { }

        #endregion

        #region Public Methods


        public override int insert(ProductPharmaFormEncyclopediaInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaFormEncyclopedias");

            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductPharmaFormEncyclopediaDALC.CRUD.Create);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EncyclopediaId);

            ProductIcdDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return 1;
        }

        public override void delete(ProductPharmaFormEncyclopediaInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaFormEncyclopedias");

            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductPharmaFormEncyclopediaDALC.CRUD.Delete);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaFormEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EncyclopediaId);

            ProductIcdDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

       

        #endregion
        public static readonly ProductPharmaFormEncyclopediaDALC Instance = new ProductPharmaFormEncyclopediaDALC();
    }
}
