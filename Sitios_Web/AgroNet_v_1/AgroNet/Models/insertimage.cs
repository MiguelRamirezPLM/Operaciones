using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class insertimage
    {
        DEAQ db = new DEAQ();
        ImageSizes IMGS = new ImageSizes();
        DivisionImagesSizes DIS = new DivisionImagesSizes();
        DivisionImages DivI = new DivisionImages();
        ActivityLog ActivityLog = new ActivityLog();

        public bool insertimages(string img, int DivisionId, int ImageSizeId, int ApplicationId, int UsersId)
        {
            int DivisionImageId = 0;
            byte ImageSize = Convert.ToByte(ImageSizeId);

            var DI = db.DivisionImages.Where(x => x.DivisionId == DivisionId && x.Active == true).ToList();

            if (DI.LongCount() > 0)
            {
                foreach (DivisionImages DImgs in DI)
                {
                    DImgs.ImageName = img.Trim();

                    db.SaveChanges();

                    DivisionImageId = DImgs.DivisionImageId;

                    ActivityLog.editimages(DivisionImageId, DivisionId, img, ApplicationId, UsersId);
                }
            }
            else
            {
                DivI.ImageTypeId = 1;
                DivI.DivisionId = DivisionId;
                DivI.ImageName = img;
                DivI.BaseURL = string.Empty;
                DivI.Active = true;

                db.DivisionImages.Add(DivI);
                db.SaveChanges();

                ActivityLog.insertimages(DivisionImageId, DivisionId, img, ApplicationId, UsersId);

                var DIM = db.DivisionImages.Where(x => x.DivisionId == DivisionId && x.Active == true && x.ImageName == img).ToList();

                foreach (DivisionImages DImgs in DIM)
                {
                    DivisionImageId = DImgs.DivisionImageId;
                }
            }

            //var DISizes = from DivisionImagesS in db.DivisionImagesSizes
            //              where DivisionImagesS.DivisionImageId == DivisionImageId
            //              && DivisionImagesS.ImageSizeId == ImageSize
            //              select DivisionImagesS;

            var DISizes = db.DivisionImagesSizes.Where(x => x.DivisionImageId == DivisionImageId && x.ImageSizeId == ImageSizeId).ToList();


            if (DISizes.LongCount() == 0)
            {
                DIS.DivisionImageId = DivisionImageId;
                DIS.ImageSizeId = ImageSize;

                db.DivisionImagesSizes.Add(DIS);
                db.SaveChanges();

                ActivityLog.insertDivisionImagesSizes(DivisionImageId, ImageSize, ApplicationId, UsersId);
            }

            return true;
        }
    }
}