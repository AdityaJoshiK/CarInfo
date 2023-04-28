using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace CarInfo.DAL
{
    public class SEC_DALBase
    {
        #region Method: dbo_PR_MST_User_SelectByPK
        public DataTable dbo_PR_MST_User_SelectByPK(string ConnStr, int? UserID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "UserId", SqlDbType.Int, UserID);

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

        #region Method: PR_MST_User_SelectByUserNamePassword
        public DataTable PR_MST_User_SelectByUserNamePassword(string ConnStr, string? UserName, string? Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_User_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

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

        #region Method: PR_MST_Client_SelectByUserNamePassword
        public DataTable PR_MST_Client_SelectByUserNamePassword(string ConnStr, string? Email, string? Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_Client_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

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

        #region Method: dbo_PR_MST_User_Insert
        public decimal? dbo_PR_MST_User_Insert(string ConnStr, string UserName, string Password, string FirstName, string LastName, string EmailAddress, string PhotoPath, DateTime? Created, DateTime? Modified)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_User_Insert");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);
                sqlDB.AddInParameter(dbCMD, "FirstName", SqlDbType.VarChar, FirstName);
                sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "EmailAddress", SqlDbType.VarChar, EmailAddress);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, Created);
                sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, Modified);

                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_MST_Client_Insert
        public DataTable? dbo_PR_MST_Client_Insert(string ConnStr, string Email, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_Client_Insert");
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);
                //sqlDB.AddInParameter(dbCMD, "FirstName", SqlDbType.VarChar, FirstName);
                //sqlDB.AddInParameter(dbCMD, "LastName", SqlDbType.VarChar, LastName);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                //sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                //sqlDB.AddInParameter(dbCMD, "Created", SqlDbType.DateTime, Created);
                //sqlDB.AddInParameter(dbCMD, "Modified", SqlDbType.DateTime, Modified);

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
    }
}
