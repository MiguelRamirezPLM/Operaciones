using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductMedicalContraindicationsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductMedicalContraindicationInfo>
    {
        #region Constructors

        private ProductMedicalContraindicationsDALC() { }
        #endregion

        public List<ProductMedicalContraindicationInfo> getProductMedicalContraindications(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductMedicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductMedicalContraindications");
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductMedicalContraindicationInfo>();
            using (IDataReader dataReader = ProductMedicalContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductMedicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
                    record.MedicalContraindicationId = Convert.ToInt32(dataReader["MedicalContraindicationId"]);
                    record.MedicalContraindicationName = dataReader["MedicalContraindicationName"].ToString();
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

        public List<ProductMedicalContraindicationInfo> getProductMedicalContraindications(int productId, int pharmaFormId, int categoryId, int divisionId,int medicalContraindicationId)
        {
            DbCommand dbCmd = ProductMedicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductMedicalContraindications");
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@medicalContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, medicalContraindicationId);
            List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductMedicalContraindicationInfo>();
            using (IDataReader dataReader = ProductMedicalContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductMedicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
                    record.MedicalContraindicationId = Convert.ToInt32(dataReader["MedicalContraindicationId"]);
                    record.MedicalContraindicationName = dataReader["MedicalContraindicationName"].ToString();
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


        public override int insert(MedinetBusinessEntries.ProductMedicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductMedicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductMedicalContraindications");
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@medicalContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.MedicalContraindicationId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);

            ProductMedicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductMedicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductMedicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductMedicalContraindications");
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@medicalContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.MedicalContraindicationId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public  void deleteAll(ProductMedicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductMedicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductMedicalContraindications");
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductMedicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        
        public static readonly ProductMedicalContraindicationsDALC Instance = new ProductMedicalContraindicationsDALC();
    }
}
