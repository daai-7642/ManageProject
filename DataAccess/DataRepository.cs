using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess
{
    public static class DataRepository
    {
        public static List<T> PageList<T,S>(int pageIndex, int pageSize, Expression<Func<T,S>> sort, Expression<Func<T, bool>> predicate, out int totalCount) where T : class
        {

            IQueryable<T> data = null;
            if (predicate != null)
            {
                data = EFContextFactory.GetCurrentDbContext().Set<T>().OrderBy(sort).Where(predicate);
            }
            else
            {
                data= EFContextFactory.GetCurrentDbContext().Set<T>().OrderBy(sort);
            }
            
            totalCount = data.Count();
            return data.Skip((pageIndex - 1) * 10).Take(pageSize).ToList();
        }
    }
}
