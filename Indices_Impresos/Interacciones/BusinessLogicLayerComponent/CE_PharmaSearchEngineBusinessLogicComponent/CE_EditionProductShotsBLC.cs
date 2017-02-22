using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PharmaSearchEngineBusinessEntries;

namespace CE_PharmaSearchEngineBusinessLogicComponent
{
    public class CE_EditionProductShotsBLC
    {
        #region Constructors

        private CE_EditionProductShotsBLC() { }

        #endregion

        #region Public methods

        public List<EditionProductShotInfo> getProductShots (int editionId, ProductByEditionInfo productByEdition)
        {
            if (editionId <= 0)
                throw new ArgumentException("Argument editionId is out of range");

            return CE_PharmaSearchEngineDataAccessComponent.CE_EditionProductShotsDALC.Instance.getEditionProductShots(editionId, productByEdition);
        }

        #endregion

        public static readonly CE_EditionProductShotsBLC Instance = new CE_EditionProductShotsBLC();
    }
}
