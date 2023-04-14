using CarInfo.Areas.CAR_Image.Models;
using CarInfo.Areas.CAR_Image.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Image.Controllers
{
    [Area("CAR_Image")]
    [Route("CAR_Image/[controller]/[action]")]
    public class CAR_ImageController : Controller
    {
        private IConfiguration Configuration;

        public CAR_ImageController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(CAR_ImageModel model)
        {
            DataTable Image = new DataTable();
            CAR_DALBase carDal = new CAR_DALBase();

            Image = carDal.PR_CAR_Image_SelectAll(model);

            #region CarDropdown
            List<MST_CarDropDownModel> carList = carDal.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            return View("CAR_ImageList", Image);
        }

        public IActionResult Save(CAR_ImageModel modelCAR_Image,List<string> images)
        {
            if (modelCAR_Image.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelCAR_Image.File.FileName);
                //model.PhotoPath = "~" + FilePath.Replace("wwwroot\\","/") + "/" + model.File.FileName;
                modelCAR_Image.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelCAR_Image.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCAR_Image.File.CopyTo(stream);
                }
            }
            try { 

            CAR_DAL dalCAR = new CAR_DAL();

            if (modelCAR_Image.ImageID == null || modelCAR_Image.ImageID == 0)
            {
                // Insert new record
                // Insert the first Image
                modelCAR_Image.PhotoPath = images[0];
                dalCAR.PR_CAR_Image_Insert(modelCAR_Image);

                // Insert the remaining images
                for (int i = 1; i < images.Count; i++)
                {
                    CAR_ImageModel newImage = new CAR_ImageModel
                    {
                        CarID = modelCAR_Image.CarID,
                        PhotoPath = images[i],
                        //UserID = modelCAR_Image.UserID
                    };
                    dalCAR.PR_CAR_Image_Insert(newImage);
                }

                TempData["ImageInsertMsg"] = "Record Inserted Succesfully";
            }
            else
            {
                // Update existing record
                modelCAR_Image.PhotoPath = images[0];
                dalCAR.PR_CAR_Image_UpdateByPK(modelCAR_Image);

                // Delete existing Image names for the record
                //dalCAR.PR_CAR_Image_DeleteImagesByImageId(modelCAR_Image.ImageID);

                // Insert new Image names for the record
                //foreach (string PhotoPath in images)
                //{
                //    CAR_ImageModel newImage = new CAR_ImageModel
                //    {
                //        CarID = modelCAR_Image.CarID,
                //        PhotoPath = PhotoPath,
                //        ImageID = modelCAR_Image.ImageID,
                //        //UserID = modelCAR_Image.UserID
                //    };
                //    dalCAR.PR_CAR_Image_Insert(newImage);
                //}

                TempData["ImageInsertMsg"] = "Record Updated Succesfully";
            }
        }
            catch (Exception ex)
            {
                TempData["ImageInsertMsg"] = "Error occurred while saving record. " + ex.Message;
            }

            return RedirectToAction("Index");

           
        }

        public IActionResult Add(int? ImageID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();

            #region CarDropdown
            List<MST_CarDropDownModel> carList = dalCAR.PR_MST_Car_DropDown();
            ViewBag.CarList = carList;
            #endregion

            #region SelectByPK
            if (ImageID != null)
            {
                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_CAR_Image_SelectByPK(str, ImageID);
                CAR_ImageModel modelCAR_Image = new CAR_ImageModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelCAR_Image.ImageID = Convert.ToInt32(dr["ImageID"]);
                    modelCAR_Image.PhotoPath = dr["PhotoPath"].ToString();
                    modelCAR_Image.CarID = Convert.ToInt32(dr["CarID"]);
                }


                return View("CAR_ImageAddEdit", modelCAR_Image);
            }
            #endregion

            return View("CAR_ImageAddEdit");
        }

        #region Delete
        public IActionResult Delete(int ImageID)
        {
            string str = Configuration.GetConnectionString("MyConnectionString");
            CAR_DAL dalCAR = new CAR_DAL();
            DataTable dt = dalCAR.PR_CAR_Image_DeleteByPK(ImageID);

            return RedirectToAction("Index");
        }

        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
