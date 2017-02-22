using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SearchSubstance
    {
         #region Constructor
        public SearchSubstance(string ActiveSubstance)
        {
            Active = ActiveSubstance;
        }

        #endregion

        #region Member

        private string Active;

        #endregion

        #region Properties

        public string ActiveSubstance
        {
            get
            {
                return Active;
            }
            set
            {
                Active = value;
            }
        }       
        #endregion
    }
}