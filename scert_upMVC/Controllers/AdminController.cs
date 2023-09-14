using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using scert_upMVC.Models;

namespace scert_upMVC.Controllers
{
    public class AdminController : Controller
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
        scert_upMVC.Models.DataServices db = new scert_upMVC.Models.DataServices();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        #region checklogin

        [HttpPost]
        public JsonResult admin_Login(string username, string password, string url)
        {


            Models.common_response Response = db.login(username, password);

            //if (Response.parameter.ToString() != "admin")
            //{
            //    url = null;
            //}

            if (Response.success == true)
            {
                if (url != null && url.ToString() != "")
                {
                    Response.message = (HttpUtility.HtmlDecode(url));
                }
                else
                {
                    if (Response.parameter.ToString() == "admin")
                    {
                        Response.message = "/admin/dashboard";
                    }
                    else
                    {
                        Response.message = "/admin/employeeDashboard";
                    }

                }
                Session["adminname"] = Response.parameter.ToString();
                Session["username"] = Response.emp_name.ToString();
                Session["emp_name"] = Response.emp_name.ToString();

            }
            return Json(Response);
        }

        #endregion

        #region logout

        public ActionResult logout()
        {
            if (Session["adminname"] != null)
            {
                Session["adminname"] = null;
                Session["data_con"] = null;
            }

            return RedirectToAction("login");
        }
        #endregion

        #region Manage News
        public ActionResult add_news(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_news news = new manage_news();


            if (id != null && id != "")
            {
                news = db.getnewsbyId(id);
            }

            return View(news);
        }

        [HttpPost]
        public ActionResult news_insert(manage_news news)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                news.filepath = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_news(news);

