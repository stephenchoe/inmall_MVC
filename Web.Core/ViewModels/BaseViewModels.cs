using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Models
{
    public enum StatusAlertType
    {
        Success,
        Error,
        Warning
    }
    public class StatusAlert
    {
        
        public string Title { get; set; }
        public StatusAlertType Type { get; set; }
        public string StatusText { get; set; }
        public bool ShowConfirmButton { get; set; }
        public bool ShowCancelButton { get; set; }

        public string CallBack { get; set; }
        public string OKCallBack { get; set; }
        public string CancelCallBack { get; set; }
       
    }


    public class DeleteConfirm : BaseForm
    {
        public int Id { get; set; }
        public string ObjectId { get; set; }
        public string UserId { get; set; }
        public string Msg { get; set; }

    }


}
