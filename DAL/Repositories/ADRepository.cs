using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface IADRepository : IRepository<AD>
    {

    }
    public class ADRepository : RepositoryBase<AD>, IADRepository
    {
        public ADRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
