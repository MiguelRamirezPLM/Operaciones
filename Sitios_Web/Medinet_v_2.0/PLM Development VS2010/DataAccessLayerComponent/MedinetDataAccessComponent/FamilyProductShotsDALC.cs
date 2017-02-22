using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class FamilyProductShotsDALC : MedinetDataAccessAdapter<FamilyProductShotInfo>
    {

        #region Constructors

        private FamilyProductShotsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(MedinetBusinessEntries.FamilyProductShotInfo businessEntity)
        {
            DbCommand dbCmd = FamilyProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyProductShots");

            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FamilyId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionProductShotId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionProductShotId);

            FamilyProductShotsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(FamilyProductShotInfo businessEntity)
        {
            DbCommand dbCmd = FamilyProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyProductShots");

            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FamilyId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionProductShotId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionProductShotId);

            FamilyProductShotsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public FamilyProductShotInfo getFamilyProductShot(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = FamilyProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFamilyProductShots");

            // Add the parameters:
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            FamilyProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamilyProductShotsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override FamilyProductShotInfo getFromDataReader(IDataReader current)
        {
            FamilyProductShotInfo record = new FamilyProductShotInfo();

            record.FamilyId = Convert.ToInt32(current["FamilyId"]);
            record.EditionProductShotId = Convert.ToInt32(current["EditionProductShotId"]);

            return record;
        }

        #endregion

        public static readonly FamilyProductShotsDALC Instance = new FamilyProductShotsDALC();

    }
}
