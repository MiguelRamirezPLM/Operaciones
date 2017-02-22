using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the different routes where are the terms documents.
    /// </summary>
    [DataContract]
    public class TermsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the TermsInfo class. Not receive parameters.
        /// </summary>
        public TermsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for TextFile.
        ///     </para>
        ///     <para>
        ///         Route where is the document in text format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string TextFile
        {
            get
            {
                return this._textFile;
            }
            set
            {
                this._textFile = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PdfFile.
        ///     </para>
        ///     <para>
        ///         Route where is the document in PDF format.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PdfFile
        {
            get
            {
                return this._pdfFile;
            }
            set
            {
                this._pdfFile = value;
            }
        }

        #endregion

        private string _textFile;
        private string _pdfFile;

    }
}
