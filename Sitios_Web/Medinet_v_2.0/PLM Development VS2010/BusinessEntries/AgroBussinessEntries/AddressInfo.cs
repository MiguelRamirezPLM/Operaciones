using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///      Class which represents the Division address.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
   public  class AddressInfo
    {
         #region Constructor

        public AddressInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionInformationId.
        ///     </para>
        ///     <para>
        ///         DivisionInformation identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionInformationId
        {
            get { return this._divisionInformationId; }
            set { this._divisionInformationId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Address.
        ///     </para>
        ///     <para>
        ///         Address name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Suburb.
        ///     </para>
        ///     <para>
        ///         Suburb name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Suburb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }
               
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Location.
        ///     </para>
        ///     <para>
        ///         City hall.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         Home zipCode.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ZipCode
        {
            get { return this._zipCode; }
            set { this._zipCode = value; }
        }
          
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for State.
        ///     </para>
        ///     <para>
        ///         State name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Lada.
        ///     </para>
        ///     <para>
        ///         Long distance code.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Lada
        {
            get
            {
                return this._lada;
            }
            set
            {
                this._lada = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Telephone.
        ///     </para>
        ///     <para>
        ///         Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Telephone
        {
            get
            {
                return this._telephone;
            }
            set
            {
                this._telephone = value;
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Fax.
        ///     </para>
        ///     <para>
        ///         Fax number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Fax
        {
            get
            {
                return this._fax;
            }
            set
            {
                this._fax = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for City.
        ///     </para>
        ///     <para>
        ///         City name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Web.
        ///     </para>
        ///     <para>
        ///         Web.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Web
        {
            get
            {
                return this._web;
            }
            set
            {
                this._web = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         Email.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Address is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _divisionInformationId;
        private string _address; 
        private string _suburb; 
        private string _zipCode; 
        private string _location; 
        private string _state;
        private string _telephone;
        private string _lada;
        private string _fax;
        private string _city;
        private string _web;
        private string _email;
        private bool _active;
    }
}
