using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MedinetBusinessEntries
{
    /// <summary>
    ///     Class which represents the ProductSymptom information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class EditionSymptomInfo
    {

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for symptomId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SymptomId
        {
            get { return this._symptomId; }
            set { this._symptomId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for editionId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int EditionId
        {
            get { return this._editionId; }
            set { this._editionId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for editionId.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Page
        {
            get { return this._page; }
            set { this._page = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for symptomName.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SymptomName
        {
            get { return this._symptomName; }
            set { this._symptomName = value; }
        }
        private int _symptomId;
        private int _editionId;
        private string _page;
        private string _symptomName;
    }

}
