using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_SyncDatabasesBusinessLogicComponent
{
    public class CE_AssignCodesBLC
    {
        #region Constructors

        private CE_AssignCodesBLC() { }

        #endregion

        #region Public Methods

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCode(string codeString)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_AssignCodesDALC.Instance.getCode(codeString);
        }

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCodeByEdition(int editionId)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_AssignCodesDALC.Instance.getCodeByEdition(editionId);
        }

        public SyncDatabaseBusinessEntries.AssignCodeInfo getCodeByIsbn(string isbn)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_AssignCodesDALC.Instance.getCodeByIsbn(isbn);
        }

        public void updateAssignCode(SyncDatabaseBusinessEntries.AssignCodeInfo businessEntity)
        {
            CE_SyncDatabasesDataAccessComponent.CE_AssignCodesDALC.Instance.update(businessEntity);
        }

        public void addAssignCode(SyncDatabaseBusinessEntries.AssignCodeInfo businessEntity)
        {
            CE_SyncDatabasesDataAccessComponent.CE_AssignCodesDALC.Instance.insert(businessEntity);   
        }

        #endregion

        public static readonly CE_AssignCodesBLC Instance = new CE_AssignCodesBLC();
    }
}
