using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Supplier : Category
    {

        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Description Description { get; set; }
    }

   
}
