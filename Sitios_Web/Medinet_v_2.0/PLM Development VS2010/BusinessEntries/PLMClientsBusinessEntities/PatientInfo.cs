using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{

    /// <summary>
    ///     Class which represents the Patient information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class PatientInfo
    {
        #region Constructor
        /// <summary>
        ///     Initializes a new instance of the PatientInfo class. Not receive parameters.
        /// </summary>
        
        public PatientInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PatientId.
        ///     </para>
        ///     <para>
        ///         Patient identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int PatientId
        {
            get { return this._patientId; }
            set { this._patientId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FirstName.
        ///     </para>
        ///     <para>
        ///         Patient First Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LastName.
        ///     </para>
        ///     <para>
        ///         Patient Last Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SecondLastName.
        ///     </para>
        ///     <para>
        ///        Patient Second Last Name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SecondLastName
        {
            get { return this._secondLastName; }
            set { this._secondLastName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Birthday.
        ///     </para>
        ///     <para>
        ///         Date of birth of Patient.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime? Birthday
        {
            get { return this._birthday; }
            set { this._birthday = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Gender.
        ///     </para>
        ///     <para>
        ///         Patient gender.
        ///     </para>
        /// </summary>
        [DataMember]
        public char Gender
        {
            get { return this._gender; }
            set { this._gender = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Height.
        ///     </para>
        ///     <para>
        ///         Patient Height.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? Height
        {
            get { return this._height; }
            set { this._height = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Weight.
        ///     </para>
        ///     <para>
        ///         Patient weight.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? Weight
        {
            get { return this._weight; }
            set { this._weight = value; }
        }

    

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Medical Diagnostic .
        ///     </para>
        ///     <para>
        ///         Indicates the medical diagnosis of a patient.
        ///     </para>
        /// </summary>
        [DataMember]

        public string MedicalDiagnostic
        {
            get { return this._medicalDiagnostic; }
            set { this._medicalDiagnostic = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Email .
        ///     </para>
        ///     <para>
        ///         Indicates the email address of a patient .
        ///     </para>
        /// </summary>
        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }


        // <summary>
        ///     <para>
        ///         Property which gets or sets a value for Initials .
        ///     </para>
        ///     <para>
        ///        Specify initials of a patient .
        ///     </para>
        /// </summary>
        [DataMember]
        public string Initials
        {
            get { return this._initials; }
            set { this._initials = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Street .
        ///     </para>
        ///     <para>
        ///         Indicates if the Street of a Patient.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Street
        {
            get { return this._street; }
            set { this._street = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Suburb .
        ///     </para>
        ///     <para>
        ///         Indicates if the Suburb of a Patient.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Suburb
        {
            get { return this._suburb; }
            set { this._suburb = value; }
        }




        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for State Name .
        ///     </para>
        ///     <para>
        ///         State identifier of a patient.
        ///     </para>
        /// </summary>
        [DataMember]
        public int StateId
        {
            get { return this._state;}
            set { this._state=value;}
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PhoneOne.
        ///     </para>
        ///     <para>
        ///         Telephone number of a patient.
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
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Patient is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }




        #endregion

        private int _patientId;
        private string _firstName;
        private string _lastName;
        private string _secondLastName;
        private DateTime? _birthday;
        private char  _gender;
        private decimal? _height;
        private decimal? _weight;
        private string _medicalDiagnostic;
        private string _email;
        private string _initials;
        private bool _active;


        private string _street;
        private string _suburb;
        private int  _state;
        private string _phoneOne;

      
        


    }
}
