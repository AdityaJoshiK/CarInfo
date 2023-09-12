using CarInfo.Areas.CAR_Make.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;

namespace CarInfo.Areas.CAR_Make.Controllers
{
    [CheckAccess]
    [Area("CAR_Make")]
    [Route("CAR_Make/[controller]/[action]")]
    public class CAR_MakeController : Controller
    {
        private IConfiguration Configuration;

        public CAR_MakeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index(CAR_MakeModel model)
        {
            String str = Configuration.GetConnectionString("myConnectionString");

            DataTable make = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            make = carDal.PR_CAR_Make_SelectAll(model);

            return View("CAR_MakeList", make);
        }

        public IActionResult Save(CAR_MakeModel modelCAR_Make)
        {
            #region Image Upload

            if (modelCAR_Make.File != null)
            {
                string FilePath = "wwwroot\\MakerLogoUpload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelCAR_Make.File.FileName);
                //modelCAR_Make.PhotoPath = "~" + FilePath.Replace("wwwroot\\","/") + "/" + modelCAR_Make.File.FileName;
                modelCAR_Make.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelCAR_Make.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCAR_Make.File.CopyTo(stream);
                }
            }

            #endregion
            #region Insert & Update

            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_Make.MakeID == null || modelCAR_Make.MakeID == 0)
            {
                DataTable dt = dalCAR.PR_CAR_Make_Insert(modelCAR_Make);
                TempData["MakeInsertMsg"] = "Record Inserted Successfully";
            }
            else
            {
                DataTable dt = dalCAR.PR_CAR_Make_UpdateByPK(modelCAR_Make);
                TempData["MakeInsertMsg"] = "Record Updated Successfully";
            }

            return RedirectToAction("Index");

            #endregion
        }

        public IActionResult Add(int? MakeID)
        {
            #region SelectByPK
            if (MakeID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");

                CAR_DAL dalCAR = new CAR_DAL();
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Make_SelectByPK(MakeID);
                CAR_MakeModel modelCAR_Make = new CAR_MakeModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Make.MakeID = Convert.ToInt32(dr["MakeID"]);
                    modelCAR_Make.MakeName = dr["MakeName"].ToString();
                }


                return View("CAR_MakeAddEdit", modelCAR_Make);
            }
            #endregion

            return View("CAR_MakeAddEdit");
        }

        #region Delete
        public IActionResult Delete(int MakeID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Make_DeleteByPK(MakeID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}