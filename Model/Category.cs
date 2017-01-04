using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Extensions;

namespace Model
{
    public abstract class Category
    {
        public int Id { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }
      
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<CategoryPhoto> Photoes { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public CategoryPhoto TopPhoto 
        {
            get
            {
                if (Photoes.IsNullOrEmpty()) return null;
                return Photoes.FirstOrDefault();
            }
            
        }

    }

    public class ProductCategory : Category
    {
        public string Prefix { get; set; }
    }

    public class EthnicGroup : Category
    { 
    
    }
}
