using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.viewmodel
{
    public class productgst
    {
        public int  product_code { get; set; }
        public int qty { get; set; }
        public int rate_per_unit { get; set; }
        public float gst_value { get; set; }
        public string product_description { get; set; }

        public int category_id { get; set; }

        public double Net_Rate { get; set; }




    }
}
