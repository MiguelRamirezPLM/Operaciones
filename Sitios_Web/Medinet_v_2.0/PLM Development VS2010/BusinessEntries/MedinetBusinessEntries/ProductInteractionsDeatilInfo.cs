using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about Product interactions.
    /// </summary>
    /// <seealso cref="MedinetBusinessEntries.IppaProductInteractionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.InteractionSubstanceProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.InteractionGroupProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductOtherInteractionsInfo"/>
    [Serializable]
    [DataContract]
    public class ProductInteractionsDeatilInfo : IppaProductInteractionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductInteractionsDeatilInfo class. Not receive parameters.
        /// </summary>
        public ProductInteractionsDeatilInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for PlainContent.
        ///     </para>
        ///     <para>
        ///         The content of the attribute, as plain text.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PlainContent
        {
            get { return this._plainContent; }
            set { this._plainContent = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SubstanceInteractions.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.InteractionSubstanceProductsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.InteractionSubstanceProductsInfo> InteractionSubstances
        {
            get
            {
                return this._substanceInteractions;
            }
            set
            {
                this._substanceInteractions = new List<MedinetBusinessEntries.InteractionSubstanceProductsInfo>();
                foreach (MedinetBusinessEntries.InteractionSubstanceProductsInfo substance in value)
                    this._substanceInteractions.Add(substance);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaGroupsInteractions.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.InteractionGroupProductsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.InteractionGroupProductsInfo> PharmacologicalGroups
        {
            get
            {
                return this._pharmacologicalGroupInteractions;
            }
            set
            {
                this._pharmacologicalGroupInteractions = new List<MedinetBusinessEntries.InteractionGroupProductsInfo>();
                foreach (MedinetBusinessEntries.InteractionGroupProductsInfo pharmacologicalGroup in value)
                    this._pharmacologicalGroupInteractions.Add(pharmacologicalGroup);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherInteractions.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.ProductOtherInteractionsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ProductOtherInteractionsInfo> OtherInteractions
        {
            get
            {
                return this._otherInteractions;
            }
            set
            {
                this._otherInteractions = new List<MedinetBusinessEntries.ProductOtherInteractionsInfo>();
                foreach (MedinetBusinessEntries.ProductOtherInteractionsInfo otherInteraction in value)
                    this._otherInteractions.Add(otherInteraction);
            }
        }

        #endregion

        private string _brand;
        private string _plainContent;
        private List<MedinetBusinessEntries.InteractionSubstanceProductsInfo> _substanceInteractions = new List<MedinetBusinessEntries.InteractionSubstanceProductsInfo>();
        private List<MedinetBusinessEntries.InteractionGroupProductsInfo> _pharmacologicalGroupInteractions = new List<MedinetBusinessEntries.InteractionGroupProductsInfo>();
        private List<MedinetBusinessEntries.ProductOtherInteractionsInfo> _otherInteractions = new List<MedinetBusinessEntries.ProductOtherInteractionsInfo>();
    }
}