using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class SessionTherapeuticUsesTree
    {
          #region Constructor

        public SessionTherapeuticUsesTree(String Letter)
        {
            Letters = Letter;
        }

        #endregion

        #region Member

        private String Letters;

        #endregion

        #region Properties

        public String Letter
        {
            get
            {
                return Letters;
            }
            set
            {
                Letters = value;
            }
        }
      
        #endregion
    }
}