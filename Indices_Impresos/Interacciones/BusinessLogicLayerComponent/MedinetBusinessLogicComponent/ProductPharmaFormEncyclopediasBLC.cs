using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductPharmaFormEncyclopediasBLC
    {
        #region Constructors

        private ProductPharmaFormEncyclopediasBLC() { }

        #endregion


        public void addProductPharmaFormEncyclopedia(ProductPharmaFormEncyclopediaInfo BEntity)
        {
            MedinetDataAccessComponent.ProductPharmaFormEncyclopediaDALC.Instance.insert(BEntity);
        }
        public void deleteProductPharmaFormEncyclopedia(ProductPharmaFormEncyclopediaInfo BEntity)
        {
            MedinetDataAccessComponent.ProductPharmaFormEncyclopediaDALC.Instance.delete(BEntity);
        }

        public static readonly ProductPharmaFormEncyclopediasBLC Instance = new ProductPharmaFormEncyclopediasBLC();
    }
}
