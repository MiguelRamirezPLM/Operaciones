using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Variable's information which related with a Medical Calculator.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class VariablesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the VariablesInfo class. Not receive parameters.
        /// </summary>
        public VariablesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for VariableId.
        ///     </para>
        ///     <para>
        ///         Variable identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int VariableId
        {
            get
            {
                return this._variableId;
            }
            set
            {
                this._variableId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for VariableName.
        ///     </para>
        ///     <para>
        ///         Variable name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string VariableName
        {
            get
            {
                return this._variableName;
            }
            set
            {
                this._variableName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Nomenclature.
        ///     </para>
        ///     <para>
        ///         Variable representation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Nomenclature
        {
            get
            {
                return this._nomenclature;
            }
            set
            {
                this._nomenclature = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for VariableValue.
        ///     </para>
        ///     <para>
        ///         Value which can be assigned to the variable.
        ///     </para>
        /// </summary>
        [DataMember]
        public string VariableValue
        {
            get
            {
                return this._variableValue;
            }
            set
            {
                this._variableValue = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Calculator is enabled or disabled.
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

        private int _variableId;
        private string _variableName;
        private string _nomenclature;
        private string _variableValue;
        private bool _active;

    }
}
