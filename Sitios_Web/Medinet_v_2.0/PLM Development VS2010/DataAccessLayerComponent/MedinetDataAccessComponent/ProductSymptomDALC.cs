using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductSymptomDALC : MedinetDataAccessAdapter<ProductSymptomInfo>
    {
        #region Constructors

       private ProductSymptomDALC() { }

        #endregion

        #region Public methods
       public override int insert(MedinetBusinessEntries.ProductSymptomInfo businessEntity)
       {
           DbCommand dbCmd = ProductSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSymptoms");

           //Add the parameters:
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SymptomId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
           
           ProductSymptomDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

           return 0;
           
       }
       public override void delete(ProductSymptomInfo businessEntity)
       {
           DbCommand dbCmd = ProductSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSymptoms");

           //Add the parameters:
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@symptomId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SymptomId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);

           ProductSymptomDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
       }

       public List<ProductSymptomInfo> getSymptomsByParticipantProducts(int editionId, int productId,int pharmaFormId)
       {
           DbCommand dbCmd = ProductSymptomDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSymptomsFromParticipantProducts");

           //Add the parameters:
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           ProductSymptomDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

           List<MedinetBusinessEntries.ProductSymptomInfo> BECollection = new List<MedinetBusinessEntries.ProductSymptomInfo>();
           using (IDataReader dataReader = ProductSymptomDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
           {
               MedinetBusinessEntries.ProductSymptomInfo record;

               // Iterates through records:
               while (dataReader.Read())
               {
                   record = new MedinetBusinessEntries.ProductSymptomInfo();
                   if (dataReader["SymptomId"] != System.DBNull.Value)
                   {
                       record.SymptomId = Convert.ToInt32(dataReader["SymptomId"]);
                   }
                   if (dataReader["SymptomId"] != System.DBNull.Value)
                   {
                       record.PharmaFormId = Convert.ToInt32(dataReader["pharmaFormId"]);
                   }
                   if (dataReader["SymptomId"] != System.DBNull.Value)
                   {
                       record.ProductId = Convert.ToInt32(dataReader["productId"]);
                   }
                   
                   BECollection.Add(record);
               }
           }
           return BECollection;
       }

        #endregion
       public static readonly ProductSymptomDALC Instance = new ProductSymptomDALC();
    }
}
