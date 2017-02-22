using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MedinetBusinessEntries
{
    [DataContract]
    public class EncyclopediaImageInfo
    {
        #region Constructors
        public EncyclopediaImageInfo() { }
        #endregion
        #region Properties
        [DataMember]
        public string encyclopediaImage
        {
            get
            {
                return this._encyclopediaImage;
            }
            set
            {
                this._encyclopediaImage = value;
            }
        }

        [DataMember]
        public int encyclopediaId
        {
            get
            {
                return this._encyclopediaId;
            }
            set
            {
                this._encyclopediaId = value;
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
            set
            {
                this._imageSize = value;
            }
        }
        public string PreviousImage
        {
            get
            {
                return this._previousImage;
            }
            set
            {
                this._previousImage = value;
            }
        }


        #endregion

        private string _encyclopediaImage;

        private int _encyclopediaId;

        private int _active;
        private int _imageId;
        private string _imageSize;
        private string _previousImage;


    }
}