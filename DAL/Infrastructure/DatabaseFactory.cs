using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        InmallContext GetInmallContext();
    }
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private InmallContext _context;
        public InmallContext GetInmallContext()
        {
            return _context ?? (_context = new InmallContext());
        }

        protected override void DisposeCore()
        {
            if (_context != null) _context.Dispose();
        }
    }
}
