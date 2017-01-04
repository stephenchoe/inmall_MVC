using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using DAL.Infrastructure;
using Model;

namespace BLL
{
    public interface IAddressService
    {
        IEnumerable<City> GetAllCities();
        City GetCityByName(string name);

        IEnumerable<District> GetDistricts(int city);
        District GetDistrictByName(string name);

        string GetZipCode(int districtId);

    }
    public class AddressService : IAddressService
    {

        private readonly ICityRepository cityRepository;
        private readonly IDistrictRepository districtRepository;

        public AddressService(ICityRepository cityRepository, IDistrictRepository districtRepository)
        {
            this.cityRepository = cityRepository;
            this.districtRepository = districtRepository;
        }

        public IEnumerable<City> GetAllCities()
        {
            return this.cityRepository.GetAll();
        }

        public City GetCityByName(string name)
        {
            return this.cityRepository.Get(c => c.Name == name);
        }

        public IEnumerable<District> GetDistricts(int city)
        {
            return this.districtRepository.GetMany(d => d.CityId == city);
        }
        public District GetDistrictByName(string name)
        {
            return this.districtRepository.Get(d => d.Name == name);
        }

        public string GetZipCode(int districtId)
        {
            var district = this.districtRepository.GetById(districtId);

            if (district == null) return "";
            return district.ZipCode;
        }

    }
}
