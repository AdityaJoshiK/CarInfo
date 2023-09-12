using CarInfo.Areas.CAR_CarWiseVariant.Models;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace CarInfo.Areas.CAR_CarWiseVariant.Controllers
{
    [CheckAccess]
    [Area("CAR_CarWiseVariant")]
    [Route("CAR_CarWiseVariant/[controller]/[action]")]
    public class CAR_CarWiseVariantController : Controller
    {
        private IConfiguration Configuration;

        public CAR_CarWiseVariantController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_CarWiseVariantModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable variants = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            variants = carDal.PR_CAR_CarWiseVariant_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = carDal.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_CarWiseVariantList", variants);
        }

        public IActionResult Save(CAR_CarWiseVariantModel modelCAR_CarWiseVariant, List<int> VariantID)
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelCAR_CarWiseVariant.CarWiseVariantID == null || modelCAR_CarWiseVariant.CarWiseVariantID == 0)
                {
                    #region Variant Add
                    // Insert the VariantNames into a separate table
                    foreach (int VariantName in VariantID)
                    {
                        CAR_CarWiseVariantModel newVariant = new CAR_CarWiseVariantModel
                        {
                            CarID = modelCAR_CarWiseVariant.CarID,
                            Price = modelCAR_CarWiseVariant.Price,
                            VariantID = VariantName
                        };
                        dalCAR.PR_CAR_CarWiseVariant_Insert(newVariant);
                    }
                    #endregion

                    //#region Variant Add
                    //// Insert the VariantNames into a separate table
                    //foreach (int VariantName in NewVariantNames)
                    //{
                    //    CAR_CarWiseVariantModel newVariant = new CAR_CarWiseVariantModel
                    //    {
                    //        CarID = modelCAR_CarWiseVariant.CarID,
                    //        Price = modelCAR_CarWiseVariant.Price,
                    //        VariantID = VariantName
                    //    };
                    //    CAR_VariantModel addVariant = new CAR_VariantModel
                    //    {
                    //        VariantName = VariantName
                    //    };
                    //    dalCAR.PR_CAR_CarWiseVariant_Insert(newVariant);
                    //    dalCAR.PR_CAR_Variant_Insert(addVariant);
                    //}
                    //#endregion

                    TempData["VariantInsertMsg"] = "Record Inserted Succesfully";
                }
                else
                {
                    // Update existing record
                    modelCAR_CarWiseVariant.VariantID = VariantID[0];
                    dalCAR.PR_CAR_CarWiseVariant_UpdateByPK(modelCAR_CarWiseVariant);

                    TempData["VariantInsertMsg"] = "Record Updated Succesfully";
                }
            }
            catch (Exception ex)
            {
                TempData["VariantInsertMsg"] = "Error occurred while saving record. " + ex.Message;
            }

            return RedirectToAction("Index");
        }



        public IActionResult Add(int? VariantID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");

            CAR_DAL dalCAR = new CAR_DAL();

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCAR.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region CarDropdown
            List<MST_CarDropDownModel> carList = new List<MST_CarDropDownModel>();
            ViewBag.CarList = carList;
            #endregion

            #region VariantDropdown
            List<CAR_VariantDropDownModel> list1 = new List<CAR_VariantDropDownModel>();
            ViewBag.VariantList = list1;
            #endregion

            #region SelectByPK
            if (VariantID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_CarWiseVariant_SelectByPK(str, VariantID);
                CAR_CarWiseVariantModel modelCAR_CarWiseVariant = new CAR_CarWiseVariantModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_CarWiseVariant.CarWiseVariantID = Convert.ToInt32(dr["CarWiseVariantID"]);
                    modelCAR_CarWiseVariant.VariantID = Convert.ToInt32(dr["VariantID"]);
                    modelCAR_CarWiseVariant.CarID = Convert.ToInt32(dr["CarID"]);
                    modelCAR_CarWiseVariant.Price = Convert.ToDecimal(dr["Price"]);
                }


                return View("CAR_CarWiseVariantAddEdit", modelCAR_CarWiseVariant);
            }
            #endregion

            return View("CAR_CarWiseVariantAddEdit");
        }

        #region Delete
        public IActionResult Delete(int VariantID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_CarWiseVariant_DeleteByPK(VariantID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult DropDownByMake(int MakeID, List<CAR_VariantDropDownModel> variant_list)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_CAR_Variant_SelectForDropDownByMakeID";
            cmd1.Parameters.AddWithValue("@MakeID", MakeID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt.Load(sdr1);
            conn.Close();

            List<CAR_VariantDropDownModel> list1 = new List<CAR_VariantDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                CAR_VariantDropDownModel vlst1 = new CAR_VariantDropDownModel();
                vlst1.VariantID = Convert.ToInt32(dr["VariantID"]);
                vlst1.VariantName = dr["VariantName"].ToString();
                variant_list.Add(vlst1);
            }

            var vModel = variant_list;
            return Json(vModel);
        }

        public IActionResult DropDownByMakeCar(int MakeID, List<MST_CarDropDownModel> Make_list)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_MST_Car_SelectForDropDownMakeID";
            cmd1.Parameters.AddWithValue("@MakeID", MakeID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt.Load(sdr1);
            conn.Close();

            List<MST_CarDropDownModel> list1 = new List<MST_CarDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                MST_CarDropDownModel vlst1 = new MST_CarDropDownModel();
                vlst1.CarID = Convert.ToInt32(dr["CarID"]);
                vlst1.Name = dr["Name"].ToString();
                Make_list.Add(vlst1);
            }

            var vModel = Make_list;
            return Json(vModel);
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
