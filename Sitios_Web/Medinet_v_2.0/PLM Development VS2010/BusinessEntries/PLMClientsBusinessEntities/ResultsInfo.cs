using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Result information associated with a Medical Calculator.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.MedicalCalculatorsInfo"/>
    [DataContract]
    public class ResultsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ResultsInfo class. Not receive parameters.
        /// </summary>
        public ResultsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResultId.
        ///     </para>
        ///     <para>
        ///         Result identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ResultId
        {
            get
            {
                return this._resultId;
            }
            set
            {
                this._resultId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResultDescription.
        ///     </para>
        ///     <para>
        ///         Result description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ResultDescription
        {
            get
            {
                return this._resultDescription;
            }
            set
            {
                this._resultDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for LowerRange.
        ///     </para>
        ///     <para>
        ///         Result's lower range.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? LowerRange
        {
            get
            {
                return this._lowerRange;
            }
            set
            {
                this._lowerRange = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for UpperRange.
        ///     </para>
        ///     <para>
        ///         Result's upper range.
        ///     </para>
        /// </summary>
        [DataMember]
        public decimal? UpperRange
        {
            get
            {
                return this._upperRange;
            }
            set
            {
                this._upperRange = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FinalResult.
        ///     </para>
        ///     <para>
        ///         Result which threw the Medical Calculator.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FinalResult
        {
            get
            {
                return this._finalResult;
            }
            set
            {
                this._finalResult = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Result is enabled or disabled.
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

        private int _resultId;
        private string _resultDescription;
        private decimal? _lowerRange;
        private decimal? _upperRange;
        private string _finalResult;
        private bool _active;

    }
}
