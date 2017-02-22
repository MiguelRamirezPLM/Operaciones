using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

using System.Linq;
namespace DEAQDataAccessComponent
{
    public sealed class ActiveSubstancesDALC : DEAQEngineDataAccessAdapter<ProductDetailByEditionInfo>
    {

        #region Constructor

        private ActiveSubstancesDALC() { }

        #endregion

        #region Public Methods
        
        #region ROC Methods
        //Retrieves information about ActiveSubstances By ActiveSubstanceId
        public DEAQBusinessEntries.ActiveSubstanceInfo rocGetActiveSubstance(int activeSubstanceId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstance(activeSubstanceId)
                                  select new DEAQBusinessEntries.ActiveSubstanceInfo
                                  {
                                      ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                      ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                      Active = activeSubstanceInfo.Active
                                  };

            List<DEAQBusinessEntries.ActiveSubstanceInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances[0] : null;
        }

        //Retrieves all ActiveSubstances By Edition and product
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByProductInfo> rocGetActiveSubstancesByProduct(int editionId, int productId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstancesByProduct(editionId, productId)
                             select new DEAQBusinessEntries.ROC.ActiveSubstanceByProductInfo
                             {
                                 ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                 ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                 CategoryId = activeSubstanceInfo.CategoryId,
                                 PharmaFormId = activeSubstanceInfo.PharmaFormId,
                                 DivisionId = activeSubstanceInfo.DivisionId
                             };

            List<DEAQBusinessEntries.ROC.ActiveSubstanceByProductInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances : null;
        }

        //Retrieves all ActiveSubstances By Edition
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstances(int numberByPage, int page, int editionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstances(numberByPage, page, editionId)
                             select new DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo
                             {
                                 ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                 ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                 RowNumber = (int)activeSubstanceInfo.RowNumber,
                                 Total = (int)activeSubstanceInfo.TOTAL
                             };

            List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances : null;
        }

        //Retrieves all ActiveSubstances By Edition and fulltext
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByFullText(int numberByPage, int page, int editionId, string fullText)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstancesByFullText(numberByPage, page, editionId, fullText)
                             select new DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo
                             {
                                 ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                 ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                 RowNumber = (int)activeSubstanceInfo.RowNumber,
                                 Total = (int)activeSubstanceInfo.TOTAL
                             };

            List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances : null;
        }

        //Retrieves all ActiveSubstances By Edition and letter
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByLetter(int numberByPage, int page, int editionId, string letter)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstancesByLetter(numberByPage, page, editionId, letter)
                             select new DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo
                             {
                                 ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                 ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                 RowNumber = (int)activeSubstanceInfo.RowNumber,
                                 Total = (int)activeSubstanceInfo.TOTAL
                             };

            List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances : null;
        }

        //Retrieves all ActiveSubstances By Edition and text
        public List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> rocGetActiveSubstancesByText(int numberByPage, int page, int editionId, string text)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var activeSubs = from activeSubstanceInfo in db.roc_spGetActiveSubstancesByText(numberByPage, page, editionId, text)
                             select new DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo
                             {
                                 ActiveSubstanceId = activeSubstanceInfo.ActiveSubstanceId,
                                 ActiveSubstanceName = activeSubstanceInfo.ActiveSubstanceName,
                                 RowNumber = (int)activeSubstanceInfo.RowNumber,
                                 Total = (int)activeSubstanceInfo.TOTAL
                             };

            List<DEAQBusinessEntries.ROC.ActiveSubstanceByTextInfo> activeSubstances = activeSubs.ToList();

            return activeSubstances.Count() > 0 ? activeSubstances : null;
        }
        #endregion     
        #region PLM Methods
       
        public List<ActiveSubstanceInfo> getAll(string isbn)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstance");

            // Add the parameters:
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);  
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public ActiveSubstanceInfo getById(string isbn , int activeSubstanceId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstance");

            // Add the parameters:
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@activesubstaceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            ActiveSubstanceInfo BECollection = new ActiveSubstanceInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection = (this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ActiveSubstanceInfo> getByProduct(string isbn, int productId,int pharmaFormId,int divisionId,int categoryId )
        {
            DbCommand dbCmd = ActiveSubstancesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesByProduct");

            // Add the parameters:
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<ActiveSubstanceInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActiveSubstancesByText");

            // Add the parameters:
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            ActiveSubstancesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<ActiveSubstanceInfo> BECollection = new List<ActiveSubstanceInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActiveSubstancesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
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
