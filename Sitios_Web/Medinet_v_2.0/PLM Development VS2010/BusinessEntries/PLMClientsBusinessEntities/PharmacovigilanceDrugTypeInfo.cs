using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the PharmacovigilanceDrugType information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class PharmacovigilanceDrugTypeInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceDrugTypeInfo class. Not receive parameters.
        /// </summary>
        public PharmacovigilanceDrugTypeInfo() { }

        #endregion


        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacovigilanceDrugTypeId.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrugType identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PharmacovigilanceDrugTypeId
        {
            get { return this._pharmacovigilanceDrugTypeId; }
            set { this._pharmacovigilanceDrugTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         PharmacovigilanceDrugTypes description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the PharmacovigilanceType is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private byte _pharmacovigilanceDrugTypeId;
        private string _description;
        private bool _active;
    }
}
