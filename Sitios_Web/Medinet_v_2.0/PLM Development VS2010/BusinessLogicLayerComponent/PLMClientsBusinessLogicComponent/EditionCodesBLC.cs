using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;


namespace PLMClientsBusinessLogicComponent
{
    public class EditionCodesBLC
    {
        #region Constructors

        private EditionCodesBLC() { }

        #endregion

        #region Public Methods

        public void addCodeToEdition(EditionCodeInfo bentity)
        {
            PLMClientsDataAccessComponent.EditionCodesDALC.Instance.insert(bentity);
        }

        #endregion

        public static readonly EditionCodesBLC Instance = new EditionCodesBLC();

    }
}
