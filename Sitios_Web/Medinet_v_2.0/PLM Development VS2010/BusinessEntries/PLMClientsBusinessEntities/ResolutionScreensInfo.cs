using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the relationship between Screen Sizes and Resolution identifiers.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [DataContract]
    public class ResolutionScreensInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ResolutionScreensInfo class. Not receive parameters.
        /// </summary>
        public ResolutionScreensInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ScreenId.
        ///     </para>
        ///     <para>
        ///         Screen identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ScreenId
        {
            get
            {
                return this._screenId;
            }
            set
            {
                this._screenId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ResolutionId.
        ///     </para>
        ///     <para>
        ///         Resolution identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public byte ResolutionId
        {
            get
            {
                return this._resolutionId;
            }
            set
            {
                this._resolutionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for BaseUrl.
        ///     </para>
        ///     <para>
        ///         Web address where is the image.
        ///     </para>
        /// </summary>
        [DataMember]
        public string BaseUrl
        {
            get
            {
                return this._baseUrl;
            }
            set
            {
                this._baseUrl = value;
            }
        }

        #endregion

        private byte _screenId;
        private byte _resolutionId;
        private string _baseUrl;
    }
}
