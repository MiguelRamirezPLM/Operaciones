using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceSource information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class PharmacovigilanceSourceInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceSourceInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceSourceInfo() { }

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
        ///         Property which gets or sets a value for AddressId.
        ///     </para>
        ///     <para>
        ///         Address identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AddressId
        {
            get { return this._addressId; }
            set { this._addressId = value; }
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
        public byte PharmaInfoTypeId
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
        public byte SourceId
        {
            get { return this._sourceId; }
            set { this._sourceId = value; }
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
        private int _addressId;
        private byte _pharmaInfoTypeId;
        private byte _sourceId;
        private DateTime? _receptionDate;

     
    }
}
