using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the content unit information associated with a product presentation.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <see cref="MedinetBusinessEntries.PresentationsInfo"/>
    /// <see cref="MedinetBusinessEntries.ExternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.InternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.WeightUnitsInfo"/>
    [DataContract]
    public class ContentUnitsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ContentUnitsInfo class. Not receive parameters.
        /// </summary>
        public ContentUnitsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ContentUnitId.
        ///     </para>
        ///     <para>
        ///         Content unit identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ContentUnitId
        {
            get
            {
                return this._contentUnitId;
            }
            set
            {
                this._contentUnitId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitName.
        ///     </para>
        ///     <para>
        ///         Content unit name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string UnitName
        {
            get
            {
                return this._unitName;
            }
            set
            {
                this._unitName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the content unit is enabled or disabled.
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

        private int _contentUnitId;
        private string _unitName;
        private bool _active;

    }
}
