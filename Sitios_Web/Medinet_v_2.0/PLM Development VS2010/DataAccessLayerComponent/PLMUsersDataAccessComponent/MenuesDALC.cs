using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class MenuesDALC : PLMUsersDataAccesAdapter<MenuInfo>
    {
        #region Constructors

        private MenuesDALC() { }

        #endregion

        #region Public Methods

        public override List<MenuInfo> getAll()
        {
            List<MenuInfo> BECollection = new List<MenuInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = MenuesDALC.InstanceDatabase.ExecuteReader(
                MenuesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMenues")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;  
        }

        public List<MenuByWebSection> getBySectionByRole(int webPageId, byte webSectionId, int roleId)
        {
            DbCommand dbCmd = MenuesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMenuesBySection");

            //Add parameters
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@webPageId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, webPageId);
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@webSectionId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, webSectionId);
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, roleId);

            List<MenuByWebSection> recordCollection = new List<MenuByWebSection>();

            using (IDataReader dataReader = MenuesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                MenuByWebSection record;
                while (dataReader.Read())
                {
                    record = new MenuByWebSection();

                    record.WebPageId = Convert.ToInt32(dataReader["WebPageId"]);
                    record.WebSectionId = Convert.ToByte(dataReader["WebSectionId"]);
                    record.MenuId = Convert.ToInt32(dataReader["MenuId"]);
                    record.RoleId = Convert.ToInt32(dataReader["RoleId"]);
                    record.MenuName = dataReader["MenuName"].ToString();

                    if (dataReader["ImageName"] != System.DBNull.Value)
                        record.ImageName = dataReader["ImageName"].ToString();

                    record.Url = dataReader["Url"].ToString();

                    recordCollection.Add(record);

                }

            }

            return recordCollection;
        }

        #endregion

        #region Protected Methods

        protected override MenuInfo getFromDataReader(IDataReader current)
        {
            MenuInfo record = new MenuInfo();

            record.MenuId = Convert.ToInt32(current["MenuId"]);
            record.ParentId = Convert.ToInt32(current["ParentId"]);
            record.MenuName = current["MenuName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly MenuesDALC Instance = new MenuesDALC();

    }
}
