using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;
using System.IO;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Services.Description;

namespace AgroNet.Controllers.Laboratories
{
    public class ImagesController : Controller
    {
        //
        // GET: /Images/
        DEAQ db = new DEAQ();
        insertimage insimage = new insertimage();
        ReplacesImageNames ReplacesImageNames = new ReplacesImageNames();

        public ActionResult Index(string Id, bool flag)
        {
            if (Id != null)
            {
                int DivisionId = int.Parse(Id);
                var Img = (from DImages in db.DivisionImages
                           join DIS in db.DivisionImagesSizes
                           on DImages.DivisionImageId equals DIS.DivisionImageId
                           join IS in db.ImageSizes
                           on DIS.ImageSizeId equals IS.ImageSizeId
                           where DImages.DivisionId == DivisionId
                           select new joinDivisionImageSizes { ImageSizes = IS, DivisionImages = DImages, DivisionImagesSizes = DIS }).ToList();
                DivInfoId DivInfoId = new DivInfoId(DivisionId, flag);
                Session["DivInfoId"] = DivInfoId;
                return View(Img);
            }
            else
            {
                DivInfoId D = (DivInfoId)Session["DivInfoId"];
                if (D != null)
                {
                    int DivisionId = D.DivisionId;
                    var Img = (from DImages in db.DivisionImages
                               join DIS in db.DivisionImagesSizes
                               on DImages.DivisionImageId equals DIS.DivisionImageId
                               join IS in db.ImageSizes
                               on DIS.ImageSizeId equals IS.ImageSizeId
                               where DImages.DivisionId == DivisionId
                               select new joinDivisionImageSizes { ImageSizes = IS, DivisionImages = DImages, DivisionImagesSizes = DIS }).ToList();
                    return View(Img);
                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
        }

        [HttpPost]
        public ActionResult newimages(HttpPostedFileBase file)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string Size = "";
            DivInfoId D = (DivInfoId)Session["DivInfoId"];
            int DivisionId = D.DivisionId;
            ImagesSizes IMSZ = (ImagesSizes)Session["ImagesSizes"];
            int ImageSizeId = IMSZ.SizeId;

            string img = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            string ID = "";

            var DI = from DivImg in db.Divisions
                     where DivImg.DivisionId == DivisionId
                     select DivImg;
            foreach (Divisions Divs in DI)
            {
                img = Divs.ShortName;

                var c = (from country in db.Countries
                         where country.CountryId == Divs.CountryId
                         select country).ToList();
                foreach (Countries _c in c)
                {
                    ID = _c.ID;
                }
            }
            img = ReplacesImageNames.replaces(img);

            var ImagesS = (from ImagesSize in db.ImageSizes
                           where ImagesSize.ImageSizeId == ImageSizeId
                           select ImagesSize).ToList();
            foreach (ImageSizes IS in ImagesS)
            {
                Size = IS.Size;

                if (IS.ImageSizeId == ImageSizeId)
                {

                    img = img + extension;
                    var path = Server.MapPath("~/App_Data/DivisionImages/" + ID + "");

                    if (System.IO.Directory.Exists(path))
                    {
                        var root = Path.Combine(path, Size);

                        if (System.IO.Directory.Exists(root))
                        {
                            var rt = Path.Combine(root, img);

                            file.SaveAs(rt);
                        }
                        else
                        {
                            var rt = Path.Combine(root, img);

                            DirectoryInfo Dirs = Directory.CreateDirectory(root);

                            file.SaveAs(rt);
                        }

                    }
                    else
                    {
                        DirectoryInfo Dir = Directory.CreateDirectory(path);

                        var root = Path.Combine(path, Size);

                        if (System.IO.Directory.Exists(root))
                        {
                            var rt = Path.Combine(root, img);

                            file.SaveAs(rt);
                        }
                        else
                        {
                            var rt = Path.Combine(root, img);

                            DirectoryInfo Dirs = Directory.CreateDirectory(root);

                            file.SaveAs(rt);
                        }
                    }
                }
            }
            bool images = insimage.insertimages(img, DivisionId, ImageSizeId, ApplicationId, UsersId);

            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showimages(string image, int DivisionImageId)
        {
            if (image != null)
            {

                var di = db.DivisionImages.Where(x => x.DivisionImageId == DivisionImageId).ToList();

                if (di.LongCount() > 0)
                {
                    int? DivisionId = di[0].DivisionId;
                    int? CountryId = 0;
                    String ID = "";

                    var d = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

                    if (d.LongCount() > 0)
                    {
                        CountryId = d[0].CountryId;

                        var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                        if (c.LongCount() > 0)
                        {
                            ID = c[0].ID;
                        }

                        var DivisionI = (from DImages in db.DivisionImages
                                         join DIS in db.DivisionImagesSizes
                                         on DImages.DivisionImageId equals DIS.DivisionImageId
                                         join IS in db.ImageSizes
                                         on DIS.ImageSizeId equals IS.ImageSizeId
                                         where DImages.ImageName == image
                                         select IS).ToList();
                        foreach (ImageSizes D in DivisionI)
                        {
                            var root = Server.MapPath("~/App_Data/DivisionImages/" + ID + "");
                            root = Path.Combine(root, D.Size);
                            var path = Path.Combine(root, image);
                            if (System.IO.File.Exists(path))
                            {
                                return File(path, "image/png");
                            }
                            else
                            {
                                string ProductShot = "not_available.png";
                                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                                return File(rootnot, "image/png");
                            }
                        }
                    }
                }
            }
            else
            {
                string ProductShot = "not_available.png";
                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                return File(rootnot, "image/png");
            }
            return View();
        }

        public ActionResult imagesizes(string sizeid)
        {
            int SizeId = int.Parse(sizeid);

            ImagesSizes ImagesSizes = new ImagesSizes(SizeId);
            Session["ImagesSizes"] = ImagesSizes;
            return View();
        }

        public ActionResult imagesdetails(int DivisionId)
        {
            var DivisionI = (from DImages in db.DivisionImages
                             join DIS in db.DivisionImagesSizes
                             on DImages.DivisionImageId equals DIS.DivisionImageId
                             join IS in db.ImageSizes
                             on DIS.ImageSizeId equals IS.ImageSizeId
                             where DImages.DivisionId == DivisionId
                             select new joinDivisionImageSizes { ImageSizes = IS, DivisionImages = DImages, DivisionImagesSizes = DIS }).ToList();
            return View(DivisionI);
        }

        public ActionResult showimagesdetails(string image, int DivisionImageId, byte size)
        {
            if (image != null)
            {
                var di = db.DivisionImages.Where(x => x.DivisionImageId == DivisionImageId).ToList();

                if (di.LongCount() > 0)
                {
                    int? DivisionId = di[0].DivisionId;
                    int? CountryId = 0;
                    String ID = "";

                    var d = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

                    if (d.LongCount() > 0)
                    {
                        CountryId = d[0].CountryId;

                        var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                        if (c.LongCount() > 0)
                        {
                            ID = c[0].ID;
                        }

                        var DivisionI = (from DImages in db.DivisionImages
                                         join DIS in db.DivisionImagesSizes
                                         on DImages.DivisionImageId equals DIS.DivisionImageId
                                         join IS in db.ImageSizes
                                         on DIS.ImageSizeId equals IS.ImageSizeId
                                         where DIS.DivisionImageId == DivisionImageId
                                         && IS.ImageSizeId == size
                                         select IS).ToList();
                        foreach (ImageSizes DI in DivisionI)
                        {
                            var root = Server.MapPath("~/App_Data/DivisionImages/" + ID + "");
                            root = Path.Combine(root, DI.Size);
                            var path = Path.Combine(root, image);

                            if(System.IO.File.Exists(path))
                            {
                                return File(path, "image/png");
                            }
                            else
                            {
                                string ProductShot = "not_available.png";
                                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                                return File(rootnot, "image/png");
                            }
                           
                        }
                    }
                }
            }
            else
            {
                string ProductShot = "not_available.png";
                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                return File(rootnot, "image/png");
            }
            return View();
        }
    }
}
