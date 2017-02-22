using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMLogsBusinessEntries;

namespace PLMLogsBusinessLogicComponent
{
    public class EditionBLC
    {

        #region Constructors

        private EditionBLC() { }

        #endregion

        #region Public Methods

        public EditionInfo getByISBN(string isbn) 
        {
            // Verify if the ISBN variable has any value:
            if (string.IsNullOrEmpty(isbn))
                throw new ArgumentNullException("The ISBN is not valid.");

            return PLMLogsDataAccessComponent.EditionDALC.Instance.getByISBN(isbn);
        }

        #endregion

        public static readonly EditionBLC Instance = new EditionBLC();

    }
}
