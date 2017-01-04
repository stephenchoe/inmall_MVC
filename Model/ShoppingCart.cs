using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public int ProductId { get; set; }
     

        public virtual Product Product { get; set; }
       
    }
}
