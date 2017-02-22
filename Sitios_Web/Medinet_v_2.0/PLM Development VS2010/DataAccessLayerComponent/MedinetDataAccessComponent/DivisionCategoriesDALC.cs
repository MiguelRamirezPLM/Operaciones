using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class DivisionCategoriesDALC : MedinetDataAccessAdapter<DivisionCategoryInfo>
    {

        #region Constructors

        private DivisionCategoriesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(DivisionCategoryInfo businessEntity)
        {

            DbCommand dbCmd = DivisionCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionCategories");

            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            DivisionCategoriesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public DivisionCategoryInfo getDivisionCategory(int divisionId, int categoryId)
        {
            DbCommand dbCmd = DivisionCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionCategories");

            // Add the parameters:
            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            DivisionCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionCategoriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override DivisionCategoryInfo getFromDataReader(System.Data.IDataReader current)
        {
            DivisionCategoryInfo record = new DivisionCategoryInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            
            return record;
        }

        #endregion

        public static readonly DivisionCategoriesDALC Instance = new DivisionCategoriesDALC();

    }
}
