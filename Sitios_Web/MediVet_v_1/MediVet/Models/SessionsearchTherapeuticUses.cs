using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionsearchTherapeuticUses
    {
        #region Constructor

        public SessionsearchTherapeuticUses(String TherapeuticName)
        {
            term = TherapeuticName;
        }

        #endregion

        #region Member

        private string term;

        #endregion

        #region Properties

        public string TherapeuticName
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