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
            PropertyInfo[] funField = typeof(Function).GetProperties();
            PropertyInfo[] vfunField = typeof(FunctionViewModel).GetProperties();
            
            for (int i = 0; i < funField.Length; i++)
            {
                PropertyInfo t = vfunField.Where(a => a.Name == funField[i].Name).FirstOrDefault();
                if (t != null)
                {
                    for (int j = 0; j < vfunField.Length; j++)
                    {
                        t.SetValue(viewModel, vfunField[j], null);
                    }
                }
            }
            return viewModel;
        }
        public Function ViewModelToEntity(FunctionViewModel viewModel)
        {
            Function entity = new Function();
            PropertyInfo[] funField = typeof(Function).GetProperties();
            PropertyInfo[] vfunField = typeof(FunctionViewModel).GetProperties();

            for (int i = 0; i < vfunField.Length; i++)
            {
                PropertyInfo t = funField.Where(a => a.Name == vfunField[i].Name).FirstOrDefault();
                if (t != null)
                {
                    for (int j = 0; j < funField.Length; j++)
                    {
                        t.SetValue(entity, funField[j], null);
                    }
                }
            }
            return entity;
        }

    }
}
