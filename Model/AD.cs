using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class AD
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public byte[] ImageFile { get; set; }
        public bool NewWindow { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
        public int Clicks { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
