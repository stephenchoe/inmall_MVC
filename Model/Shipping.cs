using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ShippingRecord
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        //出貨日期
        public DateTime ShippingDate { get; set; }

        public int ShippingCompanyId { get; set; }

        //出貨單號
        public string RecordNumber { get; set; }

        //貨運費
        public int ShippingFee { get; set; }



        //應收貨款
        public int Payment { get; set; }
        public bool Success { get; set; }


        public virtual Order Order { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }
    }


    public class ShippingCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<ShippingRecord> ShippingRecords { get; set; }
    }
   
}
