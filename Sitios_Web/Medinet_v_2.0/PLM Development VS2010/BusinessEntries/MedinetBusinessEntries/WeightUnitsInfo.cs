using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the weight unit information associated with a product presentation.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <see cref="MedinetBusinessEntries.PresentationsInfo"/>
    /// <see cref="MedinetBusinessEntries.ExternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.InternalPacksInfo"/>
    /// <see cref="MedinetBusinessEntries.ContentUnitsInfo"/>
    [DataContract]
    public class WeightUnitsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the WeightUnitsInfo class. Not receive parameters.
        /// </summary>
        public WeightUnitsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for WeightUnitId.
        ///     </para>
        ///     <para>
        ///         Weight unit identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int WeightUnitId
        {
            get
            {
                return this._weightUnitId;
            }
            set
            {
                this._weightUnitId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitName.
        ///     </para>
        ///     <para>
        ///         Weight unit name.
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
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Weight unit short name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get
            {
                return this._shortName;
            }
            set
            {
                this._shortName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the weight unit is enabled or disabled.
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

        private int _weightUnitId;
        private string _unitName;
        private string _shortName;
        private bool _active;

    }
}
