using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FreshMVC.Component
{
    public class AdminDB
    {
        public static void ClearAdminAccessRight(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_ClearRight", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }
        public static void UpdateAdminAccessRight(string Username, string UserAccess, bool isClear, string createdBy, string updatedBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_AccessRight", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pUserAccess = sqlComm.Parameters.Add("@UserAccess", SqlDbType.NVarChar, -1);
            pUserAccess.Direction = ParameterDirection.Input;
            pUserAccess.Value = UserAccess;

            var pClearData = sqlComm.Parameters.Add("@ClearData", SqlDbType.Bit);
            pClearData.Direction = ParameterDirection.Input;
            pClearData.Value = isClear;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pCreatedOn = sqlComm.Parameters.Add("@CreatedOn", SqlDbType.DateTime);
            pCreatedOn.Direction = ParameterDirection.Input;
            pCreatedOn.Value = DateTime.Now;

            SqlParameter pUpdatedBy = sqlComm.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50);
            pUpdatedBy.Direction = ParameterDirection.Input;
            pUpdatedBy.Value = updatedBy;

            SqlParameter pUpdatedOn = sqlComm.Parameters.Add("@UpdatedOn", SqlDbType.DateTime);
            pUpdatedOn.Direction = ParameterDirection.Input;
            pUpdatedOn.Value = DateTime.Now;

            SqlParameter pDeletionState = sqlComm.Parameters.Add("@DeletionState", SqlDbType.Bit);
            pDeletionState.Direction = ParameterDirection.Input;
            pDeletionState.Value = false;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetModulesAccessRight(out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetModulesAccessRight", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

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
            sqlConn.Close();

            return ds;
        }

        public static DataSet GetUserAccessRight(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetUserAccessRight", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

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
            sqlConn.Close();

            return ds;
        }

        public static DataSet TopupMerchantCashWallet(string Username, string cashName, decimal amount, string from, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_TopupMerchantCashWallet", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pCashName = sqlComm.Parameters.Add("@Cashname", SqlDbType.VarChar, 100);
            pCashName.Direction = ParameterDirection.Input;
            pCashName.Value = cashName;

            SqlParameter pFrom = sqlComm.Parameters.Add("@From", SqlDbType.VarChar, 100);
            pFrom.Direction = ParameterDirection.Input;
            pFrom.Value = from;

            SqlParameter pAmount = sqlComm.Parameters.Add("@Amount", SqlDbType.Decimal);
            pAmount.Direction = ParameterDirection.Input;
            pAmount.Value = amount;

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
            sqlConn.Close();

            return ds;
        }

        public static DataSet MerchantSubscribe(string Username, decimal amount, string from, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_MerchantSubscribe", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pFrom = sqlComm.Parameters.Add("@From", SqlDbType.VarChar, 100);
            pFrom.Direction = ParameterDirection.Input;
            pFrom.Value = from;

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
            sqlConn.Close();

            return ds;
        }

        public static void CashWalletOperation(string username, decimal netAmount, string type, int orderId, string paymentType, string referenceNo, string status, decimal serviceFee = 0, int bankId = 0, string actionBy = "SYS")
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_CashWalletOperation", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

            SqlParameter pCharge = sqlComm.Parameters.Add("@cashNum", SqlDbType.Decimal);
            pCharge.Direction = ParameterDirection.Input;
            pCharge.Value = netAmount;

            SqlParameter pType = sqlComm.Parameters.Add("@cashName", SqlDbType.NVarChar);
            pType.Direction = ParameterDirection.Input;
            pType.Value = type;

            SqlParameter pOthers = sqlComm.Parameters.Add("@appother", SqlDbType.NVarChar);
            pOthers.Direction = ParameterDirection.Input;
            pOthers.Value = referenceNo;

            SqlParameter pAppRate = sqlComm.Parameters.Add("@apprate", SqlDbType.Decimal);
            pAppRate.Direction = ParameterDirection.Input;
            pAppRate.Value = serviceFee;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.NVarChar);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            SqlParameter pBankId = sqlComm.Parameters.Add("@bankid", SqlDbType.NVarChar);
            pBankId.Direction = ParameterDirection.Input;
            pBankId.Value = bankId;

            SqlParameter pAction = sqlComm.Parameters.Add("@actionBy", SqlDbType.NVarChar);
            pAction.Direction = ParameterDirection.Input;
            pAction.Value = actionBy;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }

        public static void CashWalletOperationTemp(string username, decimal netAmount, string type, string referenceNo, int status)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_CashWalletOperationTemp", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

            SqlParameter pCharge = sqlComm.Parameters.Add("@cashNum", SqlDbType.Decimal);
            pCharge.Direction = ParameterDirection.Input;
            pCharge.Value = netAmount;

            SqlParameter pType = sqlComm.Parameters.Add("@cashName", SqlDbType.NVarChar);
            pType.Direction = ParameterDirection.Input;
            pType.Value = type;

            SqlParameter pOthers = sqlComm.Parameters.Add("@appother", SqlDbType.NVarChar);
            pOthers.Direction = ParameterDirection.Input;
            pOthers.Value = referenceNo;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }

        public static DataSet GetMerchantByUsername(string Username, out int ok, out string msg)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_GetMerchantByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();

            return ds;
        }

        public static DataSet GetAgentByUsername(string Username, out int ok, out string msg)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_GetAgentByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();

            return ds;
        }

        public static DataSet GetAdminByUsername(string Username, out int ok, out string msg)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_GetAdminByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();

            return ds;
        }

        public static DataSet GetUserByUsername(string Username, out int ok, out string msg)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_GetUserByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();

            return ds;
        }

        public static DataSet RedeemPointsByOrder(string guid, string username, out int points)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_RedeemPointsByOrder", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pGUID = sqlComm.Parameters.Add("@guid", SqlDbType.VarChar, 100);
            pGUID.Direction = ParameterDirection.Input;
            pGUID.Value = guid;

            var pUsername = sqlComm.Parameters.Add("@username", SqlDbType.VarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            var pPoints = sqlComm.Parameters.Add("@points", SqlDbType.Int);
            pPoints.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            points = (int)pPoints.Value;
            sqlConn.Close();

            return ds;
        }

        #region UpdateUserLevel2To5Data
        public static DataSet UpdateUserLevel2To5Data(string Username)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_UpdateUserLevel2To5Data", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region InsertPendingJob
        public static DataSet InsertPendingJob(string Username, string Cashname, decimal amount, string appother1, string appother2)
        {
            var sqlConn = DBConn.GetConnection();
            var da = new SqlDataAdapter();

            var sqlComm = new SqlCommand("SP_InsertPendingJob", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            var pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            var pCashname = sqlComm.Parameters.Add("@Cashname", SqlDbType.VarChar, 200);
            pCashname.Direction = ParameterDirection.Input;
            pCashname.Value = Cashname;

            var pAmount = sqlComm.Parameters.Add("@Amount", SqlDbType.Decimal);
            pAmount.Direction = ParameterDirection.Input;
            pAmount.Value = amount;

            var pAppother1 = sqlComm.Parameters.Add("@Appother1", SqlDbType.VarChar, 200);
            pAppother1.Direction = ParameterDirection.Input;
            pAppother1.Value = appother1;

            var pAppother2 = sqlComm.Parameters.Add("@Appother2", SqlDbType.VarChar, 200);
            pAppother2.Direction = ParameterDirection.Input;
            pAppother2.Value = appother2;

            da.SelectCommand = sqlComm;
            var ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion
    }
}
