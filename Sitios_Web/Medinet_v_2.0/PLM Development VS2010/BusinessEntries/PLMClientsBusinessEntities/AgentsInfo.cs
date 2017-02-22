using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Agent information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AgentsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AgentsInfo class. Not receive parameters.
        /// </summary>
        public AgentsInfo(){}

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AgentId.
        ///     </para>
        ///     <para>
        ///         Agent identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AgentId
        {
            get
            {
                return this._agentId;
            }
            set
            {
                this._agentId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TypeId.
        ///     </para>
        ///     <para>
        ///         Agent type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FirstName.
        ///     </para>
        ///     <para>
        ///         Agent name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastName.
        ///     </para>
        ///     <para>
        ///         Surname.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SecondLastName.
        ///     </para>
        ///     <para>
        ///         Second surname.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SecondLastName
        {
            get
            {
                return this._secondLastName;
            }
            set
            {
                this._secondLastName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Email.
        ///     </para>
        ///     <para>
        ///         Agent email.
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneOne.
        ///     </para>
        ///     <para>
        ///         Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneOne
        {
            get
            {
                return this._phoneOne;
            }
            set
            {
                this._phoneOne = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneTwo.
        ///     </para>
        ///     <para>
        ///         Second Telephone number.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PhoneTwo
        {
            get
            {
                return this._phoneTwo;
            }
            set
            {
                this._phoneTwo = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Agent is enabled or disabled.
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

        private int _agentId;
        private byte _typeId;
        private string _firstName;
        private string _lastName;
        private string _secondLastName;
        private string _email;
        private string _phoneOne;
        private string _phoneTwo;
        private bool _active;

    }
}
