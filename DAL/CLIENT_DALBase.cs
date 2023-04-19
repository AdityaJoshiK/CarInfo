using CarInfo.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using CarInfo.Models;

namespace CarInfo.DAL
{
    public class CLIENT_DALBase:DALHelper
    {
        #region AllCars

        public DataTable PR_Client_AllCars()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_AllCars");
                //sqldb.AddInParameter(dbCMD, "CarID", SqlDbType.Int, CarID);

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

        #region SelectRecentCars

        public DataTable PR_Client_SelectRecentCars()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_SelectRecentCars");
                //sqldb.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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

        #region CarDetail

        public DataTable PR_Client_Car_Detail(int CarID)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_CLIENT_Car_Detail");
                sqldb.AddInParameter(dbCMD, "CarID", SqlDbType.Int, CarID);

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

        #region Category

        public DataTable PR_Client_Car_Categories()
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_Car_Categories");
                //sqldb.AddInParameter(dbCMD, "CarID", SqlDbType.Int, CarID);

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

        #region CarByType

        public DataTable PR_Client_CarByType(int TypeID)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_CarByType");
                sqldb.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, TypeID);

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

        #region CarByFuelType

        public DataTable PR_Client_CarByFuelType(string FuelTypeName)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_CarByFuelType");
                sqldb.AddInParameter(dbCMD, "FuelTypeName", SqlDbType.NVarChar, FuelTypeName);

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

        #region CarByTransmissionType

        public DataTable PR_Client_CarByTransmissionType(string TransmissionTypeName)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_CarByTransmissionType");
                sqldb.AddInParameter(dbCMD, "TransmissionTypeName   ", SqlDbType.NVarChar, TransmissionTypeName);

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

        #region Filter

        public DataTable PR_Client_Filter(CLIENT_Model model)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(connectionStr);
                DbCommand dbCMD = sqldb.GetStoredProcCommand("PR_Client_Filter");
                sqldb.AddInParameter(dbCMD, "FuelTypeName", SqlDbType.NVarChar, model.FuelTypeName);
                sqldb.AddInParameter(dbCMD, "TransmissionTypeName", SqlDbType.NVarChar, model.TransmissionTypeName);
                sqldb.AddInParameter(dbCMD, "TypeID", SqlDbType.Int, model.TypeID);
                sqldb.AddInParameter(dbCMD, "MakeID", SqlDbType.Int, model.MakeID);
                sqldb.AddInParameter(dbCMD, "CarID", SqlDbType.Int, model.CarID);


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
