using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEVBusinessEntries
{
    [DataContract]
    public class TherapeuticUsesInfo
    {

        #region Constructor

        public TherapeuticUsesInfo() { }
        
        #endregion

        #region Properties

        [DataMember]
        public int TherapeuticId
        {
            get
            {
                return this._therapeuticId;
            }

            set
            {
                this._therapeuticId = value;
            }
        }

        [DataMember]
        public string TherapeuticName
        {
            get
            {
                return this._therapeuticName;
            }

            set
            {
                this._therapeuticName = value;
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

        [DataMember]
        public int? ParentId
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
        public string Llave
        {
            get
            {
                return this._llave;
            }

            set
            {
                this._llave = value;
            }
        }

        [DataMember]
        public int? CountryId
        {
            get
            {
                return this._countryId;
            }

            set
            {
                this._countryId = value;
            }
        }

        [DataMember]
        public int? IdUso
        {
            get
            {
                return this._idUso;
            }

            set
            {
                this._idUso = value;
            }
        }

        [DataMember]
        public int? Id
        {
            get
            {
                return this._id;
            }

            set
            {
                this._id = value;
            }
        }

        [DataMember]
        public int? Nivel
        {
            get
            {
                return this._nivel;
            }

            set
            {
                this._nivel = value;
            }
        }

        #endregion

        private int _therapeuticId;
        private string _therapeuticName;
        private bool _active;
        private int? _parentId;
        private string _llave;
        private int? _countryId;
        private int? _idUso;
        private int? _id;
        private int? _nivel;

    }
}
