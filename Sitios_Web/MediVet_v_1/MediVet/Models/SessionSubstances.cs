using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionSubstances
    {
        #region Constructor

        public SessionSubstances(String ActiveSubstanceName)
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