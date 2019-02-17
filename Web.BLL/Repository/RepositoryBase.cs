using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL;
using Web.Models.Entities;

namespace Web.BLL.Repository
{
    public abstract class RepositoryBase<T,TId>:IDisposable where T :BaseEntity<TId>
    {

        protected internal static MyContext db;
        private static DbSet<T> DbObject;

        public RepositoryBase()
        {
            db = db ?? new MyContext();
            if (IsDisposed) db = new MyContext();
            DbObject = db.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbObject.ToList();
        }
        public List<T> GetAll(Func<T,bool> predicate)
        {
            return DbObject.Where(predicate).ToList();
        }

        public T GetById(params object[] keys)
        {           
            return DbObject.Find(keys);
        }

        public int Insert(T entity)
        {
            DbObject.Add(entity);
            return db.SaveChanges();
        }

        public int Delete(T entity)
        {
            DbObject.Remove(entity);
            return db.SaveChanges();
        }

        public void DeleteForMark(T entity)
        {
            DbObject.Remove(entity);
        }

        public int Update()
        {
            return db.SaveChanges();
        }
        public void Update(T entity)
        {
            DbObject.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            entity.UpdateDate = DateTime.Now;
            db.SaveChanges();
        }

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }
    }
}
