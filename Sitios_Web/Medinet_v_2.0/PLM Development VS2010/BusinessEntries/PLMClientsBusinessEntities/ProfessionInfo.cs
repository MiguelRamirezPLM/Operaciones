using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the Professional practices.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ProfessionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProfessionInfo class. Not receive parameters.
        /// </summary>
        public ProfessionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProfessionId.
        ///     </para>
        ///     <para>
        ///         Profession identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public short ProfessionId
        {
            get { return this._professionId; }
            set { this._professionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Parent Profession.
        ///     </para>
        /// </summary>
        [DataMember]
        public short? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProfessionName.
        ///     </para>
        ///     <para>
        ///         Profession name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ProfessionName
        {
            get { return this._professionName; }
            set { this._professionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ReqProfessionalLicense.
        ///     </para>
        ///     <para>
        ///         Indicates if the profession requires professional license.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ReqProfessionalLicense
        {
            get { return this._reqProfessionalLicense; }
            set { this._reqProfessionalLicense = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Profession is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private short _professionId;
        private short? _parentId;
        private string _professionName;
        private bool _reqProfessionalLicense;
        private bool _active;

    }
}
