using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class PharmaFormsBLC
    {

        #region Constructors

        private PharmaFormsBLC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getPharmaFormByDrug(string code, int countryId, int editionId, int productId)
        {
            if (countryId <= 0 || editionId <= 0 || productId <= 0)
                throw new ArgumentException("The country, book edition or drug does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.PharmaFormsDALC.Instance.getByProduct(countryId, editionId, productId, (byte)Catalogs.TypeInEdition.Participante);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<MedinetBusinessEntries.PharmaceuticalFormInfo> getPharmaForms(string code, int countryId, int editionId, string description)
        {
            if (countryId <= 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PharmaSearchEngineDataAccessComponent.PharmaFormsDALC.Instance.getAll(countryId, editionId, description, (byte)Catalogs.TypeInEdition.Participante);
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        public static readonly PharmaFormsBLC Instance = new PharmaFormsBLC();

    }
}
