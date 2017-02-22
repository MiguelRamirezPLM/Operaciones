using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AgroBusinessEntries
{
    [DataContract]
    public class PresentationImages
    {
        #region Constructors
        public PresentationImages() { }
        #endregion
        #region Properties
        [DataMember]
        public string productShot{
            get{
                return this._productShot;
            }
            set
            {
                this._productShot = value;
            }
        }
       
       
        [DataMember]
        public int Active
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
        [DataMember]
        public int ImageId
        {
            get
            {
                return this._imageId;
            }
            set
            {
                this._imageId = value;
            }
        }
        public string ImageSize
        {
            get
            {
                return this._imageSize;
            }
            set{
                this._imageSize = value;
            }
        }
   
       
           
        #endregion

        private string _productShot;
   

        private int _active;
        private int _imageId;
        private string _imageSize;


    }
}
