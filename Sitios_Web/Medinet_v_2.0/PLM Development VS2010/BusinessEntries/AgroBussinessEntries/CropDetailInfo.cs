using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Crop information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Is Crop contained in the product.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class CropDetailInfo
    {
        #region Constructor

        public CropDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CropId.
        ///     </para>
        ///     <para>
        ///         Crop identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CropId
        {
            get { return this._cropId; }
            set { this._cropId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CropName.
        ///     </para>
        ///     <para>
        ///         Crop name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CropName
        {
            get { return this._cropName; }
            set { this._cropName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Crop is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ScientificName.
        ///     </para>
        ///     <para>
        ///         Scientific name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ScientificName
        {
            get { return this._scientificName; }
            set { this._scientificName = value; }
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

        private int _cropId;
        private string _cropName;
        private string _scientificName;
        private bool _active;
        private string _productName;
        private int _productId;

    }
}
