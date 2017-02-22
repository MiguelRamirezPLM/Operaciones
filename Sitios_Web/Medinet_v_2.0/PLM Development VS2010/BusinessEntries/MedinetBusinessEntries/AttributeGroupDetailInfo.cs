using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the master attribute detail.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AttributeGroupDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributeGroupDetailInfo class. Not receive parameters.
        /// </summary>
        public AttributeGroupDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeGroupId.
        ///     </para>
        ///     <para>
        ///         Master attribute identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeGroupId
        {
            get
            {
                return this._attributeGroupId;
            }
            set
            {
                this._attributeGroupId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AttributeGroupName.
        ///     </para>
        ///     <para>
        ///         Master attribute name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string AttributeGroupName
        {
            get
            {
                return this._attributeGroupName;
            }
            set
            {
                this._attributeGroupName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ModifiedAttributeGroup.
        ///     </para>
        ///     <para>
        ///         It indicates if the master attribute was modified.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool ModifiedAttributeGroup
        {
            get
            {
                return this._modifiedAttributeGroup;
            }
            set
            {
                this._modifiedAttributeGroup = value;
            }
        }

        #endregion

        private int _attributeGroupId;
        private string _attributeGroupName;
        private bool _modifiedAttributeGroup;

    }
}
