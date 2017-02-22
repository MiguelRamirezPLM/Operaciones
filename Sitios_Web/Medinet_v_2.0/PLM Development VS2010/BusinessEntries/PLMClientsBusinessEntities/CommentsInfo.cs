using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Comment information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class CommentsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CommentsInfo class. Not receive parameters.
        /// </summary>
        public CommentsInfo() 
        {
            this._commentDate = null;
            this._sentDate = null;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CommentId.
        ///     </para>
        ///     <para>
        ///         Comment identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CommentId
        {
            get
            {
                return this._commentId;
            }
            set
            {
                this._commentId = value;
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
        ///         Property which gets or sets a value for BranchId.
        ///     </para>
        ///     <para>
        ///         PLM Branch identifier.
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
        ///         Property which gets or sets a value for Content.
        ///     </para>
        ///     <para>
        ///         Comment content.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CommentDate.
        ///     </para>
        ///     <para>
        ///         Date on which the comment was made.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? CommentDate
        {
            get 
            {
                return this._commentDate;
            }
            set
            {
                this._commentDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SentDate.
        ///     </para>
        ///     <para>
        ///         Date on which the comment is sent via email.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? SentDate
        {
            get
            {
                return this._sentDate;
            }
            set
            {
                this._sentDate = value;
            }
        }

        #endregion

        private int _commentId;
        private byte _commentTypeId;
        private byte _branchId;
        private byte _businessUnitId;
        private int _distributionId;
        private int _prefixId;
        private byte _targetId;
        private string _content;
        private DateTime? _commentDate;
        private DateTime? _sentDate;
    }
}
