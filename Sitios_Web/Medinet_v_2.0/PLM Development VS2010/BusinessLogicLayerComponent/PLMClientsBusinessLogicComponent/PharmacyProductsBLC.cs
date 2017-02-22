using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class PharmacyProductsBLC
    {

        #region Constructors

        private PharmacyProductsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PharmacyProductsInfo> getByCompanyByText(int companyClientId, string textSearch)
        {
            if (companyClientId <= 0 || textSearch == null || textSearch.Trim() == "")
                throw new ArgumentException("The Company or Product is not valid.");
            else
                return PLMClientsDataAccessComponent.PharmacyProductsDALC.Instance.getByCompanyByText(companyClientId, textSearch);
        }

        public List<PLMClientsBusinessEntities.PharmacyProductsInfo> getByBranch(int branchId)
        {
            if (branchId <= 0)
                throw new ArgumentException("The Branch is not valid.");
            else
            {
                List<PLMClientsBusinessEntities.PharmacyProductsInfo> pharmacyProducts = PLMClientsDataAccessComponent.PharmacyProductsDALC.Instance.getByBranch(branchId);

                foreach (PLMClientsBusinessEntities.PharmacyProductsInfo product in pharmacyProducts)
                    product.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + "farmacias/Productos/";
                
                return pharmacyProducts;
            }
        }

        #endregion

        public static readonly PharmacyProductsBLC Instance = new PharmacyProductsBLC();

    }
}
