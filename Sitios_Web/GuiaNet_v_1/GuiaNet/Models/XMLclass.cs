using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GuiaNet.Models
{
    public class XMLclass
    {
        #region Constructor

        public XMLclass(List<String> list)
        {
            listitems = list;
        }

        #endregion

        #region Members

        public List<String> listitems;

        #endregion

        #region Properties

        public List<String> list
        {
            get
            {
                return listitems;
            }
            set
            {
                listitems = value;
            }
        }

        #endregion
    }
}