using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{

    /// <summary>
    ///     Class which represents the family information relationated with products.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.FamilyPrefixInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    [DataContract]
    public class FamilyInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the FamilyInfo class. Not receive parameters.
        /// </summary>
        public FamilyInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FamilyId.
        ///     </para>
        ///     <para>
        ///         Family Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int FamilyId
        {
            get { return this._familyId; }
            set { this._familyId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Prefix Identifier.
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
        ///         Property which gets or sets a value for FamilyString.
        ///     </para>
        ///     <para>
        ///         Family name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FamilyString
        {
            get { return this._familyString; }
            set { this._familyString = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the family is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _familyId;
        private int _prefixId;
        private string _familyString;
        private bool _active;
    }
}
