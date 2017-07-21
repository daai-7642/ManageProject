using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class FunctionViewModel
    {
        /// <summary>
        /// 功能id
        /// </summary>
        public int FunctionID { get; set; }
        /// <summary>
        /// 功能组id
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 页面url
        /// </summary>
        public string PageURL { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        public FunctionViewModel EntityToViewModel(Function entity)
        {
            FunctionViewModel viewModel = new FunctionViewModel();
            //viewModel.FunctionID
            return viewModel;
        }
    }
}
