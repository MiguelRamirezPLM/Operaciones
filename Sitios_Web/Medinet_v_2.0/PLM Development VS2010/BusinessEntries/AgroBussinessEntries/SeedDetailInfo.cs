using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Seed information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is seed contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class SeedDetailInfo
    {
        #region Constructor

        public SeedDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SeedId.
        ///     </para>
        ///     <para>
        ///         Seed identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SeedId
        {
            get { return this._seedId; }
            set { this._seedId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SeedName.
        ///     </para>
        ///     <para>
        ///         Seed name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SeedName
        {
            get { return this._seedName; }
            set { this._seedName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ScientificName.
        ///     </para>
        ///     <para>
        ///         Scientific Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ScientificName
        {
            get { return this._scientificName; }
            set { this._scientificName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Seed is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
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

        private int _seedId;
        private string _seedName;
        private string _scientificName;
        private bool _active;
        private string _productName;
        private int _productId;
    }
}
