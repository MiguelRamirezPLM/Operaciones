using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionsearchSpecies
    {
         #region Constructor

        public SessionsearchSpecies(String SpecieName)
        {
            term = SpecieName;
        }

        #endregion

        #region Member

        private string term;

        #endregion

        #region Properties

        public string SpecieName
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