using Entity;
using Log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace DataAccess
{
    public class RoleDataAccess
    {
        /// <summary>
        /// 分页展示角色
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<MyRoles> GetRolesPageList(int pageIndex, int pageSize, Expression<Func<MyRoles, string>> sort, bool isAsc, Expression<Func<MyRoles, bool>> predicate, out int totalCount)
        {
            return DataRepository.PageList<MyRoles, string>(pageIndex, pageSize, sort, isAsc, predicate, out totalCount);
        }
        public int CreateRole(MyRoles role, int[] function)
        {
            using (var scope = EFContextFactory.GetCurrentDbContext().Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in function)
                    {
                        DataRepository.Add<RoleFunction>(new RoleFunction()
                        {
                            RoleID = role.Id,
                            FunctionID = item,
                        });
                    }
                    DataRepository.Add<MyRoles>(role);
                    scope.Commit();
                    LogHelper.WriteLog("添加角色及权限","1"+role.ObjectToJson());
                    return 1;
                }
                catch (Exception ex)
                {
                    ErrorLogHelper.WriteLog(ex);
                    scope.Rollback();
                    return 0;
                }
            }

        }
    }
}
