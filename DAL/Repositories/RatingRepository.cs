using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface IRatingRepository : IRepository<Rating>
    {

    }
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
