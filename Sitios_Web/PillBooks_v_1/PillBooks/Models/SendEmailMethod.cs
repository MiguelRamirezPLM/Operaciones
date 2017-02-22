using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class SendEmailMethod
    {
        #region Constructor

        public SendEmailMethod(int? PillBookId, String PillBookName)
        {
            PBId = PillBookId;
            PBName = PillBookName;
        }

        #endregion

        #region Members

        public int? PBId;
        public string PBName;

        #endregion

        #region Properties


        public int? PillBookId
        {
            get
            {
                return PBId;
            }
            set
            {
                PBId = value;
            }
        }

        public string PillBookName
        {
            get
            {
                return PBName;
            }
            set
            {
                PBName = value;
            }

        }
        #endregion
    }
}