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
        /// <summary>
        /// 添加省到db
        /// </summary>
        /// <param name="province"></param>
        public void AddDbProvince(Address_Province province)
        {
            Address_Province result = Utility.EFContextFactory.GetCurrentDbContext().Set<Address_Province>().Find(province.ProvinceCode);
            if (result == null)
            {
                DataRepository.DB.Set<Address_Province>().Add(province);
            }
        }
        /// <summary>
        /// 添加市到db
        /// </summary>
        /// <param name="city"></param>
        public void AddDbCity(Address_City city)
        {
            Address_City result = Utility.EFContextFactory.GetCurrentDbContext().Set<Address_City>().Find(city.CityCode);
            if (result == null)
            {
                DataRepository.DB.Set<Address_City>().Add(city);
            }
        }
        /// <summary>
        /// 添加区到db
        /// </summary>
        /// <param name="county"></param>
        public void AddDbCounty(Address_County county)
        {
            Address_County result = Utility.EFContextFactory.GetCurrentDbContext().Set<Address_County>().Find(county.CountyCode);
            if (result == null)
            {
                DataRepository.DB.Set<Address_County>().Add(county);
            }
        }
        /// <summary>
        /// 添加街道到db
        /// </summary>
        /// <param name="town"></param>
        public void AddDbTown(Address_Town town)
        {
            Address_Town result = Utility.EFContextFactory.GetCurrentDbContext().Set<Address_Town>().Find(town.TownCode);
            if (result == null)
            {
                DataRepository.DB.Set<Address_Town>().Add(town);
            }
        }
        /// <summary>
        /// 添加委员会到db
        /// </summary>
        /// <param name="village"></param>
        public void AddDbVillage(Address_Village village)
        {
            Address_Village result = Utility.EFContextFactory.GetCurrentDbContext().Set<Address_Village>().Find(village.VillageCode);
            if (result == null)
            {
                DataRepository.DB.Set<Address_Village>().Add(village);
            }
        }
        public int  SaveDbAddress()
        {
            return DataRepository.DB.SaveChanges();
        }
    }
}
