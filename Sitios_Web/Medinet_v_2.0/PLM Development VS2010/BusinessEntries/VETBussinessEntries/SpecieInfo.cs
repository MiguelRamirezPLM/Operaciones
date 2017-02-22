using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Species information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]


    public class SpecieInfo
    {

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the SpecieInfo class. Not receive parameters.
        /// </summary>

        public SpecieInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SpecieId.
        ///     </para>
        ///     <para>
        ///         Specie Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SpecieId
        {
            get { return this._specieId; }
            set { this._specieId= value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SpecieName.
        ///     </para>
        ///     <para>
        ///         Name or Description of the Specie.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SpecieName
        {
            get { return this._specieName; }
            set { this._specieName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Specie is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

  
        #endregion

        private int _specieId;
        private string _specieName;
        private bool _active;


    }
}
