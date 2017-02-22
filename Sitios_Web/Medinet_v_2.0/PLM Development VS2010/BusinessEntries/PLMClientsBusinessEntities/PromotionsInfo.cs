using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the promotions information
    /// </summary>
    [DataContract]
    public class PromotionsInfo
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PromotionsInfo class. Not receive parameters.
        /// </summary>
        public PromotionsInfo() { }

        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PromotionId.
        ///     </para>
        ///     <para>
        ///         Promotion identifier.
        ///     </para>
        /// </summary>
        [DataMember]
        public int PromotionId
        {
            get
            {
                return this._promotionId;
            }
            set
            {
                this._promotionId = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for PromotionName.
        ///     </para>
        ///     <para>
        ///         Promotion name.
        ///     </para>
        /// </summary>
        [DataMember]
        public string PromotionName
        {
            get
            {
                return this._promotionName;
            }
            set
            {
                this._promotionName = value;
            }
        }

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for Active.
        ///     </para>
        ///     <para>
        ///         It indicates if the promotion is enabled or disabled.
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

        private int _promotionId;
        private string _promotionName;
        private bool _active;

    }
}
