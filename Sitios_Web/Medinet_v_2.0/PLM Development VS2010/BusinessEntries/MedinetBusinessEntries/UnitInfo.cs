using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the units of measure.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    ///     <para>
    ///         Pattern which measures the amount of active substance in a drug.
    ///     </para>
    /// </remarks>
    [DataContract]
    public class UnitInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the UnitInfo class. Not receive parameters.
        /// </summary>
        public UnitInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UnitId.
        ///     </para>
        ///     <para>
        ///         Identifier of the measure unit.
        ///     </para>
        /// </summary>
        [DataMember]
        public int UnitId
        {
            get { return this._unitId; }
            set { this._unitId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Name.
        ///     </para>
        ///     <para>
        ///         Name or description of the measure unit.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the measure unit is active.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _unitId;
        private string _name;
        private bool _active;
    }
}
