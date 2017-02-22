using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Frequency information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>

    [DataContract]

   public class FrequenciesInfo
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the FrequenciesInfo class. Not receive parameters.
        /// </summary>
        
        public FrequenciesInfo() {}
        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FrequencyId.
        ///     </para>
        ///     <para>
        ///         Frequency identifier.
        ///     </para>
        /// </summary>
        [DataMember]

        public int FrequencyId
        {
            get { return this._frequencyId; }
            set { this._frequencyId = value; }
        }


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for  Description.
        ///     </para>
        ///     <para>
        ///         Frequency Description.
        ///     </para>
        /// </summary>
        [DataMember]


        public string Frequency
        {
            get { return this._frequency; }
            set { this._frequency = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Frequency is enabled or disabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }



        #endregion


        private int _frequencyId;
        private string _frequency;
        private bool _active;


    }
}
