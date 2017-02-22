using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Option Information which is relationated with a Questionnaire.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="PLMClientsBusinessEntities.QuestionnairesInfo"/>
    /// <seealso cref="PLMClientsBusinessEntities.SummariesInfo"/>
    [DataContract]
    public class OptionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the OptionsInfo class. Not receive parameters.
        /// </summary>
        public OptionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OptionId.
        ///     </para>
        ///     <para>
        ///         Option identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int OptionId
        {
            get
            {
                return this._optionId;
            }
            set
            {
                this._optionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for OptionDescription.
        ///     </para>
        ///     <para>
        ///         Option content.
        ///     </para>
        /// </summary>
        [DataMember]
        public string OptionDescription
        {
            get
            {
                return this._optionDescription;
            }
            set
            {
                this._optionDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the Option is enabled or disabled.
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

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Order.
        ///     </para>
        ///     <para>
        ///         It indicates the option order.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte Order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        #endregion

        private int _optionId;
        private string _optionDescription;
        private bool _active;
        private byte _order;
    }
}
