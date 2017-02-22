using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents information about Product interactions.
    /// </summary>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.OtherElementsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmacologicalGroupsInfo"/>
    [DataContract]
    public class ProductInteractionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ProductInteractionsInfo class. Not receive parameters.
        /// </summary>
        public ProductInteractionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InteractionSubstances.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.ActiveSubstanceInfo> InteractionSubstances
        {
            get
            {
                return this._interactionSubstances;
            }
            set
            {
                this._interactionSubstances = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
                foreach (MedinetBusinessEntries.ActiveSubstanceInfo substance in value)
                    this._interactionSubstances.Add(substance);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacologicalGroups.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.PharmacologicalGroupsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.PharmacologicalGroupsInfo> PharmacologicalGroups
        {
            get
            {
                return this._pharmacologicalGroups;
            }
            set
            {
                this._pharmacologicalGroups = new List<MedinetBusinessEntries.PharmacologicalGroupsInfo>();
                foreach (MedinetBusinessEntries.PharmacologicalGroupsInfo pharmacologicalGroup in value)
                    this._pharmacologicalGroups.Add(pharmacologicalGroup);
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OtherInteractions.
        ///     </para>
        ///     <para>
        ///         It's a list of objects of type <see cref="MedinetBusinessEntries.OtherElementsInfo"/>
        ///     </para>
        /// </summary>
        [DataMember]
        public List<MedinetBusinessEntries.OtherElementsInfo> OtherInteractions
        {
            get
            {
                return this._otherInteractions;
            }
            set
            {
                this._otherInteractions = new List<MedinetBusinessEntries.OtherElementsInfo>();
                foreach (MedinetBusinessEntries.OtherElementsInfo otherInteraction in value)
                    this._otherInteractions.Add(otherInteraction);
            }
        }

        #endregion

        private List<MedinetBusinessEntries.ActiveSubstanceInfo> _interactionSubstances = new List<MedinetBusinessEntries.ActiveSubstanceInfo>();
        private List<MedinetBusinessEntries.PharmacologicalGroupsInfo> _pharmacologicalGroups = new List<MedinetBusinessEntries.PharmacologicalGroupsInfo>();
        private List<MedinetBusinessEntries.OtherElementsInfo> _otherInteractions = new List<MedinetBusinessEntries.OtherElementsInfo>();

    }
}
