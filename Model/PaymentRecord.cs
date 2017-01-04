using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }

        //匯款人帳號
        public string FromAccount { get; set; }

        //匯款金額
        public int Amount { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
    }
}
