using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MyRoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public List<Function> Function { get; set; }
        public static MyRoles ViewModelToEntity(MyRoleViewModel viewmdoel)
        {
            MyRoles role = new MyRoles();
            role.Id = viewmdoel.Id;
            role.Name = viewmdoel.Name;
            role.Status = viewmdoel.Status;
            role.Description = viewmdoel.Description;
            return role;
        }
    }
}
