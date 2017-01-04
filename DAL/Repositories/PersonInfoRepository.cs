using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Infrastructure;
using Model;

namespace DAL
{
    public interface IPersonInfoRepository : IRepository<PersonInfo>
    {

    }
    public class PersonInfoRepository : RepositoryBase<PersonInfo>, IPersonInfoRepository
    {
        public PersonInfoRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
