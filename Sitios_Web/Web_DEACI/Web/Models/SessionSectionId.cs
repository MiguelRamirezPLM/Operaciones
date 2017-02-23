using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionSectionId
    {
        #region constructor
        public SessionSectionId(int SectionId)
        {
            _SectionId = SectionId;
        }
        #endregion
        #region member
        private int _SectionId;
        #endregion
        #region Properties
        public int SectionId
        {
            get
            {
                return _SectionId;
            }
            set
            {
                _SectionId = value;
            }
        }
        #endregion
    }
}