using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;
using System.Data;
using System.Data.Common;

namespace EncyclopediasDataAccessComponent
{
    public class ProductEncyclopediaDALC : EncyclopediasDataAccessAdapter<ProductEncyclopediasInfo>
    {
        #region Constructors

        private ProductEncyclopediaDALC() { }

        #endregion

        #region public

        public List<ProductEncyclopediasInfo> getProductsByEncyclopedia(int encyclopediaId)
        {
            DbCommand dbCmd = ProductEncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByEncyclopedia");
            List<ProductEncyclopediasInfo> bCollection = new List<ProductEncyclopediasInfo>();
            // Add the parameters:
            ProductEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductEncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        public List<ProductEncyclopediasInfo> getProductsByEncyclopediaByEdition(int encyclopediaId,string isbn)
        {
            DbCommand dbCmd = ProductEncyclopediaDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductsByEncyclopedia");
            List<ProductEncyclopediasInfo> bCollection = new List<ProductEncyclopediasInfo>();
            // Add the parameters:
            ProductEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@encyclopediaId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, encyclopediaId);
            ProductEncyclopediaDALC.MedInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductEncyclopediaDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    bCollection.Add(this.getFromDataReader(dataReader));
            }
            return bCollection;
        }

        #endregion

        #region protected
        protected override ProductEncyclopediasInfo getFromDataReader(IDataReader current)
        {
            ProductEncyclopediasInfo productEncyclopedia = new ProductEncyclopediasInfo();

            productEncyclopedia.Brand = current["Brand"].ToString();
            productEncyclopedia.ProductId = Convert.ToInt32(current["ProductId"]);
            productEncyclopedia.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            productEncyclopedia.PharmaForm = current["PharmaForm"].ToString();
            productEncyclopedia.CategoryId = Convert.ToInt32(current["CategoryId"]);
            productEncyclopedia.CategoryName = current["CategoryName"].ToString();
            productEncyclopedia.DivisionId = Convert.ToInt32(current["DivisionId"]);
            productEncyclopedia.DivisionName = current["DivisionName"].ToString();
            productEncyclopedia.DivisionShortName = current["DivisionShortName"].ToString();

            return productEncyclopedia;
        }
        #endregion


        public static readonly ProductEncyclopediaDALC Instance = new ProductEncyclopediaDALC();
    }
}
