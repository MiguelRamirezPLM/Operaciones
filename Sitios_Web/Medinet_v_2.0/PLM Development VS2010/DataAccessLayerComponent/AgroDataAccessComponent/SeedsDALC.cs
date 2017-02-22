using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using System.Data.Common;
using System.Data;
namespace AgroDataAccessComponent
{
    public class SeedsDALC : AgroEngineDataAccessAdapter<SeedDetailInfo>
    {
        public void insertForProduct(int productId, int agrochemicalUseId)
        {
            DbCommand dbCmd = SeedsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSeedsForProduct");

            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@seedId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


            ActiveSubstancesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }


        public void deleteForProduct(int productId, int agrochemicalUseId)
        {
            DbCommand dbCmd = ActiveSubstancesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDSeedsForProduct");

            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@seedId", DbType.Int32,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


            SeedsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }





        public List<SeedDetailInfo> getSeedsNotAssociated(int editionId, int divisionId, int bookId, int productId, int pharmaformId, string brand)
        {
            List<SeedDetailInfo> BECollection = new List<SeedDetailInfo>();
            DbCommand dbCmd = SeedsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllSeeds");
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaformId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.String,
     ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            using (IDataReader dataReader = SeedsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    SeedDetailInfo record = new SeedDetailInfo();
                    record.SeedId = Convert.ToInt32(dataReader["SeedId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.SeedName = dataReader["SeedName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<SeedDetailInfo> getSeedsForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
        {
            List<SeedDetailInfo> BECollection = new List<SeedDetailInfo>();
            DbCommand dbCmd = SeedsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSeedsForProduct");
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            SeedsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = SeedsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    SeedDetailInfo record = new SeedDetailInfo();
                    record.SeedId = Convert.ToInt32(dataReader["SeedId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.SeedName = dataReader["SeedName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }
        public static readonly SeedsDALC Instance = new SeedsDALC();

    }
}
