using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Client information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ClientAdvisorInfo : ClientInfo
    {
         #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ClientInfo class. Not receive parameters.
        /// </summary>
        public ClientAdvisorInfo() 
        {
            
        }
        
        #endregion


        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for States.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.StateInfo"/>. Indicates the states
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PLMClientsBusinessEntities.StateInfo> States
        {
            get
            {
                return this._states;
            }
            set
            {
                foreach (PLMClientsBusinessEntities.StateInfo state in value)
                {
                    this._states.Add(state);
                }
            }
        }

        private List<PLMClientsBusinessEntities.StateInfo> _states = new List<StateInfo>();
    }
}
