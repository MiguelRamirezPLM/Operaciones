using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the category to which a product belongs.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.PublicityProductInfo"/>
    [DataContract]
    public class PublicityCategoryInfo
    {

        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public PublicityCategoryInfo() 
        {
            this._publicityProducts = new List<PublicityProductInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublicityCategoryIndex.
        ///     </para>
        ///     <para>
        ///         Index Category.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PublicityCategoryIndex
        {
            get
            {
                return this._publicityCategoryIndex;
            }
            set
            {
                this._publicityCategoryIndex = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HeaderImage.
        ///     </para>
        ///     <para>
        ///         Header image associated with the category.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HeaderImage
        {
            get 
            {
                return this._headerImage;
            }
            set
            {
                this._headerImage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ButtonImage.
        ///     </para>
        ///     <para>
        ///         Button image associated with the category.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ButtonImage
        {
            get
            {
                return this._buttonImage;
            }
            set
            {
                this._buttonImage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublicityProducts.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="PharmaSearchEngineBusinessEntries.PublicityProductInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PharmaSearchEngineBusinessEntries.PublicityProductInfo> PublicityProducts
        {
            get
            {
                return this._publicityProducts;
            }
            set
            {
                foreach (PublicityProductInfo product in value)
                    this._publicityProducts.Add(product);
            }
        }

        #endregion

        private int _publicityCategoryIndex;
        private string _headerImage;
        private string _buttonImage;
        private List<PharmaSearchEngineBusinessEntries.PublicityProductInfo> _publicityProducts;

    }
}
