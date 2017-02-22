using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class ProductHypersensibilityContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo>
    {
        #region Constructors

        private ProductHypersensibilityContraindicationDALC() { }

        public List<ProductHypersensibilitiesContraindicationInfo> getProductHypersensibilities(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo>();
            using (IDataReader dataReader = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
                    record.HypersensibilityId = Convert.ToInt32(dataReader["HypersensibilityId"]);
                    record.HypersensibilityName = dataReader["HypersensibilityName"].ToString();
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

        public List<ProductHypersensibilitiesContraindicationInfo> getProductHypersensibilities(int productId, int pharmaFormId, int categoryId, int divisionId, int hypersensibilityId)
        {
            DbCommand dbCmd = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@hypersensibilityId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, hypersensibilityId);

            List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo>();
            using (IDataReader dataReader = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
                    record.HypersensibilityId = Convert.ToInt32(dataReader["HypersensibilityId"]);
                    record.HypersensibilityName = dataReader["HypersensibilityName"].ToString();
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

        public override int insert(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@hypersensibilityId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HypersensibilityId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
          ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductHypersensibilitiesContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@hypersensibilityId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HypersensibilityId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
           ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        public void deleteAll(ProductHypersensibilitiesContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductHypersensibilities");
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            

            ProductHypersensibilityContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        #endregion
        public static readonly ProductHypersensibilityContraindicationDALC Instance = new ProductHypersensibilityContraindicationDALC();
    }
}
