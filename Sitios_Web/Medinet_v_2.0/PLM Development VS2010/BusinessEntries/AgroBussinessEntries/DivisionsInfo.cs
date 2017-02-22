using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AgroBusinessEntries
{
    /// <summary>
    ///     Class which represents information about a Laboratory.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Division is a specialty line of a laboratory.
    ///     </para>
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.LaboratoriesInfo"/>
    /// <seealso cref="MedinetBusinessEntries.CountriesInfo"/>
    [DataContract]
    public class DivisionsInfo 
    {
 
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the DivisionsInfo class. Not receive parameters.
        /// </summary>
        public DivisionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for DivisionId.
        ///     </para>
        ///     <para>
        ///         Division Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int DivisionId
        {
            get { return this._divisionId; }
            set { this._divisionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LaboratoryId.
        ///     </para>
        ///     <para>
        ///         Laboratory Identifier.
        ///     </para>
        /// </summary>
        public int LaboratoryId
        {
            get { return this._laboratoryId; }
            set { this._laboratoryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CountryId.
        ///     </para>
        ///     <para>
        ///         Country identifier associated with the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or description of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Division is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ShortName.
        ///     </para>
        ///     <para>
        ///         Short Name of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ShortName
        {
            get{ return this._shortName; } 
            set{ this._shortName = value; }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Image of the Division
        ///     </para>
        ///     <para>
        ///         Logo of the laboratory.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Logo
        {
            get { return this._imagen; }
            set { this._imagen = value; }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for the sizes of the Logo
        ///     </para>
        ///     <para>
        ///         Sizes of the Logo
        ///     </para>
        /// </summary>
        [DataMember]
        public string LogoSizes
        {
            get { return this._logoSizes; }
            set { this._logoSizes = value; }
        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for the sizes of the Logo
        ///     </para>
        ///     <para>
        ///         Sizes of the Logo
        ///     </para>
        /// </summary>
        [DataMember]
        public int LogoId
        {
            get { return this._logoId; }
            set { this._logoId = value; }
        }

        /// <summary>
        /// <para>
        /// Property which gets or sets the sizes 
        /// </para>
        /// <para>
        /// It indicates in what sizes are in DB
        /// </para>
        /// </summary>
        [DataMember]
        public string Sizes
        {
            get
            {
                return this._sizes;
            }
            set
            {
                this._sizes = value;
            }
        }


        #endregion

        private int _divisionId;
        private int _laboratoryId;
        private int _countryId;
        private string _description;
        private bool _active;
        private string _shortName;
        private string _imagen;
        private string _logoSizes;
        private int _logoId;
        private string _sizes;
    }
}
