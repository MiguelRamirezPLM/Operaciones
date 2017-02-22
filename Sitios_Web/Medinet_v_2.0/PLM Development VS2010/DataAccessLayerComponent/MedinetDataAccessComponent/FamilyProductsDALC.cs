using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class FamilyProductsDALC : MedinetDataAccessAdapter<FamilyProductInfo>
    {

        #region Constructors

        private FamilyProductsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(FamilyProductInfo businessEntity)
        {
            DbCommand dbCmd = FamilyProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyProducts");

            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FamilyId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            FamilyProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(FamilyProductInfo businessEntity)
        {
            DbCommand dbCmd = FamilyProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDFamilyProducts");

            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@familyId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FamilyId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            FamilyProductsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public FamilyProductInfo getFamilyProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = FamilyProductsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFamilyProducts");

            // Add the parameters:
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            FamilyProductsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = FamilyProductsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override FamilyProductInfo getFromDataReader(IDataReader current)
        {
            FamilyProductInfo record = new FamilyProductInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.FamilyId = Convert.ToInt32(current["FamilyId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);

            return record;
        }

        #endregion

        public static readonly FamilyProductsDALC Instance = new FamilyProductsDALC();

    }
}
