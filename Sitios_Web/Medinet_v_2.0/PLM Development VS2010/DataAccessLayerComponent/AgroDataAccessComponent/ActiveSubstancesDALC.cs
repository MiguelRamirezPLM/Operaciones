using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

using System.Linq;
namespace AgroDataAccessComponent
{
    public sealed class ActiveSubstancesDALC : AgroEngineDataAccessAdapter<ProductDetailByEditionInfo>
    {

        #region Constructor

        private ActiveSubstancesDALC() { }

        #endregion

        #region Public Methods
        
      
        
        #region PLM Methods


        public void insertForProduct(int productId, int agrochemicalUseId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstancesForProduct");

            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


            ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }


        public void deleteForProduct(int productId, int agrochemicalUseId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActiveSubstancesForProduct");

            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


            ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }





        public List<ActiveSubstanceInfo> getActiveSubstancesNotAssociated(int editionId, int divisionId, int bookId, int productId, int pharmaformId, string brand)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();
            DbCommand dbCmd = AgrochemicalUsesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstances");
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaformId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    ActiveSubstanceInfo record = new ActiveSubstanceInfo();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ActiveSubstanceName = dataReader["ActiveSubstanceName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<ActiveSubstanceInfo> getActiveSubstancesForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
        {
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesForProduct");
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    ActiveSubstanceInfo record = new ActiveSubstanceInfo();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.ActiveSubstanceName = dataReader["ActiveSubstanceName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }



        public List<ActiveSubstanceInfo> getAll(string isbn)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstance");

            // Add the parameters:
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);  
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public ActiveSubstanceInfo getById(string isbn , int activeSubstanceId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstance");

            // Add the parameters:
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@activesubstaceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ActiveSubstanceInfo BECollection = new ActiveSubstanceInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection = (this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ActiveSubstanceInfo> getByProduct(string isbn, int productId,int pharmaFormId,int divisionId,int categoryId )
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesByProduct");

            // Add the parameters:
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ActiveSubstanceInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesByText");

            // Add the parameters:
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        #endregion

        #endregion
        #region Pretected Methods

        protected ActiveSubstanceInfo getFromDataReader(IDataReader current)
        {
            ActiveSubstanceInfo record = new ActiveSubstanceInfo();

            record.Active = Convert.ToBoolean(current["Active"]);
            record.ActiveSubstanceName = current["ActiveSubstanceName"].ToString();
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            return record;
        }

        #endregion
       
        public static readonly ActiveSubstancesDALC Instance = new ActiveSubstancesDALC();
    }
}
