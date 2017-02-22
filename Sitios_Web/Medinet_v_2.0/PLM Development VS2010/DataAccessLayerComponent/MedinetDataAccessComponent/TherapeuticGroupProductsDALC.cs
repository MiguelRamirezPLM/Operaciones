using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class TherapeuticGroupProductsDALC : MedinetDataAccessAdapter<TherapeuticGroupProductInfo>
    {
        #region Constructors

        private TherapeuticGroupProductsDALC() { }

        #endregion

        #region Public Methods

        public bool checkGroup(int theraGroupId, int editionId, int pharmaFormId, int productId)
        {
            DbCommand dbCmd = TherapeuticGroupProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckTherapeuticGroup");

            // Add the parameters:
            TherapeuticGroupProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TherapeuticGroupProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@theraGroupId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, theraGroupId);
            TherapeuticGroupProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            TherapeuticGroupProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            TherapeuticGroupProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            TherapeuticGroupProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        #endregion

        #region Protected Methods

        protected override TherapeuticGroupProductInfo getFromDataReader(IDataReader current)
        {
            TherapeuticGroupProductInfo record = new TherapeuticGroupProductInfo();

            record.TheraGroupId = Convert.ToInt32(current["TheraGroupId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);

            return record;

        }

        #endregion

        public static readonly TherapeuticGroupProductsDALC Instance = new TherapeuticGroupProductsDALC();

    }
}
