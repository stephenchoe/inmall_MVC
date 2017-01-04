using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public abstract class PhotoViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        protected void SetValues(Photo photo)
        {
            Id = photo.Id;
            Caption = photo.Caption;
        }
    }
    public class ProductPhotoViewModel : PhotoViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string SupplierName { get; set; }
        public int ProductId { get; set; }
        public bool Top { get; set; }

        public ProductPhotoViewModel(ProductPhoto photo , string name="", int price=0,string supplier="")
        {
            SetValues(photo);
            ProductId = photo.ProductId;
            Top = photo.Top;
            Name = name;
            Price = price;
            SupplierName = supplier;
        }
    }
    public class CategoryPhotoViewModel : PhotoViewModel
    {
        public int CategoryId { get; set; }

        public CategoryPhotoViewModel(CategoryPhoto photo)
        {
            SetValues(photo);
            CategoryId = photo.CategoryId;
        }
    }

    
}