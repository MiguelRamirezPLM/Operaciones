using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the AdverseReaction information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]

    public class AdverseReactionInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the AdverseReactionInfo class. Not receive parameters.
        /// </summary>
        public AdverseReactionInfo()
        {
            _startReaction = Convert.ToDateTime("01/01/1900");
        }

        #endregion


        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for AdverseReactionId.
        ///     </para>
        ///     <para>
        ///         Adverse Reaction identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int AdverseReactionId
        {
            get { return this._adverseReactionId; }
            set { this._adverseReactionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for StartReaction.
        ///     </para>
        ///     <para>
        ///         Start Reaction date.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime StartReaction
        {
            get { return this._startReaction; }
            set { this._startReaction = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BookName.
        ///     </para>
        ///     <para>
        ///         Book name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string DescriptionReaction
        {
            get { return this._descriptionReaction; }
            set { this._descriptionReaction = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ConsequenceId.
        ///     </para>
        ///     <para>
        ///         Consequence identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ConsequenceId
        {
            get { return this._consequenceId; }
            set { this._consequenceId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Adverse Reaction is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _adverseReactionId;
        private DateTime _startReaction;
        private string _descriptionReaction;
        private byte _consequenceId;
        private bool _active;
    }

}

