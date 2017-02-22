using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class SpecialityInfo
    {
        #region Constructors

        public SpecialityInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int SpecialityId
        {
            get
            {
                return this._specialityId;
            }
            set
            {
                this._specialityId = value;
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        [DataMember]
        public bool Active
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


        #endregion

        private int _specialityId;

        private string _description;

        private bool _active;
    }
}
