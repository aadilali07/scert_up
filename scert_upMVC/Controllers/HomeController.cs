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

            return View();
        }

        public ActionResult diets()
        {

            return View();
        }

        public ActionResult pvt_institute()
        {

            return View();
        }

        #region Education material

        #region curriculum
        public ActionResult curriculum(string curriculum)
        {

            return View();
        }


        #endregion

        #region DEL_ContentMaterial

        public ActionResult DEL_ContentMaterial(string sem)
        {

            return View();
        }


        #endregion

        #region EBook

        public ActionResult EBook(string book)
        {

            return View();
        }


        #endregion

        public ActionResult training_Module(string sem)
        {

            return View();
        }

        public ActionResult Research(string sem)
        {

            return View();
        }

        public ActionResult Survey(string sem)
        {

            return View();
        }

        public ActionResult others(string sem)
        {

            return View();
        }


        #endregion



        public ActionResult Govt_order()
        {

            return View();
        }

        public ActionResult RTI()
        {

            return View();
        }

        public ActionResult Competition()
        {

            return View();
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
