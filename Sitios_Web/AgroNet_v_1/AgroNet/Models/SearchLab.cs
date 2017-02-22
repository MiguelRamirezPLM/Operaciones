using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchLab
    {
          #region Constructor
        public SearchLab(string LaboratoryName)
        {
            Laboratory = LaboratoryName;
        }

        #endregion
        #region Member

        private string Laboratory;

        #endregion

        #region Properties

        public string LaboratoryName
        {
            get
            {
                return Laboratory;
            }
            set
            {
                Laboratory = value;
            }
        }       
        #endregion
    }
}