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
    public class HomeController : Controller
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);
        scert_upMVC.Models.DataServices db = new scert_upMVC.Models.DataServices();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About_Us()
        {

            return View();
        }

        public ActionResult organization_structure()
        {

            return View();
        }


        public ActionResult Our_faculty()
        {
            List<manage_faculty> faculty = new List<manage_faculty>();

            faculty = db.getfaculty();

            return View(faculty);
        }

        #region teacher training
        public ActionResult pre_services()
        {

            return View();
        }

        public ActionResult In_services()
        {

            return View();
        }

        public ActionResult other_services()
        {

            return View();
        }

        #endregion

        public ActionResult Research_survey()
        {

            return View();
        }

        public ActionResult units()
        {
            List<Units_Models> list = new List<Units_Models>();

            list = db.getAllUnitsRecord();

            return View(list);
        }

        public ActionResult diets()
        {
            List<manage_dietlist> dietlist = new List<manage_dietlist>();

            dietlist = db.getAllDietList();


            return View(dietlist);
        }

        public ActionResult pvt_institute()
        {
            List<PvtCollege_Models> list = new List<PvtCollege_Models>();

            list = db.GetPvtCollegeList();

            return View(list);
        }

        #region Education material

        #region curriculum
        public ActionResult curriculum(string str)
        {
              List<manage_curriculum> curriculum = new List<manage_curriculum>();

            curriculum = db.getcurriculumForHome(str);

            ViewBag.str = str;
            return View(curriculum);
        }


        #endregion

        #region DEL_ContentMaterial

        public ActionResult DEL_ContentMaterial(string sem)
        {
            List<manage_curriculum> DelContent = new List<manage_curriculum>();

            DelContent = db.getDelContentForHome(sem);

            ViewBag.sem = sem;

            return View(DelContent);
        }


        #endregion

        #region EBook

        public ActionResult EBook(string book)
        {
            List<manage_ebook> DelContent = new List<manage_ebook>();

            DelContent = db.getebookForHome(book);

            ViewBag.book = book;
            return View(DelContent);
        }


        #endregion

        public ActionResult training_Module(string otcontent)
        {
            List<manage_otherEduContent> OtherEduContent = new List<manage_otherEduContent>();

            OtherEduContent = db.getOtherEduContentForHome(otcontent);

            ViewBag.str = otcontent;
            return View(OtherEduContent);
        }

        //public ActionResult Research(string sem)
        //{

        //    return View();
        //}

        //public ActionResult Survey(string sem)
        //{

        //    return View();
        //}

        //public ActionResult others(string sem)
        //{

        //    return View();
        //}


        #endregion



        public ActionResult Govt_order()
        {
            List<manage_Go> Go = new List<manage_Go>();

            Go = db.getGo();


            return View(Go);
        }

        public ActionResult RTI()
        {

            return View();
        }

        public ActionResult Competition()
        {

            List<manage_compitition> compitition = new List<manage_compitition>();

            compitition = db.getcompitition();


            return View(compitition);
        }

        public ActionResult ViewPdf(string ImageName)
        {
            // Retrieve the PDF file path based on the item's ID (replace with your logic)
            //var filePath = GetFilePathById(id);
            var filePath = ImageName;

            if (!string.IsNullOrEmpty(filePath))
            {
                // Return the PDF file for viewing
                return File(filePath, "application/pdf", Path.GetFileName(filePath));
            }
            else
            {
                // Handle the case when the PDF file is not found
                return HttpNotFound();
            }
        }


        public ActionResult Photo_Gallery()
        {

            return View();
        }

        public ActionResult Contact_US()
        {

            return View();
        }



    }
}
