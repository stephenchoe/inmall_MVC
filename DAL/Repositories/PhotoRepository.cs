using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface IProductPhotoRepository : IRepository<ProductPhoto>
    {

    }
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }

    public interface ICategoryPhotoRepository : IRepository<CategoryPhoto>
    {

    }
    public class CategoryPhotoRepository : RepositoryBase<CategoryPhoto>, ICategoryPhotoRepository
    {
        public CategoryPhotoRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
