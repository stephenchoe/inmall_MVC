using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Core.Extensions;
using Model;


namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        string sourceDirectoryPath = @"C:\Users\Stephen\Pictures\Inmall";
        void GetFiles()
        {

            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
            var dirList = sourceDirectory.GetDirectories();
            using (var context = new DAL.InmallContext())
            {
                foreach (var dir in dirList)
                {
                    CreateCategoryPhoto(context, dir);

                    var subDirList = dir.GetDirectories();
                    foreach (var subDir in dir.GetDirectories())
                    {
                        CreateProducts(context, subDir, dir.Name);
                    }


                }
            }

            // ViewBag.test = result;
        }
        void CreateProducts(DAL.InmallContext context, DirectoryInfo dir, string parentDirName)
        {
            string dirName = dir.Name;
            string productName = dirName;
            var product = new Product
            {
                Active = true,
                Name = productName,
                CreateDate = DateTime.Now,
                Price = 500,
             
                Photoes = new List<ProductPhoto>()
            };

            var imageFileInfoes = dir.GetFiles();
            foreach (var file in imageFileInfoes)
            {
                string fileName = file.Name;

                string imagePath = Path.Combine(sourceDirectoryPath, parentDirName, dirName, fileName);

                Image productImage = Image.FromFile(imagePath);
                var byteImage = ConvertImageToByteArray(productImage, System.Drawing.Imaging.ImageFormat.Jpeg);


                var photo = new ProductPhoto
                {
                    Caption = "",
                    Top = (fileName == "top"),
                    FileBytes = byteImage
                };

                product.Photoes.Add(photo);
            }

            context.Products.Add(product);
            context.SaveChanges();
        }

        void CreateCategoryPhoto(DAL.InmallContext context, DirectoryInfo dir)
        {
            string dirName = dir.Name;
            var category = context.Categories.Where(c => c.Name == dirName).FirstOrDefault();
            string fileName = "title";
            string extention = "jpg";
            string imagePath = Path.Combine(sourceDirectoryPath, dirName, String.Format("{0}.{1}", fileName, extention));

            Image titleImage = Image.FromFile(imagePath);
            var byteImage = ConvertImageToByteArray(titleImage, System.Drawing.Imaging.ImageFormat.Jpeg);

            var photo = new CategoryPhoto
            {
                CategoryId = category.Id,
                Caption = category.Name,
                Top = true,
                FileBytes = byteImage
            };

            context.CategoryPhotoes.Add(photo);
            context.SaveChanges();
        }


        public ActionResult Index()
        {

            return View();

            //using(var context=new DAL.InmallContext())
            //{
            //    var category = context.Categories.Find(24);
            //    if (category.Photoes.IsNullOrEmpty())
            //    {
            //        category.Photoes = new List<CategoryPhoto>();
            //    }

            //    Image image1 = Image.FromFile(@"C:\Users\Stephen\Pictures\LeftsidePic.jpg");
            //    var byteImage = ConvertImageToByteArray(image1, System.Drawing.Imaging.ImageFormat.Jpeg);

            //    var photo = new CategoryPhoto()
            //    {
            //        Top = true,
            //        FileBytes = byteImage
            //    };
            //    category.Photoes.Add(photo);

            //    context.SaveChanges();

            //}





        }
        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        public FileContentResult ShowPhoto()
        {

            byte[] userImage;

            using (var context = new DAL.InmallContext())
            {
                var photo = context.CategoryPhotoes.Find(15);

                userImage = photo.FileBytes;

            }

            var image = byteArrayToImage(userImage);

            Image thumbnail = image.GetThumbnailImage(385, 385, null, IntPtr.Zero);

            var thumbnailFile = ConvertImageToByteArray(thumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);

            return new FileContentResult(thumbnailFile, "image/jpeg");
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {

            Image image;

            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                image = Image.FromStream(ms);
            }


            return image;
        }



      
    }
}