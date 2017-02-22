using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class OtherElementsBLC
    {

        #region Constructors

        private OtherElementsBLC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.OtherElementsInfo> getByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("Some parameters aren't valid");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.OtherElementsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        public static readonly OtherElementsBLC Instance = new OtherElementsBLC();

    }
}
