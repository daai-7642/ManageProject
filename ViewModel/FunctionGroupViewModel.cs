using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public  class FunctionGroupViewModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int ParentID { get; set; }
        public string GroupCode { get; set; }
        public int OrderNo { get; set; }
        public short State { get; set; }
        public string Description { get; set; }
        public List<FunctionViewModel> Function { get; set; }
    }

    public class FunctionGroupDataViewModel
    {
        public List<FunctionGroup> FunctionGroup { get; set; }
        public List<Function> Function { get; set; }
    }
    public class FunctionGroupTree
    {
        public int id { get; set; }
        public string name { get; set; }
        public int pId { get; set; }
        public string  open { get; set; }
    }

}
