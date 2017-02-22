using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EncyclopediasBussinesEntries;

namespace EncyclopediasBusinessLogicComponent
{
    public class ProductsEncyclopediaBLC
    {
        #region Constructors

        private ProductsEncyclopediaBLC() { }

        #endregion

        #region Public

        public List<ProductEncyclopediasInfo> getProductsByEncyclopedia(string code,int encyclopediaId)
        {
            if (encyclopediaId <= 0)
            {
                throw new ApplicationException("The encyclopediaId doesn´t valid");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.ProductEncyclopediaDALC.Instance.getProductsByEncyclopedia(encyclopediaId);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        public List<ProductEncyclopediasInfo> getProductsByEncyclopediaByEdition(string code, int encyclopediaId,string isbn)
        {
            if (encyclopediaId <= 0)
            {
                throw new ApplicationException("The encyclopediaId doesn´t valid");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return EncyclopediasDataAccessComponent.ProductEncyclopediaDALC.Instance.getProductsByEncyclopediaByEdition(encyclopediaId, isbn);

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
        }

        #endregion

        public static readonly ProductsEncyclopediaBLC Instance = new ProductsEncyclopediaBLC();
    }
}
