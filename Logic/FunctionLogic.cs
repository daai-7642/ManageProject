﻿using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Logic
{
    /// <summary>
    /// 功能
    /// </summary>
    public class FunctionLogic
    {
        FunctionDataAccess functionDataAccess = new FunctionDataAccess();
        /// <summary>
        /// 获取功能
        /// </summary>
        /// <returns></returns>
        public List<FunctionViewModel> GetFunctionList()
        {
            return functionDataAccess.GetFunctionList();
        }
        /// <summary>
        /// 获取所有的分组，功能
        /// </summary>
        /// <returns></returns>
        public FunctionGroupDataViewModel GetFunctionGroupList()
        {
            return functionDataAccess.GetFunctionGroupList();
        }
    }
}
