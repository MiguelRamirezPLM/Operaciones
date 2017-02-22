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
    /// <see cref="MedinetBusinessEntries.InternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.ContentUnitsInfo"/>
    [DataContract]
    public class ExternalPacksInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ExternalPacksInfo class. Not receive parameters.
        /// </summary>
        public ExternalPacksInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ExternalPackId.
        ///     </para>
        ///     <para>
        ///         External pack identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ExternalPackId
        {
            get
            {
                return this._externalPackId;
            }
            set
            {
                this._externalPackId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ExternalPackName.
        ///     </para>
        ///     <para>
        ///         External pack name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ExternalPackName
        {
            get
            {
                return this._externalPackName;
            }
            set
            {
                this._externalPackName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the external pack is enabled or disabled.
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

        private int _externalPackId;
        private string _externalPackName;
        private bool _active;

    }
}
