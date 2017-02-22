using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PharmaSearchEngineBusinessEntries
{
    /// <summary>
    ///     Class which represents the Symptom information associated with a Edition.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    /// <seealso cref="MedinetBusinessEntries.SymptomInfo"/>
    [DataContract]
    public class SymptomByEditionInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the SymptomByEditionInfo class. Not receive parameters.
        /// </summary>
        public SymptomByEditionInfo() 
        {
            this._symptomChildrens = new List<SymptomByEditionInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomId.
        ///     </para>
        ///     <para>
        ///         Symptom Identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int SymptomId
        {
            get
            {
                return this._symptonId;
            }
            set
            {
                this._symptonId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ParentId.
        ///     </para>
        ///     <para>
        ///         Indicates if the Symptom have an parent Symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public int? ParentId
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomName.
        ///     </para>
        ///     <para>
        ///         Name or description of the Symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SymptomName
        {
            get
            {
                return this._symptomName;
            }
            set
            {
                this._symptomName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomColorIn.
        ///     </para>
        ///     <para>
        ///         Hexadecimal color code when the symptom is active on the website.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SymptomColorIn
        {
            get
            {
                return this._symptomColorIn;
            }
            set
            {
                this._symptomColorIn = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomColorOut.
        ///     </para>
        ///     <para>
        ///         Hexadecimal color code when the symptom is inactive on the website.
        ///     </para>
        /// </summary>
        [DataMember]
        public string SymptomColorOut
        {
            get
            {
                return this._symptomColorOut;
            }
            set
            {
                this._symptomColorOut = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for HeaderImage.
        ///     </para>
        ///     <para>
        ///         Image Name which is displayed on the web site header.
        ///     </para>
        /// </summary>
        [DataMember]
        public string HeaderImage
        {
            get
            {
                return this._headerImage;
            }
            set
            {
                this._headerImage = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for SymptomChildrens.
        ///     </para>
        ///     <para>
        ///         Is an list of objects of type <see cref="PharmaSearchEngineBusinessEntries.SymptomByEditionInfo"/>. Indicates if the Symptom have an son Symptom.
        ///     </para>
        /// </summary>
        [DataMember]
        public List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> SymptomChildrens
        {
            get
            {
                return this._symptomChildrens;
            }
            set
            {
                foreach (PharmaSearchEngineBusinessEntries.SymptomByEditionInfo children in value)
                {
                    this._symptomChildrens.Add(children);
                }
            }
        }

        #endregion

        private int _symptonId;
        private int? _parentId;
        private string _symptomName;
        private string _symptomColorIn;
        private string _symptomColorOut;
        private string _headerImage;
        private List<PharmaSearchEngineBusinessEntries.SymptomByEditionInfo> _symptomChildrens;

    }
}
