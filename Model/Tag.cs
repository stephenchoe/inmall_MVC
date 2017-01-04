using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Clicks { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
