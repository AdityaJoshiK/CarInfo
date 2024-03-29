﻿using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.CAR_CarWiseFuelType.Models;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Type.Models;
using CarInfo.Areas.MST_Car.Models;
using CarInfo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CarInfo.Areas.CAR_FuelType.Models;
using CarInfo.Areas.CAR_CarWiseTransmissionType.Models;
using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.Areas.CAR_CarWiseVariant.Models;
using CarInfo.Areas.CAR_CarWiseFeature.Models;
using CarInfo.BAL;

namespace CarInfo.Areas.MST_Car.Controllers
{
    [CheckAccess]
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

            CAR_CarWiseFeatureModel model = new CAR_CarWiseFeatureModel()
            {
                CarID = CarID,
            };

            DataTable featureDataTable = carDal.PR_CAR_CarWiseFeature_SelectAll(model);
            List<string> allFeatures = new List<string>();

            foreach (DataRow row in featureDataTable.Rows)
            {
                if (row["FeatureName"] != DBNull.Value)
                {
                    string[] features = row["FeatureName"].ToString().Split(',');
                    allFeatures.AddRange(features);
                }
            }

            DataTable variantsDataTable = carDal.PR_CAR_CarAllVariants_SelectAll(str, CarID);

            detail = carDal.dbo_PR_MST_Car_SelectByPK(CarID);

            var viewModel = new MST_CarModel
            {
                //FeatureDataTable = featureDataTable,
                CarDetailDataTable = detail,
                CarVariantsDataTable = variantsDataTable,
                features = allFeatures
            };

