using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the detail of information about a Client, which inherits from the class ClientDetailCodeInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ClientDetailCodeInfo : ClientDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientDetailInfo class. Not receive parameters.
        /// </summary>
        public ClientDetailCodeInfo() 
        {

        }

        /// <summary>
        ///     Initializes a new instance of the ClientDetailCodeInfo class.
        /// </summary>
        /// <param name="clientDetailInfo">
        ///     Basic data pertaining to a customer
        /// </param>
        public ClientDetailCodeInfo(ClientDetailInfo clientDetailInfo)
        {
            this.Profession = clientDetailInfo.Profession;
            this.OtherProfession = clientDetailInfo.OtherProfession;
            this.Speciality = clientDetailInfo.Speciality;
            this.OtherSpeciality = clientDetailInfo.OtherSpeciality;
            this.ProfessionalLicense = clientDetailInfo.ProfessionalLicense;
            this.ResidenceId = clientDetailInfo.ResidenceId;
            this.ResidenceKey = clientDetailInfo.ResidenceKey;
            this.CountryName = clientDetailInfo.CountryName;
            this.StateName = clientDetailInfo.StateName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientDetail.
        ///     </para>
        ///     <para>
        ///         It is an object of type <see cref="PLMClientsBusinessEntities.ClientDetailInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public ClientDetailInfo ClientDetail
        {
            get { return this._clientDetail; }
            set { this._clientDetail = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get { return this._codeId; }
            set { this._codeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Codestring.
        ///     </para>
        ///     <para>
        ///         Codestring associated with the customer.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Codestring
        {
            get { return this._codestring; }
            set { this._codestring = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Code Prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get
            {
                return this._prefixId;
            }
            set
            {
                this._prefixId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Prefix Name.
        ///     </para>
        ///     <para>
        ///         Code Prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixName
        {
            get
            {
                return this._prefixName;
            }
            set
            {
                this._prefixName = value;
            }
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Country ShortName
        ///     </para>
        ///         Country ShortName
        ///     <para>
        ///     
        /// </para>
        /// </summary>
        [DataMember]
        public string CountryShortName
        {
            get { return this._countryShortName; }
            set { this._countryShortName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for State ShortName
        ///     </para>
        ///         State ShortName
        ///     <para>
        ///     
        /// </para>
        /// </summary>
        [DataMember]
        public string StateShortName
        {
            get { return this._stateShortName; }
            set { this._stateShortName = value; }
        }
        

        #endregion

        private string _prefixName;
        private int _prefixId;
        private ClientDetailInfo _clientDetail;
        private int _codeId;
        private string _codestring;
        private string _countryShortName;
        private string _stateShortName;
    }
}
