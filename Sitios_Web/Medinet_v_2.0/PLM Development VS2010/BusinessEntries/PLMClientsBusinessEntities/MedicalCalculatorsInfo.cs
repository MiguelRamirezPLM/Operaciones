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
    public class MedicalCalculatorsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MedicalCalculatorsInfo class. Not receive parameters.
        /// </summary>
        public MedicalCalculatorsInfo() { }

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
        ///         Property which gets or sets a value for WebPage.
        ///     </para>
        ///     <para>
        ///         Web Page where is the Medical Calculator.
        ///     </para>
        /// </summary>
        [DataMember]
        public string WebPage
        {
            get
            {
                return this._webPage;
            }
            set
            {
                this._webPage = value;
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

        private int _calculatorId;
        private string _calculatorName;
        private string _calculatorDescription;
        private string _calculatorFormula;
        private string _sqlSyntax;
        private string _calculatorReferences;
        private string _webPage;
        private bool _active;

    }
}
