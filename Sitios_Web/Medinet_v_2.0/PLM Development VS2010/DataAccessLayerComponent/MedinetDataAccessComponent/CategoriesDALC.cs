using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class CategoriesDALC : MedinetDataAccessAdapter<CategoriesInfo>
    {
        #region Constructors

        private CategoriesDALC() { }

        #endregion

        #region Public methods

        public List<CategoriesInfo> getAll(int divisionId)
        {
            DbCommand dbCmd = CategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCategories");

            // Add the parameters:
            CategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);

            List<CategoriesInfo> BECollection = new List<CategoriesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CategoriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));

            }

            return BECollection;
        }

        public override CategoriesInfo getOne(int pk)
        {
            DbCommand dbCmd = CategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCategories");

            // Add the parameters:
            CategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CategoriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override CategoriesInfo getFromDataReader(IDataReader current)
        {
            CategoriesInfo record = new CategoriesInfo();

            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.Description = current["Description"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CategoriesDALC Instance = new CategoriesDALC();
    }
}
