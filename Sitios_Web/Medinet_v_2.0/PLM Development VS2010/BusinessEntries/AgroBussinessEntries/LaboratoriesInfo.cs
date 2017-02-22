using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents the Laboratory Information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Laboratory which publishes the products of a Edition.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class LaboratoriesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the LaboratoriesInfo class. Not receive parameters.
        /// </summary>
        public LaboratoriesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LaboratoryId.
        ///     </para>
        ///     <para>
        ///         Laboratory Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LaboratoryName.
        ///     </para>
        ///     <para>
        ///         Name or description of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string LaboratoryName
        {
            get { return this._laboratoryName; }
            set { this._laboratoryName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ImageName.
        ///     </para>
        ///     <para>
        ///         Name of the image associated with the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ImageName
        {
            get { return this._imageName; }
            set { this._imageName = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Laboratory is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _laboratoryId;
        private string _laboratoryName;
        private string _imageName;
        private bool _active;
       

    }
}
