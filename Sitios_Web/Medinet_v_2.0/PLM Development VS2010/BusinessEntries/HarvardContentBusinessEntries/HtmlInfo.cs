using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace HarvardContentBusinessEntries
{      
    /// <summary>
    ///     Class which represents a HTML Template properties.
    /// </summary>
    [DataContract]
    public class HtmlInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the HtmlInfo class. Not receive parameters.
        /// </summary>
        public HtmlInfo() { }
     
        #endregion

        #region Propierities

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Body.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openBody
        {
            get { return "<body>"; }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close body.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeBody
        { 
            get { return "</body>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Paragraph.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openParagraph
        { 
            get { return "<p>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Paragraph Close.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeParagraph
        { 
            get { return "</p>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Section.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openSection
        { 
            get { return "<section>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Section.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeSection
        { 
            get { return "</section>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Head level 1.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openHead1
        { 
            get { return "<h1>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Head level 1.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeHead1
        { 
            get { return "</h1>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Head level 2.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openHead2
        { 
            get { return "<h2>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Head level 2.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeHead2
        { 
            get { return "</h2>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Head Level 3.
        ///     </para>
        /// </summary>
        [DataMember]
        public String openHead3
        { 
            get { return "<h3>"; } 
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets a value for Html Close Head level 3.
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeHead3
        { 
            get { return "</h3>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Italic Letter
        ///     </para>
        /// </summary>
        [DataMember]
        public String openItalic
        { 
            get { return "<i>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Italic Letter
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeItalic
        { 
            get { return "</i>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for break
        ///     </para>
        /// </summary>
        [DataMember]
        public String breaks
        { 
            get { return "<br>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Bold Letter
        ///     </para>
        /// </summary>
        [DataMember]
        public String openBold
        { 
            get { return "<b>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Bold Letter
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeBold
        { 
            get { return "</b>"; } 
        }
        
        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open List
        ///     </para>
        /// </summary>
        [DataMember]
        public String openList
        { 
            get { return "<ul>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close List
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeList
        { 
            get { return "</ul>"; } 
        }

        /// <summary>
        ///     <para>
        ///         Property which gets a value for Open Table
        ///     </para>
        /// </summary>
        [DataMember]
        public String openTable
        { 
            get { return "<table>"; } 
        }
                    
        /// <summary>
        ///     <para>
        ///         Property which gets a value for Close Table
        ///     </para>
        /// </summary>
        [DataMember]
        public String closeTable
        { 
            get { return "</table>"; } 
        }

        #endregion

        public static readonly HtmlInfo Instance = new HtmlInfo();

    }
}
