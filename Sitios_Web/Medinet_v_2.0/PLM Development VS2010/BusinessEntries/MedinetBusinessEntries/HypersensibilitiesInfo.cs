using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
   public class HypersensibilitiesInfo
    {
        #region Constructor
        public HypersensibilitiesInfo() { }
         #endregion
        [DataMember]
        public int HypersensibilityId
        {
            get { return this.hypersensibilityId; }
            set { this.hypersensibilityId = value; }
        }
        [DataMember]
        public string HypersensibilityName
        {
            get { return this._hypersensibilityName; }
            set { this._hypersensibilityName = value; }
        }
        private int hypersensibilityId;
        private string _hypersensibilityName;
    }
}
