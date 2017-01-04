using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core.Extensions;

namespace WebApp.Models
{
    public abstract class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }



        protected void SetValues(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
    public class MainCategoryViewModel : CategoryViewModel
    {
        public string Prefix { get; set; }

        public CategoryPhotoViewModel PhotoViewModel { get; set; }

        public virtual ICollection<CategoryPhotoViewModel> SubCategoryPhotoViewList { get; set; }

        public MainCategoryViewModel(Category category, ICollection<Category> subCategories)
        {
            SetValues(category);

            PhotoViewModel = new CategoryPhotoViewModel(category.TopPhoto);

            if (category is ProductCategory)
            {
                var productCategory = category as ProductCategory;
                Prefix = productCategory.Prefix;
            }

            SubCategoryPhotoViewList = new List<CategoryPhotoViewModel>();
            foreach (var sub in subCategories)
            {
                SubCategoryPhotoViewList.Add(new CategoryPhotoViewModel(sub.TopPhoto));
            }
        }
    }

    public class SubCategoryViewModel : CategoryViewModel
    {
        public int ParentId { get; set; }
        public string ParentName { get; set; }

        public ProductSortModel ProductSortModel { get; set; }

        public int ProductCount
        {
            get { return ProductPhotoViewList.Count; }
        }
        public virtual ICollection<ProductPhotoViewModel> ProductPhotoViewList { get; set; }


        public SubCategoryViewModel(Category category, int parentId, string parentName, IEnumerable<Product> productList = null)
        {
            SetValues(category);
            ParentId = parentId;
            ParentName = parentName;


            if (productList.IsNullOrEmpty()) return;

            ProductPhotoViewList = new List<ProductPhotoViewModel>();
            foreach (var product in productList)
            {
                var photo = product.Photoes.Where(p => p.Top).FirstOrDefault();
                string name = product.Name;
                int price = product.Price;
                string supplier = product.Supplier.Name;
                ProductPhotoViewList.Add(new ProductPhotoViewModel(photo, name, price, supplier));
            }


        }

    

    }

   
}