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
    public class RoleLogic
    {
        RoleDataAccess roleDataAccess = new RoleDataAccess();
        // <summary>
        /// 分页展示角色
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MyRoles> GetRolesPageList(int pageIndex, int pageSize, Expression<Func<MyRoles,string >> sort,Expression<Func<MyRoles, bool>> predicate,out int totalCount)
        {
            return DataRepository.PageList<MyRoles,string >(pageIndex, pageSize, sort, predicate, out totalCount);
        }
        public int CreateRole(MyRoles role)
        {
            return DataRepository.Add<MyRoles>(role);
        }
    }
}
