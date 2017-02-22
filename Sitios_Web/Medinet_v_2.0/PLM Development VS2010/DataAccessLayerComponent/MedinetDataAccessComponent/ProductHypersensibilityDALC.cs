using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class ProductHypersensibilityDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductHypersensibilitiesInfo>
    {
        #region Constructors

        private ProductHypersensibilityDALC() { }

        public List<HypersensibilitiesInfo> getProductHypersensibilities(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductHypersensibilityDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.HypersensibilitiesInfo> BECollection = new List<MedinetBusinessEntries.HypersensibilitiesInfo>();
            using (IDataReader dataReader = ProductHypersensibilityDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.HypersensibilitiesInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.HypersensibilitiesInfo();
                    record.HypersensibilityId = Convert.ToInt32(dataReader["HypersensibilityId"]);
                    record.HypersensibilityName = dataReader["HypersensibilityName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public override int insert(MedinetBusinessEntries.ProductHypersensibilitiesInfo businessEntity)
        {
            DbCommand dbCmd = ProductHypersensibilityDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@hypersensibilityId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HypersensibilityId);
            ProductHypersensibilityDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
            return 0;
        }

        public override void delete(ProductHypersensibilitiesInfo businessEntity)
        {
            DbCommand dbCmd = ProductHypersensibilityDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductHypersensibilityDALC.MedInstanceDatabase.AddParameter(dbCmd, "@hypersensibilityId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HypersensibilityId);
            ProductHypersensibilityDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        #endregion
        public static readonly ProductHypersensibilityDALC Instance = new ProductHypersensibilityDALC();
    }
}
