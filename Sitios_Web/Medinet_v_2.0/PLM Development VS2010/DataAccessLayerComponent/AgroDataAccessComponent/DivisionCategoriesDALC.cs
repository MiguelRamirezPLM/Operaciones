using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public class DivisionCategoriesDALC : AgroDataAccessAdapter<DivisionCategoryInfo>
    {

        #region Constructors

        private DivisionCategoriesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(DivisionCategoryInfo businessEntity)
        {

            DbCommand dbCmd = DivisionCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDivisionCategories");

            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            DivisionCategoriesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public DivisionCategoryInfo getDivisionCategory(int divisionId, int categoryId)
        {
            DbCommand dbCmd = DivisionCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDivisionCategoriesToAddProducts");

            // Add the parameters:
            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            DivisionCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DivisionCategoriesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
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
