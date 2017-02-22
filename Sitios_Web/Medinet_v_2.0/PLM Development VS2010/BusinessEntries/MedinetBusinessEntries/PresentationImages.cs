using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MedinetBusinessEntries
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
        public int presentationId
        {
            get
            {
                return this._presentationId;
            }
            set
            {
                this._presentationId = value;
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
        public string PreviousImage
        {
            get
            {
                return this._previousImage;
            }
            set{
                this._previousImage = value;
            }
        }
       
           
        #endregion

        private string _productShot;
        
        private int _presentationId;

        private int _active;
        private int _imageId;
        private string _imageSize;
        private string _previousImage;
  

    }
}
