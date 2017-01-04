using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface IDistrictRepository : IRepository<District>
    {

    }
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
