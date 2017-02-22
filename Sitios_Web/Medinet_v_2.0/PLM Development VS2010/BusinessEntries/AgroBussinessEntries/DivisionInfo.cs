using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{    
    /// <summary>
    ///     Class which represents the Laboratory information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class DivisionInfo
    {

                #region Constructor

        public DivisionInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionName.
        ///     </para>
        ///     <para>
        ///         Name or Description of the Division.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DivisionName
        {
            get { return this._divisionName; }
            set { this._divisionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Short Name of the Division.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

  
        #endregion

        private int _divisionId;
        private string _divisionName;
        private string _shortName;
    }
}
