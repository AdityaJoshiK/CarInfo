using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CarInfo.Areas.MST_Car.Controllers
{
    [Area("MST_Car")]
    [Route("MST_Car/[controller]/[action]")]
    public class MST_CarController : Controller
    {
        private IConfiguration Configuration;

        public MST_CarController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(MST_CarModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_MST_Car_SelectAll();

            return View("MST_CarList", make);
        }

        public IActionResult Detail(int CarID)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable detail = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            detail = carDal.dbo_PR_MST_Car_SelectByPK(CarID);

            return View("MST_CarDetail", detail);
        }

        public IActionResult Save(MST_CarModel modelMST_Car)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelMST_Car.CarID == null || modelMST_Car.CarID == 0)
            {
                DataTable dt = dalCAR.PR_MST_Car_Insert(modelMST_Car);
                TempData["DealerInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_MST_Car_UpdateByPK(modelMST_Car);
                TempData["DealerInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? CarID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");

            #region MakeDropdown
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            SqlCommand cmd = conn1.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CAR_Make_SelectForDropDown";
            //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = CV.UserID();
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd.ExecuteReader();
            dt1.Load(sdr1);

            List<CAR_MakeDropDownModel> list = new List<CAR_MakeDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                CAR_MakeDropDownModel vlst = new CAR_MakeDropDownModel();
                vlst.MakeID = Convert.ToInt32(dr["MakeID"]);
                vlst.MakeName = dr["MakeName"].ToString();
                list.Add(vlst);
            }
            ViewBag.MakeList = list;
            #endregion

            #region SelectByPK
            if (CarID != null)
            {
                //string str = Configuration.GetConnectionString("myConnectionString");

                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_MST_Car_SelectByPK(CarID);
                MST_CarModel modelMST_Car = new MST_CarModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_Car.CarID = Convert.ToInt32(dr["CarID"]);
                    modelMST_Car.MakeID = Convert.ToInt32(dr["MakeID"]);
                    modelMST_Car.Name = dr["Name"].ToString();
                    modelMST_Car.Price = Convert.ToDecimal(dr["Price"]);
                    modelMST_Car.Year = Convert.ToInt32(dr["Year"]);
                }


                return View("MST_CarAddEdit", modelMST_Car);
            }
            #endregion

            return View("MST_CarAddEdit");
        }

        #region Delete
        public IActionResult Delete(int CarID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_MST_Car_DeleteByPK(CarID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

    }
}
