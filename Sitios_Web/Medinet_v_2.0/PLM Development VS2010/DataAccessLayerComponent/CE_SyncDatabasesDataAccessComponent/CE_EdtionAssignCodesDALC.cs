using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_EdtionAssignCodesDALC : CE_SyncDatabasesDataAccessAdapter<EditionAssignCodeInfo>
    {
        #region Constructors

        private CE_EdtionAssignCodesDALC() { }

        #endregion

        #region Public Methods
        //JADJ 30/March/2011
        public override int insert(EditionAssignCodeInfo businessEntity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n INSERT INTO EditionAssignCodes (EditionId,AssignId)");
            sb.Append("\n VALUES(");
            sb.Append("\n '" + businessEntity.EditionId + "',");
            sb.Append("\n '" + businessEntity.AssignId + "'");
            sb.Append("\n )");

            int result = CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

            return result;
        }

        //JADJ 31/March/2011
        public override void delete(EditionAssignCodeInfo businessEntity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n DELETE FROM EditionAssignCodes");
            sb.Append("\n WHERE EditionId = '" + businessEntity.EditionId + "'");
            sb.Append("\n AND AssignId = '" + businessEntity.AssignId + "'");

            CE_AssignCodesDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_AssignCodesDALC.SyncInstanceDatabase.CloseSharedConnection();

        }
        #endregion

        public static readonly CE_EdtionAssignCodesDALC Instance = new CE_EdtionAssignCodesDALC();
    }
}
