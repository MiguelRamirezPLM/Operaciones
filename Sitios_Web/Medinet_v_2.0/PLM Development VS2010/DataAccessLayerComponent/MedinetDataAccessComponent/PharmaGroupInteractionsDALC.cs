using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class PharmaGroupInteractionsDALC : MedinetDataAccessAdapter<PharmaGroupInteractionsInfo>
    {

        #region Constructors

        private PharmaGroupInteractionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PharmaGroupInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = PharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPharmaGroupInteractions");

            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);

            PharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(PharmaGroupInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = PharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDPharmaGroupInteractions");

            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaGroupId);

            PharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<PharmaGroupInteractionsInfo> getPharmaGroupInteractions(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = PharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaGroupInteractions");

            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<PharmaGroupInteractionsInfo> BECollection = new List<PharmaGroupInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PharmaGroupInteractionsInfo> getPharmaGroupInteractions(int activeSubstanceId)
        {
            DbCommand dbCmd = PharmaGroupInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaGroupInteractions");

            PharmaGroupInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<PharmaGroupInteractionsInfo> BECollection = new List<PharmaGroupInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PharmaGroupInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override PharmaGroupInteractionsInfo getFromDataReader(IDataReader current)
        {
            PharmaGroupInteractionsInfo record = new PharmaGroupInteractionsInfo();

            record.ProductContentId = Convert.ToInt32(current["ProductContentId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.PharmaGroupId = Convert.ToInt32(current["PharmaGroupId"]);

            return record;
        }

        #endregion

        public static readonly PharmaGroupInteractionsDALC Instance = new PharmaGroupInteractionsDALC();

    }
}
