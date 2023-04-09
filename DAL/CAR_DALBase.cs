using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using CarInfo.BAL;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Review.Models;
using CarInfo.Areas.CAR_FuelType.Models;
using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.CAR_Type.Models;
using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.Areas.CAR_Dealer.Models;
using System.Reflection;
using CarInfo.Areas.MST_Car.Models;
using System.Data.SqlClient;

namespace CarInfo.DAL
{
    public class CAR_DALBase:DALHelper
    {
        #region CAR_Make

        #region PR_CAR_Make_SelctAll & Filter

        public DataTable PR_CAR_Make_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Make_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_Make_Insert

        public DataTable PR_CAR_Make_Insert(CAR_MakeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeName", SqlDbType.NVarChar, model.MakeName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Make_SelectByPK

        public DataTable dbo_PR_CAR_Make_SelectByPK(int? MakeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, MakeID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Make_UpdateByPK

        public DataTable PR_CAR_Make_UpdateByPK(CAR_MakeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "MakeName", SqlDbType.NVarChar, model.MakeName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Make_DeleteByPK

        public DataTable PR_CAR_Make_DeleteByPK(int MakeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, MakeID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region CAR_Feature
        #region PR_CAR_Feature_SelctAll & Filter

        public DataTable PR_CAR_Feature_SelectAll(string FeatureName = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Feature_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_Feature_Insert

        public DataTable PR_CAR_Feature_Insert(CAR_FeatureModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Feature_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "FeatureName", SqlDbType.NVarChar, model.FeatureName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Feature_SelectByPK

        public DataTable dbo_PR_CAR_Feature_SelectByPK(string conn, int? FeatureID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Feature_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "FeatureID", SqlDbType.Int, FeatureID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Feature_UpdateByPK

        public DataTable PR_CAR_Feature_UpdateByPK(CAR_FeatureModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Feature_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "FeatureID", SqlDbType.Int, model.FeatureID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "FeatureName", SqlDbType.NVarChar, model.FeatureName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Feature_DeleteByPK

        public DataTable PR_CAR_Feature_DeleteByPK(int FeatureID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Feature_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "FeatureID", SqlDbType.Int, FeatureID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region CAR_FuelType
        #region PR_CAR_FuelType_SelctAll & Filter

        public DataTable PR_CAR_FuelType_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_FuelType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_FuelType_Insert

        public DataTable PR_CAR_FuelType_Insert(CAR_FuelTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_FuelType_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "FuelTypeName", SqlDbType.NVarChar, model.FuelTypeName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_FuelType_SelectByPK

        public DataTable dbo_PR_CAR_FuelType_SelectByPK(int? FuelTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_FuelType_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "FuelTypeID", SqlDbType.Int, FuelTypeID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_FuelType_UpdateByPK

        public DataTable PR_CAR_FuelType_UpdateByPK(CAR_FuelTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_FuelType_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "FuelTypeID", SqlDbType.Int, model.FuelTypeID);
                sqlDB.AddInParameter(dbCMD, "FuelTypeName", SqlDbType.NVarChar, model.FuelTypeName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_FuelType_DeleteByPK

        public DataTable PR_CAR_FuelType_DeleteByPK(int FuelTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_FuelType_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "FuelTypeID", SqlDbType.Int, FuelTypeID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region CAR_Type
        #region PR_CAR_Type_SelctAll & Filter

        public DataTable PR_CAR_Type_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Type_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_Type_Insert

        public DataTable PR_CAR_Type_Insert(CAR_TypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Type_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "TypeName", SqlDbType.NVarChar, model.TypeName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Type_SelectByPK

        public DataTable dbo_PR_CAR_Type_SelectByPK(int? TypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Type_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, TypeID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Type_UpdateByPK

        public DataTable PR_CAR_Type_UpdateByPK(CAR_TypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Type_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, model.TypeID);
                sqlDB.AddInParameter(dbCMD, "TypeName", SqlDbType.NVarChar, model.TypeName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Type_DeleteByPK

        public DataTable PR_CAR_Type_DeleteByPK(int TypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Type_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, TypeID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion
        #endregion

        #region CAR_Review
        #region PR_CAR_Review_SelctAll & Filter

        public DataTable PR_CAR_Review_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Review_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_Review_Insert

        public DataTable PR_CAR_Review_Insert(CAR_ReviewModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Review_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "ReviewText", SqlDbType.NVarChar, model.ReviewText);
                sqlDB.AddInParameter(dbCMD, "Rating", SqlDbType.Int, model.Rating);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Review_SelectByPK

        public DataTable dbo_PR_CAR_Review_SelectByPK(string conn, int? ReviewID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Review_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "ReviewID", SqlDbType.Int, ReviewID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Review_UpdateByPK

        public DataTable PR_CAR_Review_UpdateByPK(CAR_ReviewModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Review_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ReviewID", SqlDbType.Int, model.ReviewID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "ReviewText", SqlDbType.NVarChar, model.ReviewText);
                sqlDB.AddInParameter(dbCMD, "Rating", SqlDbType.Int, model.Rating);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Review_DeleteByPK

        public DataTable PR_CAR_Review_DeleteByPK(int ReviewID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Review_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ReviewID", SqlDbType.Int, ReviewID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region CAR_Dealer

        #region PR_CAR_Dealer_SelctAll & Filter

        public DataTable PR_CAR_Dealer_SelectAll(string DealerName = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Dealer_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_Dealer_Insert

        public DataTable PR_CAR_Dealer_Insert(CAR_DealerModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Dealer_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, model.Address);
                sqlDB.AddInParameter(dbCMD, "City", SqlDbType.NVarChar, model.City);
                sqlDB.AddInParameter(dbCMD, "State", SqlDbType.NVarChar, model.State);
                sqlDB.AddInParameter(dbCMD, "Country", SqlDbType.NVarChar, model.Country);
                sqlDB.AddInParameter(dbCMD, "Phone", SqlDbType.NVarChar, model.Phone);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Dealer_SelectByPK

        public DataTable dbo_PR_CAR_Dealer_SelectByPK(int? DealerID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Dealer_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "DealerID", SqlDbType.Int, DealerID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Dealer_UpdateByPK

        public DataTable PR_CAR_Dealer_UpdateByPK(CAR_DealerModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Dealer_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "DealerID", SqlDbType.Int, model.DealerID);
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, model.Address);
                sqlDB.AddInParameter(dbCMD, "City", SqlDbType.NVarChar, model.City);
                sqlDB.AddInParameter(dbCMD, "State", SqlDbType.NVarChar, model.State);
                sqlDB.AddInParameter(dbCMD, "Country", SqlDbType.NVarChar, model.Country);
                sqlDB.AddInParameter(dbCMD, "Phone", SqlDbType.NVarChar, model.Phone);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Dealer_DeleteByPK

        public DataTable PR_CAR_Dealer_DeleteByPK(int DealerID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Dealer_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "DealerID", SqlDbType.Int, DealerID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion
        #endregion

        #region CAR_Variant

        #region PR_CAR_Variant_SelctAll & Filter

        public DataTable PR_CAR_Variant_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Variant_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
        #region PR_CAR_Variant_Insert

        public DataTable PR_CAR_Variant_Insert(CAR_VariantModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Variant_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantName", SqlDbType.NVarChar, model.VariantName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Variant_SelectByPK

        public DataTable dbo_PR_CAR_Variant_SelectByPK(string conn, int? VariantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Variant_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, VariantID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_Variant_UpdateByPK

        public DataTable PR_CAR_Variant_UpdateByPK(CAR_VariantModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Variant_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, model.VariantID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantName", SqlDbType.NVarChar, model.VariantName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_Variant_DeleteByPK

        public DataTable PR_CAR_Variant_DeleteByPK(int VariantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Variant_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, VariantID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region CAR_TransmissionType

        #region PR_CAR_TransmissionType_SelctAll & Filter

        public DataTable PR_CAR_TransmissionType_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_TransmissionType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PR_CAR_TransmissionType_Insert

        public DataTable PR_CAR_TransmissionType_Insert(CAR_TransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_TransmissionType_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "TransmissionTypeName", SqlDbType.NVarChar, model.TransmissionTypeName);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_TransmissionType_SelectByPK

        public DataTable dbo_PR_CAR_TransmissionType_SelectByPK(int? TransmissionTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_TransmissionType_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "TransmissionTypeID", SqlDbType.Int,TransmissionTypeID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_CAR_TransmissionType_UpdateByPK

        public DataTable PR_CAR_TransmissionType_UpdateByPK(CAR_TransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_TransmissionType_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "TransmissionTypeID", SqlDbType.Int, model.TransmissionTypeID);
                sqlDB.AddInParameter(dbCMD, "TransmissionTypeName", SqlDbType.NVarChar, model.TransmissionTypeName);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_CAR_TransmissionType_DeleteByPK

        public DataTable PR_CAR_TransmissionType_DeleteByPK(int TransmissionTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_TransmissionType_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "TransmissionTypeID", SqlDbType.Int, TransmissionTypeID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region MST_Car

        #region PR_MST_Car_SelctAll & Filter

        public DataTable PR_MST_Car_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_MST_Car_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                using (IDataReader dr = sqldb.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion       

        #region PR_MST_Car_Insert

        public DataTable PR_MST_Car_Insert(MST_CarModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
                sqlDB.AddInParameter(dbCMD, "Year", SqlDbType.Int, model.Year);
                sqlDB.AddInParameter(dbCMD, "Price", SqlDbType.Decimal, model.Price);

                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception ex)
            {

                return null;

            }
        }

        #endregion

        #region PR_MST_Car_SelectByPK

        public DataTable dbo_PR_MST_Car_SelectByPK(int? CarID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, CarID);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }

        #endregion

        #region PR_MST_Car_UpdateByPK

        public DataTable PR_MST_Car_UpdateByPK(MST_CarModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
                sqlDB.AddInParameter(dbCMD, "Year", SqlDbType.Int, model.Year);
                sqlDB.AddInParameter(dbCMD, "Price", SqlDbType.Decimal, model.Price);


                DataTable dt = new DataTable();

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #region PR_MST_Car_DeleteByPK

        public DataTable PR_MST_Car_DeleteByPK(int CarID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, CarID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception e)
            {

                return null;

            }
        }
        #endregion

        #endregion

        #region Dropdowns

        #region CAR_MakeDropDown

        public List<CAR_MakeDropDownModel> PR_CAR_Make_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_MakeDropDownModel> list = new List<CAR_MakeDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_MakeDropDownModel vlst = new CAR_MakeDropDownModel();
                    vlst.MakeID = Convert.ToInt32(dr["MakeID"]);
                    vlst.MakeName = dr["MakeName"].ToString();
                    list.Add(vlst);
                }

                return list;
            }
            catch (Exception e)
            {
                // Handle exception
                return null;
            }
        }


        #endregion

        #region PR_MST_Car_DropDown
        public List<MST_CarDropDownModel> PR_MST_Car_DropDown()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionStr);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_Car_SelectForDropDown";
                //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = CV.UserID();
                DataTable dt = new DataTable();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                List<MST_CarDropDownModel> list = new List<MST_CarDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    MST_CarDropDownModel car = new MST_CarDropDownModel();
                    car.CarID = Convert.ToInt32(dr["CarID"]);
                    car.CarName = dr["Name"].ToString();
                    list.Add(car);
                }

                return list;
            }
            catch (Exception e)
            {
                // Handle exception as needed
                return null;
            }
        }
        #endregion

        #endregion
    }
}
