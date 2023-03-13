using System.Data;
using System.Data.SqlClient;
using System;

namespace FreshMVC.Component
{
    public class MerchantGeneralDB
    {
        public static DataSet GetMerchantListing(string storeProcedure, int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand(storeProcedure, sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pPages = sqlComm.Parameters.Add("@pages", SqlDbType.Int);
            pPages.Direction = ParameterDirection.Output;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            pages = (int)pPages.Value;
            sqlConn.Close();

            return ds;
        }

        public static DataSet GetReceiptListingByUsername(string username, int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetReceiptListingByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pPages = sqlComm.Parameters.Add("@pages", SqlDbType.Int);
            pPages.Direction = ParameterDirection.Output;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            pages = (int)pPages.Value;
            sqlConn.Close();

            return ds;
        }

        public static void InsertSMSFullLog(string url, string vendor, string key, string phone, string countryCode)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_InsertSMSFullLog", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUrl = sqlComm.Parameters.Add("@url", SqlDbType.NVarChar, 3000);
            pUrl.Direction = ParameterDirection.Input;
            pUrl.Value = url;

            SqlParameter pVendor = sqlComm.Parameters.Add("@vendor", SqlDbType.NVarChar, 100);
            pVendor.Direction = ParameterDirection.Input;
            pVendor.Value = vendor;

            SqlParameter pKey = sqlComm.Parameters.Add("@key", SqlDbType.NVarChar, 100);
            pKey.Direction = ParameterDirection.Input;
            pKey.Value = key;

            SqlParameter pPhone = sqlComm.Parameters.Add("@phone", SqlDbType.NVarChar, 100);
            pPhone.Direction = ParameterDirection.Input;
            pPhone.Value = phone;

            SqlParameter pCountryCode = sqlComm.Parameters.Add("@countryCode", SqlDbType.NVarChar, 100);
            pCountryCode.Direction = ParameterDirection.Input;
            pCountryCode.Value = countryCode;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }

        #region GetRedPacketListingByUsername
        public static DataSet GetRedPacketListingByUsername(string username)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetRedPacketListingByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region RedPacketWalletOperation
        public static DataSet RedPacketWalletOperation(string username, string cashName, decimal cashNum, string appuser = "", decimal appnumber = 0, int apprate = 0, string appother = "", int status = 0)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_RedPacketWalletOperation", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pCashName = sqlComm.Parameters.Add("@cashName", SqlDbType.VarChar, 100);
            pCashName.Direction = ParameterDirection.Input;
            pCashName.Value = cashName;

            SqlParameter pCashNum = sqlComm.Parameters.Add("@cashNum", SqlDbType.Decimal);
            pCashNum.Precision = 14;
            pCashNum.Scale = 2;
            pCashNum.Direction = ParameterDirection.Input;
            pCashNum.Value = cashNum;

            SqlParameter pAppUser = sqlComm.Parameters.Add("@appuser", SqlDbType.VarChar, 200);
            pAppUser.Direction = ParameterDirection.Input;
            pAppUser.Value = appuser;

            SqlParameter pAppNumber = sqlComm.Parameters.Add("@appnumber", SqlDbType.Decimal);
            pAppNumber.Precision = 14;
            pAppNumber.Scale = 2;
            pAppNumber.Direction = ParameterDirection.Input;
            pAppNumber.Value = appnumber;

            SqlParameter pAppRate = sqlComm.Parameters.Add("@apprate", SqlDbType.Int);
            pAppRate.Direction = ParameterDirection.Input;
            pAppRate.Value = apprate;

            SqlParameter pAppOther = sqlComm.Parameters.Add("@appother", SqlDbType.VarChar, 200);
            pAppOther.Direction = ParameterDirection.Input;
            pAppOther.Value = appother;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);


            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetRedPacketClaimListingByUsername
        public static DataSet GetRedPacketClaimListingByUsername(string username)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetRedPacketClaimListingByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region CashWalletOperation
        public static DataSet CashWalletOperation(string username, string cashName, decimal cashNum, string appuser = "", decimal appnumber = 0, int apprate = 0, string appother = "", int status = 0)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_CashWalletOperation", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pCashName = sqlComm.Parameters.Add("@cashName", SqlDbType.VarChar, 100);
            pCashName.Direction = ParameterDirection.Input;
            pCashName.Value = cashName;

            SqlParameter pCashNum = sqlComm.Parameters.Add("@cashNum", SqlDbType.Decimal);
            pCashNum.Precision = 14;
            pCashNum.Scale = 2;
            pCashNum.Direction = ParameterDirection.Input;
            pCashNum.Value = cashNum;

            SqlParameter pAppUser = sqlComm.Parameters.Add("@appuser", SqlDbType.VarChar, 200);
            pAppUser.Direction = ParameterDirection.Input;
            pAppUser.Value = appuser;

            SqlParameter pAppNumber = sqlComm.Parameters.Add("@appnumber", SqlDbType.Decimal);
            pAppNumber.Precision = 14;
            pAppNumber.Scale = 2;
            pAppNumber.Direction = ParameterDirection.Input;
            pAppNumber.Value = appnumber;

            SqlParameter pAppRate = sqlComm.Parameters.Add("@apprate", SqlDbType.Int);
            pAppRate.Direction = ParameterDirection.Input;
            pAppRate.Value = apprate;

            SqlParameter pAppOther = sqlComm.Parameters.Add("@appother", SqlDbType.VarChar, 200);
            pAppOther.Direction = ParameterDirection.Input;
            pAppOther.Value = appother;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);


            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetSponsorTreeByUsername
        public static DataSet GetSponsorTreeByUsername(string username)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetSponsorTreeByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion
    }
}
