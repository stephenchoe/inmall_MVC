using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Photo
    {
        public int Id { get; set; }
        public byte[] FileBytes { get; set; }
        public string Caption { get; set; }
        public bool Top { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class ProductPhoto : Photo
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

    public class CategoryPhoto : Photo
    {
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }


}
