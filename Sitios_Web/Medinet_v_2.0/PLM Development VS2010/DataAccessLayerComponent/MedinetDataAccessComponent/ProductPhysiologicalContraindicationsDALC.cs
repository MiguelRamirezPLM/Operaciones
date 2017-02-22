using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductPhysiologicalContraindicationsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo>
    {
         #region Constructors

        private ProductPhysiologicalContraindicationsDALC() { }

        public List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> getProductPhysContraindications(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPhysContraindications");
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo>();
            using (IDataReader dataReader = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
                    record.PhysContraindicationId = Convert.ToInt32(dataReader["PhysContraindicationId"]);
                    record.PhysContraindicationName = dataReader["PhysContraindicationName"].ToString();
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

        public List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> getProductPhysContraindications(int productId, int pharmaFormId, int categoryId, int divisionId, int physContraindicationId)
        {
            DbCommand dbCmd = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPhysContraindications");
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@physContraindicationId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, physContraindicationId);
            List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo>();
            using (IDataReader dataReader = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
                    record.PhysContraindicationId = Convert.ToInt32(dataReader["PhysContraindicationId"]);
                    record.PhysContraindicationName = dataReader["PhysContraindicationName"].ToString();
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

        public override int insert(MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPhysContraindications");
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@physContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PhysContraindicationId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductPhysiologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPhysContraindications");
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@physContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PhysContraindicationId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }
        public void deleteAll(ProductPhysiologicalContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPhysContraindications");
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            

            ProductPhysiologicalContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        #endregion
        public static readonly ProductPhysiologicalContraindicationsDALC Instance = new ProductPhysiologicalContraindicationsDALC();
    }
}
