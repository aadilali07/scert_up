using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scert_upMVC.Models
{
    public class manage_curriculum
    {

        public String id  { get; set; }
        public String Course  { get; set; }
        public String Subject { get; set; }
        public String File_Name  { get; set; }
        public String File_Path  { get; set; }
    }

    public class manage_Go
    {

        public String id { get; set; }
        public String GONo { get; set; }
        public String date { get; set; }
        public String Subject { get; set; }
        public String File_Name { get; set; }
        public String File_Path { get; set; }
    }

    public class manage_ebook
    {
        public String id { get; set; }
        public String Course { get; set; }
        public String Subject { get; set; }
        public String File_Name { get; set; }
        public String File_Path { get; set; }
    }

    public class manage_otherEduContent
    {
        public String id { get; set; }
        public String Course { get; set; }
        public String Subject { get; set; }
        public String File_Name { get; set; }
        public String File_Path { get; set; }
    }
    public class manage_dietlist
    {
        public string id { get; set; }
        public string Diet_Name { get; set; }
        public string Address { get; set; }
        public string InchargeName { get; set; }
        public string Designation { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }

}