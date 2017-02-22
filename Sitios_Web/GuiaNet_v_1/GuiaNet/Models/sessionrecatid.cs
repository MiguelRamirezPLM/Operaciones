using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class sessionrecatid
    {
         #region Constructor
        public sessionrecatid(int CategoryId)
        {
            _CategoryId = CategoryId;
        }

        #endregion

        #region Member

        private int _CategoryId;

        #endregion

        #region Properties

        public int CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                _CategoryId = value;
            }
        }
      
        #endregion
    }
}