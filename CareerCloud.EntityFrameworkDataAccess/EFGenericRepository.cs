using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        protected CareerCloudContext careerCloudContext { get; set; }
        public EFGenericRepository()
        {
            this.careerCloudContext = new CareerCloudContext();
        }
        public void Add(params T[] items)
        {
            foreach(var item in items) 
            {
                careerCloudContext.Entry(item).State = EntityState.Added;
            }
            careerCloudContext.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = careerCloudContext.Set<T>();
            foreach (Expression<Func<T, object>> navProperty in navigationProperties)
            {
                query = query.Include<T, object>(navProperty);
            }
            return query.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = careerCloudContext.Set<T>();
            foreach (Expression<Func<T, object>> navProperty in navigationProperties)
            {
                query = query.Include<T, object>(navProperty);
            }
            return query.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = careerCloudContext.Set<T>();
            foreach(Expression<Func<T, object>> navProperty in navigationProperties) 
            {
                query = query.Include<T, object>(navProperty);
            }
            return query.FirstOrDefault(where);
        }

        public void Remove(params T[] items)
        {
            foreach (var item in items)
            {
                careerCloudContext.Set<T>().Remove(item);
            }
            careerCloudContext.SaveChanges();
        }

        public void Update(params T[] items)
        {
            foreach (var item in items)
            {
                careerCloudContext.Set<T>().Update(item);
            }
            careerCloudContext.SaveChanges();
        }
    }
}
