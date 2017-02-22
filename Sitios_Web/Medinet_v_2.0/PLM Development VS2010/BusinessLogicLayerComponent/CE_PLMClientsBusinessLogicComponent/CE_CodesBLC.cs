using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_PLMClientsBusinessLogicComponent
{
    public class CE_CodesBLC
    {
        #region Constructors

        private CE_CodesBLC() { }

        #endregion

        #region Public Methods

        public bool validCode(string codeString)
        {
            return CE_SyncDatabasesBusinessLogicComponent.CE_AssignCodesBLC.Instance.getCode(codeString) != null;
        }

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCodeByIsbn(string isbn)
        {
            return CE_SyncDatabasesBusinessLogicComponent.CE_AssignCodesBLC.Instance.getCodeByIsbn(isbn);
        }

        public void saveCode(string codeString, string isbn)
        { 
            /* 
             * Step (1) Get current codeString given by ISBN: 
             * 
             */
            SyncDatabaseBusinessEntries.AssignCodeInfo currentAssignCodeInfo = this.getCodeByIsbn(isbn);

            /* 
             * Step (2) Get current edition given by ISBN: 
             * 
             */
            SyncDatabaseBusinessEntries.EditionInfo editionInfo = CE_SyncDatabasesBusinessLogicComponent.CE_EditionsBLC.Instance.getEditionByISBN(isbn);

            /* 
             * Step (3) Delete association between current codeString and edition:
             * 
             */
            SyncDatabaseBusinessEntries.EditionAssignCodeInfo editionAssignCodeInfo = new SyncDatabaseBusinessEntries.EditionAssignCodeInfo();
            editionAssignCodeInfo.AssignId = currentAssignCodeInfo.AssignId;
            editionAssignCodeInfo.EditionId = editionInfo.EditionId;

            CE_SyncDatabasesBusinessLogicComponent.CE_EdtionAssignCodesBLC.Instance.remove(editionAssignCodeInfo);

            /* 
             * Step (4) Inactive current assign code:
             * 
             */
            currentAssignCodeInfo.Active = false;
            CE_SyncDatabasesBusinessLogicComponent.CE_AssignCodesBLC.Instance.updateAssignCode(currentAssignCodeInfo);

            /* 
             * Step (5) Insert new code:
             * 
             */
            SyncDatabaseBusinessEntries.AssignCodeInfo newAssignCode = new SyncDatabaseBusinessEntries.AssignCodeInfo();
            newAssignCode.CodeString = codeString;
            newAssignCode.AddedDate = DateTime.Now;
            newAssignCode.Active = true;
            newAssignCode.AllowUpdates = true;

            CE_SyncDatabasesBusinessLogicComponent.CE_AssignCodesBLC.Instance.addAssignCode(newAssignCode);

            /* 
             * Step (6) Insert the association between Edition and new code:
             * 
             */
            SyncDatabaseBusinessEntries.EditionAssignCodeInfo newEditionAssignCodeInfo = new SyncDatabaseBusinessEntries.EditionAssignCodeInfo();
            newEditionAssignCodeInfo.AssignId = newAssignCode.AssignId;
            newEditionAssignCodeInfo.EditionId = editionInfo.EditionId;

            CE_SyncDatabasesBusinessLogicComponent.CE_EdtionAssignCodesBLC.Instance.add(newEditionAssignCodeInfo);

        }

        #endregion
        
        public static readonly CE_CodesBLC Instance = new CE_CodesBLC();

    }
}
