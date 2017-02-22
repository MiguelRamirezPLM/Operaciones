using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VetBusinessEntries
{
    /// <summary>
    ///     Class which represents information of the Therapeutics.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Transmits information between the application layers.
    ///     </para>
    /// </remarks>
    [DataContract]

    public class TherapeuticInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TherapeuticInfo class. Not receive parameters.
        /// </summary>
        public TherapeuticInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TherapeuticId.
        ///     </para>
        ///     <para>
        ///         Therapeutic Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int TherapeuticId
        {
            get { return this._therapeuticId; }
            set { this._therapeuticId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic have an parent Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get { return this._parentId; }
            set { this._parentId = value; }
        }

        

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Description.
        ///     </para>
        ///     <para>
        ///         Name or Description of the Therapeutic.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TherapeuticName
        {
            get { return this._therapeuticName; }
            set { this._therapeuticName = value; }
        }

        

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Therapeutic is enabled.
        ///     </para>
        /// </summary>
        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }



        #endregion

        private int _therapeuticId;
        private int? _parentId;
        private string _therapeuticName;
        private bool _active;






    }
}
