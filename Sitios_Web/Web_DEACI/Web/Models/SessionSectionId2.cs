using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionSectionId2
    {
        #region constructor
        public SessionSectionId2(int SectionId2)
        {
            _SectionId2 = SectionId2;
        }
        #endregion
        #region member
        private int _SectionId2;
        #endregion
        #region Properties
        public int SectionId
        {
            get
            {
                return _SectionId2;
            }
            set
            {
                _SectionId2 = value;
            }
        }
        #endregion
    }
}