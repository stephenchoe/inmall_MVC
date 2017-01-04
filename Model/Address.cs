using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Address
    {
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TelCode { get; set; }
        public virtual ICollection<District> Districts { get; set; }

       
    }
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

    }
}
