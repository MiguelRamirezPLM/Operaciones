using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the Therapeutics.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         ATC.- (Anatomical, Therapeutic, Chemical classification system). Is an index of drug substances and drugs, organized by therapeutic groups.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class TherapeuticInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic have an parent Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticKey.
        ///     </para>
        ///     <para>
        ///         Therapeutic key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TherapeuticKey
        {
            get { return this._therapeuticKey; }
            set { this._therapeuticKey = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or Description of the Therapeutic.
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
        ///         Property which gets or sets a value for SpanishDescription.
        ///     </para>
        ///     <para>
        ///         Name or Description in Spanish of the Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SpanishDescription
        {
            get { return this._spanishDescription; }
            set { this._spanishDescription = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic is enabled.
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
        ///         Property which gets or sets a value for TherapeuticSons.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic have an son Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticSons
        {
            get { return this._therapeuticSons; }
            set { this._therapeuticSons = value; }
        }

        #endregion

        private int _therapeuticId;
        private int? _parentId;
        private string _therapeuticKey;
        private string _description;
        private string _spanishDescription;
        private bool _active;
        private int _therapeuticSons;
    }
}
