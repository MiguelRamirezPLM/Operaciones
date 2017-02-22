using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class PharmacologicalGroupsBLC
    {

        #region Constructors

        private PharmacologicalGroupsBLC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PharmacologicalGroupsInfo> getByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("Some parameters aren't valid");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.PharmacologicalGroupsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        public static readonly PharmacologicalGroupsBLC Instance = new PharmacologicalGroupsBLC();

    }
}
