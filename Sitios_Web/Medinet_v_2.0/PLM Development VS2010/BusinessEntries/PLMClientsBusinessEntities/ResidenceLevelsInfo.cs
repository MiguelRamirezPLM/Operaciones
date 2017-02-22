using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Residence level's information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ResidenceLevelsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ResidenceLevelsInfo class. Not receive parameters.
        /// </summary>
        public ResidenceLevelsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceId.
        ///     </para>
        ///     <para>
        ///         Residence identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte? ResidenceId
        {
            get
            {
                return this._residenceId;
            }
            set
            {
                this._residenceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceKey.
        ///     </para>
        ///     <para>
        ///         Residence key.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ResidenceKey
        {
            get
            {
                return this._residenceKey;
            }
            set
            {
                this._residenceKey = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResidenceName.
        ///     </para>
        ///     <para>
        ///         Residence name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ResidenceName
        {
            get
            {
                return this._residenceName;
            }
            set
            {
                this._residenceName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Residence level is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool? Active
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

        private byte? _residenceId;
        private string _residenceKey;
        private string _residenceName;
        private bool? _active;

    }
}
