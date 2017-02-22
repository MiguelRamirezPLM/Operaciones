using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Medical Calculator information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class CalculatorsDetailInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the CalculatorsDetailInfo class. Not receive parameters.
        /// </summary>
        public CalculatorsDetailInfo()
        {
            this._variablesList = new List<PLMClientsBusinessEntities.VariablesInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CalculatorId.
        ///     </para>
        ///     <para>
        ///         Calculator identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int CalculatorId
        {
            get
            {
                return this._calculatorId;
            }
            set
            {
                this._calculatorId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CalculatorName.
        ///     </para>
        ///     <para>
        ///         Calculator name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CalculatorName
        {
            get
            {
                return this._calculatorName;
            }
            set
            {
                this._calculatorName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CalculatorDescription.
        ///     </para>
        ///     <para>
        ///         Calculator description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CalculatorDescription
        {
            get
            {
                return this._calculatorDescription;
            }
            set
            {
                this._calculatorDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CalculatorFormula.
        ///     </para>
        ///     <para>
        ///         Calculator formula.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CalculatorFormula
        {
            get
            {
                return this._calculatorFormula;
            }
            set
            {
                this._calculatorFormula = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SQLSyntax.
        ///     </para>
        ///     <para>
        ///         Syntax which is obtained the result of an operation.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SQLSyntax
        {
            get
            {
                return this._sqlSyntax;
            }
            set
            {
                this._sqlSyntax = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for CalculatorReferences.
        ///     </para>
        ///     <para>
        ///         Information sources.
        ///     </para>
        /// </summary>
        [DataMember]
        public string CalculatorReferences
        {
            get
            {
                return this._calculatorReferences;
            }
            set
            {
                this._calculatorReferences = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for VariablesList.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.VariablesInfo"/>. Indicates if the Calculator has got variables.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.VariablesInfo> VariablesList
        {
            get
            {
                return this._variablesList;
            }
            set
            {
                this._variablesList = new List<PLMClientsBusinessEntities.VariablesInfo>();

                foreach (PLMClientsBusinessEntities.VariablesInfo variable in value)
                    this._variablesList.Add(variable);
            }
        }

        #endregion

        private int _calculatorId;
        private string _calculatorName;
        private string _calculatorDescription;
        private string _calculatorFormula;
        private string _sqlSyntax;
        private string _calculatorReferences;
        private List<PLMClientsBusinessEntities.VariablesInfo> _variablesList;

    }
}
