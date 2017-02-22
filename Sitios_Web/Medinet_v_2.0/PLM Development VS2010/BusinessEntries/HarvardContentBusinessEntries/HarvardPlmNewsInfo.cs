using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace HarvardContentBusinessEntries
{
    /// <summary>
    ///     Class which represents the news content.
    /// </summary>
    [DataContract]
    public class HarvardPlmNewsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the HarvardPlmNewsInfo class. Not receive parameters.
        /// </summary>
        public HarvardPlmNewsInfo() { }

        #endregion

        #region Propierities

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for title.
        ///     </para>
        /// </summary>
        [DataMember]
        public String title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for datePosting.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime datePosting
        {
            get { return this._datePosting; }
            set { this._datePosting = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for datePublish.
        ///     </para>
        /// </summary>
        [DataMember]
        public DateTime datePublish
        {
            get { return this._datePublish; }
            set { this._datePublish = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for content.
        ///     </para>
        /// </summary>
        [DataMember]
        public String content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for contenId.
        ///     </para>
        /// </summary>
        [DataMember]
        public string contentId
        {
            get { return this._contentId; }
            set { this._contentId = value; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for contentTypeId.
        ///     </para>
        /// </summary>
        [DataMember]
        public int contentTypeId
        {
            get { return this._contentTypeId; }
            set { this._contentTypeId = value; }
        }

        #endregion

        private String _title;
        private DateTime _datePublish;
        private String _content;
        private DateTime _datePosting;
        private string _contentId;
        private int _contentTypeId;

    }
}
