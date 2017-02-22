using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ProductContentsBLC
    {

        #region Constructors

        private ProductContentsBLC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.ProductContentsInfo> getProductContents(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The parameters are not valid.");

            return MedinetDataAccessComponent.ProductContentsDALC.Instance.getProductContents(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly ProductContentsBLC Instance = new ProductContentsBLC();

    }
}
