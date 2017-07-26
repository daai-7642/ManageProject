using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class AddressLogic
    {
        public int AddAddress(Entity.AddressBase address)
        {
            AddressBase result= Utility.EFContextFactory.GetCurrentDbContext().Set<AddressBase>().Find(address.Code);
            if (result == null)
            {
                return DataRepository.Add<AddressBase>(address);
            }
            return -1;
        }
        public void AddDbAddress(Entity.AddressBase address)
        {
            AddressBase result = Utility.EFContextFactory.GetCurrentDbContext().Set<AddressBase>().Find(address.Code);
            if (result == null)
            {
                DataRepository.DB.Set<AddressBase>().Add(address);
            }
        }
        public int  SaveDbAddress()
        {
            return DataRepository.DB.SaveChanges();
        }
    }
}
