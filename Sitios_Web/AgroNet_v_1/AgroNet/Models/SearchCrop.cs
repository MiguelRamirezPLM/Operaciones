using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchCrop
    {
         #region Constructor
        public SearchCrop(string CropName)
        {
            crop = CropName;
        }

        #endregion

        #region Member

        private string crop;

        #endregion

        #region Properties

        public string CropName
        {
            get
            {
                return crop;
            }
            set
            {
                crop = value;
            }
        }       
        #endregion
    }
}