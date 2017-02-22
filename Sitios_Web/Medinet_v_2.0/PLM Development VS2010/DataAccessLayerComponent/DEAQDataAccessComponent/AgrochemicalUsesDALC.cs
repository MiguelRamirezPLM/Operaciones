using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using System.Linq;
using AgroBusinessEntries;

namespace DEAQDataAccessComponent
{
    public class AgrochemicalUsesDALC : DEAQEngineDataAccessAdapter<DEAQBusinessEntries.AgrochemicalUseInfo>
    {

        #region Constructor

        private AgrochemicalUsesDALC() { }

        #endregion

        #region Public Methods

        #region ROC Methods
        //Retrieves information about AgrochemicalUse by UseId
        public DEAQBusinessEntries.AgrochemicalUseInfo rocGetUse(int agrochemicalUseId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUse(agrochemicalUseId)
                          select new DEAQBusinessEntries.AgrochemicalUseInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              UseKey = agrUseInfo.UseKey,
                              Active = agrUseInfo.Active
                          };

            List<DEAQBusinessEntries.AgrochemicalUseInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses[0] : null;
        }

        //Retrieves all AgrochemicalUses by Edition and ParentId
        public List<DEAQBusinessEntries.AgrochemicalUseInfo> rocGetUseByParent(int editionId, int parentId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUseByParent(editionId, parentId)
                          select new DEAQBusinessEntries.AgrochemicalUseInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              UseKey = agrUseInfo.UseKey,
                              Active = agrUseInfo.Active
                          };

            List<DEAQBusinessEntries.AgrochemicalUseInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses : null;
        }

        //Retrieves all AgrochemicalUses by Edition
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUses(int editionId, int numberByPage, int page)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUses(editionId, numberByPage, page)
                          select new DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              RowNumber = (int)agrUseInfo.RowNumber,
                              Total = (int)agrUseInfo.TOTAL
                          };

            List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses : null;
        }

        //Retrieves all AgrochemicalUses by Edition and FullText
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByFullText(int editionId, int numberByPage, int page, string fullText)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUsesByFullText(editionId, numberByPage, page, fullText)
                          select new DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              RowNumber = (int)agrUseInfo.RowNumber,
                              Total = (int)agrUseInfo.TOTAL
                          };

            List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses : null;
        }

        //Retrieves all AgrochemicalUses by Edition and letter
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByLetter(int editionId, int numberByPage, int page, string letter)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUsesByLetter(editionId, numberByPage, page, letter)
                          select new DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              RowNumber = (int)agrUseInfo.RowNumber,
                              Total = (int)agrUseInfo.TOTAL
                          };

            List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses : null;
        }

        //Retrieves all AgrochemicalUses by Edition and text
        public List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> rocGetUsesByText(int editionId, int numberByPage, int page, string text)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var agrUses = from agrUseInfo in db.roc_spGetUsesByText(editionId, numberByPage, page, text)
                          select new DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo
                          {
                              AgrochemicalUseId = agrUseInfo.AgrochemicalUseId,
                              AgrochemicalUseName = agrUseInfo.AgrochemicalUseName,
                              RowNumber = (int)agrUseInfo.RowNumber,
                              Total = (int)agrUseInfo.TOTAL
                          };

            List<DEAQBusinessEntries.ROC.AgrochemicalUseByTextInfo> agrochemicalUses = agrUses.ToList();

            return agrochemicalUses.Count() > 0 ? agrochemicalUses : null;
        }

        #endregion

        #region PLM Methods

        public List<AgrochemicalUseDetailInfo> getAll(string isbn)
        {
            DbCommand dbCmd = AgrochemicalUsesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUses");

            // Add the parameters:
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            List<AgrochemicalUseDetailInfo> BECollection = new List<AgrochemicalUseDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgrochemicalUsesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public AgrochemicalUseDetailInfo getById(string isbn, int useId)
        {
            DbCommand dbCmd = AgrochemicalUsesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUses");

            // Add the parameters:
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "useId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, useId);
            AgrochemicalUseDetailInfo BECollection = new AgrochemicalUseDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgrochemicalUsesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection =(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<AgrochemicalUseDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = AgrochemicalUsesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsesByProduct");

            // Add the parameters:
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            List<AgrochemicalUseDetailInfo> BECollection = new List<AgrochemicalUseDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgrochemicalUsesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<AgrochemicalUseDetailInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = AgrochemicalUsesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUsesByText");

            // Add the parameters:
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            AgrochemicalUsesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<AgrochemicalUseDetailInfo> BECollection = new List<AgrochemicalUseDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgrochemicalUsesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #endregion

        #region Pretected Methods

        protected AgrochemicalUseDetailInfo getFromDataReader(IDataReader current)
        {
            AgrochemicalUseDetailInfo record = new AgrochemicalUseDetailInfo();

            record.AgrochemicalUseId = Convert.ToInt32(current["AgrochemicalUseId"]);
            record.AgrochemicalUseName = current["AgrochemicalUseName"].ToString();
            record.UseKey = current["UseKey"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion
        public static readonly AgrochemicalUsesDALC Instance = new AgrochemicalUsesDALC();

    }
}
