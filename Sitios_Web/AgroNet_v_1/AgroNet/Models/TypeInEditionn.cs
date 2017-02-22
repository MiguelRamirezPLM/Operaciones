using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class TypeInEditionn
    {
           #region Constructor
        public TypeInEditionn(string TypeInEdition)
        {
            type = TypeInEdition;
        }

        #endregion

        #region Member

        private string type;

        #endregion

        #region Properties

        public string TypeInEdition
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }       
        #endregion
    }
}