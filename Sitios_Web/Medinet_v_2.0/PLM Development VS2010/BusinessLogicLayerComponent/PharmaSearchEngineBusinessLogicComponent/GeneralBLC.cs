using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public sealed class GeneralBLC
    {

        #region Constructors

        private GeneralBLC() { }

        #endregion

        #region Public Methods

        public List<ProductByEditionInfo> searchText(string code, byte countryId, int editionId, string attributes, string text)
        {
            if (countryId == 0 || editionId <= 0)
                throw new ArgumentException("The country or book edition does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    PSELogsBusinessLogicComponent.PSE_TrackingBLC.Instance.addAttributeLog(code, editionId, Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["PSE_Source"]),
                        (byte)PSELogsBusinessEntities.Catalogs.SearchTypes.Parametrizada, (byte)PSELogsBusinessEntities.Catalogs.Entities.Contenido, "Text=" + text, attributes);

                    return PharmaSearchEngineDataAccessComponent.ProductsDALC.Instance.searchText(countryId, editionId, attributes, text);
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        public static readonly GeneralBLC Instance = new GeneralBLC();

    }
}
