using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using CarInfo.BAL;
using CarInfo.Areas.CAR_Make.Models;
using CarInfo.Areas.CAR_Review.Models;
using CarInfo.Areas.CAR_FuelType.Models;

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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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

        public DataTable dbo_PR_CAR_Make_SelectByPK(string conn, int? MakeID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CAR_Make_SelectByPK");

                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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

        #region PR_CAR_Variant_SelctAll & Filter

        public DataTable PR_CAR_Variant_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Variant_SelectAll");

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

        #region PR_MST_Car_SelctAll & Filter

        public DataTable PR_MST_Car_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_MST_Car_SelectAll");

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
        
        #region PR_CAR_Feature_SelctAll & Filter

        public DataTable PR_CAR_Feature_SelectAll(string FeatureName = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Feature_SelectAll");

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

        #region PR_CAR_Type_SelctAll & Filter

        public DataTable PR_CAR_Type_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Type_SelectAll");

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

        #region PR_CAR_TransmissionType_SelctAll & Filter

        public DataTable PR_CAR_TransmissionType_SelectAll(string FeatureName = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_TransmissionType_SelectAll");

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

        #region CAR_Review
        #region PR_CAR_Review_SelctAll & Filter

        public DataTable PR_CAR_Review_SelectAll()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Review_SelectAll");

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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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

                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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
                //sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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

        #region PR_CAR_Dealer_SelctAll & Filter

        public DataTable PR_CAR_Dealer_SelectAll(string DealerName = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CAR_Dealer_SelectAll");

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

       
    }
}
