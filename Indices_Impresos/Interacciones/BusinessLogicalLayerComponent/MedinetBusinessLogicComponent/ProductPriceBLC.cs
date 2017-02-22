using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;
using System.Linq;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductPriceBLC
    {
         #region Constructors

        private ProductPriceBLC() { }

        #endregion

        #region Public Methods

        public List<PriceSourceInfo> getUnits()
        {
            return MedinetDataAccessComponent.PriceSourceDALC.Instance.getAll();
        }

        #endregion

        public static readonly ProductPriceBLC Instance = new ProductPriceBLC();
    }
}
