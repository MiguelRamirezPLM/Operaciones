using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VetBusinessEntries
{

    /// <summary>
    ///     Class which represents the Active Substance information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    /// </remarks>
    [DataContract]
     public class ActiveSubstanceInfo
    {

        #region Constructor


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
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
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
        public string ActiveSubstanceName
        {
            get
            {
                return this._activeSubstanceName;
            }
            set
            {
                this._activeSubstanceName = value;
            }
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
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _activeSubstanceId;
        private string _activeSubstanceName;
        private bool _active;




    }
}
