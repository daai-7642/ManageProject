using Entity;
using FMCG.Utility.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Cache;
using ViewModel;

namespace DataAccess
{
    /// <summary>
    /// 功能
    /// </summary>
    public class FunctionDataAccess
    {
        /// <summary>
        /// 获取功能
        /// </summary>
        /// <returns></returns>
        public List<FunctionViewModel> GetFunctionList()
        {
            List<FunctionViewModel> list = new List<FunctionViewModel>();
            var db = new SuperDBEntities();

            var data = new SuperDBEntities().Function.ToList();
            foreach (var item in data)
            {
                list.Add(new FunctionViewModel().EntityToViewModel(item));
            }
            return list;
        }
        /// <summary>
        /// 获取所有的分组，功能
        /// </summary>
        /// <returns></returns>
        public FunctionGroupDataViewModel GetFunctionGroupList()
        {
            FunctionGroupDataViewModel Data = new FunctionGroupDataViewModel();
            var functionGroupData = EFContextFactory.GetCurrentDbContext().Set<FunctionGroup>().Where(a => a.State == 1);
            var functionData = EFContextFactory.GetCurrentDbContext().Set<Function>().Where(a => a.Status == 1);
            Data.FunctionGroup = functionGroupData.ToList();
            Data.Function = functionData.ToList();
            return Data;
        }
        /// <summary>
        /// 存储过程获取所有的权限
        /// </summary>
        /// <returns></returns>
        public List<FunctionGroupTree> GetFunctionAndGroupList()
        {

            object obj = RedisHelper.Get<List<FunctionGroupTree>>("GetFunctionAndGroupList");

            //object obj = CacheFactory.Cache.GetCache<List<FunctionGroupTree>>("GetFunctionAndGroupList");
            if (obj == null)
            {
                List<FunctionGroupTree> list = DataRepository.DB.Database.SqlQuery<FunctionGroupTree>("exec Pr_FunctionGroupGetList").ToList();
                //CacheFactory.Cache.SetCache<List<FunctionGroupTree>>("GetFunctionAndGroupList", list);
                RedisHelper.Set<List<FunctionGroupTree>>("GetFunctionAndGroupList",list);
                obj = list;
            }
            //CacheFactory.Cache.InitCache();

            return obj as List<FunctionGroupTree>;
        }
    }
}
