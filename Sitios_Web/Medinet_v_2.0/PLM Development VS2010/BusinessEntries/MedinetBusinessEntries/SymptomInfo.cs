using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the symptom information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class SymptomInfo
    {

        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public SymptomInfo()
        {
            this._symptomId = -1;
            this._symptomName = null;
            this._active = false;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomId.
        ///     </para>
        ///     <para>
        ///         Symptom Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SymptomId
        {
            get { return this._symptomId; }
            set { this._symptomId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomName.
        ///     </para>
        ///     <para>
        ///         Name or description of the Symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SymptomName
        {
            get { return this._symptomName; }
            set { this._symptomName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Symptom is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _symptomId;
        private string _symptomName;
        private bool _active;
    }
}

