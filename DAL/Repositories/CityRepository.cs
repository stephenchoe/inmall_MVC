using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface ICityRepository : IRepository<City>
    { 
        
    }
    public class CityRepository :RepositoryBase<City>, ICityRepository
    {
        public CityRepository(IDatabaseFactory dbFactory)
            :base(dbFactory)
        { 
           
        }
    }
}
