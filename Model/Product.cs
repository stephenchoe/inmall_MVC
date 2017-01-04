using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Extensions;

namespace Model
{
    public class Product
    {
        public int Id { get; set; }
        public int OnlineId { get; set; }
        public int Clicks { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductSummary Summary { get; set; }
        public ProductFeatures Features { get; set; }
        public ProductSpecification Specification { get; set; }
        public Description Description { get; set; }
     
        public int WaitDays { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool Active { get; set; }
        public int RatingStar
        {
            get
            {
                if (Ratings.IsNullOrEmpty()) return 0;
                var most = Ratings.GroupBy(r => r.Star).OrderByDescending(gp => gp.Count()).Take(1).Select(c => c.Key).FirstOrDefault();
                return most;
            }
        }

        public Category DefaultCategory
        {
            get
            {
                return Categories.OfType<ProductCategory>().FirstOrDefault();
            }
        }

        public ProductPhoto DefaultPhoto
        {
            get
            {
                var photo = Photoes.Where(p => p.Top).FirstOrDefault();
                if (photo != null) return photo;
                return Photoes.FirstOrDefault();
            }
        }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductPhoto> Photoes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<PayWay> PayWays { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }


        public Supplier Supplier 
        {
            
            get 
            {
                if (Categories.IsNullOrEmpty()) return null;
                return Categories.OfType<Supplier>().FirstOrDefault();
            }
        }

    }

    public class ProductFeatures
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
    }
   



    public class ProductSummary
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
    }

    public class ProductSpecification
    {
        public string MadeIn { get; set; }
        public string MadeBy { get; set; }
        public string Size { get; set; }
        public string PS { get; set; }
    }

    public class Description
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
