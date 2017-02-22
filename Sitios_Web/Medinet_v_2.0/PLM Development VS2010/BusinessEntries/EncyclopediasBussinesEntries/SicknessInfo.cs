using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which represents information about a Sickness.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class SicknessInfo
    {
         #region Constructors

        ///<summary> 
        /// Initializes a new instance of the SicknessInfo class.
        ///</summary>

        public SicknessInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SicknessId.
        ///     </para>
        ///     <para>
        ///         Sickness Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SicknessId
        {

            get { return this._sicknessId; }
            set { this._sicknessId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SicknessName.
        ///     </para>
        ///     <para>
        ///         Name
        ///     </para>
        /// </summary>
        [DataMember]
        public string SicknessName
        {

            get { return this._sicknessName; }
            set { this._sicknessName = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates the status of the sickness.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {

            get { return this._active; }
            set { this._active = value; }

        }

        #endregion
        private int _sicknessId;
        private string _sicknessName;
        private bool _active;
    }

}
