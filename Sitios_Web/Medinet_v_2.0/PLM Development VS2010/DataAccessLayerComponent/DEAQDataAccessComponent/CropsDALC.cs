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
    public class CropsDALC : DEAQEngineDataAccessAdapter<CropInfo>
    {
        #region Constructor

        private CropsDALC() { }

        #endregion

        #region Public Methods

        #region ROC Methods
        //Retrieves All Information From Crops
        public CropInfo rocGetCrop(int cropId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var crop = from cropRow in db.roc_spGetCrop(cropId)
                       select new CropInfo
                       {
                           CropId = cropRow.CropId,
                           CropName = cropRow.CropName,
                           Active = cropRow.Active
                       };

            List<CropInfo> cropInfo = crop.ToList();

            return cropInfo.Count > 0 ? cropInfo[0] : null;
        }

         //Retrieves Information From Crops By ProuctId
        public List<CropInfo> rocGetCropsByProductId(int productId, int editionId)
        {
            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var crops = from cropRow in db.roc_spGetCropsByProductId(productId, editionId)
                       select new CropInfo
                       {
                           CropId = cropRow.CropId,
                           CropName = cropRow.CropName
                       };

            List<CropInfo> cropsInfo = crops.ToList();

            return cropsInfo.Count > 0 ? cropsInfo : null;
        }

        //Retrives Information From Crops By Edition
        public List<CropByEditionInfo> rocGetCrops(int numberByPage, int page, int editionId)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var crops = from cropsRow in db.roc_spGetCrops(numberByPage, page, editionId)
                        select new CropByEditionInfo
                        {
                            Total = (int)cropsRow.TOTAL,
                            CropId = cropsRow.CropId,
                            CropName = cropsRow.CropName,
                            RowNumber =(int)cropsRow.RowNumber
                        };

            List<CropByEditionInfo> cropsInfo = crops.ToList();

            return cropsInfo.Count > 0 ? cropsInfo : null; 

        }

        //Retrives Information From Crops By Fulltext
        public List<CropByEditionInfo> rocGetCropsByFullText(int numberByPage, int page, int editionId, string text)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var cropFull = from cropsRow in db.roc_spGetCropsByFullText(numberByPage, page, editionId, text)
                           select new CropByEditionInfo
                           {
                               Total = (int)cropsRow.TOTAL,
                               CropId = cropsRow.CropId,
                               CropName = cropsRow.CropName,
                               RowNumber = (int)cropsRow.RowNumber
                           };

            List<CropByEditionInfo> cropFullInfo = cropFull.ToList();

            return cropFullInfo.Count > 0 ? cropFullInfo : null;
        }

        //Retrieves Information From Crops By Letter
        public List<CropByEditionInfo> rocGetCropsByLetter(int numberByPage, int page, int editionId, string letter)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var cropLetter = from cropLetRow in db.roc_spGetCropsByLetter(numberByPage, page, editionId, letter)
                             select new CropByEditionInfo
                             {
                                 Total = (int)cropLetRow.TOTAL,
                                 CropId = cropLetRow.CropId,
                                 CropName = cropLetRow.CropName,
                                 RowNumber = (int)cropLetRow.RowNumber
                             };

            List<CropByEditionInfo> cropLetInfo = cropLetter.ToList();

            return cropLetInfo.Count > 0 ? cropLetInfo : null;
        }

        //Retrieves Information From Crops By Text
        public List<CropByEditionInfo> rocGetCropBtText(int numberByPage, int page, int editionId, string text)
        {

            DEAQDataClassesDataContext db = new DEAQDataClassesDataContext();

            var cropText = from cropTextRow in db.roc_spGetCropsByText(numberByPage, page, editionId, text)
                           select new CropByEditionInfo
                           {
                               Total = (int)cropTextRow.TOTAL,
                               CropId = cropTextRow.CropId,
                               CropName = cropTextRow.CropName,
                               RowNumber = (int)cropTextRow.RowNumber
                           };

            List<CropByEditionInfo> cropTextInfo = cropText.ToList();

            return cropTextInfo.Count > 0 ? cropTextInfo : null;

        }

       




        #endregion

        #region PLM Methods
        public List<CropDetailInfo> getAll(string isbn)
        {
            DbCommand dbCmd = CropsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCrops");

            // Add the parameters:
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            List<CropDetailInfo> BECollection = new List<CropDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CropsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public CropDetailInfo getById(string isbn, int cropId)
        {
            DbCommand dbCmd = CropsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCrops");

            // Add the parameters:
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@cropId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, cropId);
            CropDetailInfo BECollection = new CropDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CropsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection=(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<CropDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = CropsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCropsByProduct");

            // Add the parameters:
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaformid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            List<CropDetailInfo> BECollection = new List<CropDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CropsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<CropDetailInfo> getByText(string isbn, string text)
        {
            DbCommand dbCmd = CropsDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCropsByText");

            // Add the parameters:
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            CropsDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@text", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, text);
            List<CropDetailInfo> BECollection = new List<CropDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CropsDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }
        #endregion
        
        
        #endregion

        #region Pretected Methods

        protected CropDetailInfo getFromDataReader(IDataReader current)
        {
            CropDetailInfo record = new CropDetailInfo();

            record.CropId = Convert.ToInt32(current["CropId"]);
            record.CropName = current["CropName"].ToString();
            record.ScientificName = current["ScientificName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            return record;
        }

        #endregion
        public static readonly CropsDALC Instace = new CropsDALC();

    }
}
