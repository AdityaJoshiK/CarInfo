using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using CarInfo.BAL;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Review.Models;
using CarInfo.Areas.CAR_CarWiseFuelType.Models;
using CarInfo.Areas.CAR_Feature.Models;
using CarInfo.Areas.CAR_Type.Models;
using CarInfo.Areas.CAR_TransmissionType.Models;
using CarInfo.Areas.CAR_Variant.Models;
using CarInfo.Areas.CAR_Dealer.Models;
using System.Reflection;
using CarInfo.Areas.MST_Car.Models;
using System.Data.SqlClient;
using CarInfo.Areas.CAR_Image.Models;
using CarInfo.Areas.CAR_FuelType.Models;
using CAR_CarWiseFuelTypeDropDownModel = CarInfo.Areas.CAR_CarWiseFuelType.Models.CAR_CarWiseFuelTypeDropDownModel;
using CarInfo.Areas.CAR_CarWiseTransmissionType.Models;
using CarInfo.Areas.CAR_CarWiseVariant.Models;
using CarInfo.Areas.CAR_CarWiseFeature.Models;

namespace CarInfo.DAL
{
    public class CAR_DALBase:DALHelper
    {
        #region CAR_Make

        #region PR_CAR_Make_SelctAll & Filter

        public DataTable PR_CAR_Make_SelectAll(CAR_MakeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Make_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.MakeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Make_SelectByMakeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.MakeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeName", DbType.String, model.MakeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeName", DbType.String, DBNull.Value);
                    }
                }

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
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);

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
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);


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

        public DataTable PR_CAR_Feature_SelectAll(CAR_FeatureModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Feature_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if(model.FeatureName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Feature_SelectByCarIDFeatureName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.FeatureName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@FeatureName", DbType.String, model.FeatureName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@FeatureName", DbType.String, DBNull.Value);
                    }
                }

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

        #region CAR_CarWiseFeature

        #region PR_CAR_CarWiseFeature_SelctAll & Filter

        public DataTable PR_CAR_CarWiseFeature_SelectAll(CAR_CarWiseFeatureModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseFeature_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.CarID != null || model.FeatureName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseFeature_SelectByCarIDFeatureName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.CarID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, model.CarID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, DBNull.Value);
                    }

                    if (model.FeatureName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@FeatureName", DbType.String, model.FeatureName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@FeatureName", DbType.String, DBNull.Value);
                    }
                }

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

        #region PR_CAR_CarWiseFeature_Insert

        public DataTable PR_CAR_CarWiseFeature_Insert(CAR_CarWiseFeatureModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFeature_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, model.VariantID);
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

        #region PR_CAR_CarWiseFeature_SelectByPK

        public DataTable dbo_PR_CAR_CarWiseFeature_SelectByPK(string conn, int? FeatureID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFeature_SelectByPK");

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

        #region PR_CAR_CarWiseFeature_UpdateByPK

        public DataTable PR_CAR_CarWiseFeature_UpdateByPK(CAR_CarWiseFeatureModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFeature_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "FeatureID", SqlDbType.Int, model.FeatureID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, model.VariantID);
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

        #region PR_CAR_CarWiseFeature_DeleteByPK

        public DataTable PR_CAR_CarWiseFeature_DeleteByPK(int FeatureID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFeature_DeleteByPK");
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

        #region CAR_CarWiseFuelType
        #region PR_CAR_CarWiseFuelType_SelctAll & Filter

        public DataTable PR_CAR_CarWiseFuelType_SelectAll(CAR_CarWiseFuelTypeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseFuelType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.FuelTypeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseFuelType_SelectByFuelTypeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.FuelTypeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@FuelTypeName", DbType.String, model.FuelTypeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@FuelTypeName", DbType.String, DBNull.Value);
                    }
                }

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

        #region PR_CAR_CarWiseFuelType_Insert

        public DataTable PR_CAR_CarWiseFuelType_Insert(CAR_CarWiseFuelTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFuelType_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
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

        #region PR_CAR_CarWiseFuelType_SelectByPK

        public DataTable dbo_PR_CAR_CarWiseFuelType_SelectByPK(int? FuelTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFuelType_SelectByPK");

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

        #region PR_CAR_CarWiseFuelType_UpdateByPK

        public DataTable PR_CAR_CarWiseFuelType_UpdateByPK(CAR_CarWiseFuelTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFuelType_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
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

        #region PR_CAR_CarWiseFuelType_DeleteByPK

        public DataTable PR_CAR_CarWiseFuelType_DeleteByPK(int FuelTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseFuelType_DeleteByPK");
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

        #region CAR_FuelType
        #region PR_CAR_FuelType_SelctAll & Filter

        public DataTable PR_CAR_FuelType_SelectAll(CAR_FuelTypeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_FuelType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.FuelTypeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_FuelType_SelectByFuelTypeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.FuelTypeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@FuelTypeName", DbType.String, model.FuelTypeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@FuelTypeName", DbType.String, DBNull.Value);
                    }
                }

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

        public DataTable PR_CAR_Type_SelectAll(CAR_TypeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Type_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.TypeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Type_SelectByTypeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.TypeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@TypeName", DbType.String, model.TypeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@TypeName", DbType.String, DBNull.Value);
                    }
                }

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

        public DataTable PR_CAR_Review_SelectAll(CAR_ReviewModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Review_SelectAll");

                //sqldb.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());

                if (model.CarID != null || model.ReviewText != null || model.Rating != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Review_SelectByCarIDReviewTextRating");
                    sqldb.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());

                    if (model.CarID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, model.CarID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, DBNull.Value);
                    }

                    if (model.ReviewText != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@ReviewText", DbType.String, model.ReviewText);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@ReviewText", DbType.String, DBNull.Value);
                    }
                    if (model.Rating != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@Rating", DbType.String, model.Rating);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@Rating", DbType.String, DBNull.Value);
                    }
                }

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
                sqlDB.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());
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

                sqlDB.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());

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
                sqlDB.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());
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
                sqlDB.AddInParameter(dbCMD, "ClientID", SqlDbType.Int, ClientCV.ClientID());

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

        public DataTable PR_CAR_Dealer_SelectAll(CAR_DealerModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Dealer_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.MakeID != null || model.Name != null || model.Country != null || model.State != null || model.City != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Dealer_SelectByMakeIDNameCountryStateCity");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.MakeID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeID", DbType.Int32, model.MakeID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeID", DbType.Int32, DBNull.Value);
                    }

                    if (model.Name != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@Name", DbType.String, model.Name);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@Name", DbType.String, DBNull.Value);
                    }

                    if (model.Country != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@Country", DbType.String, model.Country);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@Country", DbType.String, DBNull.Value);
                    }

                    if (model.State != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@State", DbType.String, model.State);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@State", DbType.String, DBNull.Value);
                    }

                    if (model.City != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@City", DbType.String, model.City);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@City", DbType.String, DBNull.Value);
                    }
                }

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

        public DataTable PR_CAR_Variant_SelectAll(CAR_VariantModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Variant_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.MakeID != null || model.VariantName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Variant_SelectByMakeIDVariantName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.MakeID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeID", DbType.Int32, model.MakeID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@MakeID", DbType.Int32, DBNull.Value);
                    }

                    if (model.VariantName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@VariantName", DbType.String, model.VariantName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@VariantName", DbType.String, DBNull.Value);
                    }
                }

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
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
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
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
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

        #region CAR_CarWiseVariant

        #region PR_CAR_CarWiseVariant_SelctAll & Filter

        public DataTable PR_CAR_CarWiseVariant_SelectAll(CAR_CarWiseVariantModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseVariant_SelectAll");

                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.CarID != null || model.CarWiseVariantID != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseVariant_SelectByCarIDVariantName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.CarID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, model.CarID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, DBNull.Value);
                    }

                    if (model.CarWiseVariantID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarWiseVariantID", DbType.String, model.CarWiseVariantID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarWiseVariantID", DbType.String, DBNull.Value);
                    }
                }

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
        #region PR_CAR_CarWiseVariant_Insert

        public DataTable PR_CAR_CarWiseVariant_Insert(CAR_CarWiseVariantModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseVariant_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, model.VariantID);
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

        #region PR_CAR_CarWiseVariant_SelectByPK

        public DataTable dbo_PR_CAR_CarWiseVariant_SelectByPK(string conn, int? CarWiseVariantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseVariant_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "CarWiseVariantID", SqlDbType.Int, CarWiseVariantID);


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

        #region PR_CAR_CarWiseVariant_UpdateByPK

        public DataTable PR_CAR_CarWiseVariant_UpdateByPK(CAR_CarWiseVariantModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseVariant_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarWiseVariantID", SqlDbType.Int, model.CarWiseVariantID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "VariantID", SqlDbType.Int, model.VariantID);
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

        #region PR_CAR_CarWiseVariant_DeleteByPK

        public DataTable PR_CAR_CarWiseVariant_DeleteByPK(int CarWiseVariantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseVariant_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CarWiseVariantID", SqlDbType.Int, CarWiseVariantID);
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

        public DataTable PR_CAR_TransmissionType_SelectAll(CAR_TransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_TransmissionType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.TransmissionTypeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_TransmissionType_SelectByTransmissionTypeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.TransmissionTypeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@TransmissionTypeName", DbType.String, model.TransmissionTypeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@TransmissionTypeName", DbType.String, DBNull.Value);
                    }
                }

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

        #region CAR_CarWiseTransmissionType

        #region PR_CAR_CarWiseTransmissionType_SelctAll & Filter

        public DataTable PR_CAR_CarWiseTransmissionType_SelectAll(CAR_CarWiseTransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.CarID != null || model.TransmissionTypeName != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_SelectByTransmissionTypeName");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.CarID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, model.CarID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, DBNull.Value);
                    }

                    if (model.TransmissionTypeName != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@TransmissionTypeName", DbType.String, model.TransmissionTypeName);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@TransmissionTypeName", DbType.String, DBNull.Value);
                    }
                }

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

        #region PR_CAR_CarWiseTransmissionType_Insert

        public DataTable PR_CAR_CarWiseTransmissionType_Insert(CAR_CarWiseTransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
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

        #region PR_CAR_CarWiseTransmissionType_SelectByPK

        public DataTable dbo_PR_CAR_CarWiseTransmissionType_SelectByPK(int? TransmissionTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "TransmissionTypeID", SqlDbType.Int, TransmissionTypeID);


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

        #region PR_CAR_CarWiseTransmissionType_UpdateByPK

        public DataTable PR_CAR_CarWiseTransmissionType_UpdateByPK(CAR_CarWiseTransmissionTypeModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
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

        #region PR_CAR_CarWiseTransmissionType_DeleteByPK

        public DataTable PR_CAR_CarWiseTransmissionType_DeleteByPK(int TransmissionTypeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_CarWiseTransmissionType_DeleteByPK");
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

        //public DataTable PR_MST_Car_Insert(MST_CarModel model)
        //{
        //    try
        //    {
        //        SqlDatabase sqlDB = new SqlDatabase(connectionStr);
        //        DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_Insert");
        //        sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
        //        sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
        //        sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, model.TypeID);
        //        sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
        //        sqlDB.AddInParameter(dbCMD, "Year", SqlDbType.Int, model.Year);
        //        sqlDB.AddInParameter(dbCMD, "Price", SqlDbType.Decimal, model.Price);

        //        DataTable dt = new DataTable();

        //        using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
        //        {
        //            dt.Load(dr);

        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;

        //    }
        //}

        public DataTable PR_MST_Car_Insert(MST_CarModel model, out int carID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Car_InsertCheck");
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, model.TypeID);
                //sqlDB.AddInParameter(dbCMD, "FuelTypeID", SqlDbType.Int, model.TypeID);

                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "Year", SqlDbType.Int, model.Year);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);

                SqlParameter carIDParam = new SqlParameter("@CarID", SqlDbType.Int);
                carIDParam.Direction = ParameterDirection.Output;
                dbCMD.Parameters.Add(carIDParam); // Add the output parameter

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                // Retrieve the output parameter value
                carID = Convert.ToInt32(dbCMD.Parameters["@CarID"].Value);

                return dt;
            }
            catch (Exception ex)
            {
                // Handle exception as needed
                carID = 0; // Set carID to default value
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
                sqlDB.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, model.TypeID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqlDB.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, model.Name);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "Year", SqlDbType.Int, model.Year);


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

        #region CAR_Image
        #region PR_CAR_Image_SelctAll & Filter

        public DataTable PR_CAR_Image_SelectAll(CAR_ImageModel model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Image_SelectAll");
                sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                if (model.CarID != null)
                {
                    dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Image_SelectByCarID");
                    sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (model.CarID != null)
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, model.CarID);
                    }
                    else
                    {
                        sqldb.AddInParameter(dbCMD, "@CarID", DbType.Int32, DBNull.Value);
                    }
                }

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

        #region PR_CAR_Image_Insert

        public DataTable PR_CAR_Image_Insert(CAR_ImageModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Image_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);

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

        #region PR_CAR_Image_SelectByPK

        public DataTable dbo_PR_CAR_Image_SelectByPK(string conn, int? ImageID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Image_SelectByPK");

                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                sqlDB.AddInParameter(dbCMD, "ImageID", SqlDbType.Int, ImageID);


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

        #region PR_CAR_Image_UpdateByPK

        public DataTable PR_CAR_Image_UpdateByPK(CAR_ImageModel model)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Image_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ImageID", SqlDbType.Int, model.ImageID);
                sqlDB.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, model.PhotoPath);


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

        #region PR_CAR_Image_DeleteByPK

        public DataTable PR_CAR_Image_DeleteByPK(int ImageID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Image_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ImageID", SqlDbType.Int, ImageID);
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

        #region CAR_TypeDropDown

        public List<CAR_TypeDropDownModel> PR_CAR_Type_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Type_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_TypeDropDownModel> list = new List<CAR_TypeDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_TypeDropDownModel vlst = new CAR_TypeDropDownModel();
                    vlst.TypeID = Convert.ToInt32(dr["TypeID"]);
                    vlst.TypeName = dr["TypeName"].ToString();
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

        #region CAR_FuelTypeDropDown

        public List<CAR_FuelTypeDropDownModel> PR_CAR_FuelType_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_FuelType_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_FuelTypeDropDownModel> list = new List<CAR_FuelTypeDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_FuelTypeDropDownModel vlst = new CAR_FuelTypeDropDownModel();
                    vlst.FuelTypeID = Convert.ToInt32(dr["FuelTypeID"]);
                    vlst.FuelTypeName = dr["FuelTypeName"].ToString();
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

        #region CAR_TransmissionTypeDropDown

        public List<CAR_TransmissionTypeDropDownModel> PR_CAR_TransmissionType_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_TransmissionType_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_TransmissionTypeDropDownModel> list = new List<CAR_TransmissionTypeDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_TransmissionTypeDropDownModel vlst = new CAR_TransmissionTypeDropDownModel();
                    vlst.TransmissionTypeID = Convert.ToInt32(dr["TransmissionTypeID"]);
                    vlst.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
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
                    car.Name = dr["Name"].ToString();
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

        #region CAR_VariantDropDown

        public List<CAR_VariantDropDownModel> PR_CAR_Variant_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Variant_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_VariantDropDownModel> list = new List<CAR_VariantDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_VariantDropDownModel vlst = new CAR_VariantDropDownModel();
                    vlst.VariantID = Convert.ToInt32(dr["VariantID"]);
                    vlst.VariantName = dr["VariantName"].ToString();
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

        #region CAR_FeatureDropDown

        public List<CAR_FeatureDropDownModel> PR_CAR_Feature_DropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Feature_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                List<CAR_FeatureDropDownModel> list = new List<CAR_FeatureDropDownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CAR_FeatureDropDownModel vlst = new CAR_FeatureDropDownModel();
                    vlst.FeatureID = Convert.ToInt32(dr["FeatureID"]);
                    vlst.FeatureName = dr["FeatureName"].ToString();
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

        #region PR_MST_Car_TotalRecords

        public int PR_MST_Car_TotalRecords()
        {
            int totalRecords = 0;
            string connectionString = connectionStr; // Replace with your actual connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("PR_MST_Car_TotalRecords", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                        }
                    }
                }
            }

            return totalRecords;
        }

        #endregion

        #endregion
    }
}
