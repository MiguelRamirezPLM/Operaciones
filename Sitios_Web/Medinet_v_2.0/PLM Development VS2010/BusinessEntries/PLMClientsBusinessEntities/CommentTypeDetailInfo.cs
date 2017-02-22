using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Company branches and CommentTypes
    /// </summary>
    [DataContract]
    public class CommentTypeDetailInfo
    {
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CommentTypeDetailInfo class. Not receive parameters.
        /// </summary>
        public CommentTypeDetailInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchId.
        ///     </para>
        ///     <para>
        ///         Branch identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte BranchId
        {
            get
            {
                return this._branchId;
            }
            set
            {
                this._branchId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BranchName.
        ///     </para>
        ///     <para>
        ///         Branch name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BranchName
        {
            get
            {
                return this._branchName;
            }
            set
            {
                this._branchName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BusinessUnitId.
        ///     </para>
        ///     <para>
        ///         Business unit identifier associated with the Company Branch.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte BusinessUnitId
        {
            get
            {
                return this._businessUnitId;
            }
            set
            {
                this._businessUnitId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BUnitName.
        ///     </para>
        ///     <para>
        ///         Business Unit name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BUnitName
        {
            get
            {
                return this._bUnitName;
            }
            set
            {
                this._bUnitName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CommentTypeId.
        ///     </para>
        ///     <para>
        ///         Comment type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte CommentTypeId
        {
            get
            {
                return this._commentTypeId;
            }
            set
            {
                this._commentTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeName.
        ///     </para>
        ///     <para>
        ///         Comment type name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeDescription.
        ///     </para>
        ///     <para>
        ///         Comment type description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TypeDescription
        {
            get
            {
                return this._typeDescription;
            }
            set
            {
                this._typeDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionId.
        ///     </para>
        ///     <para>
        ///         Distribution identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DistributionId
        {
            get
            {
                return this._distributionId;
            }
            set
            {
                this._distributionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DistributionName.
        ///     </para>
        ///     <para>
        ///         Distribution name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DistributionName
        {
            get
            {
                return this._distributionName;
            }
            set
            {
                this._distributionName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrefixId.
        ///     </para>
        ///     <para>
        ///         Code Prefix identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PrefixId
        {
            get
            {
                return this._prefixId;
            }
            set
            {
                this._prefixId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Output medium identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TargetId
        {
            get
            {
                return this._targetId;
            }
            set
            {
                this._targetId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetName.
        ///     </para>
        ///     <para>
        ///         Output medium name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TargetName
        {
            get
            {
                return this._targetName;
            }
            set
            {
                this._targetName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         Email which is sent Commentary .
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        #endregion

        private byte _branchId;
        private string _branchName;
        private byte _businessUnitId;
        private string _bUnitName;
        private byte _commentTypeId;
        private string _typeName;
        private string _typeDescription;
        private int _distributionId;
        private string _distributionName;
        private int _prefixId;
        private byte _targetId;
        private string _targetName;
        private string _email;

    }
}
