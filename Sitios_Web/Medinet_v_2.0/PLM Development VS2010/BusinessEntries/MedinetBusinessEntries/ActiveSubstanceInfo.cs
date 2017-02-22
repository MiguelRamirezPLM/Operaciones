using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Active Substance information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is an active ingredient contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class ActiveSubstanceInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ActiveSubstanceInfo class. Not receive parameters.
        /// </summary>
        public ActiveSubstanceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         Active Substance identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get { return this._activesubstanceId; }
            set { this._activesubstanceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Active Substance name.
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
        ///         Property which gets or sets a value for EnglishDescription.
        ///     </para>
        ///     <para>
        ///         English name of the Active Substance.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EnglishDescription
        {
            get { return this._englishDescription; }
            set { this._englishDescription = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the ActiveSubstance is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     Property which gets or sets a value for Enunciative.
        /// </summary>
        [DataMember]
        public bool Enunciative
        {
            get { return this._enunciative; }
            set { this._enunciative = value; }
        }

        #endregion

        private int _activesubstanceId;
        private string _description;
        private string _englishDescription;
        private bool _active;
        private bool _enunciative;
    }
}
