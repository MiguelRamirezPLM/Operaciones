using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
namespace MedinetDataAccessComponent
{
   public class ProductSubstanceInteractionsDALC:MedinetDataAccessAdapter<MedinetBusinessEntries.ProductSubstanceInteractionsInfo>
    {
        #region Construtors

        private ProductSubstanceInteractionsDALC() { }

        #endregion
        #region Propierties
        public List<ProductSubstanceInteractionsInfo> getInteractions(int categoryId,int pharmaFormId,int productId,int divisionId) 
        {
            List<ProductSubstanceInteractionsInfo> interactions = new List<ProductSubstanceInteractionsInfo>();

            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductSubstanceInteractions");
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = ProductSubstanceInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductSubstanceInteractionsInfo record = new ProductSubstanceInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceInteractId = Convert.ToInt32(dataReader["SubstanceInteractId"]);
                    record.SubstanceInteraction = dataReader["SubstanceInteraction"].ToString();

                    interactions.Add(record);
                }
            
            }
            return interactions;
        }

        public List<ProductSubstanceInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            List<ProductSubstanceInteractionsInfo> interactions = new List<ProductSubstanceInteractionsInfo>();

            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductSubstanceInteractions");
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@susbtanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            using (IDataReader dataReader = PresentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductSubstanceInteractionsInfo record = new ProductSubstanceInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceInteractId = Convert.ToInt32(dataReader["SubstanceInteractId"]);
                    record.SubstanceInteraction = dataReader["SubstanceInteraction"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public List<ProductSubstanceInteractionsInfo> getInteractionsSubstances(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstanceId)
        {
            List<ProductSubstanceInteractionsInfo> interactions = new List<ProductSubstanceInteractionsInfo>();

            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductSubstanceInteractions");
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@susbtanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@TypeSelect", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            using (IDataReader dataReader = PresentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductSubstanceInteractionsInfo record = new ProductSubstanceInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceInteractId = Convert.ToInt32(dataReader["SubstanceInteractId"]);
                    record.SubstanceInteraction = dataReader["SubstanceInteraction"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public override int insert(ProductSubstanceInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstanceInteractions");

            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceInteractId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceInteractId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            
                ProductSubstanceInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
                return  Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }
       
       public override void delete(ProductSubstanceInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstanceInteractions");
            
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceInteractId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceInteractId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductSubstanceInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductSubstanceInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }
        #endregion
        public static readonly ProductSubstanceInteractionsDALC Instance = new ProductSubstanceInteractionsDALC();
    }
}
