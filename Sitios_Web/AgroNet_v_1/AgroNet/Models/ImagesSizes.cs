using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class ImagesSizes
    {
        #region Constructor
        public ImagesSizes(int SizeId)
        {
            ImageSizeId = SizeId;
        }

        #endregion

        #region Member

        private int ImageSizeId;

        #endregion

        #region Properties

        public int SizeId
        {
            get
            {
                return ImageSizeId;
            }
            set
            {
                ImageSizeId = value;
            }
        }
        #endregion
    }
}