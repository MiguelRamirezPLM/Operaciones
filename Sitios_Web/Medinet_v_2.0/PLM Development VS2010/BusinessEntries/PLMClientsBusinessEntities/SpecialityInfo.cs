using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Speciality information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class SpecialityInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SpecialityInfo class. Not receive parameters.
        /// </summary>
        public SpecialityInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SpecialityId.
        ///     </para>
        ///     <para>
        ///         Speciality identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public short SpecialityId
        {
            get { return this._specialityId; }
            set { this._specialityId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SpecialityName.
        ///     </para>
        ///     <para>
        ///         Speciality name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SpecialityName
        {
            get { return this._specialityName; }
            set { this._specialityName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Speciality is enabled or disabled.
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
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Speciality short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

        #endregion

        private short _specialityId;
        private string _specialityName;
        private bool _active;
        private string _shortName;

    }
}
