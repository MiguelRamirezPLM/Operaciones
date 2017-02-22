using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents information about the Client's professional practice.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ProfessionalPracticeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProfessionalPracticeInfo class. Not receive parameters.
        /// </summary>
        public ProfessionalPracticeInfo() { }
        
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PracticeId.
        ///     </para>
        ///     <para>
        ///         Professional practice identifier .
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PracticeId
        {
            get { return this._practiceId; }
            set { this._practiceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Professional practice description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Professional practice is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _practiceId;
        private string _description;
        private bool _active;

    }
}
