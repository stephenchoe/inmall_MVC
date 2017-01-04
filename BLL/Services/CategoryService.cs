using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using DAL;
using DAL.Infrastructure;

namespace BLL
{
    public interface ICategoryService
    {
        Category GetById(int id);
       
        void Delete(int id);

        IEnumerable<Category> GetTopCategories(bool active = true);
        IEnumerable<Category> GetSubCategories(int parentId, bool active = true);
        IEnumerable<Category> GetRandonTopCategories(int count);

        Category Create(Category category);
        void Update(Category category);

        Category GetParent(Category category);

    }
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly ISupplierRepository supplierRepository;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IProductCategoryRepository productCategoryRepository, ISupplierRepository supplierRepository)
        {
            var context = unitOfWork.Context;
            categoryRepository.Context = context;
            productCategoryRepository.Context = context;
            supplierRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.categoryRepository = categoryRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.supplierRepository = supplierRepository;
        }

        public Category GetById(int id)
        {
            return categoryRepository.GetById(id);
        }
        public Category Create(Category category)
        {
            categoryRepository.Insert(category);
            Save();

            if (category.Id > 0) return category;
            return null;
        }
        public void Update(Category category)
        {
            categoryRepository.Update(category);
            Save();
        }
        public void Delete(int id)
        {
            var category = GetById(id);

            categoryRepository.Delete(category);
            Save();

        }
        public IEnumerable<Category> GetTopCategories(bool active = true)
        {
            return categoryRepository.GetMany(c => c.Parent == 0 && c.Active == active).OrderByDescending(c => c.DisplayOrder);
        }

        public IEnumerable<Category> GetSubCategories(int parentId, bool active = true)
        {
            return categoryRepository.GetMany(c => c.Parent == parentId && c.Active == active).OrderByDescending(c => c.DisplayOrder);
        }
        public IEnumerable<Category> GetRandonTopCategories(int count)
        {
            var allTops = GetTopCategories();
            var randomTops= allTops.OrderBy(x => Guid.NewGuid()).Take(count);

            return randomTops;
        }
        public Category GetParent(Category category)
        {
            if (category.Parent < 1) return null;
            return GetById(category.Parent);
        }

        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
