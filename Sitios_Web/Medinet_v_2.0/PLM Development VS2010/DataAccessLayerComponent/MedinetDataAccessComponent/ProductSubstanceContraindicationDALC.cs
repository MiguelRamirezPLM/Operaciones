using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;

namespace MedinetDataAccessComponent
{
    public class ProductSubstanceContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductSubstanceContraindicationInfo>
    {
         #region Constructors

        private ProductSubstanceContraindicationDALC() { }

        public List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> getContraindicationSubstances(int productId, int pharmaFormId, int categoryId, int divisionId)
        {
            DbCommand dbCmd = ProductSubstanceContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductSubstanceContraindications");
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> contraindications = new List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo>();
            using (IDataReader dataReader = ProductSubstanceContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductSubstanceContraindicationInfo record = new ProductSubstanceContraindicationInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceContraindicationId = Convert.ToInt32(dataReader["SubsContraindicationId"]);
                    record.SubstanceContraindication = dataReader["SubstanceContraindication"].ToString();

                    contraindications.Add(record);
                }

            }
            return contraindications;
        }

        public override int insert(MedinetBusinessEntries.ProductSubstanceContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstanceContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstanceContraindications");
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@subsContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceContraindicationId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductSubstanceContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstanceContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstanceContraindications");
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@subsContraindicationId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceContraindicationId);

            ProductSubstanceContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public void deleteAll(ProductSubstanceContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstanceContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstanceContraindications");
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            ProductSubstanceContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        public List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> getContraindicationSubstances(int productId, int pharmaFormId, int categoryId, int divisionId, int activeSubstanceId)
        {
            List<ProductSubstanceContraindicationInfo> contraindications = new List<ProductSubstanceContraindicationInfo>();

            DbCommand dbCmd = ProductSubstanceContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductSubstanceContraindications");
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@susbtanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductSubstanceContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@TypeSelect", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            using (IDataReader dataReader = ProductSubstanceContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductSubstanceContraindicationInfo record = new ProductSubstanceContraindicationInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceContraindicationId = Convert.ToInt32(dataReader["SubsContraindicationId"]);
                    record.SubstanceContraindication = dataReader["SubstanceContraindication"].ToString();

                    contraindications.Add(record);
                }

            }
            return contraindications;
        }
        #endregion
        public static readonly ProductSubstanceContraindicationDALC Instance = new ProductSubstanceContraindicationDALC();
    }
}
