using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            viewModel.FunctionID = entity.FunctionID;
            viewModel.FunctionName = entity.FunctionName;
            viewModel.GroupID = entity.GroupID;
            viewModel.PageURL = entity.PageURL;
            viewModel.OrderNo = entity.OrderNo;
            viewModel.Description = entity.Description;
            viewModel.Status = entity.Status;
            return viewModel;
        }
        public Function ViewModelToEntity(FunctionViewModel viewModel)
        {
            Function entity = new Function();
            entity.FunctionID = viewModel.FunctionID;
            entity.FunctionName = viewModel.FunctionName;
            entity.GroupID = viewModel.GroupID;
            entity.PageURL = viewModel.PageURL;
            entity.OrderNo = viewModel.OrderNo;
            entity.Description = viewModel.Description;
            entity.Status = viewModel.Status;
            return entity;
        }

    }
}
