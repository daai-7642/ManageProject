using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogLogic
    {
        LogDataAccess logDataAccess = new LogDataAccess();
        /// <summary>
        /// 分页日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MyLogger> GetLogsPageList(int pageIndex, int pageSize, Expression<Func<MyLogger, DateTime>> sort, bool isAsc, Expression<Func<MyLogger, bool>> predicate, out int totalCount)
        {
            return logDataAccess.GetLogsPageList(pageIndex,pageSize,sort,isAsc,predicate,out totalCount);
        }
    }
}
