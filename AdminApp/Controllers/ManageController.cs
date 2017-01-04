using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AdminApp.Models;
using System.Xml;
using System.IO;

using DAL;
using BLL;
using Model;
using System.Collections.Generic;
using Core.Extensions;
using Web.Core;

namespace AdminApp.Controllers
{
    public class ManageController : Controller
    {

        public ActionResult CreateSeedData()
        {
            return View("SeedData");
        }
        void SeedData()
        {
            string dataPath = "";
            XmlDocument doc = new XmlDocument();


            using (var context = new InmallContext())
            {
                SeedDataCreator seedDataCreator = new SeedDataCreator(context);
                 dataPath = Server.MapPath("~/App_Data/Products.xml");
                // dataPath = Server.MapPath("~/App_Data/Categories.xml");
                //dataPath = Server.MapPath("~/App_Data/EthnicGroups.xml");
                doc.Load(dataPath);


               // seedDataCreator.CreateTags(doc);

            }

            ViewBag.test = "Done";

        }

        void CreateCategoryPhoto()
        {
            string folderPath = @"C:\Users\Stephen\Pictures\Inmall\目錄圖片";
            var directory = new DirectoryInfo(folderPath);
            var allImage = directory.GetFiles();

            using (var context = new InmallContext())
            {
                foreach (FileInfo imageFileOnfo in allImage)
                {
                    string name = imageFileOnfo.Name.Replace(".jpg", "").Replace("_", "/"); ;
                    var category = context.Categories.Where(s => s.Name.Trim() == name).FirstOrDefault();

                    AddCategoryPhoto(imageFileOnfo, category);
                }

                context.SaveChanges();
            }
        }

        //void CreateProductPhoto()
        //{
        //    ViewBag.test = "";

        //    string folderPath = @"C:\Users\Stephen\Pictures\Inmall";
        //    var directory = new DirectoryInfo(folderPath);

        //    using (var context = new InmallContext())
        //    {
        //        foreach (var parentDir in directory.GetDirectories())
        //        {
        //            var titleImage = parentDir.GetFiles().Where(file => file.Name == "title.jpg").FirstOrDefault();
        //            var category = GetCategoryByFolderName(parentDir.Name, context);
        //            AddCategoryPhoto(titleImage, category);

        //            foreach (var subDir in parentDir.GetDirectories())
        //            {
        //                var subCategory = GetCategoryByFolderName(subDir.Name, context);
        //                foreach (var productDir in subDir.GetDirectories())
        //                {

        //                    var product = GetProductByFolderName(productDir.Name, context);
        //                    if (product != null)
        //                    {
        //                        AddProductPhotoes(productDir, product);
        //                    }

        //                }
        //            }

        //        }

        //        context.SaveChanges();
        //    }

        //    ViewBag.test = "Done";

        //}


        Category GetCategoryByFolderName(string folderName, InmallContext context)
        {
            string categoryName = folderName.Replace("_", "/");

            var category = context.Categories.Where(c => c.Name == folderName).FirstOrDefault();

            if (category != null) return category;

            category = new ProductCategory() { Name = categoryName, Photoes = new List<CategoryPhoto>() };
            context.Categories.Add(category);

            return category;
        }

        void AddCategoryPhoto(FileInfo imageFileInfo, Category category)
        {
            if (category.Photoes.IsNullOrEmpty())
            {
                category.Photoes = new List<CategoryPhoto>();
                var bytesImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
                var photo = new CategoryPhoto
                {
                    Caption = category.Name,
                    Top = true,
                    FileBytes = bytesImageFile
                };
                category.Photoes.Add(photo);
            }
        }
        Product GetProductByFolderName(string folderName, InmallContext context)
        {
            string productName = folderName.Replace("_", "/");

            var product = context.Products.Where(c => c.Name == folderName).FirstOrDefault();
            return product;
            //if (product != null) return product;

            //product = new Product() { Name = productName, Photoes = new List<ProductPhoto>() };
            //context.Products.Add(product);

            //return product;
        }

        void AddProductPhotoes(DirectoryInfo productDir, Product product)
        {
            if (product.Photoes.IsNullOrEmpty()) product.Photoes = new List<ProductPhoto>();
            foreach (var imageFileInfo in productDir.GetFiles())
            {
                bool top = imageFileInfo.Name == "top.jpg";
                var bytesImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
                var photo = new ProductPhoto
                {
                    Caption = product.Name,
                    Top = top,
                    FileBytes = bytesImageFile
                };
                product.Photoes.Add(photo);
            }
        }


        void CreateAds()
        {
            var adList = new List<AD>();
            string folderPath = @"C:\Users\Stephen\Pictures\Inmall\ad\";
            var ad = new AD
            {
                NewWindow = false,
                Title = "袋代相傳",
                Url = "/Product/Category/162"
            };
            var imageFileInfo = new FileInfo(folderPath + "a.jpg");
            ad.ImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
            adList.Add(ad);

            ad = new AD
            {
                NewWindow = false,
                Title = "全館免運",
                Url = ""
            };
            imageFileInfo = new FileInfo(folderPath + "c.png");
            ad.ImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
            adList.Add(ad);

            ad = new AD
            {
                NewWindow = false,
                Title = "那都蘭工作室",
                Url = "/Product/Category/155"
            };
            imageFileInfo = new FileInfo(folderPath + "b.jpg");
            ad.ImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
            adList.Add(ad);

            ad = new AD
            {
                NewWindow = false,
                Title = "豐禾有機農場",
                Url = "/Product/Category/165"
            };
            imageFileInfo = new FileInfo(folderPath + "d.jpg");
            ad.ImageFile = ImageHelper.GetByteFromImageFile(imageFileInfo);
            adList.Add(ad);

            using (var context = new InmallContext())
            {
                foreach (var item in adList)
                {
                    context.Ads.Add(item);
                }

                context.SaveChanges();
            }
        }

    }
}