using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the event category information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.EventsInfo"/>
    [DataContract]
   public class EventsTypeInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EventCategoriesInfo class. Not receive parameters.
        /// </summary>
        public EventsTypeInfo() { }

        #endregion

        
        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeId.
        ///     </para>
        ///     <para>
        ///         Type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value TypeName.
        ///     </para>
        ///     <para>
        ///         Type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the Category is enabled or disabled.
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

        private byte _typeId;
        private string _typeName;
        private bool _active;
    }
}
