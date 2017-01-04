using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core.Extensions;

namespace WebApp.Models
{
    public class ProductSortModel
    {
        public string Query { get; set; }
        public string DisplayName { get; set; }
        public int CategoryId { get; set; }

        public string CurrentSort { get; set; }
        public string CurrentDir { get; set; }
        public string Sort { get; set; }
        public string Dir { get; set; }

    }
    //Search
    public class ProductSearchModel
    {
        public ProductSortModel ProductSortModel { get; set; }

        public int ProductCount
        {
            get { return ProductPhotoViewList.Count; }
        }
        public virtual ICollection<ProductPhotoViewModel> ProductPhotoViewList { get; set; }
        public ProductSearchModel()
        {

        }

        public ProductSearchModel(IEnumerable<Product> productList)
        {
          
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