﻿using CarInfo.Areas.CAR_Review.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CarInfo.Areas.CAR_Review.Controllers
{
    [Area("CAR_Review")]
    [Route("CAR_Review/[controller]/[action]")]
    public class CAR_ReviewController : Controller
    {
        private IConfiguration Configuration;

        public CAR_ReviewController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable reviews = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            reviews = carDal.PR_CAR_Review_SelectAll();

            return View("CAR_ReviewList", reviews);
        }

        public IActionResult Save(CAR_ReviewModel modelCAR_Review)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_Review.ReviewID == null || modelCAR_Review.ReviewID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_Review_Insert(modelCAR_Review);
                TempData["ReviewInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_Review_UpdateByPK(modelCAR_Review);
                TempData["ReviewInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? ReviewID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            #region CarDropdown
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            SqlCommand cmd = conn1.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Car_SelectForDropDown";
            //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = CV.UserID();
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd.ExecuteReader();
            dt1.Load(sdr1);

            List<MST_CarDropDownModel> list = new List<MST_CarDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                MST_CarDropDownModel vlst = new MST_CarDropDownModel();
                vlst.CarID = Convert.ToInt32(dr["CarID"]);
                vlst.CarName = dr["Name"].ToString();
                list.Add(vlst);
            }
            ViewBag.CarList = list;
            #endregion

            #region SelectByPK
            if (ReviewID != null)
            {
                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Review_SelectByPK(str, ReviewID);
                CAR_ReviewModel modelCAR_Review = new CAR_ReviewModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Review.ReviewID = Convert.ToInt32(dr["ReviewID"]);
                    modelCAR_Review.ReviewText = dr["ReviewText"].ToString();
                    modelCAR_Review.CarID = Convert.ToInt32(dr["CarID"]);
                    modelCAR_Review.Rating = Convert.ToInt32(dr["Rating"]);
                }


                return View("CAR_ReviewAddEdit", modelCAR_Review);
            }
            #endregion

            return View("CAR_ReviewAddEdit");
        }

        #region Delete
        public IActionResult Delete(int ReviewID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Review_DeleteByPK(ReviewID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
