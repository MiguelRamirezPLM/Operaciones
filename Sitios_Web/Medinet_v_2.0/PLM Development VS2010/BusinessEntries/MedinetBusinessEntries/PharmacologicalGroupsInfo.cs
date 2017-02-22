using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about Pharmacological Groups
    /// </summary>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.OtherElementsInfo"/>
    [DataContract]
    public class PharmacologicalGroupsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacologicalGroupsInfo class. Not receive parameters.
        /// </summary>
        public PharmacologicalGroupsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmaGroupId.
        ///     </para>
        ///     <para>
        ///         Pharmacological Group Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PharmaGroupId
        {
            get
            {
                return this._pharmaGroupId;
            }
            set
            {
                this._pharmaGroupId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for GroupName.
        ///     </para>
        ///     <para>
        ///         Pharmacological Group name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string GroupName
        {
            get
            {
                return this._groupName;
            }
            set
            {
                this._groupName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Pharmacological Group is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get
            {
                return this._active;
            }
            set
            {
                this._active = value;
            }
        }

        #endregion

        private int _pharmaGroupId;
        private string _groupName;
        private bool _active;

    }
}
