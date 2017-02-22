using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class searchcategoryreference
    {
        #region Constructor

        public searchcategoryreference(string Description)
        {
            category = Description;
        }

        #endregion

        #region Member

        private string category;

        #endregion

        #region Properties

        public string Description
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        #endregion
    }
}