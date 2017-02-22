using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{    /// <summary>
    ///     Class which represents the Active Substance information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is an active ingredient contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class ActiveSubstanceInfo
    {

        #region Constructor

        public ActiveSubstanceInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         Active Substance identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ActiveSubstanceId
        {
            get
            {
                return this._activeSubstanceId;
            }
            set
            {
                this._activeSubstanceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Active Substance name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstanceName
        {
            get
            {
                return this._activeSubstanceName;
            }
            set
            {
                this._activeSubstanceName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the ActiveSubstance is enabled.
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
        [DataMember]
        public string ProductName
        {
            get
            {
                return this._productName;
            }
            set
            {
                this._productName = value;
            }
        }

        [DataMember]
        public int ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        #endregion

        private int _activeSubstanceId;
        private string _activeSubstanceName;
        private bool _active;
        private string _productName;
        private int _productId;

    }
}
