using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace PLMClientsBusinessEntities
{
    /// <summary>
    ///     Class which represents the search detail of contents information and its results.
    /// </summary>
    /// <remarks>
    ///     Transmits information between the application layers.
    /// </remarks>
    [Serializable]
    [DataContract]

    public class PharmacovigilanceParentDrugInfo:PharmacovigilanceDrugInfo
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the PharmacovigilanceDrugInfo class. Not receives parameters.
        /// </summary>

        public PharmacovigilanceParentDrugInfo()
        {
            this._childrenPharmacovigilanceDrugsInfo = new List<PharmacovigilanceDrugInfo>();
        }


        #endregion

        #region Properties

        /// <summary>
        ///     <para>
        ///         Property which gets or sets a value for ChildrenPharmacovigilanceDrugsInfo.
        ///     </para>
        ///     <para>
        ///         It is a list of objects of type <see cref="PLMClientsBusinessEntities.PharmacovigilaceDrugInfo"/>.
        ///     </para>
        /// </summary>

        [DataMember]
        public List<PharmacovigilanceDrugInfo> ChildrenPharmacovigilanceDrugs
        {
            get { return this._childrenPharmacovigilanceDrugsInfo; }

            set {  
                  this._childrenPharmacovigilanceDrugsInfo= new List<PharmacovigilanceDrugInfo>();
                  foreach (PharmacovigilanceDrugInfo vigilanceDrugInfo in value)
                      this._childrenPharmacovigilanceDrugsInfo.Add(vigilanceDrugInfo);
            }
        }

        #endregion

  
            private List<PharmacovigilanceDrugInfo> _childrenPharmacovigilanceDrugsInfo;
    }
}
