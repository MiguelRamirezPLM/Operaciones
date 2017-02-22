using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_EditionPackagesDALC : CE_SyncDatabasesDataAccessAdapter<EditionPackageInfo>
    {
        #region Constructors

        private CE_EditionPackagesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionPackageInfo businessEntity)
        {
            CE_EditionPackagesDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nINSERT INTO [EditionPackages] ([PackUpdId],[EditionId],[DbId],[TableId],[Active])");
            sb.Append("\nVALUES(" + businessEntity.PackUpdId + "," + businessEntity.EditionId);
            sb.Append("," + businessEntity.DbId + "," + businessEntity.TableId + ",1)");

            CE_EditionPackagesDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_EditionPackagesDALC.SyncInstanceDatabase.CloseSharedConnection();

            return businessEntity.PackUpdId;

        }

        #endregion

        #region Protected Methods

        protected override EditionPackageInfo getFromDataReader(IDataReader current)
        {
            EditionPackageInfo record = new EditionPackageInfo();

            record.PackUpdId = Convert.ToInt32(current["PackUpdId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DbId = Convert.ToInt32(current["DbId"]);
            record.TableId = Convert.ToByte(current["TableId"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CE_EditionPackagesDALC Instance = new CE_EditionPackagesDALC();
    }
}
