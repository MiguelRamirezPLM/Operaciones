using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductPharmaGroupInteractionsDALC:MedinetDataAccessAdapter<MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo>
    {
         #region Construtors

        private ProductPharmaGroupInteractionsDALC() { }

        #endregion

        #region Propierties
        public List<ProductPharmaGroupsInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            List<ProductPharmaGroupsInteractionsInfo> interactions = new List<ProductPharmaGroupsInteractionsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductPharmaGroupInteractions");
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductPharmaGroupsInteractionsInfo record = new ProductPharmaGroupsInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public List<ProductPharmaGroupsInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            List<ProductPharmaGroupsInteractionsInfo> interactions = new List<ProductPharmaGroupsInteractionsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductPharmaGroupInteractions");
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@susbtanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                { 
                    ProductPharmaGroupsInteractionsInfo record = new ProductPharmaGroupsInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public List<ProductPharmaGroupsInteractionsInfo> getInteractionsGroups(int categoryId, int pharmaFormId, int productId, int divisionId, int pharmaGroupId,int activeSubstanceId)
        {
            List<ProductPharmaGroupsInteractionsInfo> interactions = new List<ProductPharmaGroupsInteractionsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetProductPharmaGroupInteractions");
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaGroupId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@susbtanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId );
            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductPharmaGroupsInteractionsInfo record = new ProductPharmaGroupsInteractionsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public override int insert(ProductPharmaGroupsInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmacologicalGroupInteractions");

            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);

            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); 
        }

        public override void delete(ProductPharmaGroupsInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmacologicalGroupInteractions");

            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }
       
        #endregion
        public static readonly ProductPharmaGroupInteractionsDALC Instance = new ProductPharmaGroupInteractionsDALC();
    }
}
