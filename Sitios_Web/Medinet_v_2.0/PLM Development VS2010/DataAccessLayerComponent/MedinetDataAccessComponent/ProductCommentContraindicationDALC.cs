using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class ProductCommentContraindicationDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductCommentContraindicationsInfo>
    {
        #region Constructors

        private ProductCommentContraindicationDALC() { }

        #endregion

        public List<ProductCommentContraindicationsInfo> getCommentsContraindications(int productId, int pharmaFormId, int categoryId, int divisionId)
        {
            DbCommand dbCmd = ProductCommentContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCommentContraindications");
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            List<MedinetBusinessEntries.ProductCommentContraindicationsInfo> BECollection = new List<MedinetBusinessEntries.ProductCommentContraindicationsInfo>();
            using (IDataReader dataReader = ProductCommentContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductCommentContraindicationsInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
                    record.ProductCommentId = Convert.ToInt32(dataReader["ProductCommentId"]);
                    record.Comments = dataReader["Comments"].ToString();
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

        public List<ProductCommentContraindicationsInfo> getCommentsContraindications(int productId, int pharmaFormId, int categoryId, int divisionId,int productCommentId)
        {
            DbCommand dbCmd = ProductCommentContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCommentContraindications");
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productCommentId", DbType.Int32,
         ParameterDirection.Input, string.Empty, DataRowVersion.Current, productCommentId);
            List<MedinetBusinessEntries.ProductCommentContraindicationsInfo> BECollection = new List<MedinetBusinessEntries.ProductCommentContraindicationsInfo>();
            using (IDataReader dataReader = ProductCommentContraindicationDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.ProductCommentContraindicationsInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
                    record.ProductCommentId = Convert.ToInt32(dataReader["ProductCommentId"]);
                    record.Comments = dataReader["Comments"].ToString();
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

        public override int insert(MedinetBusinessEntries.ProductCommentContraindicationsInfo businessEntity)
        {
            DbCommand dbCmd = ProductCommentContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCommentContraindications");
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@comments", DbType.String,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comments);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
         ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductCommentContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }
        public override void delete(MedinetBusinessEntries.ProductCommentContraindicationsInfo businessEntity)
        {
            DbCommand dbCmd = ProductCommentContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCommentContraindications");
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productCommentId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductCommentId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public void deleteAll(MedinetBusinessEntries.ProductCommentContraindicationsInfo businessEntity)
        {
            DbCommand dbCmd = ProductCommentContraindicationDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCommentContraindications");
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCommentContraindicationDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            
            ProductCommentContraindicationDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
        public static readonly ProductCommentContraindicationDALC Instance = new ProductCommentContraindicationDALC();
    }
}
