using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Valid { get; set; }

        public string ReceiverName { get; set; }
        public Address ReceiverAddress { get; set; }

        //運費
        public int Freight { get; set; }

        //貨款
        public int ProductPrice
        {
            get
            {
                return this.Details.Select(d => d.Amount).Sum();
            }
        }

        //金額總計= 貨款 + 運費
        public int TotalAmount 
        {
            get
            {
                return ProductPrice + Freight;
            }
        }

        //判斷是否出貨成功
      
        public bool ShippingSuccess
        {
            get 
            {
                var record = this.ShippingRecords.Where(r => r.Success).FirstOrDefault();
                return record != null ? true : false;
            }
        }

        public virtual ICollection<OrderDetail> Details { get; set; }

        //出貨紀錄
        public virtual ICollection<ShippingRecord> ShippingRecords { get; set; }

        //匯款紀錄
        public virtual ICollection<PaymentRecord> PaymentRecords { get; set; }
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
        //單價
        public int Price { get; set; }

        //金額=數量x單價
        public int Amount 
        {

            get { return this.Price * Quantity; }
            
        }



        public virtual Order Order { get; set; }

    }

   



}
