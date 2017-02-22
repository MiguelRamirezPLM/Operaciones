using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{

    /// <summary>
    ///     Class which represents the prefix information which is relationated with families.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.FamilyInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class FamilyPrefixInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the FamilyPrefixInfo class. Not receive parameters.
        /// </summary>
        public FamilyPrefixInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get { return this._prefixId; }
            set { this._prefixId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EditionId.
        ///     </para>
        ///     <para>
        ///         Edition identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixTypeId.
        ///     </para>
        ///     <para>
        ///         Prefix type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? PrefixTypeId
        {
            get { return this._prefixTypeId; }
            set { this._prefixTypeId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixName.
        ///     </para>
        ///     <para>
        ///         Prefix name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixName
        {
            get { return this._prefixName; }
            set { this._prefixName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CurrentValue.
        ///     </para>
        ///     <para>
        ///         Families' number which are associated with the prefix.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CurrentValue
        {
            get { return this._currentValue; }
            set { this._currentValue = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixDescription.
        ///     </para>
        ///     <para>
        ///         Prefix description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PrefixDescription
        {
            get { return this._prefixDescription; }
            set { this._prefixDescription = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the prefix is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _prefixId;
        private int _editionId;
        private int? _prefixTypeId;
        private string _prefixName;
        private int _currentValue;
        private string _prefixDescription;
        private bool _active;

    }
}
