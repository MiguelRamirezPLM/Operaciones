using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the Description Therapeutics.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class TherapeuticDescriptionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticDescriptionInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticDescriptionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticDescriptionId.
        ///     </para>
        ///     <para>
        ///         Identifier of the therapeutic's description.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticDescriptionId
        {
            get { return this._therapeuticDescriptionId; }
            set { this._therapeuticDescriptionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Explanation.
        ///     </para>
        ///     <para>
        ///         Detailed description of the therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Explanation
        {
            get { return this._explanation; }
            set { this._explanation = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic Description is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _therapeuticDescriptionId;
        private int _therapeuticId;
        private string _explanation;
        private bool _active;

    }
}
