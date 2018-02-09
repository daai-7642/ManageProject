using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Linq.Expressions;

namespace DataAccess
{
     public class LogDataAccess
    {
        /// <summary>
        /// 分页日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MyLogger> GetLogsPageList(int pageIndex, int pageSize, Expression<Func<MyLogger, DateTime>> sort, bool isAsc, Expression<Func<MyLogger, bool>> predicate, out int totalCount)
        {
            return DataRepository.PageList<MyLogger, DateTime>(pageIndex, pageSize, sort, isAsc, predicate, out totalCount);
        }
    }
}
