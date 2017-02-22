using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the data of the domicile of a Laboratory.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    [DataContract]
    public class DivisionInformationInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DivisionInformationInfo class. Not receive parameters.
        /// </summary>
        public DivisionInformationInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionInfId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionInfId
        {
            get { return this._divisionInfId; }
            set { this._divisionInfId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Address.
        ///     </para>
        ///     <para>
        ///         Street name and number.
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
        ///         Name of the suburb.
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
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         Zip Code of the suburb.
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
        ///         Property which gets or sets a value for Telephone.
        ///     </para>
        ///     <para>
        ///         Telephone number of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Telephone
        {
            get { return this._telephone; }
            set { this._telephone = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Fax.
        ///     </para>
        ///     <para>
        ///         Fax number of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Fax
        {
            get { return this._fax; }
            set { this._fax = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Web.
        ///     </para>
        ///     <para>
        ///         Web page of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Web
        {
            get { return this._web; }
            set { this._web = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for City.
        ///     </para>
        ///     <para>
        ///         City where the laboratory is located.
        ///     </para>
        /// </summary>
        [DataMember]
        public string City
        {
            get { return this._city; }
            set { this._city = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for State.
        ///     </para>
        ///     <para>
        ///         State where the laboratory is located.
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
        ///         Property which or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         E-mail address of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Contact.
        ///     </para>
        ///     <para>
        ///         Responsible for providing laboratory information.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Contact
        {
            get { return this._contact; }
            set { this._contact = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Image.
        ///     </para>
        ///     <para>
        ///         Image which identifies each laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Image
        {
            get { return this._image; }
            set { this._image = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the DivisionInformation is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Web address where the image of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get { return this._baseUrl; }
            set { this._baseUrl = value; }
        }

        #endregion

        private int _divisionInfId;
        private int _divisionId;
        private string _address;
        private string _suburb;
        private string _zipCode;
        private string _telephone;
        private string _fax;
        private string _web;
        private string _city;
        private string _state;
        private string _email;
        private string _contact;
        private string  _image;
        private string _baseUrl;
        private bool _active;
    }
}
