using CarInfo.Areas.CAR_Review.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using CarInfo.Models;
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
        public IActionResult Index(CAR_ReviewModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable reviews = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            reviews = carDal.PR_CAR_Review_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = carDal.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_ReviewList", reviews);
        }

        public IActionResult Save(DataTable model)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            CAR_ReviewModel modelCAR_Review = new CAR_ReviewModel();
            modelCAR_Review.ReviewText = model.Rows[0]["ReviewText"].ToString(); // Extract ReviewText value from the DataTable

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



        //public IActionResult Save(CAR_ReviewModel modelCAR_Review)
        //{
        //    #region Insert & Update

        //    string str = Configuration.GetConnectionString("MyConnectionString");
        //    CAR_DAL dalCAR = new CAR_DAL();

        //    if (modelCAR_Review.ReviewID == null || modelCAR_Review.ReviewID == 0)
        //    {
        //        DataTable dt = dalCAR.PR_CAR_Review_Insert(modelCAR_Review);
        //        TempData["ReviewInsertMsg"] = "Record Inserted Succesfully";
        //    }
        //    else
        //    {
        //        DataTable dt = dalCAR.PR_CAR_Review_UpdateByPK(modelCAR_Review);
        //        TempData["ReviewInsertMsg"] = "Record Updated Succesfully";
        //    }

        //    return RedirectToAction("Index");

        //    #endregion
        //}

        public IActionResult Add(int? ReviewID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (ReviewID != null)
            {
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
