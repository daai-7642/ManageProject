using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public static class DBEntity
    {
        public static DbContext DB
        {
            get { return new SuperDBEntities(); }
        }
    }
}
