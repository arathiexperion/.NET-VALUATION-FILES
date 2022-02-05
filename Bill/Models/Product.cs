using System;
using System.Collections.Generic;

namespace Bill.Models
{
    public partial class Product
    {
        public int ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int? Qty { get; set; }
        public int? RatePerUnit { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
