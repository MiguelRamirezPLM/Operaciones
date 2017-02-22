using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the external pack information associated with a product presentation.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <see cref="MedinetBusinessEntries.PresentationsInfo"/>
    /// <see cref="MedinetBusinessEntries.WeightUnitsInfo"/>
    /// <see cref="MedinetBusinessEntries.ExternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.ContentUnitsInfo"/>
    [DataContract]
    public class InternalPacksInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the InternalPacksInfo class. Not receive parameters.
        /// </summary>
        public InternalPacksInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalPackId.
        ///     </para>
        ///     <para>
        ///         Internal pack identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int InternalPackId
        {
            get
            {
                return this._internalPackId;
            }
            set
            {
                this._internalPackId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InternalPackName.
        ///     </para>
        ///     <para>
        ///         Internal pack name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InternalPackName
        {
            get
            {
                return this._internalPackName;
            }
            set
            {
                this._internalPackName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the internal pack is enabled or disabled.
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

        private int _internalPackId;
        private string _internalPackName;
        private bool _active;

    }
}
