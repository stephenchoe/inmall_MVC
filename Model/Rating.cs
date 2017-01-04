using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserHostAddress { get; set; }
        public int Star { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
