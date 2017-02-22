using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a ICD.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EncyclopediasInfo
    {

        #region Constructors

        ///<summary> 
        /// Initializes a new instance of the ICDinfo class.
        ///</summary>

        public EncyclopediasInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaId.
        ///     </para>
        ///     <para>
        ///         Encyclopedia Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaId
        {

            get { return this._encyclopediaId; }
            set { this._encyclopediaId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaName.
        ///     </para>
        ///     <para>
        ///       Name of the Encyclopedia.   
        ///     </para>
        /// </summary> 
        [DataMember]
        public string EncyclopediaName
        {

            get { return this._encyclopediaName; }
            set { this._encyclopediaName = value; }

        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicate the status for the encyclopedia.  
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
        ///         Property which gets or sets a value for EncyclopediaTypeId.
        ///     </para>
        ///     <para>
        ///          Value for  the type id of Encyclopedia.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaTypeId
        {
            get { return this._encyclopediaTypeId; }
            set { this._encyclopediaTypeId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaImageId.
        ///     </para>
        ///     <para>
        ///          Value for  the Image ID of Encyclopedia.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ImageId
        {
            get { return this._encyclopediaImageId; }
            set { this._encyclopediaImageId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaImage.
        ///     </para>
        ///     <para>
        ///          Value for  the Image of Encyclopedia.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Imagen
        {
            get { return this._imagen; }
            set { this._imagen = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaImageSizes.
        ///     </para>
        ///     <para>
        ///          Value for  the sizes of the Encyclopedia Image.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ImageSizes
        {
            get { return this._encyclopediaImageSizes; }
            set { this._encyclopediaImageSizes = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Sizes.
        ///     </para>
        ///     <para>
        ///          Value for  the sizes of the Encyclopedia Image Sizes.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Sizes
        {
            get { return this._sizes; }
            set { this._sizes = value; }

        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ICD.
        ///     </para>
        ///     <para>
        ///          Value for  the icd of an Encyclopedia.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ICD
        {
            get { return this._icd; }
            set { this._icd = value; }

        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaType.
        ///     </para>
        ///     <para>
        ///          Value for  the type of an Encyclopedia.
        ///     </para>
        /// </summary>
        [DataMember]
        public string EncyclopediaType
        {
            get { return this._encyclopediaType; }
            set { this._encyclopediaType = value; }

        }


        #endregion



        private int _encyclopediaId;
        private string _encyclopediaName;
        private bool _active;
        private int _encyclopediaTypeId;
        private int _encyclopediaImageId;
        private string _imagen;
        private string _encyclopediaImageSizes;
        private string _sizes;
        private string _icd;
        private string _encyclopediaType;


    }
}
