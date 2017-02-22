using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class EditionProductShotsDALC : MedinetDataAccessAdapter<EditionProductShotsInfo>
    {

        #region Constructors

        private EditionProductShotsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionProductShotsInfo businessEntity)
        {
            DbCommand dbCmd = EditionProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionProductShots");

            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionProductShotId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@psTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PSTypeId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productShot", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductShot);

            if (businessEntity.QtyCells != null)
                EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@qtyCells", DbType.Byte,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.QtyCells);

            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            EditionProductShotsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = EditionProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionProductShots");

            // Add the parameters:
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionProductShotId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Delete record:
            EditionProductShotsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<EditionProductShotsInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = EditionProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetEditionProductShots");

            // Add the parameters:
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<EditionProductShotsInfo> BECollection = new List<EditionProductShotsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DivisionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public override EditionProductShotsInfo getOne(int pk)
        {
            DbCommand dbCmd = EditionProductShotsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionProductShots");

            // Add the parameters:
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EditionProductShotsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionProductShotId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionProductShotsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override EditionProductShotsInfo getFromDataReader(IDataReader current)
        {
            EditionProductShotsInfo record = new EditionProductShotsInfo();

            record.EditionProductShotId = Convert.ToInt32(current["EditionProductShotId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PSTypeId = Convert.ToByte(current["PSTypeId"]);

            if (current["ProductShot"] != DBNull.Value)
                record.ProductShot = current["ProductShot"].ToString();

            if (current["QtyCells"] != DBNull.Value)
                record.QtyCells = Convert.ToByte(current["QtyCells"]);

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly EditionProductShotsDALC Instance = new EditionProductShotsDALC();

    }
}
