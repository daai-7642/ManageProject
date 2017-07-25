using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
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
            List < FunctionViewModel > list= new List<FunctionViewModel>();
            var db = new SuperDBEntities();
            db.Function.Add(new Function() {
                FunctionName="测试",
                PageURL="www.baidu.com",
                Description=""
            });
            db.FunctionGroup.Add(new FunctionGroup()
            {
                GroupName = "系统管理"
            });
            db.SaveChanges();
            var data=new SuperDBEntities().Function.ToList();
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
            var functionGroupData = EFContextFactory.GetCurrentDbContext().Set<FunctionGroup>().Where(a=>a.State==1);
            var functionData = EFContextFactory.GetCurrentDbContext().Set<Function>().Where(a=>a.Status==1);
            Data.FunctionGroup = functionGroupData.ToList();
            Data.Function = functionData.ToList();
            return Data;
        }
    }
}
