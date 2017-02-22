using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EncyclopediasBussinesEntries
{
    /// <summary>
    ///     Class which represents information about a Encyclopedia.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EncyclopediaInfo
    {

        #region Constructors

        ///<summary> 
        /// Initializes a new instance of the Encyclopediainfo class.
        ///</summary>

        public EncyclopediaInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaId.
        ///     </para>
        ///     <para>
        ///         Encyclopedia Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaId
        {

            get { return this._encyclopediaId; }
            set { this._encyclopediaId = value; }

        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaName.
        ///     </para>
        ///     <para>
        ///       Name of the Encyclopedia.   
        ///     </para>
        /// </summary> 
        [DataMember]
        public string EncyclopediaName
        {

            get { return this._encyclopediaName; }
            set { this._encyclopediaName = value; }

        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicate the status for the encyclopedia.  
        ///     </para>
        /// </summary>


        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }

        }
        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for EncyclopediaTypeId.
        ///     </para>
        ///     <para>
        ///          Value for  the Encyclopedia type.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EncyclopediaTypeId
        {
            get { return this._encyclopediaTypeId; }
            set { this._encyclopediaTypeId = value; }

        }

        #endregion



        private int _encyclopediaId;
        private string _encyclopediaName;
        private bool _active;
        private int _encyclopediaTypeId;
    }
}
