using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VetBusinessEntries
{

    /// <summary>
    ///     Class which represents the Laboratory,ActiveSubstances , Species and Therapeutics of a Product.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="VetBussinessEntries.ProductByEditionInfo"/>
    /// <seealso cref="VetBussinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="VetBussinessEntries.TherapeuticInfo"/>
    /// <seealso cref="VetBussinessEntries.DivionInfo"/>
    /// <seealso cref="VetBussinessEntries.SpecieInfo"/>


    [DataContract]
     public class SearchResultsInfo
     {
         #region Constructors
         /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public SearchResultsInfo() 
        {
            this._products = new List<ProductByEditionInfo>();
            this._substances = new List<ActiveSubstanceInfo>();
            this._therapeutics = new List<TherapeuticInfo>();
            this._labs = new List<DivisionInfo>();
            this._species = new List<SpecieInfo>();

        }

        #endregion


         #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Products.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="ProductByEditionInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ProductByEditionInfo> Products
        {
            get { return this._products; }
            set
            {
                foreach (ProductByEditionInfo product in value)
                    this._products.Add(product);
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for  Sustances.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="ActiveSubstancesInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<ActiveSubstanceInfo> Substances
        {
            get { return this._substances; }
            set
            {
                foreach (ActiveSubstanceInfo substance in value)
                    this._substances.Add(substance);
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Therapeutics.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="TherapeuticInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<TherapeuticInfo> Therapeutics
        {
            get { return this._therapeutics; }
            set
            {
                foreach (TherapeuticInfo therapeutic in value)
                    this._therapeutics.Add(therapeutic);
            }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Laboratoies.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="DivisionsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<DivisionInfo> Labs
        {
            get { return this._labs; }
            set
            {
                foreach (DivisionInfo lab in value)
                    this._labs.Add(lab);
            }
        }



        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Species.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="SpeciesInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<SpecieInfo> Species
        {
            get { return this._species; }
            set
            {
                foreach (SpecieInfo specie in value)
                    this._species.Add(specie);
            }
        }


         #endregion




        private List<ProductByEditionInfo> _products;
        private List<ActiveSubstanceInfo> _substances;
        private List<TherapeuticInfo> _therapeutics;
        private List<DivisionInfo> _labs;
        private List<SpecieInfo> _species;

     }
}
