using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ConcentrationsDALC : MedinetDataAccessAdapter<ConcentrationInfo>
    {
        #region Constructors

        private ConcentrationsDALC() { }

        #endregion

        #region Public methods

        public override int insert(ConcentrationInfo businessEntity)
        {
            DbCommand dbCmd = ConcentrationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOtherConcentrations");

            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ConcentrationsDALC.CRUD.Create);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@unitId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UnitId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@quantity", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Quantity);

            ConcentrationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public List<ConcentrationInfo> getAllByProduct(int pharmaFormId, int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = ConcentrationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOtherConcentrations");

            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ConcentrationsDALC.CRUD.Read);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);
            
            List<ConcentrationInfo> BECollection = new List<ConcentrationInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ConcentrationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;   
        }

        public override void update(ConcentrationInfo businessEntity)
        {
            ConcentrationsDALC.MedInstanceDatabase.ExecuteNonQuery("dbo.plm_spCRUDOtherConcentrations",
                ConcentrationsDALC.CRUD.Update,
                businessEntity.ConcentrationId,
                businessEntity.PharmaFormId,
                businessEntity.ProductId,
                businessEntity.ActiveSubstanceId,
                businessEntity.UnitId,
                businessEntity.Quantity);
        }
        
        public override void  delete(int pk)
        {
            DbCommand dbCmd = ConcentrationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOtherConcentrations");

            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ConcentrationsDALC.CRUD.Delete);
            ConcentrationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@otherConcentrationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);
            
            ConcentrationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected methods

        protected override ConcentrationInfo getFromDataReader(System.Data.IDataReader current)
        {
            ConcentrationInfo record = new ConcentrationInfo();

            record.ConcentrationId = Convert.ToInt32(current["OtherConcentrationId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);

            if (current["UnitId"] != DBNull.Value)
                record.UnitId = Convert.ToInt32(current["UnitId"]);

            if (current["Quantity"] != DBNull.Value)
                record.Quantity = Convert.ToInt32(current["Quantity"]);

            return record;
        }

        #endregion

        public static readonly ConcentrationsDALC Instance = new ConcentrationsDALC();

    }
}
