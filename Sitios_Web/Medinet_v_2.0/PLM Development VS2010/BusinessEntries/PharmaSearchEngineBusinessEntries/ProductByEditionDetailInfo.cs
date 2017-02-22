using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Active Substances, Indications,Therapeutics and ICDs of a Product, that inherits from the class ProductByEditionInfo.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.IndicationInfo"/>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ICDInfo"/>
    [DataContract]
    public class ProductByEditionDetailInfo : ProductByEditionInfo
    {

        #region Constructors

        /// <summary>
        ///     Constructor that initializes the properties. Not receive parameters.
        /// </summary>
        public ProductByEditionDetailInfo()
        {
            this._indications = new List<MedinetBusinessEntries.IndicationInfo>();
            this._substances = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
            this._therapeutics = new List<MedinetBusinessEntries.TherapeuticInfo>();
            this._icds = new List<MedinetBusinessEntries.ICDInfo>();
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
        ///         Property which gets or sets a value for Indications.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.IndicationInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.IndicationInfo> Indications
        {
            get { return this._indications; }
            set
            {
                foreach (MedinetBusinessEntries.IndicationInfo indication in value)
                    this._indications.Add(indication);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Therapeutics.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.TherapeuticInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.TherapeuticInfo> Therapeutics
        {
            get { return this._therapeutics; }
            set
            {
                foreach (MedinetBusinessEntries.TherapeuticInfo therapeutic in value)
                    this._therapeutics.Add(therapeutic);
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
        ///         Property which gets or sets a value for ICDs.
        ///     </para>
        ///     <para>
        ///         It is an list of objects of type <see cref="MedinetBusinessEntries.ICDInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ICDInfo> ICDs
        {
            get { return this._icds; }
            set
            {
                foreach (MedinetBusinessEntries.ICDInfo icd in value)
                    this._icds.Add(icd);
            }
        }


        #endregion

        protected List<MedinetBusinessEntries.ActiveSubstanceInfo> _substances;
        protected List<MedinetBusinessEntries.IndicationInfo> _indications;
        protected List<MedinetBusinessEntries.TherapeuticInfo> _therapeutics;
        protected List<MedinetBusinessEntries.ICDInfo> _icds;

        protected List<PresentationByProductInfo> _presentations = new List<PresentationByProductInfo>();

    }
}
