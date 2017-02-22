using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class OtherInteractionsDALC : MedinetDataAccessAdapter<OtherInteractionsInfo>
    {

        #region Constructors

        private OtherInteractionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(OtherInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = OtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOtherInteractions");

            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);

            OtherInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(OtherInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = OtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDOtherInteractions");

            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ElementId);

            OtherInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<OtherInteractionsInfo> getOtherInteractions(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = OtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOtherInteractions");

            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<OtherInteractionsInfo> BECollection = new List<OtherInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = OtherInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<OtherInteractionsInfo> getOtherInteractions(int activeSubstanceId)
        {
            DbCommand dbCmd = OtherInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOtherInteractions");

            OtherInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<OtherInteractionsInfo> BECollection = new List<OtherInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = OtherInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override OtherInteractionsInfo getFromDataReader(IDataReader current)
        {
            OtherInteractionsInfo record = new OtherInteractionsInfo();

            record.ProductContentId = Convert.ToInt32(current["ProductContentId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.ElementId = Convert.ToInt32(current["ElementId"]);

            return record;
        }

        #endregion

        public static readonly OtherInteractionsDALC Instance = new OtherInteractionsDALC();

    }
}
