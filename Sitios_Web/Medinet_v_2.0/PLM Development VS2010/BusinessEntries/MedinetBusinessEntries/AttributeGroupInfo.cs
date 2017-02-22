using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the master attribute information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AttributeGroupInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AttributeGroupInfo class. Not receive parameters.
        /// </summary>
        public AttributeGroupInfo() { }

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
        ///         Property which gets or sets a value for AttributeGroupOrder.
        ///     </para>
        ///     <para>
        ///         Master attribute order.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AttributeGroupOrder
        {
            get
            {
                return this._attributeGroupOrder;
            }
            set
            {
                this._attributeGroupOrder = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the master attribute is enabled or disabled.
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

        private int _attributeGroupId;
        private string _attributeGroupName;
        private int _attributeGroupOrder;
        private bool _active;

    }
}
