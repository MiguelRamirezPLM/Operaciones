using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the Attribute information. 
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         An Attribute is an item which is part of a IPP.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class AttributesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributesInfo class. Not receive parameters.
        /// </summary>
        public AttributesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeId.
        ///     </para>
        ///     <para>
        ///         Attribute identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeId
        {
            get { return this._attributeId; }
            set { this._attributeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or description of the attribute.
        ///     </para>
        ///     
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     Property which gets or sets a value for TechnicalSheet.
        /// </summary>
        /// <remarks>
        ///     Indicates if the Attribute belongs to a Technical Sheet.
        /// </remarks>
        [DataMember]
        public bool TechnicalSheet
        {
            get { return this._technicalSheet; }
            set { this._technicalSheet = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Attribute is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _attributeId;
        private string _description;
        private bool _technicalSheet;
        private bool _active;
    }
}
