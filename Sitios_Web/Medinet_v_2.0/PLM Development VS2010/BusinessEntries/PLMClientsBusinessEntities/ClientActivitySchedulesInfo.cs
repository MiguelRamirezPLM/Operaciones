using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the schedule information in which the client do an activity.
    /// </summary>
    [DataContract]
    public class ClientActivitySchedulesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientActivitySchedulesInfo class. Not receive parameters.
        /// </summary>
        public ClientActivitySchedulesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ClientId.
        ///     </para>
        ///     <para>
        ///         Client identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
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
        ///         Property which gets or sets a value for DeviceId.
        ///     </para>
        ///     <para>
        ///         Device identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte DeviceId
        {
            get
            {
                return this._deviceId;
            }
            set
            {
                this._deviceId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TargetId.
        ///     </para>
        ///     <para>
        ///         Target identifier.
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
        ///         Property which gets or sets a value for ActivityTypeId.
        ///     </para>
        ///     <para>
        ///         Activity type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ActivityTypeId
        {
            get
            {
                return this._activityTypeId;
            }
            set
            {
                this._activityTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ActivitySchedule.
        ///     </para>
        ///     <para>
        ///         Activity schedule.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ActivitySchedule
        {
            get
            {
                return this._activitySchedule;
            }
            set
            {
                this._activitySchedule = value;
            }
        }

        #endregion

        private int _clientId;
        private int _codeId;
        private byte _deviceId;
        private byte _targetId;
        private byte _activityTypeId;
        private string _activitySchedule;

    }
}
