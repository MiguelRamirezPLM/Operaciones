using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
namespace MedinetDataAccessComponent
{
   public class IppaProductContraindicationsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.IppaProductContraindicationsInfo>
    {
         #region Construtors

       private IppaProductContraindicationsDALC() { }

        #endregion

       #region Propierties
       public int checkProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstance)
       {

           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckProductContraindication");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstance);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
              ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);

           IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);

           return Convert.ToInt32(dbCmd.Parameters["@return"].Value); ;
       }

       public List<IppaProductContraindicationsInfo> getIppaProductsContraindications(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstanceId)
       {
           List<IppaProductContraindicationsInfo> contraindications = new List<IppaProductContraindicationsInfo>();

           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductContraindications");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
           using (IDataReader dataReader = IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
               {
                   IppaProductContraindicationsInfo record = new IppaProductContraindicationsInfo();
                   record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                   record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                   record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                   record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                   record.ProductId = Convert.ToInt32(dataReader["ProductId"]);


                   contraindications.Add(record);
               }

           }
           return contraindications;
       }

       public List<IppaProductContraindicationsInfo> getIppaProductsContraindications(int categoryId, int pharmaFormId, int productId, int divisionId)
       {
           List<IppaProductContraindicationsInfo> contraindications = new List<IppaProductContraindicationsInfo>();

           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductContraindications");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           using (IDataReader dataReader = IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
               {
                   IppaProductContraindicationsInfo record = new IppaProductContraindicationsInfo();
                   record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                   record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                   record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                   record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                   record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                   //record.IMStatusId = Convert.ToInt32(dataReader["StatusId"].ToString());
                   //record.IMStatus= dataReader["Description"].ToString();

                   contraindications.Add(record);
               }

           }
           return contraindications;
       }

       public String getHtmlContentContraindicationByProduct(int categoryId, int pharmaFormId, int productId, int divisionId)
       {
           string html = "";

           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetHtmlContentContraindications");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           using (IDataReader dataReader = IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
           {
               while (dataReader.Read())
               {
                   html += dataReader["HTMLContent"].ToString();
               }

           }
           return html;
       }

       public void checkStatusCMProductChanges(int categoryId, int pharmaFormId, int productId, int divisionId)
       {
           DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCheckStatusProductCM");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

       }

       public override void delete(IppaProductContraindicationsInfo businessEntity)
       {


           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductContraindications");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);

       }

       public void insert(IppaProductContraindicationsInfo businessEntity,bool deleteClasification)
       {
           DbCommand dbCmd = IppaProductContraindicationsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductContraindications");
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@statusCM", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StatusId);
           IppaProductContraindicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@deleteCM", DbType.Int32,
             ParameterDirection.Input, string.Empty, DataRowVersion.Current, Convert.ToInt32(deleteClasification));
           IppaProductContraindicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
           
       }

       public override void update(IppaProductContraindicationsInfo businessEntity)
       {
           DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductContraindications");
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
           IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@statusCM", DbType.Int32,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StatusId);
           IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
       }

       #endregion
       public static readonly IppaProductContraindicationsDALC Instance = new IppaProductContraindicationsDALC();
    }
}
