using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Linq;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class AgrochemicalUsesDALC : AgroEngineDataAccessAdapter<AgrochemicalUseInfo>
    {

        #region Constructor

        private AgrochemicalUsesDALC() { }

        #endregion



        #region Pretected Methods


       public void insertForProduct(int productId, int agrochemicalUseId)
        {
            DbCommand dbCmd = AgrochemicalUsesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAgrochemicalUsesForProduct");

            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@agrochemicalUseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


            AgrochemicalUsesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
          
        }


       public void deleteForProduct(int productId, int agrochemicalUseId)
       {
           DbCommand dbCmd = AgrochemicalUsesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDAgrochemicalUsesForProduct");

           AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
           AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@agrochemicalUseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, agrochemicalUseId);


           AgrochemicalUsesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

       }





        public List<AgrochemicalUseInfo> getAgrochemicalUsesNotAssociated(int editionId, int divisionId, int bookId, int productId,int pharmaformId, string brand)
        {
            List<AgrochemicalUseInfo> BECollection = new List<AgrochemicalUseInfo>();
            DbCommand dbCmd = AgrochemicalUsesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgrochemicalUses");
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaformId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@brand", DbType.String,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, brand);
            using (IDataReader dataReader = AgrochemicalUsesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    AgrochemicalUseInfo record = new AgrochemicalUseInfo();
                    record.AgrochemicalUseId = Convert.ToInt32(dataReader["AgrochemicalUseId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.AgrochemicalUseName = dataReader["AgrochemicalUseName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<AgrochemicalUseInfo> getAgrochemicalUsesForProduct(int editionId, int divisionId, int bookId,int productId, int pharmaFormId)
        {
            List<AgrochemicalUseInfo> BECollection = new List<AgrochemicalUseInfo>();
            DbCommand dbCmd = AgrochemicalUsesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgrochemicalUsesForProduct");
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@bookId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, bookId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AgrochemicalUsesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaformId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            using (IDataReader dataReader = AgrochemicalUsesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {


                while (dataReader.Read())
                {
                    AgrochemicalUseInfo record = new AgrochemicalUseInfo();
                    record.AgrochemicalUseId = Convert.ToInt32(dataReader["AgrochemicalUseId"]);
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.AgrochemicalUseName = dataReader["AgrochemicalUseName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.ProductName = dataReader["ProductName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }



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
