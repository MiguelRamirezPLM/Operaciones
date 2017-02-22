using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
namespace MedinetDataAccessComponent
{
    public class ProductOtherInteractionsDALC:MedinetDataAccessAdapter<MedinetBusinessEntries.ProductOtherInteractionsInfo>
    {
        #region Construtors
        private ProductOtherInteractionsDALC() { }
        #endregion
        
        #region Propierties
        public List<ProductOtherInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            List<ProductOtherInteractionsInfo> interactions = new List<ProductOtherInteractionsInfo>();

            DbCommand dbCmd = ProductOtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductotherInteractions");
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = ProductOtherInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductOtherInteractionsInfo record = new ProductOtherInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName = dataReader["ElementName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }
        public List<ProductOtherInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            List<ProductOtherInteractionsInfo> interactions = new List<ProductOtherInteractionsInfo>();

            DbCommand dbCmd = ProductOtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductotherInteractions");
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            using (IDataReader dataReader = ProductOtherInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductOtherInteractionsInfo record = new ProductOtherInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName = dataReader["ElementName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public List<ProductOtherInteractionsInfo> getInteractionsElements(int categoryId, int pharmaFormId, int productId, int divisionId,int elementId, int activeSubstanceId)
        {
            List<ProductOtherInteractionsInfo> interactions = new List<ProductOtherInteractionsInfo>();

            DbCommand dbCmd = ProductOtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductotherInteractions");
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, elementId);
            using (IDataReader dataReader = ProductOtherInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductOtherInteractionsInfo record = new ProductOtherInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName = dataReader["ElementName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public override int insert(ProductOtherInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductOtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherElementInteractions");

            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementID", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductOtherInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductOtherInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductOtherElementInteractions");

            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementID", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductOtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

             
            ProductOtherInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        #endregion
        public static readonly ProductOtherInteractionsDALC Instance = new ProductOtherInteractionsDALC();
    }
}
