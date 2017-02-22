using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceSourceDetail information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>

    public class PharmacovigilanceSourceDetailInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceSourceDetailInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceSourceDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceSourceId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceSource identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmacovigilanceSourceId
        {
            get { return this._pharmacovigilanceSourceId; }
            set { this._pharmacovigilanceSourceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaSourceInfoTypeId.
        ///     </para>
        ///     <para>
        ///         PharmaSourceInfoType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmaSourceInfoTypeId
        {
            get { return this._pharmaSourceInfoTypeId; }
            set { this._pharmaSourceInfoTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Name.
        ///     </para>
        ///     <para>
        ///         Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaInfoTypeId.
        ///     </para>
        ///     <para>
        ///         PharmaInfoType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? PharmaInfoTypeId
        {
            get { return this._pharmaInfoTypeId; }
            set { this._pharmaInfoTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SourceId.
        ///     </para>
        ///     <para>
        ///         Source identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? SourceId
        {
            get { return this._sourceId; }
            set { this._sourceId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Street.
        ///     </para>
        ///     <para>
        ///         Street identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Street
        {
            get { return this._street; }
            set { this._street = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Suburb.
        ///     </para>
        ///     <para>
        ///         Suburb identifier.
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
        ///         Property which gets or sets a value for StateId.
        ///     </para>
        ///     <para>
        ///         State identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int StateId
        {
            get { return this._stateId; }
            set { this._stateId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ZipCode.
        ///     </para>
        ///     <para>
        ///         ZipCode identifier.
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
        ///         Property which gets or sets a value for Phone.
        ///     </para>
        ///     <para>
        ///         Phone identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Phone
        {
            get { return this._phone; }
            set { this._phone = value; }
        }



        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ReceptionDate.
        ///     </para>
        ///     <para>
        ///         Reception Date identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? ReceptionDate
        {
            get { return this._receptionDate; }
            set { this._receptionDate = value; }
        }


        #endregion

        private int _pharmacovigilanceSourceId;
        private byte _pharmaSourceInfoTypeId;
        private string _name;
        private byte? _pharmaInfoTypeId;
        private byte? _sourceId;
        private DateTime? _receptionDate;

        private string _street;
        private string _suburb;
        private int _stateId;
        private string _zipCode;
        private string _phone;
       




    }

}
