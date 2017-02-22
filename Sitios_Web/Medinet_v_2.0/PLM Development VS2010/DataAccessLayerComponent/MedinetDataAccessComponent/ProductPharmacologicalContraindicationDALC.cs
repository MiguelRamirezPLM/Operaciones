using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductPharmacologicalContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo>
    {
         #region Constructors

        private ProductPharmacologicalContraindicationDALC() { }

        public List<ProductPharmacologicalContraindicationInfo> getProductPharmacologicalContraindications(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaContraindications");
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo>();
            using (IDataReader dataReader = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
                    record.PharmaContraindicationId = Convert.ToInt32(dataReader["PharmaContraindicationId"]);
                    record.PharmaContraindicationName = dataReader["PharmaContraindicationName"].ToString();
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

        public List<ProductPharmacologicalContraindicationInfo> getProductPharmacologicalContraindications(int productId, int pharmaFormId, int categoryId, int divisionId, int pharmaContraindicationId)
        {
            DbCommand dbCmd = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaContraindications");
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaContraindicationId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaContraindicationId);
            List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo>();
            using (IDataReader dataReader = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
                    record.PharmaContraindicationId = Convert.ToInt32(dataReader["PharmaContraindicationId"]);
                    record.PharmaContraindicationName = dataReader["PharmaContraindicationName"].ToString();
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

        public override int insert(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaContraindications");
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaContraindicationId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductPharmacologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaContraindications");
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaContraindicationId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public void deleteAll(ProductPharmacologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaContraindications");
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            

            ProductPharmacologicalContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        #endregion
        public static readonly ProductPharmacologicalContraindicationDALC Instance = new ProductPharmacologicalContraindicationDALC();
    }
}
