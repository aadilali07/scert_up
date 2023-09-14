using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scert_upMVC.Models
{
    public class manage_news
    {
        public string id { get; set; }
        public string heading { get; set; }
        public string desc { get; set; }
        public string date { get; set; }
        public string filepath { get; set; }
        public string fileName { get; set; }
        public HttpPostedFileBase fileupload1 { get; set; }
    }

    public class manage_photoGallery
    {
        public string id { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string File_Name { get; set; }
        public HttpPostedFileBase fileupload1 { get; set; }
    }

    public class manage_Video
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public HttpPostedFileBase fileupload1 { get; set; }
    }

    public class Units_Models
    {
        public string Id { get; set; }
        public string District { get; set; }
        public string Inst_Name { get; set; }
        public string Address { get; set; }
        public string InchargeName { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }

    public class PvtCollege_Models
    {
        public string Id { get; set; }
        public string District { get; set; }
        public string Inst_Name { get; set; }
        public string Seats { get; set; }

    }
}