using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchDiv
    {
         #region Constructor
        public SearchDiv(string DivisionName)
        {
            DivName = DivisionName;
        }

        #endregion

        #region Member

        private string DivName;

        #endregion

        #region Properties

        public string DivisionName
        {
            get
            {
                return DivName;
            }
            set
            {
                DivName = value;
            }
        }       
        #endregion
    }
}