using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
    public class IppaProductInteractionsDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.IppaProductInteractionsInfo>
    {
         #region Construtors

        private IppaProductInteractionsDALC() { }

        #endregion

        #region Propierties
        public int checkProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstance)
        {

            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckProductInteraction");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstance);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@return", DbType.Int32,
               ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            
            IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@return"].Value); ;
        }
        
        public List<IppaProductInteractionsInfo> getIppaProductsInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            List<IppaProductInteractionsInfo> interactions = new List<IppaProductInteractionsInfo>();

            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductInteractions");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            using (IDataReader dataReader = IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    IppaProductInteractionsInfo record = new IppaProductInteractionsInfo();
                    record.ActiveSubstanceId = Convert.ToInt32(dataReader["ActiveSubstanceId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    //record.IMStatusId = Convert.ToInt32(dataReader["IMStatusId"]);
                    //record.IMStatus = dataReader["Description"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public List<IppaProductInteractionsInfo> getIppaProductsInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            List<IppaProductInteractionsInfo> interactions = new List<IppaProductInteractionsInfo>();

            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductInteractions");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            using (IDataReader dataReader = IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    IppaProductInteractionsInfo record = new IppaProductInteractionsInfo();
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    //record.IMStatusId = Convert.ToInt32(dataReader["StatusId"].ToString());
                    //record.IMStatus= dataReader["Description"].ToString();

                    interactions.Add(record);
                }

            }
            return interactions;
        }

        public String getHtmlContentInteractionByProduct(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            string Htmlinteractions = "";

            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spGetHtmlContentInteractions");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            using (IDataReader dataReader = IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    Htmlinteractions += dataReader["HTMLContent"].ToString();
                }

            }
            return Htmlinteractions;
        }

        public void checkStatusIMProductChanges(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCheckStatusProductIM");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            
        }

        public override void delete(IppaProductInteractionsInfo businessEntity)
        {
         

            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductInteractions");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId );
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
            
        }

        public override int insert(IppaProductInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductInteractions");
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
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
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@statusIM", DbType.Int32,
              ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IMStatusId);
            IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
            return 0;
        }

        public override void update(IppaProductInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = IppaProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDIppaProductInteractions");
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
            IppaProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@statusIM", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IMStatusId);
            IppaProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd);
        }
        #endregion
        public static readonly IppaProductInteractionsDALC Instance = new IppaProductInteractionsDALC();
    }
}
