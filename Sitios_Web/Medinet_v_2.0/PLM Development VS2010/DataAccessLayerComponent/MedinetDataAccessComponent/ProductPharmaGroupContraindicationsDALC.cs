using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductPharmaGroupContraindicationsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo>
    {
          #region Constructors
        
        private ProductPharmaGroupContraindicationsDALC() { }

        public List<ProductPharmaGroupContraindicationInfo> getProductPharmaGroupContraindications(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaGroupContraindications");
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<ProductPharmaGroupContraindicationInfo> BECollection = new List<ProductPharmaGroupContraindicationInfo>();
            using (IDataReader dataReader = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName= dataReader["GroupName"].ToString();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    BECollection.Add(record);
                }
            }
            return BECollection;
            
        }

        public List<ProductPharmaGroupContraindicationInfo> getProductPharmaGroupContraindications(int productId, int pharmaFormId, int categoryId, int divisionId, int pharmaGroupId)
        {
            DbCommand dbCmd = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaGroupContraindications");
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaGroupId);
            List<ProductPharmaGroupContraindicationInfo> BECollection = new List<ProductPharmaGroupContraindicationInfo>();
            using (IDataReader dataReader = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    BECollection.Add(record);
                }
            }
            return BECollection;

        }

        public override int insert(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaGroupContraindications");
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
         ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductPharmaGroupContraindicationInfo businessEntity)
        {
             DbCommand dbCmd = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaGroupContraindications");
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public  void deleteAll(ProductPharmaGroupContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaGroupContraindications");
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            
            ProductPharmaGroupContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        #endregion
        public static readonly ProductPharmaGroupContraindicationsDALC Instance = new ProductPharmaGroupContraindicationsDALC();
    }
}
