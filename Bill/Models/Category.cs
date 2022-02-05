using System;
using System.Collections.Generic;

namespace Bill.Models
{
    public partial class Category
    {
        public Category()
        {
            Gst = new HashSet<Gst>();
            Product = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Gst> Gst { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
