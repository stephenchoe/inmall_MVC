using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface ICartItemRepository : IRepository<CartItem>
    {

    }
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
