using CarInfo.Areas.CAR_Dealer.Models;
using CarInfo.Areas.CAR_Dealer.Models;
using CarInfo.Areas.CAR_Dealer.Models;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CarInfo.Areas.CAR_Dealer.Controllers
{
    [Area("CAR_Dealer")]
    [Route("CAR_Dealer/[controller]/[action]")]
    public class CAR_DealerController : Controller
    {
        private IConfiguration Configuration;

        public CAR_DealerController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_DealerModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");
                
            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_CAR_Dealer_SelectAll(model.Name);

            return View("CAR_DealerList", make);
        }

        public IActionResult Save(CAR_DealerModel modelCAR_Dealer)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_Dealer.DealerID == null || modelCAR_Dealer.DealerID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_Dealer_Insert(modelCAR_Dealer);
                TempData["DealerInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_Dealer_UpdateByPK(modelCAR_Dealer);
                TempData["DealerInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? DealerID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");

            CAR_DAL dalCAR = new CAR_DAL();


            #region MakeDropdown
            List<CAR_MakeDropDownModel> list = dalCAR.PR_CAR_Make_DropDown();
            ViewBag.MakeList = list;

            #endregion

            #region SelectByPK
            if (DealerID != null)
            {
                //string str = Configuration.GetConnectionString("myConnectionString");

                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Dealer_SelectByPK(DealerID);
                CAR_DealerModel modelCAR_Dealer = new CAR_DealerModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Dealer.DealerID = Convert.ToInt32(dr["DealerID"]);
                    modelCAR_Dealer.MakeID = Convert.ToInt32(dr["MakeID"]);
                    modelCAR_Dealer.Name = dr["Name"].ToString();
                    modelCAR_Dealer.Address = dr["Address"].ToString();
                    modelCAR_Dealer.Country = dr["Country"].ToString();
                    modelCAR_Dealer.State = dr["State"].ToString();
                    modelCAR_Dealer.City = dr["City"].ToString();
                    modelCAR_Dealer.Phone = dr["Phone"].ToString();
                }


                return View("CAR_DealerAddEdit", modelCAR_Dealer);
            }
            #endregion

            return View("CAR_DealerAddEdit");
        }

        #region Delete
        public IActionResult Delete(int DealerID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Dealer_DeleteByPK(DealerID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
