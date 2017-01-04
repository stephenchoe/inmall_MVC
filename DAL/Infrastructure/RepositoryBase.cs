using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;


namespace DAL.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);


        InmallContext Context { get; set; }
    }

    public abstract class RepositoryBase<T> where T : class
    {

        private IDbSet<T> _dbSet;
        private IDbSet<T> DbSet
        {
            get { return _dbSet ?? (_dbSet = Context.Set<T>()); }

        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        private InmallContext context;
        public InmallContext Context
        {
            get { return context ?? (context = DatabaseFactory.GetInmallContext()); }
            set { context = value; }
        }

        protected RepositoryBase(IDatabaseFactory dbFactory)
        {
            DatabaseFactory = dbFactory;
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);

        }
        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual T GetById(long id)
        {
            return DbSet.Find(id);
        }
        public virtual T GetById(string id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).ToList();
        }
        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).FirstOrDefault<T>();
        }
    }



}
