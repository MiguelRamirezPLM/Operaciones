using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about Product presentations.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductPresentationsInfo : ProductCategoriesInfo
    {
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductPresentationsInfo class. Not receive parameters.
        /// </summary>
        public ProductPresentationsInfo() { }

        #endregion
        
        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActiveSubstanceId.
        ///     </para>
        ///     <para>
        ///         ActiveSubastance Identifier.
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
        ///         Property which gets or sets a value for ActiveSubstance.
        ///     </para>
        ///     <para>
        ///         Active substance's Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActiveSubstance
        {
            get
            {
                return this._activeSubstance;
            }
            set
            {
                this._activeSubstance = value;
            }
        }
        

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Brand.
        ///     </para>
        ///     <para>
        ///         Product Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Brand
        {
            get { return this._brand; }
            set { this._brand = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PresentationId.
        ///     </para>
        ///     <para>
        ///         Presentation identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PresentationId
        {
            get
            {
                return this._presentationId;
            }
            set
            {
                this._presentationId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Presentation.
        ///     </para>
        ///     <para>
        ///         Presentation description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Presentation
        {
            get
            {
                return this._presentation;
            }
            set
            {
                this._presentation = value;
            }
        }

        #endregion

        private int _activeSubstanceId;
        private string _activeSubstance;
        private string _brand;
        private int _presentationId;
        private string _presentation;
    }
}
