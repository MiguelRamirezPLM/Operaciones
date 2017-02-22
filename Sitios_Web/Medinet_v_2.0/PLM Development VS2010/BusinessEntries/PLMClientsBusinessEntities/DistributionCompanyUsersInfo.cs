using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the association between application, code and user.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class DistributionCompanyUsersInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DistributionCompanyUsersInfo class. Not receive parameters.
        /// </summary>
        public DistributionCompanyUsersInfo() { }

        #endregion

        #region Properties

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
        ///         Property which gets or sets a value for CompanyUserId.
        ///     </para>
        ///     <para>
        ///         User identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CompanyUserId
        {
            get
            {
                return this._companyUserId;
            }
            set
            {
                this._companyUserId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CodeId.
        ///     </para>
        ///     <para>
        ///         Code identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CodeId
        {
            get
            {
                return this._codeId;
            }
            set
            {
                this._codeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InicialDate.
        ///     </para>
        ///     <para>
        ///         Distribution's Start date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime InitialDate
        {
            get
            {
                return this._initialDate;
            }
            set
            {
                this._initialDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalDate.
        ///     </para>
        ///     <para>
        ///         Distribution's Ending date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime FinalDate
        {
            get
            {
                return this._finalDate;
            }
            set
            {
                this._finalDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the association is enabled or disabled.
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

        private int _distributionId;
        private int _companyUserId;
        private int _codeId;
        private DateTime _initialDate;
        private DateTime _finalDate;
        private bool _active;

    }
}
