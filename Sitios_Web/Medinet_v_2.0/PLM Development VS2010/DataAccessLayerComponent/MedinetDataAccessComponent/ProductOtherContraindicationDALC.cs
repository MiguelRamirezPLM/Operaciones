using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class ProductOtherContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductOtherContraindicationInfo>
    {
         #region Constructors

        private ProductOtherContraindicationDALC() { }

        public List<ProductOtherContraindicationInfo> getProductOtherContraindications(int productId,int pharmaFormId,int categoryId,int divisionId)
        {
            DbCommand dbCmd = ProductOtherContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherContraindications");
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductOtherContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductOtherContraindicationInfo>();
            using (IDataReader dataReader = ProductOtherContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductOtherContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName= dataReader["ElementName"].ToString();
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

        public List<ProductOtherContraindicationInfo> getProductOtherContraindications(int productId, int pharmaFormId, int categoryId, int divisionId,int elementId)
        {
            DbCommand dbCmd = ProductOtherContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherContraindications");
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
           ParameterDirection.Input, string.Empty, DataRowVersion.Current, elementId);
            List<MedinetBusinessEntries.ProductOtherContraindicationInfo> BECollection = new List<MedinetBusinessEntries.ProductOtherContraindicationInfo>();
            using (IDataReader dataReader = ProductOtherContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductOtherContraindicationInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName = dataReader["ElementName"].ToString();
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

        public override int insert(MedinetBusinessEntries.ProductOtherContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductOtherContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherContraindications");
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
         ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductOtherContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductOtherContraindicationInfo businessEntity)
        {
          DbCommand dbCmd = ProductOtherContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherContraindications");
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
         ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public void deleteAll(ProductOtherContraindicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductOtherContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherContraindications");
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOtherContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            
            ProductOtherContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        #endregion
        public static readonly ProductOtherContraindicationDALC Instance = new ProductOtherContraindicationDALC();

    }
}
