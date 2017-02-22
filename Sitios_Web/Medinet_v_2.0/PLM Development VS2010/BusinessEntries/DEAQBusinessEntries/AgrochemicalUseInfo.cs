using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{
    [DataContract]
    public class AgrochemicalUseInfo
    {

        #region Constructor

        public AgrochemicalUseInfo() { }
        
        #endregion

        #region Properties

        [DataMember]
        public int AgrochemicalUseId
        {
            get
            {
                return this._agrochemicalUseId;
            }
            set
            {
                this._agrochemicalUseId = value;
            }
        }

        [DataMember]
        public string AgrochemicalUseName
        {
            get
            {
                return this._agrochemicalUseName;
            }
            set
            {
                this._agrochemicalUseName = value;
            }
        }

        [DataMember]
        private int? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        [DataMember]
        public string UseKey
        {
            get
            {
                return this._useKey;
            }
            set
            {
                this._useKey = value;
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

        private int _agrochemicalUseId;
        private string _agrochemicalUseName;
        private int? _parentId;
        private string _useKey;
        private bool _active;

    }
}
