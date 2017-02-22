using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class attrproduction
    {
         #region Constructor
        public attrproduction(String productname, String propag, String attrb, String laboratoryname)
        {
            pname = productname;
            prop = propag;
            attr = attrb;
            labn = laboratoryname;
        }

        #endregion

        #region Member

        private String pname;
        private String prop;
        private String attr;
        private String labn;

        #endregion

        #region Properties

        public String productname
        {
            get
            {
                return pname;
            }
            set
            {
                pname = value;
            }
        }

        public String propag
        {
            get
            {
                return prop;
            }
            set
            {
                prop = value;
            }
        }

        public String attrb
        {
            get
            {
                return attr;
            }
            set
            {
                attr = value;
            }
        }

        public String laboratoryname
        {
            get
            {
                return labn;
            }
            set
            {
                labn = value;
            }
        }
      
        #endregion
    }
}