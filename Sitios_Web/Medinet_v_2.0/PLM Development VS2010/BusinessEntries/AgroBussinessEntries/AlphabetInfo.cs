using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents information about an element of the alphabet.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AlphabetInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AlphabetInfo class. Not receive parameters.
        /// </summary>
        public AlphabetInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AlphabetId.
        ///     </para>
        ///     <para>
        ///         Identifier of a letter of the alphabet.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte AlphabetId
        {
            get { return this._alphabetId; }
            set { this._alphabetId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Letter.
        ///     </para>
        ///     <para>
        ///         Letter of the alphabet.
        ///     </para>
        /// </summary>
        [DataMember]
        public char Letter
        {
            get { return this._letter; }
            set { this._letter = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Letter is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _alphabetId;
        private char _letter;
        private bool _active;
    }
}
