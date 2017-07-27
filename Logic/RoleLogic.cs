using DataAccess;
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
        public List<MyRoles> GetRolesPageList(int pageIndex, int pageSize, Expression<Func<MyRoles,string >> sort,bool isAsc,Expression<Func<MyRoles, bool>> predicate,out int totalCount)
        {
            return DataRepository.PageList<MyRoles,string >(pageIndex, pageSize, sort, isAsc, predicate, out totalCount);
        }
        public int CreateRole(MyRoles role)
        {
            return DataRepository.Add<MyRoles>(role);
        }
        public int CreateRole(MyRoles role,int[] function)
        {
            return roleDataAccess.CreateRole(role,function);
        }
        public int UpdateRoleStatus(MyRoles role)
        {
            List<string> fileds = new List<string>();
            fileds.Add("Status");
            return DataRepository.UpdateEntityFields<MyRoles>(role, fileds);
        }
        public MyRoleViewModel Find(string id)
        {
            MyRoleViewModel roleView = new MyRoleViewModel();
            var role= DataRepository.DB.Set<MyRoles>().Find(id);
            roleView.Id = role.Id;
            roleView.Name = role.Name;
            roleView.Description = role.Description;
            List<Function>  data = (from f in DataRepository.DB.Set<Function>()
                        join  d in DataRepository.DB.Set<RoleFunction>()
                        on f.FunctionID equals d.FunctionID
                        where d.RoleID == id
                        select f).ToList();
            roleView.Function = data;
            return roleView;
        }
        public int EditRole(MyRoles role, int[] function)
        {
            using (var scope = EFContextFactory.GetCurrentDbContext().Database.BeginTransaction())
            {
                try
                {
                    DataRepository.DB.Database.ExecuteSqlCommand("delete from rolefunction where roleid='" + role.Id + "'");
                    foreach (var item in function)
                    {
                        DataRepository.DB.Set<RoleFunction>().Add(new RoleFunction
                        {
                            RoleID = role.Id,
                            FunctionID = item,
                        });
                    }
                    List<string> fields = new List<string>();
                    fields.Add("Description");
                    fields.Add("Name");
                    DataRepository.UpdateSetEntityFields<MyRoles>(role,fields);
                    DataRepository.DB.SaveChanges();
                    scope.Commit();
                    LogHelper.WriteLog("修改角色及权限", "1", function + role.ObjectToJson());
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
