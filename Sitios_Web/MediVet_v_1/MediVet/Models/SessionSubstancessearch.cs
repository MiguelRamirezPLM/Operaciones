using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionSubstancessearch
    {
        #region Constructor

        public SessionSubstancessearch(String ActiveSubstanceName)
        {
            term = ActiveSubstanceName;
        }

        #endregion

        #region Member

        private string term;

        #endregion

        #region Properties

        public string ActiveSubstanceName
        {
            get
            {
                return term;
            }
            set
            {
                term = value;
            }
        }
        #endregion
    }
}