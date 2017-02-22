using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE_SyncDatabasesBusinessLogicComponent
{
    public class CE_EditionsBLC
    {
        #region Constructors

        private CE_EditionsBLC() { }

        #endregion

        #region Public Methods

        public SyncDatabaseBusinessEntries.EditionInfo getEditionByISBN(string isbn)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_EditionsDALC.Instance.getByISBN(isbn);
        }

        public SyncDatabaseBusinessEntries.EditionInfo getByAssignCode(string isbn, string codeString)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_EditionsDALC.Instance.getByAssignCode(isbn, codeString);
        }

        #endregion

        public static readonly CE_EditionsBLC Instance = new CE_EditionsBLC();

    }
}
