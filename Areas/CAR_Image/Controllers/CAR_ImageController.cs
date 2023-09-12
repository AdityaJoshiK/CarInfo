using CarInfo.Areas.CAR_Image.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.BAL;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace CarInfo.Areas.CAR_Image.Controllers
{
    [CheckAccess]
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

        [HttpPost]
        public async Task<IActionResult> Save(CAR_ImageModel modelCAR_Image)
        {
            if (modelCAR_Image.Files != null && modelCAR_Image.Files.Count > 0)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                CAR_DAL dalCAR = new CAR_DAL();

                foreach (var file in modelCAR_Image.Files)
                {
                    string fileName = file.FileName;
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string uniqueFileName = fileName;

                    int counter = 1;
                    while (System.IO.File.Exists(Path.Combine(path, uniqueFileName)))
                    {
                        uniqueFileName = $"{fileNameWithoutExtension}({counter++}){fileExtension}";
                    }

                    string fileNameWithPath = Path.Combine(path, uniqueFileName);

                    var newImage = new CAR_ImageModel
                    {
                        CarID = modelCAR_Image.CarID,
                        PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + uniqueFileName,
                    };

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    dalCAR.PR_CAR_Image_Insert(newImage);
                }

                TempData["ImageInsertMsg"] = "Records Saved Successfully";
            }
            else if (modelCAR_Image.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = modelCAR_Image.File; // Get the file

                string fileName = file.FileName;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string fileExtension = Path.GetExtension(fileName);
                string uniqueFileName = fileName;

                int counter = 1;
                while (System.IO.File.Exists(Path.Combine(path, uniqueFileName)))
                {
                    uniqueFileName = $"{fileNameWithoutExtension}({counter++}){fileExtension}";
                }

                string fileNameWithPath = Path.Combine(path, uniqueFileName);

                var newImage = new CAR_ImageModel
                {
                    CarID = modelCAR_Image.CarID,
                    PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + uniqueFileName,
                };

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                CAR_DAL dalCAR = new CAR_DAL();
                // Update existing record
                newImage.ImageID = modelCAR_Image.ImageID;
                dalCAR.PR_CAR_Image_UpdateByPK(newImage);

                TempData["ImageInsertMsg"] = "Records Updated Successfully";
            }
            else
            {
                TempData["ImageInsertMsg"] = "No file selected for upload";
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
