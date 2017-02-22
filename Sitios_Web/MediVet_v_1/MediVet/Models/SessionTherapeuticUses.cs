using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionTherapeuticUses
    {
         #region Constructor

        public SessionTherapeuticUses(String TherapeuticName)
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