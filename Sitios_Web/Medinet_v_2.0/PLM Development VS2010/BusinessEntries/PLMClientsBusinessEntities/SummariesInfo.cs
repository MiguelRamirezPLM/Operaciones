using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Summary Information which is relationated with a Option.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.OptionsInfo"/>
    [DataContract]
    public class SummariesInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SummariesInfo class. Not receive parameters.
        /// </summary>
        public SummariesInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SummaryId.
        ///     </para>
        ///     <para>
        ///         Summary identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SummaryId
        {
            get
            {
                return this._summaryId;
            }
            set
            {
                this._summaryId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SummaryDescription.
        ///     </para>
        ///     <para>
        ///         Summary content.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SummaryDescription
        {
            get
            {
                return this._summaryDescription;
            }
            set 
            {
                this._summaryDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Summary is enabled or disabled.
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

        private int _summaryId;
        private string _summaryDescription;
        private bool _active;
    }
}
