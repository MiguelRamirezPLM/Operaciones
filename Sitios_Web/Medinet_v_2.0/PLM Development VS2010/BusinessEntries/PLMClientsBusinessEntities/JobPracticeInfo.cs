using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the relationship between customer and workplace.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.ClientInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.JobPlaceInfo"/>
    [Serializable]
    [DataContract]
    public class JobPracticeInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the JobPracticeInfo class. Not receive parameters.
        /// </summary>
        public JobPracticeInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Customer identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PracticeId.
        ///     </para>
        ///     <para>
        ///         Professional practice identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte PracticeId
        {
            get { return this._practiceId; }
            set { this._practiceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for JobPlaceId.
        ///     </para>
        ///     <para>
        ///         Workplace identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte JobPlaceId
        {
            get { return this._jobPlaceId; }
            set { this._jobPlaceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for InstitutionName.
        ///     </para>
        ///     <para>
        ///         Institution name where the customer works.
        ///     </para>
        /// </summary>
        [DataMember]
        public string InstitutionName
        {
            get { return this._institutionName; }
            set { this._institutionName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PositionName.
        ///     </para>
        ///     <para>
        ///         Position name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PositionName
        {
            get { return this._positionName; }
            set { this._positionName = value; }
        }

        #endregion

        private int _clientId;
        private byte _practiceId;
        private byte _jobPlaceId;
        private string _institutionName;
        private string _positionName;

    }
}
