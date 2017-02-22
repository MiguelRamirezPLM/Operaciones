using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents information about other drug interactions
    /// </summary>
    /// <seealso cref="MedinetBusinessEntries.ProductsInfo"/>
    /// <seealso cref="MedinetBusinessEntries.ActiveSubstanceInfo"/>
    /// <seealso cref="MedinetBusinessEntries.PharmacologicalGroupsInfo"/>
    [DataContract]
    public class OtherElementsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the OtherElementsInfo class. Not receive parameters.
        /// </summary>
        public OtherElementsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElementId.
        ///     </para>
        ///     <para>
        ///         Element Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ElementId
        {
            get
            {
                return this._elementId;
            }
            set
            {
                this._elementId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ElementName.
        ///     </para>
        ///     <para>
        ///         Element name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ElementName
        {
            get
            {
                return this._elementName;
            }
            set
            {
                this._elementName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Interaction is enabled or disabled.
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

        private int _elementId;
        private string _elementName;
        private bool _active;

    }
}
