using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scert_upMVC.Models
{
    public class common_response
    {
        public string message { get; set; }
        public string parameter { get; set; }
        public Boolean success { get; set; }
        public string emp_name { get; set; }
    }
}