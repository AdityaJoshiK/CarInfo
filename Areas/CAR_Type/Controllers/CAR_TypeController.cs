using CarInfo.Areas.CAR_Type.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Type.Controllers
{
    [Area("CAR_Type")]
    [Route("CAR_Type/[controller]/[action]")]
    public class CAR_TypeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_TypeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable Type = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            Type = carDal.PR_CAR_Type_SelectAll();

            return View("CAR_TypeList", Type);
        }

        public IActionResult Save(CAR_TypeModel modelCAR_Type)
        {
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_Type.TypeID == null || modelCAR_Type.TypeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_Type_Insert(modelCAR_Type);
                TempData["TypeInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_Type_UpdateByPK(modelCAR_Type);
                TempData["TypeInsertMsg"] = "Record Updated Succesfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? TypeID)
        {
            #region SelectByPK
            if (TypeID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");

                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Type_SelectByPK(TypeID);
                CAR_TypeModel modelCAR_Type = new CAR_TypeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Type.TypeID = Convert.ToInt32(dr["TypeID"]);
                    modelCAR_Type.TypeName = dr["TypeName"].ToString();
                }


                return View("CAR_TypeAddEdit", modelCAR_Type);
            }
            #endregion

            return View("CAR_TypeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int TypeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Type_DeleteByPK(TypeID);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
