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
    public sealed class SeedsDALC : DEAQEngineDataAccessAdapter<SeedInfo>
    {
        #region Constructor

        private SeedsDALC() { }

        #endregion


        #region Public Methods
        #region ROC Methods
        //Retrieves Information From Seed
        public SeedInfo rocGetSeed(int seedId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var seed = from seedRow in db.roc_spGetSeed(seedId)
                       select new SeedInfo
                       {
                           SeedId = seedRow.SeedId,
                           SeedName = seedRow.SeedName,
                           ScientificName = seedRow.ScientificName,
                           Active = seedRow.Active
                       };

            List<SeedInfo> seedInfo = seed.ToList();

            return seedInfo.Count > 0 ? seedInfo[0] : null;
        }

        //Retrieves All Information The Seeds By Page
        public List <SeedByPageInfo> rocGetSeeds(int numberByPage, int page)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var seeds = from seedsRow in db.roc_spGetSeeds(numberByPage, page)
                        select new SeedByPageInfo
                        {
                            Total = (int)seedsRow.TOTAL,
                            SeeId = seedsRow.SeedId,
                            SeedName = seedsRow.SeedName,
                            ScientificName = seedsRow.ScientificName,
                            Active = seedsRow.Active,
                            RowNumber = (int)seedsRow.RowNumber

                        };

            List<SeedByPageInfo> seedsInfo = seeds.ToList();

            return seedsInfo.Count > 0 ? seedsInfo : null;

        }


        //Retrieves All Information The Seeds By FullText
        public List<SeedByPageInfo> rocGetSeedsByFullText(int numberByPage, int page, string text)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var seedfull = from seedFullRow in db.roc_spGetSeedsByFullText(numberByPage, page, text)
                           select new SeedByPageInfo
                           {
                               Total = (int)seedFullRow.TOTAL,
                               SeeId = seedFullRow.SeedId,
                               SeedName = seedFullRow.SeedName,
                               ScientificName = seedFullRow.ScientificName,
                               Active = seedFullRow.Active,
                               RowNumber = (int)seedFullRow.RowNumber
                           };

            List<SeedByPageInfo> seedsFullInfo = seedfull.ToList();

            return seedsFullInfo.Count > 0 ? seedsFullInfo : null;
        }

        //Retrieves All Information The Seeds By Letter
        public List<SeedByPageInfo> rocGetSeedsByLetter(int numberByPage, int page, string letter)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var seedsLet = from seedLetRow in db.roc_spGetSeedsByLetter(numberByPage, page, letter)
                           select new SeedByPageInfo
                           {
                               Total = (int)seedLetRow.TOTAL,
                               SeeId = seedLetRow.SeedId,
                               SeedName = seedLetRow.SeedName,
                               ScientificName = seedLetRow.ScientificName,
                               Active = seedLetRow.Active,
                               RowNumber = (int)seedLetRow.RowNumber
                           };

            List<SeedByPageInfo> seedsLetInfo = seedsLet.ToList();

            return seedsLetInfo.Count > 0 ? seedsLetInfo : null;
        }

        //Retrieves All Information The Seeds By Text
        public List<SeedByPageInfo> rocGetSeedsByText(int numberByPage, int page, string text)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var seedsText = from seeTextRow in db.roc_spGetSeedsByText(numberByPage, page, text)
                            select new SeedByPageInfo
                            {
                                Total = (int)seeTextRow.TOTAL,
                                SeeId = seeTextRow.SeedId,
                                SeedName = seeTextRow.SeedName,
                                ScientificName = seeTextRow.ScientificName,
                                Active = seeTextRow.Active,
                                RowNumber = (int)seeTextRow.RowNumber
                            };

            List<SeedByPageInfo> seedsTextInfo = seedsText.ToList();

            return seedsTextInfo.Count > 0 ? seedsTextInfo : null;
        
        }

#endregion

        #region PLM Methods
        
        public List<SeedDetailInfo> getAll(string isbn)
        {
            DbCommand dbCmd = SeedsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSeeds");

            // Add the parameters:
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            List<SeedDetailInfo> BECollection = new List<SeedDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SeedsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public SeedDetailInfo getById(string isbn, int seedId)
        {
            DbCommand dbCmd = SeedsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSeeds");

            // Add the parameters:
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@seedId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, seedId);
            SeedDetailInfo BECollection = new SeedDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SeedsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection=(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<SeedDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = SeedsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSeedsByProduct");

            // Add the parameters:
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            List<SeedDetailInfo> BECollection = new List<SeedDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SeedsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<SeedDetailInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = SeedsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSeedsByText");

            // Add the parameters:
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            SeedsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<SeedDetailInfo> BECollection = new List<SeedDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SeedsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        #endregion
        #endregion

        #region Pretected Methods

        protected SeedDetailInfo getFromDataReader(IDataReader current)
        {
            SeedDetailInfo record = new SeedDetailInfo();

            record.SeedId = Convert.ToInt32(current["SeedId"]);
            record.SeedName = current["SeedName"].ToString();
            record.ScientificName = current["ScientificName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly SeedsDALC Instance = new SeedsDALC();
    }
}
