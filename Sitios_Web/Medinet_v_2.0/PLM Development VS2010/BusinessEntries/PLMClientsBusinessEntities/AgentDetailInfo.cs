using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Agent information and the branches associated with him.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class AgentDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AgentDetailInfo class. Not receive parameters.
        /// </summary>
        public AgentDetailInfo() { }

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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PharmacyList.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.BranchDetailInfo"/>. Indicates if the Agent has got Pharmacies.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.BranchDetailInfo> PharmacyList
        {
            get
            {
                return this._pharmacyList;
            }
            set
            {
                foreach (PLMClientsBusinessEntities.BranchDetailInfo pharmacy in value)
                {
                    this._pharmacyList.Add(pharmacy);
                }
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PrivateHospitals.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.BranchDetailInfo"/>. Indicates if the Agent has got Private Hospitals.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.BranchDetailInfo> PrivateHospitals
        {
            get
            {
                return this._privateHospitals;
            }
            set
            {
                foreach (PLMClientsBusinessEntities.BranchDetailInfo hospital in value)
                {
                    this._privateHospitals.Add(hospital);
                }
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublicHospitals.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.BranchDetailInfo"/>. Indicates if the Agent has got Public Hospitals.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.BranchDetailInfo> PublicHospitals
        {
            get
            {
                return this._publicHospitals;
            }
            set
            {
                foreach (PLMClientsBusinessEntities.BranchDetailInfo hospital in value)
                {
                    this._publicHospitals.Add(hospital);
                }
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
        private List<PLMClientsBusinessEntities.BranchDetailInfo> _pharmacyList = new List<BranchDetailInfo>();
        private List<PLMClientsBusinessEntities.BranchDetailInfo> _privateHospitals = new List<BranchDetailInfo>();
        private List<PLMClientsBusinessEntities.BranchDetailInfo> _publicHospitals = new List<BranchDetailInfo>();

    }
}
