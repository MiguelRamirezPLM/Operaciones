using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AgroBusinessEntries
{
    [DataContract]
    public class LogoInfo
    {
        #region Constructors
        public LogoInfo() { }
        #endregion
        #region Properties
        [DataMember]
        public string divisionShot
        {
            get
            {
                return this._divisionShot;
            }
            set
            {
                this._divisionShot = value;
            }
        }

        [DataMember]
        public int DivisionId
        {
            get
            {
                return this._divisionId;
            }
            set
            {
                this._divisionId = value;
            }
        }
        [DataMember]
        public int Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }
        [DataMember]
        public int LogoId
        {
            get
            {
                return this._logoId;
            }
            set
            {
                this._logoId = value;
            }
        }
        public string LogoSize
        {
            get
            {
                return this._logoSize;
            }
            set
            {
                this._logoSize = value;
            }
        }


        #endregion

        private string _divisionShot;

        private int _divisionId;

        private int _active;
        private int _logoId;
        private string _logoSize;


    }
}