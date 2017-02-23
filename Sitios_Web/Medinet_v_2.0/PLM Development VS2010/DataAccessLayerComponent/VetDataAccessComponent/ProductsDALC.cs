using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetDataAccessComponent
{
    public sealed class ProductsDALC : VetDataAccessAdapter<ProductByEditionInfo>
    {
        #region Constructors

        private ProductsDALC() { }

        #endregion



        #region Public Methods

        public List<ProductByEditionInfo> getProductsByText(int editionId, string searchText)
        {
            List<ProductByEditionInfo> BeCollection = new List<ProductByEditionInfo>();
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProducts");

            // Add the parameters
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);


            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }


        }

        public List<ProductByEditionInfo> getByActiveSubstance(int editionId, int activeSubstanceId)
        {

            List<ProductByEditionInfo> BeCollection = new List<ProductByEditionInfo>();
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProducts");

            // Add the parameters
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            


            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }
        }

        public List<ProductByEditionInfo> getByTherapeutic(int editionId, int therapeuticId)

        {
            List<ProductByEditionInfo> BeCollection = new List<ProductByEditionInfo>();
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProducts");

            // Add the parameters
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@therapeuticId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);



            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }

        }

        public List<ProductByEditionInfo> getByDivision(int editionId, int divisionId)
        {

            List<ProductByEditionInfo> BeCollection = new List<ProductByEditionInfo>();
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProducts");

            // Add the parameters
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);



            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }

        }

        public List<ProductByEditionInfo> getBySpecie(int editionId, int specieId)
        {
            List<ProductByEditionInfo> BeCollection = new List<ProductByEditionInfo>();
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProducts");

            // Add the parameters
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@specieId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, specieId);



            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                while (dataReader.Read())
                {
                    BeCollection.Add(this.getFromDataReader(dataReader));
                }

                return BeCollection;
            }


        }

        public ProductDetailInfo getProductAttributes(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
 
            DbCommand dbCmd = ProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVETProductAttributes");
     



            // Add the parameters:
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);


   


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {

                if (dataReader.Read())
                    return this.getFromDataReaderDetail(dataReader);
                else
                    return null;
            }



        }

        #endregion


        #region Protected Methods

        protected override ProductByEditionInfo getFromDataReader(IDataReader current)
        {

            ProductByEditionInfo record = new ProductByEditionInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ProductName = current["ProductName"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();

            if (current["DivisionShortName"] != DBNull.Value)
                record.DivisionShortName = current["DivisionShortName"].ToString();

            else
                record.DivisionShortName = null;

            record.CategotyId = Convert.ToInt32(current["CategoryId"]);
            record.CategoryName = current["CategoryName"].ToString();

            return record;


        }

        protected ProductDetailInfo getFromDataReaderDetail(IDataReader current)
        {
            ProductDetailInfo record = new ProductDetailInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ProductName = current["ProductName"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.DivisionName = current["DivisionName"].ToString();

            return record;
        }


        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();



    }
}
