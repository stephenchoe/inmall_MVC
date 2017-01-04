using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core.Extensions;

namespace WebApp.Models
{
    //Details
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int OnlineId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public IList<string> Summary { get; set; }
        public IList<string> Features { get; set; }
        public ProductSpecification Specification { get; set; }
        public Description Description { get; set; }

        public int WaitDays { get; set; }

        public IList<ProductPhotoViewModel> PhotoViewList { get; set; }

        public SupplierViewModel SupplierView { get; set; }


        public ProductRatingModel RatingModel { get; set; }

        public SubCategoryViewModel CategoryViewModel { get; set; }

        public ProductViewModel(Product product, string userHostAddress)
        {
            Id = product.Id;
            OnlineId = product.OnlineId;
            Name = product.Name;
            Price = product.Price;

            
            Description = product.Description;
            WaitDays = product.WaitDays;

            Summary = new List<string>();
            if (!String.IsNullOrEmpty(product.Summary.Item1)) Summary.Add(product.Summary.Item1);
            if (!String.IsNullOrEmpty(product.Summary.Item2)) Summary.Add(product.Summary.Item2);
            if (!String.IsNullOrEmpty(product.Summary.Item3)) Summary.Add(product.Summary.Item3);
            if (!String.IsNullOrEmpty(product.Summary.Item4)) Summary.Add(product.Summary.Item4);
            if (!String.IsNullOrEmpty(product.Summary.Item5)) Summary.Add(product.Summary.Item5);

            Features = new List<string>();
            if (!String.IsNullOrEmpty(product.Features.Item1)) Features.Add(product.Features.Item1);
            if (!String.IsNullOrEmpty(product.Features.Item2)) Features.Add(product.Features.Item2);
            if (!String.IsNullOrEmpty(product.Features.Item3)) Features.Add(product.Features.Item3);
            if (!String.IsNullOrEmpty(product.Features.Item4)) Features.Add(product.Features.Item4);
            if (!String.IsNullOrEmpty(product.Features.Item5)) Features.Add(product.Features.Item5);


            Specification = product.Specification;

            SupplierView = new SupplierViewModel(product.Supplier);

            if (!product.Photoes.IsNullOrEmpty())
            {
                var photoList = product.Photoes.OrderByDescending(p => p.DisplayOrder);
                PhotoViewList = new List<ProductPhotoViewModel>();
                foreach (var photo in photoList)
                {
                    PhotoViewList.Add(new ProductPhotoViewModel(photo));
                }
            }

            RatingModel = new ProductRatingModel(product, userHostAddress);

        }




    }

   
    public class ProductRatingModel
    {
        public int ProductId { get; set; }
        public int RatingStar { get; set; }
        public int Star { get; set; }
        public bool CanRating { get; set; }

        public ProductRatingModel(Product product, string userHostAddress)
        {
            ProductId = product.Id;
            RatingStar = product.RatingStar;

            var rated = product.Ratings.Where(r => r.UserHostAddress == userHostAddress).FirstOrDefault();
            CanRating = (rated == null);
        }

        public ProductRatingModel()
        {
           
        }

       

    }

    public class SupplierViewModel
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Description Description { get; set; }
        public SupplierViewModel(Supplier supplier)
        {
            Name = supplier.Name;
            Contact = supplier.Contact;
            Tel = supplier.Tel;
            Phone = supplier.Phone;
            Email = supplier.Email;
            Address = supplier.Address;
            Description = supplier.Description;
        }
    }
}