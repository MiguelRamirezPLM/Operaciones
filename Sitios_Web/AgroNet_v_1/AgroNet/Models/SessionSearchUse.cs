using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class SessionSearchUse
    {
        #region Constructor
        public SessionSearchUse(string AgrochemicalUseName)
        {
            Active = AgrochemicalUseName;
        }

        #endregion

        #region Member

        private string Active;

        #endregion

        #region Properties

        public string AgrochemicalUseName
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