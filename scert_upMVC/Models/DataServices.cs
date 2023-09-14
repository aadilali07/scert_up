using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using scert_upMVC.Models;

namespace scert_upMVC.Models
{
    public class DataServices
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConn"].ConnectionString);

        #region login
        public Models.common_response login(string username, string password)
        {
            Models.common_response Response = new Models.common_response();

            #region Validation

            if (username == null || username == "")
            {
                Response.message = "Invalid Username.";
                return Response;
            }

            if (password == null || password == "")
            {
                Response.message = "Invalid password (must include atleast 8 charaters,uppercase and lowercase alphabhet, one number and one special charater).";
                return Response;
            }

            username = username.Replace("'", "''").Trim();
            password = password.Replace("'", "''").Trim();


            #endregion;

            #region Check User
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Login");
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader sda = cmd.ExecuteReader();


                if (sda.HasRows)
                {
                    sda.Read();
                    Response.success = true;
                    Response.parameter = username;
                    Response.emp_name = sda["UserName"].ToString();

                    return Response;
                }
                sda.Close();
                con.Close();

                Response.message = "Invalid username or password!.";

            }
            #endregion;

            return Response;
        }
        #endregion

        #region check Session

        public Models.common_response adminssioncheck(string viewdashboaradds)
        {
            Models.common_response response = new Models.common_response();

            if (HttpContext.Current.Session["adminname"] != null)
            {
                response.success = true;
                response.parameter = HttpContext.Current.Session["adminname"].ToString();
            }

            return response;
        }


        #endregion


        #region Manage News
        public bool insert_news(scert_upMVC.Models.manage_news news)
        {

            SqlCommand cmd = new SqlCommand("sp_AddNews", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Heading", news.heading);
            cmd.Parameters.AddWithValue("@Description", news.desc);
            cmd.Parameters.AddWithValue("@Date", news.date);
            cmd.Parameters.AddWithValue("@File_Name", news.fileName);
            cmd.Parameters.AddWithValue("@File_Path", news.filepath);
            cmd.Parameters.AddWithValue("@id", news.id);

            if (news.id != null && news.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateNewsById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "addnews");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_news> getnews()
        {
            List<manage_news> news = new List<manage_news>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_AddNews", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getnewslist");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_news pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_news();
                    pro.id = sdr["id"].ToString();
                    pro.heading = sdr["Heading"].ToString();
                    pro.desc = sdr["Description"].ToString();
                    pro.date = sdr["Date"].ToString();
                    pro.fileName = sdr["File_Name"].ToString();
                    pro.filepath = sdr["File_Path"].ToString();

                    news.Add(pro);
                }
            }
            con.Close();


            return news;
        }

        public manage_news getnewsbyId(string id)
        {
            manage_news news = new manage_news();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_AddNews", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getnewslist");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                    sdr.Read();
                    news.id = sdr["id"].ToString();
                    news.heading = sdr["Heading"].ToString();
                    news.desc = sdr["Description"].ToString();
                    news.date = sdr["Date"].ToString();
                    news.fileName = sdr["File_Name"].ToString();
                    news.filepath = sdr["File_Path"].ToString();

            }
            sdr.Close();
            con.Close();


            return news;
        }

        public bool delet_news(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_AddNews", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteAddNewsById");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Manage compitition
        public bool insert_compitition(scert_upMVC.Models.manage_compitition compitition)
        {

            SqlCommand cmd = new SqlCommand("sp_Competition", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Comp_Name", compitition.Comp_Name);
            cmd.Parameters.AddWithValue("@Comp_Date", compitition.Comp_Date);
            cmd.Parameters.AddWithValue("@Comp_File_Name", compitition.Comp_File_Name);
            cmd.Parameters.AddWithValue("@Comp_File_Path", compitition.Comp_File_Path);
            cmd.Parameters.AddWithValue("@Result_Date", compitition.Result_Date);
            cmd.Parameters.AddWithValue("@Result_File_Name", compitition.Result_File_Name);
            cmd.Parameters.AddWithValue("@Result_File_Path", compitition.Result_File_Path);
            cmd.Parameters.AddWithValue("@id", compitition.id);

            if (compitition.id != null && compitition.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateCompetitionById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddCompetition");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_compitition> getcompitition()
        {
            List<manage_compitition> compitition = new List<manage_compitition>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Competition", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCompetitionList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_compitition pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_compitition();
                    pro.id = sdr["id"].ToString();
                    pro.Comp_Name = sdr["Comp_Name"].ToString();
                    pro.Comp_Date = sdr["Comp_Date"].ToString();
                    pro.Comp_File_Name = sdr["Comp_File_Name"].ToString();
                    pro.Comp_File_Path = sdr["Comp_File_Path"].ToString();
                    pro.Result_Date = sdr["Result_Date"].ToString();
                    pro.Result_File_Name = sdr["Result_File_Name"].ToString();
                    pro.Result_File_Path = sdr["Result_File_Path"].ToString();

                    compitition.Add(pro);
                }
            }
            con.Close();


            return compitition;
        }

        public manage_compitition getcompititionbyId(string id)
        {
            manage_compitition compitition = new manage_compitition();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Competition", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCompetitionById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                compitition.id = sdr["id"].ToString();
                compitition.id = sdr["id"].ToString();
                compitition.Comp_Name = sdr["Comp_Name"].ToString();
                compitition.Comp_Date = sdr["Comp_Date"].ToString();
                compitition.Comp_File_Name = sdr["Comp_File_Name"].ToString();
                compitition.Comp_File_Path = sdr["Comp_File_Path"].ToString();
                compitition.Result_Date = sdr["Result_Date"].ToString();
                compitition.Result_File_Name = sdr["Result_File_Name"].ToString();
                compitition.Result_File_Path = sdr["Result_File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return compitition;
        }

        public bool delet_compitition(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_Competition", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteCompetitionBYId" +
                "");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Manage curriculum
        public bool insert_curriculum(scert_upMVC.Models.manage_curriculum curriculum)
        {

            SqlCommand cmd = new SqlCommand("sp_Curriculum", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Course", curriculum.Course);
            cmd.Parameters.AddWithValue("@Subject", curriculum.Subject);
            cmd.Parameters.AddWithValue("@File_Name", curriculum.File_Name);
            cmd.Parameters.AddWithValue("@File_Path", curriculum.File_Path);
            cmd.Parameters.AddWithValue("@id", curriculum.id);

            if (curriculum.id != null && curriculum.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateCurriculumById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddCurriculum");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_curriculum> getcurriculum()
        {
            List<manage_curriculum> curriculum = new List<manage_curriculum>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Curriculum", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCurriculumList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_curriculum pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_curriculum();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    curriculum.Add(pro);
                }
            }
            con.Close();


            return curriculum;
        }

        public List<manage_curriculum> getcurriculumForHome(string str)
        {
            List<manage_curriculum> curriculum = new List<manage_curriculum>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Curriculum", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getcurriculumForHome");
            cmd.Parameters.AddWithValue("@Course", str);
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_curriculum pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_curriculum();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    curriculum.Add(pro);
                }
            }
            con.Close();


            return curriculum;
        }

        public manage_curriculum getcurriculumbyId(string id)
        {
            manage_curriculum curriculum = new manage_curriculum();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Curriculum", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getCurriculumById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                curriculum.id = sdr["id"].ToString();
                curriculum.Course = sdr["Course"].ToString();
                curriculum.Subject = sdr["Subject"].ToString();
                curriculum.File_Name = sdr["File_Name"].ToString();
                curriculum.File_Path = sdr["File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return curriculum;
        }

        public bool delet_curriculum(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_Curriculum", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteCurriculumBYId" +
                "");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Manage DelContent
        public bool insert_DelContent(scert_upMVC.Models.manage_curriculum curriculum)
        {

            SqlCommand cmd = new SqlCommand("sp_DledContent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Course", curriculum.Course);
            cmd.Parameters.AddWithValue("@Subject", curriculum.Subject);
            cmd.Parameters.AddWithValue("@File_Name", curriculum.File_Name);
            cmd.Parameters.AddWithValue("@File_Path", curriculum.File_Path);
            cmd.Parameters.AddWithValue("@id", curriculum.id);

            if (curriculum.id != null && curriculum.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateDledContentById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddDledContent");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_curriculum> getDelContent()
        {
            List<manage_curriculum> curriculum = new List<manage_curriculum>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DledContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDledContentList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_curriculum pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_curriculum();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    curriculum.Add(pro);
                }
            }
            con.Close();


            return curriculum;
        }

        public List<manage_curriculum> getDelContentForHome(string sem)
        {
            List<manage_curriculum> curriculum = new List<manage_curriculum>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DledContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDelContentForHome");
            cmd.Parameters.AddWithValue("@Course", sem);
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_curriculum pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_curriculum();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    curriculum.Add(pro);
                }
            }
            con.Close();


            return curriculum;
        }

        public manage_curriculum getDelContentbyId(string id)
        {
            manage_curriculum curriculum = new manage_curriculum();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DledContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDledContentById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                curriculum.id = sdr["id"].ToString();
                curriculum.Course = sdr["Course"].ToString();
                curriculum.Subject = sdr["Subject"].ToString();
                curriculum.File_Name = sdr["File_Name"].ToString();
                curriculum.File_Path = sdr["File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return curriculum;
        }

        public bool delet_DelContent(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_DledContent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteDledContentBYId" +
                "");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Manage faculty
        public bool insert_faculty(scert_upMVC.Models.manage_faculty faculty)
        {

            SqlCommand cmd = new SqlCommand("sp_Faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", faculty.Name);
            cmd.Parameters.AddWithValue("@Designation", faculty.Designation);
            cmd.Parameters.AddWithValue("@JoiningDate", faculty.JoiningDate);
            cmd.Parameters.AddWithValue("@MobileNo", faculty.MobileNo);
            cmd.Parameters.AddWithValue("@EmailId", faculty.EmailId);
            cmd.Parameters.AddWithValue("@id", faculty.id);

            if (faculty.id != null && faculty.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateFacultyById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "addFaculty");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_faculty> getfaculty()
        {
            List<manage_faculty> faculty = new List<manage_faculty>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getFacultyList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_faculty pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_faculty();
                    pro.id = sdr["id"].ToString();
                    pro.Name = sdr["Name"].ToString();
                    pro.Designation = sdr["Designation"].ToString();
                    pro.JoiningDate = sdr["JoiningDate"].ToString();
                    pro.MobileNo = sdr["MobileNo"].ToString();
                    pro.EmailId = sdr["EmailId"].ToString();

                    faculty.Add(pro);
                }
            }
            con.Close();


            return faculty;
        }

        public manage_faculty getfacultybyId(string id)
        {
            manage_faculty faculty = new manage_faculty();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getFacultyById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                faculty.id = sdr["id"].ToString();
                faculty.Name = sdr["Name"].ToString();
                faculty.Designation = sdr["Designation"].ToString();
                faculty.JoiningDate = sdr["JoiningDate"].ToString();
                faculty.MobileNo = sdr["MobileNo"].ToString();
                faculty.EmailId = sdr["EmailId"].ToString();


            }
            sdr.Close();
            con.Close();


            return faculty;
        }

        public bool delet_faculty(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_Faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteFacultyBYId" +
                "");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Manage Go
        public bool insert_Go(scert_upMVC.Models.manage_Go Go)
        {

            SqlCommand cmd = new SqlCommand("sp_Go", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GONo", Go.GONo);
            cmd.Parameters.AddWithValue("@Subject", Go.Subject);
            cmd.Parameters.AddWithValue("@Date", Go.date);
            cmd.Parameters.AddWithValue("@File_Name", Go.File_Name);
            cmd.Parameters.AddWithValue("@File_Path", Go.File_Path);
            cmd.Parameters.AddWithValue("@id", Go.id);

            if (Go.id != null && Go.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateGoById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddGo");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_Go> getGo()
        {
            List<manage_Go> Go = new List<manage_Go>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Go", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getGoList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_Go pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_Go();
                    pro.id = sdr["id"].ToString();
                    pro.GONo = sdr["GONo"].ToString();
                    pro.date = sdr["date"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    Go.Add(pro);
                }
            }
            con.Close();


            return Go;
        }

        public manage_Go getGobyId(string id)
        {
            manage_Go Go = new manage_Go();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Go", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getGoById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                Go.id = sdr["id"].ToString();
                Go.GONo = sdr["GONo"].ToString();
                Go.date = sdr["date"].ToString();
                Go.Subject = sdr["Subject"].ToString();
                Go.File_Name = sdr["File_Name"].ToString();
                Go.File_Path = sdr["File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return Go;
        }

        public bool delet_Go(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_Go", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteGoBYId" +
                "");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Manage photoGallery
        public bool insert_photoGallery(scert_upMVC.Models.manage_photoGallery photoGallery)
        {

            SqlCommand cmd = new SqlCommand("sp_photoGallery", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ImageTitle", photoGallery.ImageTitle);
            cmd.Parameters.AddWithValue("@ImagePath", photoGallery.ImagePath);
            cmd.Parameters.AddWithValue("@File_Name", photoGallery.File_Name);
            cmd.Parameters.AddWithValue("@id", photoGallery.id);

            if (photoGallery.id != null && photoGallery.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updatePhotoGalleryById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddPhotoGallery");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_photoGallery> getphotoGallery()
        {
            List<manage_photoGallery> photoGallery = new List<manage_photoGallery>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_photoGallery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getphotoGalleryList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_photoGallery pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_photoGallery();
                    pro.id = sdr["id"].ToString();
                    pro.ImageTitle = sdr["ImageTitle"].ToString();
                    pro.ImagePath = sdr["ImagePath"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();

                    photoGallery.Add(pro);
                }
            }
            con.Close();


            return photoGallery;
        }

        public manage_photoGallery getphotoGallerybyId(string id)
        {
            manage_photoGallery photoGallery = new manage_photoGallery();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_photoGallery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getphotoGalleryById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                photoGallery.id = sdr["id"].ToString();
                photoGallery.ImageTitle = sdr["ImageTitle"].ToString();
                photoGallery.ImagePath = sdr["ImagePath"].ToString();
                photoGallery.File_Name = sdr["File_Name"].ToString();


            }
            sdr.Close();
            con.Close();


            return photoGallery;
        }

        public bool delet_photoGallery(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_photoGallery", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deletephotoGalleryBYId");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Manage Video
        public bool insert_Video(scert_upMVC.Models.manage_Video Video)
        {

            SqlCommand cmd = new SqlCommand("sp_AddVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", Video.Title);
            cmd.Parameters.AddWithValue("@Description", Video.Description);
            cmd.Parameters.AddWithValue("@Url", Video.Url);
            cmd.Parameters.AddWithValue("@id", Video.id);

            if (Video.id != null && Video.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateVideoById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "addVideo");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_Video> getVideo()
        {
            List<manage_Video> Video = new List<manage_Video>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_AddVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getVideoList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_Video pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_Video();
                    pro.id = sdr["id"].ToString();
                    pro.Title = sdr["Title"].ToString();
                    pro.Description = sdr["Description"].ToString();
                    pro.Url = sdr["Url"].ToString();

                    Video.Add(pro);
                }
            }
            con.Close();


            return Video;
        }

        public manage_Video getVideobyId(string id)
        {
            manage_Video Video = new manage_Video();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_AddVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getVideoById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                Video.id = sdr["id"].ToString();
                Video.Title = sdr["Title"].ToString();
                Video.Description = sdr["Description"].ToString();
                Video.Url = sdr["Url"].ToString();


            }
            sdr.Close();
            con.Close();


            return Video;
        }

        public bool delet_Video(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_AddVideo", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteAddVideoBYId");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion


        #region Units

        public bool AddUnits(Units_Models obj)
        {
            SqlCommand cmd = new SqlCommand("sp_Units", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@District", obj.District);
            cmd.Parameters.AddWithValue("@Inst_Name", obj.Inst_Name);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@InchargeName", obj.InchargeName);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Website", obj.Website);

            if (obj.Id != null && obj.Id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateUnitsById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddUnits");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Units_Models> getAllUnitsRecord()
        {
            List<Units_Models> lst = new List<Units_Models>();
            Units_Models units;

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Units", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getUnitsList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    units = new Units_Models();
                    units.Id = sdr["Id"].ToString();
                    units.District = sdr["District"].ToString();
                    units.Inst_Name = sdr["Inst_Name"].ToString();
                    units.Address = sdr["Address"].ToString();
                    units.InchargeName = sdr["InchargeName"].ToString();
                    units.Designation = sdr["Designation"].ToString();
                    units.Phone = sdr["Phone"].ToString();
                    units.Email = sdr["Email"].ToString();
                    units.Website = sdr["Website"].ToString();
                    lst.Add(units);
                }

            }
            con.Close();
            return lst;



        }


        public List<Units_Models> GetUnitsRecordById(string id)
        {
            List<Units_Models> list = new List<Units_Models>();
            Units_Models units = new Units_Models();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Units", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getUnitsById");
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    units.Id = sdr["Id"].ToString();
                    units.District = sdr["District"].ToString();
                    units.Inst_Name = sdr["Inst_Name"].ToString();
                    units.Address = sdr["Address"].ToString();
                    units.InchargeName = sdr["InchargeName"].ToString();
                    units.Designation = sdr["Designation"].ToString();
                    units.Phone = sdr["Phone"].ToString();
                    units.Email = sdr["Email"].ToString();
                    units.Website = sdr["Website"].ToString();
                    list.Add(units);

                }
            }
            con.Close();

            return list;
        }

        public bool UpdateUnits(Units_Models obj)
        {

            SqlCommand cmd = new SqlCommand("sp_Units", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "updateUnitsById");
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@District", obj.District);
            cmd.Parameters.AddWithValue("@Inst_Name", obj.Inst_Name);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@InchargeName", obj.InchargeName);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Website", obj.Website);
            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int DeleteUnitsRecord(Units_Models obj)
        {
            SqlCommand cmd = new SqlCommand("sp_Units", con);
            int row = 0;
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "deleteUnitsById");
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return row;
        }


        #endregion

        #region manage private college

        public bool AddPvtCollege(PvtCollege_Models obj)
        {
            SqlCommand cmd = new SqlCommand("sp_PvtCollege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@District", obj.District);
            cmd.Parameters.AddWithValue("@Inst_Name", obj.Inst_Name);
            cmd.Parameters.AddWithValue("@Seats", obj.Seats);

            if (obj.Id != null && obj.Id != "")
            {
                cmd.Parameters.AddWithValue("@Action", "updatePvtCollegeById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddPvtCollege");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<PvtCollege_Models> GetPvtCollegeList()
        {
            List<PvtCollege_Models> lst = new List<PvtCollege_Models>();
            PvtCollege_Models pvtCollege;

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PvtCollege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getPvtCollegeList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pvtCollege = new PvtCollege_Models();
                    pvtCollege.Id = sdr["Id"].ToString();
                    pvtCollege.District = sdr["District"].ToString();
                    pvtCollege.Inst_Name = sdr["Inst_Name"].ToString();
                    pvtCollege.Seats = sdr["Seats"].ToString();
                    lst.Add(pvtCollege);
                }

            }
            con.Close();
            return lst;



        }


        public List<PvtCollege_Models> GetPvtCollegeById(string Id)
        {
            List<PvtCollege_Models> list = new List<PvtCollege_Models>();
            PvtCollege_Models units = new PvtCollege_Models();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PvtCollege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getPvtCollegeById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    units.Id = sdr["Id"].ToString();
                    units.District = sdr["District"].ToString();
                    units.Inst_Name = sdr["Inst_Name"].ToString();
                    units.Seats = sdr["Seats"].ToString();
                    list.Add(units);

                }
            }
            con.Close();

            return list;
        }


        public bool DeletePvtCollegeRecordById(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_PvtCollege", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deletePvtCollegeById");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region manage Ebook 

        public bool insert_ebook(scert_upMVC.Models.manage_ebook ebook)
        {

            SqlCommand cmd = new SqlCommand("sp_EBook", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Course", ebook.Course);
            cmd.Parameters.AddWithValue("@Subject", ebook.Subject);
            cmd.Parameters.AddWithValue("@File_Name", ebook.File_Name);
            cmd.Parameters.AddWithValue("@File_Path", ebook.File_Path);
            cmd.Parameters.AddWithValue("@id", ebook.id);

            if (ebook.id != null && ebook.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateEBookById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddEBook");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_ebook> getebook()
        {
            List<manage_ebook> ebook = new List<manage_ebook>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_EBook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getEBookList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_ebook pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_ebook();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    ebook.Add(pro);
                }
            }
            con.Close();


            return ebook;
        }

        public List<manage_ebook> getebookForHome(string book)
        {
            List<manage_ebook> ebook = new List<manage_ebook>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_EBook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getebookForHome");
            cmd.Parameters.AddWithValue("@Course", book);
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_ebook pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_ebook();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    ebook.Add(pro);
                }
            }
            con.Close();


            return ebook;
        }

        public manage_ebook getebookbyId(string id)
        {
            manage_ebook ebook = new manage_ebook();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_EBook", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getEBookById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                ebook.id = sdr["id"].ToString();
                ebook.Course = sdr["Course"].ToString();
                ebook.Subject = sdr["Subject"].ToString();
                ebook.File_Name = sdr["File_Name"].ToString();
                ebook.File_Path = sdr["File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return ebook;
        }

        public bool delete_ebook(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_EBook", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteEBookBYId" +
                "");
            cmd.Parameters.AddWithValue("@Id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion

        #region manage dietlist


        public bool insert_dietlist(manage_dietlist obj)
        {
            SqlCommand cmd = new SqlCommand("sp_DietList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "AddDietList");
            cmd.Parameters.AddWithValue("@Id", obj.id);
            cmd.Parameters.AddWithValue("@Diet_Name", obj.Diet_Name);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@InchargeName", obj.InchargeName);
            cmd.Parameters.AddWithValue("@Designation", obj.Designation);
            cmd.Parameters.AddWithValue("@Contact", obj.Contact);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Website", obj.Website);
            if (obj.id != null && obj.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateDietListById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddDietList");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<manage_dietlist> getAllDietList()
        {
            List<manage_dietlist> lst = new List<manage_dietlist>();
            manage_dietlist dietlist;

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DietList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDietList");
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    dietlist = new manage_dietlist();
                    dietlist.id = sdr["Id"].ToString();
                    dietlist.Diet_Name = sdr["Diet_Name"].ToString();
                    dietlist.Address = sdr["Address"].ToString();
                    dietlist.InchargeName = sdr["InchargeName"].ToString();
                    dietlist.Designation = sdr["Designation"].ToString();
                    dietlist.Contact = sdr["Contact"].ToString();
                    dietlist.Email = sdr["Email"].ToString();
                    dietlist.Website = sdr["Website"].ToString();
                    lst.Add(dietlist);
                }

            }
            con.Close();
            return lst;



        }


        public manage_dietlist getdietlistbyId(string Id)
        {
            manage_dietlist diet = new manage_dietlist();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_DietList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getDietListById");
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                diet.id = sdr["Id"].ToString();
                diet.Diet_Name = sdr["Diet_Name"].ToString();
                diet.Address = sdr["Address"].ToString();
                diet.InchargeName = sdr["InchargeName"].ToString();
                diet.Designation = sdr["Designation"].ToString();
                diet.Contact = sdr["Contact"].ToString();
                diet.Email = sdr["Email"].ToString();
                diet.Website = sdr["Website"].ToString();


            }
            sdr.Close();
            con.Close();

            return diet;


        }



        public bool DeleteDietListRecord(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_DietList", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteDietListBYId" +
                "");
            cmd.Parameters.AddWithValue("@Id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        #endregion

        #region manage OtherEduContent 

        public bool insert_OtherEduContent(scert_upMVC.Models.manage_otherEduContent oherEducontent)
        {

            SqlCommand cmd = new SqlCommand("sp_OtherEduContent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Course", oherEducontent.Course);
            cmd.Parameters.AddWithValue("@Subject", oherEducontent.Subject);
            cmd.Parameters.AddWithValue("@File_Name", oherEducontent.File_Name);
            cmd.Parameters.AddWithValue("@File_Path", oherEducontent.File_Path);
            cmd.Parameters.AddWithValue("@id", oherEducontent.id);

            if (oherEducontent.id != null && oherEducontent.id != "")
            {

                cmd.Parameters.AddWithValue("@Action", "updateOtherEduContentById");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "AddOtherEduContent");
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<manage_otherEduContent> getOtherEduContent()
        {
            List<manage_otherEduContent> oherEducontent = new List<manage_otherEduContent>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_OtherEduContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getOtherEduContentList");
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_otherEduContent pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_otherEduContent();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    oherEducontent.Add(pro);
                }
            }
            con.Close();


            return oherEducontent;
        }

        public List<manage_otherEduContent> getOtherEduContentForHome(string otcontent)
        {
            List<manage_otherEduContent> oherEducontent = new List<manage_otherEduContent>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_OtherEduContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getOtherEduContentForHome");
            cmd.Parameters.AddWithValue("@Course", otcontent);
            SqlDataReader sdr = cmd.ExecuteReader();
            manage_otherEduContent pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new manage_otherEduContent();
                    pro.id = sdr["id"].ToString();
                    pro.Course = sdr["Course"].ToString();
                    pro.Subject = sdr["Subject"].ToString();
                    pro.File_Name = sdr["File_Name"].ToString();
                    pro.File_Path = sdr["File_Path"].ToString();

                    oherEducontent.Add(pro);
                }
            }
            con.Close();


            return oherEducontent;
        }

        public manage_otherEduContent getOtherEduContentbyId(string id)
        {
            manage_otherEduContent oherEducontent = new manage_otherEduContent();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_OtherEduContent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "getOtherEduContentById");
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                oherEducontent.id = sdr["id"].ToString();
                oherEducontent.Course = sdr["Course"].ToString();
                oherEducontent.Subject = sdr["Subject"].ToString();
                oherEducontent.File_Name = sdr["File_Name"].ToString();
                oherEducontent.File_Path = sdr["File_Path"].ToString();


            }
            sdr.Close();
            con.Close();


            return oherEducontent;
        }

        public bool delete_OtherEduContent(string id)
        {

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_OtherEduContent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "deleteOtherEduContentById" +
                "");
            cmd.Parameters.AddWithValue("@Id", id);

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion

    }
}