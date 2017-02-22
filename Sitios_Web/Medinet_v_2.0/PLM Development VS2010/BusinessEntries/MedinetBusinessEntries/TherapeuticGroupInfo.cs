using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the Theraputic Groups.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.TherapeuticInfo"/>
    [DataContract]
    public class TherapeuticGroupInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticGroupInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticGroupInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TheraGroupId.
        ///     </para>
        ///     <para>
        ///         Identifier of the Therapeutic Group.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TheraGroupId
        {
            get { return this._theraGroupId; }
            set { this._theraGroupId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic Group have an parent Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for GroupName.
        ///     </para>
        ///     <para>
        ///         name of the Therapeutic Group.
        ///     </para>
        /// </summary>
        [DataMember]
        public string GroupName
        {
            get { return this._groupName; }
            set { this._groupName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic Group is active.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _theraGroupId;
        private int? _parentId;
        private string _groupName;
        private bool _active;

    }
}
