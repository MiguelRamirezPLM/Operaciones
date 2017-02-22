using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
namespace AgroDataAccessComponent
{
    public class CropsDALC : AgroEngineDataAccessAdapter<ProductDetailByEditionInfo>
    {
         #region Constructor

        private CropsDALC() { }

        #endregion
        #region Methods
        #region Select
        public AgroEntityFramework.Crops getCropById(int cropId)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.Crops
                        where d.CropId == cropId
                        select d).Single();

            AgroEntityFramework.Crops crops = query;

            return crops;
        }

        public List<AgroEntityFramework.Crops> getCropsByStateId(int stateId){
            
            List<AgroEntityFramework.Crops> crops;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = from d in context.Crops
                          join sc in context.StateCrops on d.CropId equals sc.CropId
                          where sc.StateId == stateId
                          orderby d.CropName
                          select d;

                crops = pes.ToList() ;

            }
            return crops;
        }

        public List<AgroEntityFramework.Crops> getCrops()
        {
            List<AgroEntityFramework.Crops> crops;
            using (var context = new AgroEntityFramework.DEAQEntites())
            {
                var pes = from d in context.Crops
                          select d;
                crops = pes.ToList();
            }
            return crops;
        }


        #endregion
        #region update

        public void updateCrop(AgroEntityFramework.Crops crop)
        {
            AgroEntityFramework.DEAQEntites db = new AgroEntityFramework.DEAQEntites();

            var query = (from d in db.Crops

                         where d.CropId == crop.CropId

                         select d).Single();

            query.CropName = crop.CropName; 
            query.ScientificName = crop.ScientificName;
            
            db.SaveChanges();
        }

        #endregion

        public void insertForProduct(int productId, int cropId)
        {
            DbCommand dbCmd = CropsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCropsForProduct");

            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@cropId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, cropId);


            CropsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }


        public void deleteForProduct(int productId, int cropId)
        {
            DbCommand dbCmd = CropsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCropsForProduct");

            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@cropId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, cropId);


            CropsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }





        public List<CropDetailInfo> getCropsNotAssociated(int editionId, int divisionId, int bookId, int productId, int pharmaformId, string brand)
        {
            List<CropDetailInfo> BECollection = new List<CropDetailInfo>();
            DbCommand dbCmd = CropsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllCrops");
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaformId);
           CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.String,
    ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
           using (IDataReader dataReader = CropsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    CropDetailInfo record = new CropDetailInfo();
                    record.CropId = Convert.ToInt32(dataReader["CropId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.CropName = dataReader["CropName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<CropDetailInfo> getCropsForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
        {
            List<CropDetailInfo> BECollection = new List<CropDetailInfo>();
            DbCommand dbCmd = CropsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCropsForProduct");
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            CropsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = CropsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    CropDetailInfo record = new CropDetailInfo();
                    record.CropId = Convert.ToInt32(dataReader["CropId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.CropName = dataReader["CropName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }
       
        #endregion
        public static readonly CropsDALC Instance = new CropsDALC();
    }
}
