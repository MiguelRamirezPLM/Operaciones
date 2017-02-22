using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionrecategorization
    {
        #region Constructor

        public sessionrecategorization(bool flag)
        {
            _flag = flag;

        }

        #endregion

        #region Member

        public bool _flag;

        #endregion

        #region Properties

        public bool flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
            }
        }
        #endregion
    }
}