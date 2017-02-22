using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the relationship between Editions and Presentations.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PresentationsInfo"/>
    [DataContract]
    public class EditionPresentationsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the EditionPresentationsInfo class. Not receive parameters.
        /// </summary>
        public EditionPresentationsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get
            {
                return this._editionId;
            }
            set
            {
                this._editionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ProductId.
        ///     </para>
        ///     <para>
        ///         Product Identifier.
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

        #endregion

        private int _editionId;
        private int _presentationId;

    }
}
