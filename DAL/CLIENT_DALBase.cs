using CarInfo.BAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace CarInfo.DAL
{
    public class CLIENT_DALBase:DALHelper
    {
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

        #region CarType

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
    }
}
