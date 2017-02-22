using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductIcdBLC
    {
        #region Constructors

        private ProductIcdBLC() { }

        #endregion


        #region Public Methods

        public void addICD(ProductICDInfo BEntity)
        {
            MedinetDataAccessComponent.ProductIcdDALC.Instance.insert(BEntity);

        }

        public void removeICD(ProductICDInfo BEntity)
        {
            MedinetDataAccessComponent.ProductIcdDALC.Instance.delete(BEntity);
        }

        public bool checkICDByProduct(int ICDId, int productId, int pharmaFormId)
        {
            if (productId <= 0 || ICDId <= 0)
                throw new ArgumentException("The Therapeutic or product or pharmaform does not exist.");

            return (MedinetDataAccessComponent.ProductIcdDALC.Instance.getICDByIDByProduct(ICDId, productId,pharmaFormId) == null ? false : true);
        }

        public string InsertICD(List<ProductICDInfo> icds) {
            foreach (ProductICDInfo item in icds)
            {
                if (!this.checkICDByProduct(item.ICDId,item.ProductId, item.PharmaFormId))
                {
                    this.addICD(item);
                }    
            }
            return "";
        }

        #endregion
        public static readonly ProductIcdBLC Instance = new ProductIcdBLC();
    }
}
