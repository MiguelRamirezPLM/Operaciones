using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Speciality information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class SpecialityGuidesInfo : SpecialityInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SpecialityGuidesInfo class. Not receive parameters.
        /// </summary>
        
        public SpecialityGuidesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for electronic Information.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.ElectronicInformationByTargetInfo"/>..
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> MedicalGuides
        {
            get
            {
                return this._medicalGuides;
            }
            set
            {
                this._medicalGuides = new List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo>();

                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo eventItem in value)
                    this._medicalGuides.Add(eventItem);
            }
        }

#endregion
        private List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> _medicalGuides = new List<ElectronicInformationByTargetInfo>();
        
    }
}
