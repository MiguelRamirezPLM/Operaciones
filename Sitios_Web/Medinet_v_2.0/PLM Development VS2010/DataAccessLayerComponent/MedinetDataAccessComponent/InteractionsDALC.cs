using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class InteractionsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.ProductInteractionsDetailInfo>
    {
        #region Constructors

        private InteractionsDALC() { }

        #endregion

        #region Public Methods

        #region Products

        public void getSubstanceInteractionsByProduct(int categoryId, int pharmaFormId, int productId, int divisionId, string guid)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSubstanceInteractionsByProduct");


            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);

            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);  
        }

        public void getGroupInteractionsByProduct(int categoryId, int pharmaFormId, int productId, int divisionId, string guid)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetGroupInteractionsByProduct");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);
            
            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public void getOtherInteractionsByProduct(int categoryId, int pharmaFormId, int productId, int divisionId, string guid)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOtherInteractionsByProduct");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);
            
            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ProductInteractionsDetailInfo> getProductByInteractions(string guid)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductByInteractions");

            List<ProductInteractionsDetailInfo> BECollection = new List<ProductInteractionsDetailInfo>();

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);

            using (IDataReader dataReader = InteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReaderInt(dataReader));
            }
            return BECollection;
        }

        public List<InteractionSubstanceProductsInfo> getSubstanceInteractionByProducts(string guid, int countryId)
        {
            List<InteractionSubstanceProductsInfo> interactions = new List<InteractionSubstanceProductsInfo>();

            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetSubstanceInteractionsByProducts");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            using (IDataReader dataReader = ProductSubstanceInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    InteractionSubstanceProductsInfo record = new InteractionSubstanceProductsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceInteractId = Convert.ToInt32(dataReader["SubstanceInteractId"]);
                    record.SubstanceInteraction = dataReader["SubstanceInteraction"].ToString();
                    record.IActiveSubstance = dataReader["IActiveSubstance"].ToString();
                    record.IActiveSubstanceId = Convert.ToInt32(dataReader["IActiveSubstanceId"]);
                    record.ICategoryId = Convert.ToInt32(dataReader["ICategoryId"]);
                    record.IDivisionId = Convert.ToInt32(dataReader["IDivisionId"]);
                    record.IPharmaFormId = Convert.ToInt32(dataReader["IPharmaFormId"]);
                    record.IProductId = Convert.ToInt32(dataReader["IProductId"]);
                    record.IBrand = dataReader["IBrand"].ToString();

                    interactions.Add(record);
                }
            }
            return interactions;
        }

        public List<InteractionGroupProductsInfo> getGroupInteractionsByProducts(string guid)
        {
            List<InteractionGroupProductsInfo> interactions = new List<InteractionGroupProductsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetGroupInteractionsByProducts");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);

            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    InteractionGroupProductsInfo record = new InteractionGroupProductsInfo();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm  = dataReader["PharmaForm"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.IPharmaGroupId = Convert.ToInt32(dataReader["IPharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();
                    record.ActiveSubstanceIdGI = Convert.ToInt32(dataReader["ActiveSubstanceIdGI"]);
                    record.IBrand = dataReader["IBrand"].ToString();
                    record.IProductId = Convert.ToInt32(dataReader["IProductId"]);
                    record.IDivisionId = Convert.ToInt32(dataReader["IDivisionId"]);
                    record.ICategoryId = Convert.ToInt32(dataReader["ICategoryId"]);
                    record.IPharmaFormId = Convert.ToInt32(dataReader["IPharmaFormId"]);
                    record.IPharmaForm  = dataReader["IPharmaForm"].ToString();
                    record.IActiveSubstanceId = Convert.ToInt32(dataReader["IActiveSubstanceId"]);
                    record.IActiveSubstance = dataReader["IActiveSubstance"].ToString();

                    interactions.Add(record);
                }
            }
            return interactions;
        }

        public List<ProductOtherInteractionsInfo> getOtherInteractionsByProducts(string guid)
        {
            List<ProductOtherInteractionsInfo> interactions = new List<ProductOtherInteractionsInfo>();

            DbCommand dbCmd = ProductOtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetOtherInteractionsByProducts");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);

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

        public List<ProductPresentationsInfo> getSubstitutesByProducts(int activeSubstanceId, int countryId)
        {
            List<ProductPresentationsInfo> substitutes = new List<ProductPresentationsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetSubstitutesBySubstance");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductPresentationsInfo record = new ProductPresentationsInfo();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.Presentation = dataReader["Presentation"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();

                    substitutes.Add(record);
                }
            }
            return substitutes;
        }

        public void deleteInteractionsByProduct(string guid)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spDeleteInteractionsByProduct");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@guid", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, guid);

            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Substances

        public void insert(MedinetBusinessEntries.ActiveSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDtmpActSubsInteractions");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Description);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@englishDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EnglishDescription);

            ActiveSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public void delete(MedinetBusinessEntries.ActiveSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDtmpActSubsInteractions");

            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ActiveSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ActiveSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ActiveSubstanceInfo> getAllSusbtancesInteractions()
        {
            DbCommand dbCmd = ActiveSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllSusbtancesInteractions");

            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            using (IDataReader dataReader = ActiveSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ActiveSubstanceInfo record = new ActiveSubstanceInfo();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]); 
                    record.Description = dataReader["Description"].ToString();
                    record.EnglishDescription = dataReader["EnglishDescription"].ToString();
                    //record.Active = Convert.ToBoolean(dataReader["Active"]);
                    //record.Enunciative = Convert.ToBoolean(dataReader["Enunciative"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public void getSubstancesInteractionsBySubstances(int activeSubstanceId)
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSubstanceInteractionBySubstance");

            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<InteractionSubstanceProductsInfo> getSubstanceInteractionBySubstances()
        {
            List<InteractionSubstanceProductsInfo> interactions = new List<InteractionSubstanceProductsInfo>();

            DbCommand dbCmd = ProductSubstanceInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetSubstanceInteractionBySubstances");

            using (IDataReader dataReader = ProductSubstanceInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    InteractionSubstanceProductsInfo record = new InteractionSubstanceProductsInfo();
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.SubstanceInteractId = Convert.ToInt32(dataReader["SubstanceInteractId"]);
                    record.SubstanceInteraction = dataReader["SubstanceInteraction"].ToString();
                    record.IBrand = dataReader["Brand"].ToString();

                    interactions.Add(record);
                }
            }
            return interactions;
        }

        public List<ProductPresentationsInfo> getSubstitutesBySubstances(int activeSubstanceId)
        {
            List<ProductPresentationsInfo> substitutes = new List<ProductPresentationsInfo>();

            DbCommand dbCmd = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetSubstitutesBySubstance");

            // Add the parameters:
            InteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            using (IDataReader dataReader = ProductPharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    ProductPresentationsInfo record = new ProductPresentationsInfo();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.Presentation = dataReader["Presentation"].ToString();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.ActiveSubstance = dataReader["ActiveSubstance"].ToString();

                    substitutes.Add(record);
                }
            }
            return substitutes;
        }
        
        public void deleteInteractionsBySubstance()
        {
            DbCommand dbCmd = InteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spDeleteInteractionsBySubstance");

            InteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #endregion

        #region ProtectedMethods

        protected ProductInteractionsDetailInfo getFromDataReaderInt(IDataReader current)
        {
            ProductInteractionsDetailInfo record = new ProductInteractionsDetailInfo();

            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaForm = current["PharmaForm"].ToString();
            if (current["PlainContent"] != System.DBNull.Value)
                record.PlainContent = current["PlainContent"].ToString();
            if (current["HTMLContent"] != System.DBNull.Value)
                record.HTMLContent = current["HTMLContent"].ToString();
            
            return record;
        }
       
        #endregion     

        public static readonly InteractionsDALC Instance = new InteractionsDALC();
    }
}