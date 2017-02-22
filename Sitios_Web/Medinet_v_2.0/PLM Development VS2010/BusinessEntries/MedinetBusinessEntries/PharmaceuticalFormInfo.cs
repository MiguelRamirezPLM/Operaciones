using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a Pharmaceutical Form.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Pharmaceutical Form is the way in which there is a drug.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class PharmaceuticalFormInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmaceuticalFormInfo class. Not receive parameters.
        /// </summary>
        public PharmaceuticalFormInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaFormId.
        ///     </para>
        ///     <para>
        ///         Pharmaceutical Form Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaFormId
        {
            get{ return this._pharmaFormId; }
            set{ this._pharmaFormId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or description of the PharmaceuticalForm.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get{ return this._description; }
            set{ this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EnglishDescription.
        ///     </para>
        ///     <para>
        ///         Name or description in English of the PharmaceuticalForm.
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
        ///         Indicates if the PharmaceuticalForm is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get{ return this._active; }
            set{ this._active = value; }
        }

        #endregion

        private int _pharmaFormId;
        private string _description;
        private string _englishDescription;
        private bool _active;

    }
}
