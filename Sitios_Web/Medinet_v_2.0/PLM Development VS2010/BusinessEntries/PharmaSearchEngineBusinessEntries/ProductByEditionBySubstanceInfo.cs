using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a Product, that inherits from the class ProductByEditionInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.ProductByEditionInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    public class ProductByEditionBySubstanceInfo : ProductByEditionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductByEditionBySubstanceInfo class. Not receive parameters.
        /// </summary>
        public ProductByEditionBySubstanceInfo()
        {
            this._substances = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Substances.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> Substances
        {
            get { return this._substances; }
            set
            {
                foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in value)
                    this._substances.Add(substance);
            }
        }

        #endregion

        protected List<MedinetBusinessEntries.ActiveSubstanceInfo> _substances;

    }
}
