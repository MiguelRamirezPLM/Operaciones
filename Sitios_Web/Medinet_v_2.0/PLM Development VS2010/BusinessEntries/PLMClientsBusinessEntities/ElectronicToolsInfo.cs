using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the Electronic Tool information.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ElectronicToolsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ElectronicToolsInfo class. Not receive parameters.
        /// </summary>
        public ElectronicToolsInfo() 
        {
            this._publishedDate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolId.
        ///     </para>
        ///     <para>
        ///         Electronic tool identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int ToolId
        {
            get
            {
                return this._toolId;
            }
            set
            {
                this._toolId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolTypeId.
        ///     </para>
        ///     <para>
        ///         Tool type identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ToolTypeId
        {
            get
            {
                return this._toolTypeId;
            }
            set
            {
                this._toolTypeId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolTilte.
        ///     </para>
        ///     <para>
        ///         Tool title.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ToolTilte
        {
            get
            {
                return this._toolTitle;
            }
            set
            {
                this._toolTitle = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ToolDescription.
        ///     </para>
        ///     <para>
        ///         Tool description.
        ///     </para>
        /// </summary>
        [DataMember]
        public string ToolDescription
        {
            get
            {
                return this._toolDescription;
            }
            set
            {
                this._toolDescription = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PublishedDate.
        ///     </para>
        ///     <para>
        ///         Publication date tool's.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime PublishedDate
        {
            get
            {
                return this._publishedDate;
            }
            set
            {
                this._publishedDate = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for FileName.
        ///     </para>
        ///     <para>
        ///         File name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Url.
        ///     </para>
        ///     <para>
        ///         Tool URL.
        ///     </para>
        /// </summary>
        [DataMember]
        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         Indicates if the Tool is enabled or disabled.
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

        private int _toolId;
        private byte _toolTypeId;
        private string _toolTitle;
        private string _toolDescription;
        private DateTime _publishedDate;
        private string _fileName;
        private string _url;
        private bool _active;

    }
}