            //return View("MST_CarDetail", detail);
            return View("MST_CarDetail", viewModel);
        }

        //public IActionResult Save(MST_CarModel modelMST_Car)
        //{
        //    #region Insert & Update

        //    string str = Configuration.GetConnectionString("MyConnectionString");
        //    CAR_DAL dalCAR = new CAR_DAL();

        //    if (modelMST_Car.CarID == null || modelMST_Car.CarID == 0)
        //    {
        //        DataTable dt = dalCAR.PR_MST_Car_Insert(modelMST_Car);
        //        TempData["DealerInsertMsg"] = "Record Inserted Succesfully";
        //    }
        //    else
        //    {
        //        DataTable dt = dalCAR.PR_MST_Car_UpdateByPK(modelMST_Car);
        //        TempData["DealerInsertMsg"] = "Record Updated Succesfully";
        //    }

        //    return RedirectToAction("Index");

        //    #endregion
        //}

        public IActionResult Save(MST_CarModel modelMST_Car, List<string> FeatureNames, List<string> NewFeatureNames, List<string> FuelTypeNames, List<string> TransmissionTypeNames, List<int> VariantNames)
        {
            if (modelMST_Car.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelMST_Car.File.FileName);
                //modelMST_Car.PhotoPath = "~" + FilePath.Replace("wwwroot\\","/") + "/" + modelMST_Car.File.FileName;
                modelMST_Car.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_Car.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_Car.File.CopyTo(stream);
                }
            }
            try
            {
                string connectionString = Configuration.GetConnectionString("MyConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

                if (modelMST_Car.CarID == null || modelMST_Car.CarID == 0)
                {
                    // Insert new record
                    int carID = 0;
                    dalCAR.PR_MST_Car_Insert(modelMST_Car, out carID);
                    TempData["CarInsertMsg"] = "Record Inserted Successfully";

                    // Fetch the CarID after inserting the record
                    modelMST_Car.CarID = carID;

                    #region Feature Add
                    // Insert the FeatureNames into a separate table
                    foreach (string featureName in FeatureNames)
                    {
                        CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                        {
                            CarID = modelMST_Car.CarID,
                            FeatureName = featureName
                        };
                        dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                    }
                    #endregion

                    #region Feature Add
                    // Insert the FeatureNames into a separate table
                    foreach (string featureName in NewFeatureNames)
                    {
                        CAR_CarWiseFeatureModel newFeature = new CAR_CarWiseFeatureModel
                        {
                            CarID = modelMST_Car.CarID,
                            FeatureName = featureName
                        };
                        CAR_FeatureModel addFeature = new CAR_FeatureModel
                        {
                            FeatureName = featureName
                        };
                        dalCAR.PR_CAR_CarWiseFeature_Insert(newFeature);
                        dalCAR.PR_CAR_Feature_Insert(addFeature);
                    }
                    #endregion

                    #region FuelType Add
                    // Insert the FuelTypes into a separate table
                    foreach (string fuelType in FuelTypeNames)
                    {
                        CAR_CarWiseFuelTypeModel newFuelType = new CAR_CarWiseFuelTypeModel
                        {
                            CarID = modelMST_Car.CarID,
                            FuelTypeName = fuelType
                        };
                        dalCAR.PR_CAR_CarWiseFuelType_Insert(newFuelType);
                    }
                    #endregion

                    #region TransmissionType Add
                    // Insert the TransmissionTypes into a separate table
                    foreach (string TransmissionType in TransmissionTypeNames)
                    {
                        CAR_CarWiseTransmissionTypeModel newTransmissionType = new CAR_CarWiseTransmissionTypeModel
                        {
                            CarID = modelMST_Car.CarID,
                            TransmissionTypeName = TransmissionType
                        };
                        dalCAR.PR_CAR_CarWiseTransmissionType_Insert(newTransmissionType);
                    }
                    #endregion

                    #region Variant Add
                    // Insert the Variants into a separate table
                    foreach (int Variant in VariantNames)
                    {
                        CAR_CarWiseVariantModel newVariant = new CAR_CarWiseVariantModel
                        {
                            CarID = modelMST_Car.CarID,
                            VariantID = Variant
                        };
                        dalCAR.PR_CAR_CarWiseVariant_Insert(newVariant);
                    }
                    #endregion

                }
                else
                {
                    // Update existing record
                    DataTable dt = dalCAR.PR_MST_Car_UpdateByPK(modelMST_Car);
                    TempData["DealerInsertMsg"] = "Record Updated Successfully";

                    // Update the FeatureNames in the separate table
                    // You can implement the update logic here based on your requirements
                }
            }
            catch (Exception ex)
            {
                TempData["DealerInsertMsg"] = "Error occurred while saving record. " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Add(int? CarID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
                CAR_DAL dalCAR = new CAR_DAL();

            #region makeDropdown
            List<CAR_MakeDropDownModel> makeList = dalCAR.PR_CAR_Make_DropDown();
            ViewBag.MakeList = makeList;
            #endregion

            #region TypeDropdown
            List<CAR_TypeDropDownModel> TypeList = dalCAR.PR_CAR_Type_DropDown();
            ViewBag.TypeList = TypeList;
            #endregion

            #region FuelTypeDropdown
            List<CAR_FuelTypeDropDownModel> FuelTypeList = dalCAR.PR_CAR_FuelType_DropDown();
            ViewBag.FuelTypeList = FuelTypeList;
            #endregion

            #region TransmissionTypeDropdown
            List<CAR_TransmissionTypeDropDownModel> TransmissionTypeList = dalCAR.PR_CAR_TransmissionType_DropDown();
            ViewBag.TransmissionTypeList = TransmissionTypeList;
            #endregion

            #region FeatureDropdown
            List<CAR_FeatureDropDownModel> FeatureList = dalCAR.PR_CAR_Feature_DropDown();
            ViewBag.FeatureList = FeatureList;
            #endregion

            #region VariantDropdown
            List<CAR_VariantDropDownModel> list1 = new List<CAR_VariantDropDownModel>();
            ViewBag.VariantList = list1;
            #endregion

            #region SelectByPK
            if (CarID != null)
            {
                //string str = Configuration.GetConnectionString("myConnectionString");

                SqlDatabase sqlDB = new SqlDatabase(str);
                DataTable dt = dalCAR.dbo_PR_MST_Car_SelectByPK(CarID);
                MST_CarModel modelMST_Car = new MST_CarModel();



                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_Car.CarID = Convert.ToInt32(dr["CarID"]);
                    modelMST_Car.TypeID = Convert.ToInt32(dr["TypeID"]);
                    modelMST_Car.MakeID = Convert.ToInt32(dr["MakeID"]);
                    modelMST_Car.Name = dr["Name"].ToString();
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

        public IActionResult DropDownByMake(int MakeID, List<CAR_VariantDropDownModel> Variant_list)
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
                Variant_list.Add(vlst1);
            }

            var vModel = Variant_list;
            return Json(vModel);
        }

    }
}
