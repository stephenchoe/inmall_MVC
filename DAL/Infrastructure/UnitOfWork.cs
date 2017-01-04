using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public interface IUnitOfWork
    {
        InmallContext Context { get; set; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private InmallContext _context;

        public InmallContext Context
        {
            get { return _context ?? (_context = _databaseFactory.GetInmallContext()); }

            set { _context = value; }
        }

        public UnitOfWork(IDatabaseFactory dbFactory)
        {
            this._databaseFactory = dbFactory;
        }
       
        public void Commit()
        {
            Context.Commit();
        }
    }

   
}
