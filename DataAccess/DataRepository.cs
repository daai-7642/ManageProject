using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess
{
    public static class DataRepository
    {
        private static DbContext DB
        {
            get
            {
                return EFContextFactory.GetCurrentDbContext();
            }
        }
        public static List<T> PageList<T,S>(int pageIndex, int pageSize, Expression<Func<T,S>> sort, Expression<Func<T, bool>> predicate, out int totalCount) where T : class
        {

            IQueryable<T> data = null;
            if (predicate != null)
            {
                data = DB.Set<T>().OrderBy(sort).Where(predicate);
            }
            else
            {
                data= DB.Set<T>().OrderBy(sort);
            }
            
            totalCount = data.Count();
            return data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        public static int Add<T>(T t) where T: class
        {
            try
            {
                DB.Set<T>().Add(t);
                return DB.SaveChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
