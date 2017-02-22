using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{

    /// <summary>
    ///     Class which represents the relationship between the product shot and it family.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.EditionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.DivisionsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmaceuticalFormInfo"/>
    /// <seealso cref="MedinetBusinessEntries.FamilyInfo"/>
    /// <seealso cref="MedinetBusinessEntries.EditionProductShotsInfo"/>
    [DataContract]
    public class FamilyProductShotInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the FamilyProductShotInfo class. Not receive parameters.
        /// </summary>
        public FamilyProductShotInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FamilyId.
        ///     </para>
        ///     <para>
        ///         Family identifier.
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
        ///         Property which gets or sets a value for EditionProductShotId.
        ///     </para>
        ///     <para>
        ///         Product shot Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionProductShotId
        {
            get { return this._editionProductShotId; }
            set { this._editionProductShotId = value; }
        }

        #endregion

        private int _familyId;
        private int _editionProductShotId;
    }
}
