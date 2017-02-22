using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionTypesLI
    {
        #region Constructor
        public SessionTypesLI(int? Quantity, string Types)
        {
            qtty = Quantity;
            _type = Types;
        }

        #endregion

        #region Member

        private int? qtty;
        private string _type;

        #endregion

        #region Properties

        public int? Quantity
        {
            get
            {
                return qtty;
            }
            set
            {
                qtty = value;
            }
        }

        public string Types
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        #endregion
    }
}