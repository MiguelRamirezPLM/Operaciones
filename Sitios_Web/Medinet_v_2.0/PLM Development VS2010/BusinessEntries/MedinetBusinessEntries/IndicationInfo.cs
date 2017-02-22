using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a therapeutic indication.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class IndicationInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the IndicationInfo class. Not receive parameters.
        /// </summary>
        public IndicationInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for IndicationId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Indication Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int IndicationId
        {
            get { return this._indicationId; }
            set { this._indicationId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Identifier of the source Therapeutic Indication.
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
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or description of therapeutic indication.
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
        ///         Indicates if the therapeutic indication is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _indicationId;
        private int? _parentId;
        private string _description;
        private bool _active;
        
    }
}
