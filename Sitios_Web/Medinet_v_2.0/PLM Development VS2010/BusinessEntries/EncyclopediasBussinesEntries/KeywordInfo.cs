using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which represents information about a KeywordInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class KeywordInfo
    {
         #region Constructors

        ///<summary> 
        /// Initializes a new instance of the ICDinfo class.
        ///</summary>

        public KeywordInfo() { }

        #endregion

        #region properties
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for KeyWordId.
        ///     </para>
        ///     <para>
        ///          KeyWord identifier.
        ///     </para>
        /// </summary> 
        [DataMember]
        public int KeyWordId
        {
            get { return this._keyWordId; }
            set { this._keyWordId = value; }

        }

        
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for KeyWord.
        ///     </para>
        ///     <para>
        ///          Name in English description associated with KeyWord.
        ///     </para>
        /// </summary> 
        [DataMember]
        public string KeyWord
        {
            get { return this._keyWord; }
            set { this._keyWord = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Keyword is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }
        #endregion

        private int _keyWordId;
        private string _keyWord;
        private bool _active;   
    }
}
