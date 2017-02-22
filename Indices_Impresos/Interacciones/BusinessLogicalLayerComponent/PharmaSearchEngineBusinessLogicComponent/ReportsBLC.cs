using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineBusinessLogicComponent
{
    public class ReportsBLC
    {
        #region Constructors

        private ReportsBLC() { }

        #endregion

        #region public methods

        #region Vendors

        public List<VendorDetailInfo> getProductByVendor(int editionId, int moduleId, int divisionId, string vendor)
        {
            if (editionId <= 0 || moduleId <= 0 || divisionId <= 0 || vendor == null)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                List<VendorDetailInfo> Product = new List<VendorDetailInfo>();
                Product = PharmaSearchEngineDataAccessComponent.ReportsDALC.Instance.getProductsByVendor(editionId, moduleId, divisionId, vendor);
                return Product;
            }
        }

        public List<VendorDetailInfo> getAllProductByVendor(int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                List<VendorDetailInfo> Product = new List<VendorDetailInfo>();
                Product = PharmaSearchEngineDataAccessComponent.ReportsDALC.Instance.getAllProductsByVendor(editionId);
                return Product;
            }
        }

        #endregion

        #region Designer

        public List<DesignerDetailInfo> getDesignerByVendor(int editionId, int moduleId, int divisionId, string vendor)
        {
            if (editionId <= 0 || moduleId <= 0 || divisionId <= 0 || vendor == null)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                List<DesignerDetailInfo> Product = new List<DesignerDetailInfo>();
                Product = PharmaSearchEngineDataAccessComponent.ReportsDALC.Instance.getDesignerByVendor(editionId, moduleId, divisionId, vendor);
                return Product;
            }
        }
        #endregion

        #region Medic

        public List<MedicDetailInfo> getProductByMedic(int editionId, int moduleId, int divisionId, string vendor)
        {
            if (editionId <= 0 || moduleId <= 0 || divisionId <= 0 || vendor == null)
                throw new ArgumentException("The book edition, division, category or drug does not exist");
            else
            {
                List<MedicDetailInfo> Product = new List<MedicDetailInfo>();
                Product = PharmaSearchEngineDataAccessComponent.ReportsDALC.Instance.getProductsByMedic(editionId, moduleId, divisionId, vendor);
                return Product;
            }
        }

        #endregion

        #endregion

        public static readonly ReportsBLC Instance = new ReportsBLC();
    }
}