            if (dd == true)
            {
                TempData["Message"] = "News Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_news");
        }

        public ActionResult news_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_news> news = new List<manage_news>();

            news = db.getnews();


            return View(news);

        }

        public ActionResult delet_news(string id)
        {

            db.delet_news(id);


            return RedirectToAction("news_view");
        }

        #endregion

        #region Manage compitition
        public ActionResult add_compitition(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_compitition compitition = new manage_compitition();


            if (id != null && id != "")
            {
                compitition = db.getcompititionbyId(id);
            }

            return View(compitition);
        }

        [HttpPost]
        public ActionResult compitition_insert(manage_compitition compitition)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                compitition.Comp_File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_compitition(compitition);

            if (dd == true)
            {
                TempData["Message"] = "compitition Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_compitition");
        }

        public ActionResult compitition_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_compitition> compitition = new List<manage_compitition>();

            compitition = db.getcompitition();


            return View(compitition);

        }

        public ActionResult delet_compitition(string id)
        {

            db.delet_compitition(id);


            return RedirectToAction("compitition_view");
        }

        #endregion

        #region Manage curriculum
        public ActionResult add_curriculum(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_curriculum curriculum = new manage_curriculum();


            if (id != null && id != "")
            {
                curriculum = db.getcurriculumbyId(id);
            }

            return View(curriculum);
        }

        [HttpPost]
        public ActionResult curriculum_insert(manage_curriculum curriculum)
        {

            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                curriculum.File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_curriculum(curriculum);

            if (dd == true)
            {
                TempData["Message"] = "curriculum Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_curriculum");
        }

        public ActionResult curriculum_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_curriculum> curriculum = new List<manage_curriculum>();

            curriculum = db.getcurriculum();


            return View(curriculum);

        }

        public ActionResult delet_curriculum(string id)
        {

            db.delet_curriculum(id);


            return RedirectToAction("curriculum_view");
        }

        #endregion

        #region Manage DelContent
        public ActionResult add_DelContent(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_curriculum DelContent = new manage_curriculum();


            if (id != null && id != "")
            {
                DelContent = db.getDelContentbyId(id);
            }

            return View(DelContent);
        }

        [HttpPost]
        public ActionResult DelContent_insert(manage_curriculum DelContent)
        {

            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                DelContent.File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_DelContent(DelContent);

            if (dd == true)
            {
                TempData["Message"] = "DelContent Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_DelContent");
        }

        public ActionResult DelContent_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_curriculum> DelContent = new List<manage_curriculum>();

            DelContent = db.getDelContent();


            return View(DelContent);

        }

        public ActionResult delet_DelContent(string id)
        {

            db.delet_DelContent(id);


            return RedirectToAction("DelContent_view");
        }

        #endregion

        #region Manage faculty
        public ActionResult add_faculty(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_faculty faculty = new manage_faculty();


            if (id != null && id != "")
            {
                faculty = db.getfacultybyId(id);
            }

            return View(faculty);
        }

        [HttpPost]
        public ActionResult faculty_insert(manage_faculty faculty)
        {
            var dd = db.insert_faculty(faculty);

            if (dd == true)
            {
                TempData["Message"] = "faculty Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_faculty");
        }

        public ActionResult faculty_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_faculty> faculty = new List<manage_faculty>();

            faculty = db.getfaculty();


            return View(faculty);

        }

        public ActionResult delet_faculty(string id)
        {

            db.delet_faculty(id);


            return RedirectToAction("faculty_view");
        }

        #endregion

        #region Manage Go
        public ActionResult add_Go(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_Go Go = new manage_Go();


            if (id != null && id != "")
            {
                Go = db.getGobyId(id);
            }

            return View(Go);
        }

        [HttpPost]
        public ActionResult Go_insert(manage_Go Go)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                Go.File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_Go(Go);

            if (dd == true)
            {
                TempData["Message"] = "Go Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_Go");
        }

        public ActionResult Go_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_Go> Go = new List<manage_Go>();

            Go = db.getGo();


            return View(Go);

        }

        public ActionResult delet_Go(string id)
        {

            db.delet_Go(id);


            return RedirectToAction("Go_view");
        }

        #endregion

        #region Manage photoGallery
        public ActionResult add_photoGallery(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_photoGallery photoGallery = new manage_photoGallery();


            if (id != null && id != "")
            {
                photoGallery = db.getphotoGallerybyId(id);
            }

            return View(photoGallery);
        }

        [HttpPost]
        public ActionResult photoGallery_insert(manage_photoGallery photoGallery)
        {
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                photoGallery.ImagePath = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_photoGallery(photoGallery);

            if (dd == true)
            {
                TempData["Message"] = "photoGallery Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_photoGallery");
        }

        public ActionResult photoGallery_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_photoGallery> photoGallery = new List<manage_photoGallery>();

            photoGallery = db.getphotoGallery();


            return View(photoGallery);

        }

        public ActionResult delet_photoGallery(string id)
        {

            db.delet_photoGallery(id);


            return RedirectToAction("photoGallery_view");
        }

        #endregion

        #region Manage Video
        public ActionResult add_Video(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_Video Video = new manage_Video();


            if (id != null && id != "")
            {
                Video = db.getVideobyId(id);
            }

            return View(Video);
        }

        [HttpPost]
        public ActionResult Video_insert(manage_Video Video)
        {
            

            var dd = db.insert_Video(Video);

            if (dd == true)
            {
                TempData["Message"] = "Video Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_Video");
        }

        public ActionResult Video_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_Video> Video = new List<manage_Video>();

            Video = db.getVideo();


            return View(Video);

        }

        public ActionResult delet_Video(string id)
        {

            db.delet_Video(id);


            return RedirectToAction("Video_view");
        }

        #endregion


        #region units

        public ActionResult AddUnits(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            Units_Models unit = new Units_Models();

            if (id != null && id != "")
            {
                unit = db.GetUnitsRecordById(id).FirstOrDefault();
            }

            return View(unit);
        }

        [HttpPost]
        public ActionResult AddUnits(Units_Models obj)
        {
            var dd = db.AddUnits(obj);

            if (dd == true)
            {
                TempData["Message"] = "Go Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }

            return RedirectToAction("AddUnits");

        }

        public ActionResult Unitslist()
        {
            List<Units_Models> list = new List<Units_Models>();

            list = db.getAllUnitsRecord();

            return View(list);
        }

        [HttpPost]
        public ActionResult DeleteUnitsRecord(Units_Models units)
        {
            db.DeleteUnitsRecord(units);
            TempData["deleterecord"] = "<script>alert('deleted succussfully')</script>";
            return RedirectToAction("Unitslist");
        }

        #endregion

        #region manage private college

        public ActionResult AddPvtCollege(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            PvtCollege_Models unit = new PvtCollege_Models();

            if (id != null && id != "")
            {
                unit = db.GetPvtCollegeById(id).FirstOrDefault();
            }

            return View(unit);

        }

        [HttpPost]
        public ActionResult AddPvtCollege(PvtCollege_Models obj)
        {

            var dd = db.AddPvtCollege(obj);
            if (dd == true)
            {
                TempData["Message"] = "Go Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }

            return RedirectToAction("PvtCollegelist");

        }

        public ActionResult PvtCollegelist()
        {
            List<PvtCollege_Models> list = new List<PvtCollege_Models>();

            list = db.GetPvtCollegeList();

            return View(list);
        }


        public ActionResult DeletePvtCollege(string id)
        {
            db.DeletePvtCollegeRecordById(id);

            return RedirectToAction("PvtCollegelist");
        }


        #endregion

        #region Manage Ebook
        public ActionResult add_ebook(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_ebook ebook = new manage_ebook();


            if (id != null && id != "")
            {
                ebook = db.getebookbyId(id);
            }

            return View(ebook);
        }

        [HttpPost]
        public ActionResult ebook_insert(manage_ebook ebook)
        {

            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                ebook.File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_ebook(ebook);

            if (dd == true)
            {
                TempData["Message"] = "ebook Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_ebook");
        }

        public ActionResult ebook_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_ebook> ebook = new List<manage_ebook>();

            ebook = db.getebook();


            return View(ebook);

        }

        public ActionResult delete_ebook(string id)
        {

            db.delete_ebook(id);


            return RedirectToAction("ebook_view");
        }

        #endregion

        #region manage Dietlist
        public ActionResult add_Dietlist(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_dietlist dietlist = new manage_dietlist();


            if (id != null && id != "")
            {
                dietlist = db.getdietlistbyId(id);
            }

            return View(dietlist);
        }

        [HttpPost]
        public ActionResult insert_Dietlist(manage_dietlist diet)
        {


            var dd = db.insert_dietlist(diet);

            if (dd == true)
            {
                TempData["Message"] = "dietlist Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_Dietlist");
        }

        public ActionResult Dietlist_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_dietlist> dietlist = new List<manage_dietlist>();

            dietlist = db.getAllDietList();


            return View(dietlist);

        }

        public ActionResult delet_DietList(string id)
        {

            db.DeleteDietListRecord(id);


            return RedirectToAction("Dietlist_view");
        }


        #endregion

        #region  manage OtherEduContent
        public ActionResult add_OtherEduContent(string id)
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            var userid = Session["userid"];
            manage_otherEduContent OtherEduContent = new manage_otherEduContent();


            if (id != null && id != "")
            {
                OtherEduContent = db.getOtherEduContentbyId(id);
            }

            return View(OtherEduContent);
        }

        [HttpPost]
        public ActionResult OtherEduContent_insert(manage_otherEduContent OtherEduContent)
        {

            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));


            HttpPostedFileBase file1 = Request.Files["fileupload1"];

            string uploadpayslipss1;

            if (file1 != null && file1.FileName.ToString() != "")
            {
                uploadpayslipss1 = DateTime.Now.ToString("ddMMyy") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                OtherEduContent.File_Path = "/tempimage/" + uploadpayslipss1;

            }

            var dd = db.insert_OtherEduContent(OtherEduContent);

            if (dd == true)
            {
                TempData["Message"] = "OtherEduContent Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }


            return RedirectToAction("add_OtherEduContent");
        }

        public ActionResult OtherEduContent_view()
        {
            Models.common_response Response = db.adminssioncheck("");
            if (Response.success == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            }

            List<manage_otherEduContent> OtherEduContent = new List<manage_otherEduContent>();

            OtherEduContent = db.getOtherEduContent();


            return View(OtherEduContent);

        }

        public ActionResult delete_OtherEduContent(string id)
        {
            db.delete_OtherEduContent(id);

            return RedirectToAction("OtherEduContent_view");
        }

        #endregion
    }
}