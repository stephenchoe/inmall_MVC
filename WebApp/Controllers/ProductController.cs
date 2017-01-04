using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApp.Models;
using BLL;
using Model;
using System.Configuration;
using Core.Extensions;
using Web.Core;
using Web.Core.Models;


namespace WebApp.Controllers
{
    public class ProductController : AppControllerBase
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IPhotoService photoService;
        private readonly ITagService tagService;

        public ProductController(IErrorLogService errorLogService, IProductService productService, ICategoryService categoryService, 
                                    IPhotoService photoService, ITagService tagService)
            : base(errorLogService)
        {

            this.productService = productService;
            this.categoryService = categoryService;
            this.photoService = photoService;
            this.tagService = tagService;
        }

        public ActionResult Details(string pid, string cid = "")
        {

            int productId = pid.ToInt();
            if (productId < 1) return HttpNotFound();

            int categoryId = cid.ToInt();

            try
            {
                var product = productService.GetById(productId);
                if (product == null) return HttpNotFound();
                string userHostAddress = Request.UserHostAddress;

                var model = new ProductViewModel(product, userHostAddress);

                Category category;
                if (categoryId < 1) category = product.DefaultCategory;
                else
                {
                    category = product.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
                    if (category == null) category = product.DefaultCategory;
                }

                var parent = categoryService.GetParent(category);
                PageTitle = String.Format("{0} > {1}", parent.Name, category.Name);


                model.CategoryViewModel = new SubCategoryViewModel(category, parent.Id, parent.Name);
                return View(model);
            }
            catch (Exception ex)
            {
                HandleException(ex);

                return ErrorPage();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rating(ProductRatingModel model)
        {
            try
            {

                var product = productService.GetById(model.ProductId);
                if (product == null) return ThrowJsonError("");

                productService.AddRating(product, model.Star, Request.UserHostAddress);

                return Json(new { rating = product.RatingStar }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ThrowJsonError();
            }

        }


        public ActionResult Category(string id, string sort = "", string dir = "")
        {
            int categoryId = id.ToInt();
            if (categoryId < 1) return HttpNotFound();
            try
            {
                var category = categoryService.GetById(categoryId);
                if (category == null) return HttpNotFound();

                if (category.Parent == 0)
                {
                    return CategorySubMenu(category);
                }

                var parent = categoryService.GetParent(category);
                PageTitle = String.Format("{0} > {1}", parent.Name, category.Name);


                var sortModel = new ProductSortModel
                {
                    CategoryId = category.Id
                };

                var productList = productService.GetProductsByCategory(category);

                var orderedproductList = OrderProductList(sortModel, productList, sort, dir);


                var model = new SubCategoryViewModel(category, parent.Id, parent.Name, orderedproductList);
                model.ProductSortModel = sortModel;

                return View(model);


            }
            catch (Exception ex)
            {
                HandleException(ex);

                return ErrorPage();
            }
        }

        public ActionResult HotTags()
        {   
            int count = 5;
            var hotTags = tagService.GetHotTags(count);
            return PartialView("_HotTags", hotTags);
        }
        public ActionResult Search(string query, string sort = "", string dir = "")
        {
            try
            {
                var sortModel = new ProductSortModel
                {
                    Query = query,
                };
                PageTitle = String.Format("搜尋-「{0}」", query);
                bool orderByAccuracy = (sort == "accuracy");
                var productList = productService.SearchProductsByKeyword(query, orderByAccuracy);

                if (productList.IsNullOrEmpty()) return View(new ProductSearchModel { ProductSortModel = sortModel });

                var orderedproductList = OrderProductList(sortModel, productList, sort, dir);


                var model = new ProductSearchModel(orderedproductList);
                model.ProductSortModel = sortModel;

                return View(model);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ErrorPage();
            }
          
        }

        IEnumerable<Product> OrderProductList(ProductSortModel sortModel, IEnumerable<Product> productList, string sort, string dir)
        {
            List<Product> orderedProductList = null;
            bool orderByDesc = !(dir == "asc");
            if (sort == "price")
            {
                if (orderByDesc) orderedProductList = productList.OrderByDescending(p => p.Price).ToList();
                else orderedProductList = productList.OrderBy(p => p.Price).ToList();
            }
            else if (sort == "date")
            {
                if (orderByDesc) orderedProductList = productList.OrderByDescending(p => p.CreateDate).ToList();
                else orderedProductList = productList.OrderBy(p => p.CreateDate).ToList();
            }
            else if (sort == "accuracy")
            {
                orderedProductList = productList.ToList();
            }
            else    //人氣排序
            {
                sort = "clicks";
                if (orderByDesc) orderedProductList = productList.OrderByDescending(p => p.Clicks).ToList();
                else orderedProductList = productList.OrderBy(p => p.Clicks).ToList();
            }

            sortModel.CurrentSort = sort;
            sortModel.CurrentDir = (orderByDesc ? "" : "asc");

            return orderedProductList;
        }

        ActionResult CategorySubMenu(Category category)
        {
            PageTitle = category.Name;

            var subCategories = categoryService.GetSubCategories(category.Id);
            var model = new MainCategoryViewModel(category, subCategories.ToList());

            return View("CategorySubMenu", model);
        }

        public FileContentResult ProductPhoto(int id, int width = 0, int height = 0)
        {

            var photo = photoService.GetProductPhotoById(id);
            if (photo == null) photo = photoService.DefaultProductPhoto();

            return DisplayPhoto(photo, width, height);
        }

        public FileContentResult CategoryPhoto(int id, int width = 0, int height = 0)
        {
            var photo = photoService.GetCategoryPhotoById(id);
            if (photo == null) photo = photoService.DefaultCategoryPhoto();

            return DisplayPhoto(photo, width, height);
        }

        FileContentResult DisplayPhoto(Photo photo, int width, int height)
        {
            if (width == 0 && height == 0)
            {
                return new FileContentResult(photo.FileBytes, "image/jpeg");
            }

            byte[] thumbnailByteFile = photoService.GetThumbnailPhotoFile(photo, width, height);
            return new FileContentResult(thumbnailByteFile, "image/jpeg");
        }





    }

}
