using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Active Substances and Presentations of a Product, that inherits from the class ProductByEditionInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="PharmaSearchEngineBusinessEntries.PresentationByProductInfo"/>
    [DataContract]
    public class ProductByEditionShortDetailInfo : ProductByEditionInfo
    {
        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public ProductByEditionShortDetailInfo()
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Presentations.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="PharmaSearchEngineBusinessEntries.PresentationsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PharmaSearchEngineBusinessEntries.PresentationByProductInfo> Presentations
        {
            get { return this._presentations; }
            set
            {
                foreach (PharmaSearchEngineBusinessEntries.PresentationByProductInfo presentation in value)
                    this._presentations.Add(presentation);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets the total of rows in the query.
        ///     </para>
        ///     <para>
        ///         Total rows 
        ///     </para>
        /// </summary>
        [DataMember]
        public int? TotalRows
        {
            get { return this._totalRows; }
            set { this._totalRows = value; }
        }


        #endregion
        
        private List<MedinetBusinessEntries.ActiveSubstanceInfo> _substances;
        private List<PresentationByProductInfo> _presentations = new List<PresentationByProductInfo>();
        private int? _totalRows;
    }
}
