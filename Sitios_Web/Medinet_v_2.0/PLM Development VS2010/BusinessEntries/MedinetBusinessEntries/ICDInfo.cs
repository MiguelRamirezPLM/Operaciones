﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a ICD.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
     [DataContract]
    public class ICDInfo
    {
        #region Constructors

        ///<summary> 
        /// Initializes a new instance of the ICDinfo class.
        ///</summary>

        public ICDInfo() { }

        #endregion


        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICDId.
        ///     </para>
        ///     <para>
        ///         ICD Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ICDId
        {

            get { return this._icdId; }
            set { this._icdId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///       Name of the Parent associated with the ICD.   
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
        ///         Property which gets or sets a value for ICDKey.
        ///     </para>
        ///     <para>
        ///         Name of the key associated with the ICD.  
        ///     </para>
        /// </summary>
 

        [DataMember]
        public string ICDKey
        {
            get { return this._icdKey; }
            set { this._icdKey = value; }

        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Spanish Description.
        ///     </para>
        ///     <para>
        ///          Name in Spanish description associated with ICD.
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
        ///         Property which gets or sets a value for English Description.
        ///     </para>
        ///     <para>
        ///          Name in English description associated with ICD.
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
        ///         Property which gets or sets a value for ICDActive.
        ///     </para>
        ///     <para>
        ///         Indicates if the ICD is enabled.
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
        ///         Property which gets or sets a value for the count of Sons.
        ///     </para>
        ///     <para>
        ///         ICD count Sons.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ICDSons
        {

            get { return this._icdSons; }
            set { this._icdSons = value; }

        }


        #endregion



        private int _icdId;
        private int? _parentId;
        private string _icdKey;
        private string _spanishDescription;
        private string _englishDescription;
        private int _icdSons;
        private bool _active;

    }
}