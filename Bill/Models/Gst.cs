using System;
using System.Collections.Generic;

namespace Bill.Models
{
    public partial class Gst
    {
        public int GstId { get; set; }
        public int? CategoryId { get; set; }
        public double? GstValue { get; set; }

        public virtual Category Category { get; set; }
    }
}
