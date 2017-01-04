using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using DAL;
using DAL.Infrastructure;
using Core.Extensions;

namespace BLL
{
    public interface IProductService
    {
        Product GetById(int id);
        Product Create(Product product);
        void Update(Product product);
        void Delete(int id);

        void AddRating(Product product, int star, string userHostAddress);

        IEnumerable<Product> GetProductsByCategory(Category category, bool active = true);

        IEnumerable<Product> SearchProductsByKeyword(string keyword, bool orderByAccuracy = false, bool active = true);
    }
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly ITagRepository tagRepository;
        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, ITagRepository tagRepository)
        {
            var context = unitOfWork.Context;
            productRepository.Context = context;
            tagRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.tagRepository = tagRepository;
        }

        public Product GetById(int id)
        {
            return productRepository.GetById(id);
        }
        public Product Create(Product product)
        {
            productRepository.Insert(product);
            Save();

            if (product.Id > 0) return product;
            return null;
        }
        public void Update(Product product)
        {
            productRepository.Update(product);
            Save();
        }
        public void Delete(int id)
        {
            var product = GetById(id);

            productRepository.Delete(product);
            Save();

        }

        public void AddRating(Product product, int star, string userHostAddress)
        {
            product.Ratings.Add(new Rating { Star = star, UserHostAddress = userHostAddress });
            Save();
        }

        public IEnumerable<Product> GetProductsByCategory(Category category, bool active = true)
        {
            return category.Products.Where(p => p.Active == active).ToList();

        }

        public IEnumerable<Product> SearchProductsByKeyword(string keyword, bool orderByAccuracy = false, bool active = true)
        {
            Tag tag = tagRepository.Get(t => t.Name == keyword);
            if (tag != null) return GetProductsByTag(tag, keyword, active);

            var tagList = tagRepository.GetMany(t => t.Name.Contains(keyword));
            if ((tagList.IsNullOrEmpty()))
            { 
                return productRepository.GetMany(c => c.Active == active && c.Name.Contains(keyword));
            }

            return GetProductsByTagList(tagList, keyword, active);
        }

        IEnumerable<Product> GetProductsByTag(Tag tag, string keyword = "", bool active = true)
        {
            var  products= tag.Products.Where(t => t.Active == active).ToList();
            if (String.IsNullOrEmpty(keyword)) return products;

            return OrderProductsByAccuracy(products, keyword);
        }
        IEnumerable<Product> GetProductsByTagList(IEnumerable<Tag> tags, string keyword = "", bool active = true)
        {
            var productList = new List<Product>();
            foreach (var tag in tags)
            {
                foreach (var p in tag.Products)
                {
                    if(p.Active && (!productList.Contains(p)))
                    {
                        productList.Add(p);
                    }
                   
                }
            }

            if (String.IsNullOrEmpty(keyword)) return productList;

            return OrderProductsByAccuracy(productList, keyword);
        }
        IEnumerable<Product> OrderProductsByAccuracy(ICollection<Product> products, string keyword)
        {
            var targetProducts = from p in products
                                 let match = (p.Name.Contains(keyword) ? 1 : 0)                              
                                 orderby match descending, p.Name
                                 select p;

            return targetProducts.ToList(); ;
        }

        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
