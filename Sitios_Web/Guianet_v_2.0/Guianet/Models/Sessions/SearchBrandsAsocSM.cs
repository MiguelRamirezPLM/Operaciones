using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SearchBrandsAsocSM
    {
        #region Constructor
        public SearchBrandsAsocSM(string BrandName)
        {
            Brand = BrandName;
        }

        #endregion

        #region Member

        private string Brand;

        #endregion

        #region Properties

        public string BrandName
        {
            get
            {
                return Brand;
            }
            set
            {
                Brand = value;
            }
        }
        #endregion
    }
}