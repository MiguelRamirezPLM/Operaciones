using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesBusinessLogicComponent
{
    public sealed class CE_UpdatesBLC
    {
        #region Constructors

        private CE_UpdatesBLC() { }

        #endregion

        #region Public Methods

        public void executeUpdates(string code, EditionPackSQLTextInfo[] packs)
        {
            foreach (EditionPackSQLTextInfo pack in packs)
            {
                if (!checkPack(pack.PackSqlTextId))
                {
                    CE_SyncDatabasesDataAccessComponent.CE_EditionPackSqlTextsDALC.Instance.insert(pack);
                }

                CE_SyncDatabasesDataAccessComponent.ISyncDatabases sync = SyncDatabaseFactory.Factory.getInstance("MEDINET");

                UpdateMessageInfo message = sync.executeQuery(pack.SqlText);

                if (message.UpdateStatus)
                {
                    ClientCodesUpdatedInfo update = new ClientCodesUpdatedInfo();

                    update.CodeString = code;
                    update.PackSqlTextId = pack.PackSqlTextId;
                    update.SentDate = pack.AddedDate;

                    CE_SyncDatabasesDataAccessComponent.CE_ClientCodesUpdatedDALC.Instance.insert(update);

                    this.updateVersion(pack, code);

                }
                else
                {
                    ClientCodesErrorInfo error = new ClientCodesErrorInfo();

                    error.CodeString = code;
                    error.PackSqlTextId = pack.PackSqlTextId;
                    error.ErrorDate = message.ExecuteDate;
                    error.ErrorMessage = message.UpdateMessage;

                    CE_SyncDatabasesDataAccessComponent.CE_ClientCodesErrorsDALC.Instance.insert(error);

                    return;
                }

            }

        }
               

        #region ClientCodesUpdated

        public List<ClientCodesUpdatedInfo> getUpdatesExecuted(string code)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_ClientCodesUpdatedDALC.Instance.getUpdatesExecuted(code);
        }

        public void setUpdates(ClientCodesUpdatedInfo[] updates)
        {
            foreach(ClientCodesUpdatedInfo update in updates)
            {
                CE_SyncDatabasesDataAccessComponent.CE_ClientCodesUpdatedDALC.Instance.update(update);
            }
        }

        #endregion

        #region ClientCodesErrors

        public List<ClientCodesErrorInfo> getErrors(string code)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_ClientCodesErrorsDALC.Instance.getErrors(code);
        }

        public void setErrors(ClientCodesErrorInfo[] errors)
        {
            foreach (ClientCodesErrorInfo error in errors)
            {
                CE_SyncDatabasesDataAccessComponent.CE_ClientCodesErrorsDALC.Instance.update(error);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private void updateVersion(EditionPackSQLTextInfo pack, string code)
        {
            VersionInfo version = CE_VersionsBLC.Instance.getVersion(code);

            version.PackCount = version.PackCount + 1;

            version.VersionNumber = version.EditionId.ToString() + "." + version.DbId.ToString() + "." + version.PackCount.ToString();

            CE_VersionsBLC.Instance.updateVersion(code, version);

        }

        private bool checkPack(int packSqlTextId)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_EditionPackSqlTextsDALC.Instance.checkPack(packSqlTextId);
        }

        #endregion

        public static readonly CE_UpdatesBLC Instance = new CE_UpdatesBLC();

    }
}
