using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SearchBrandsSM
    {
        #region Constructor
        public SearchBrandsSM(string BrandName)
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