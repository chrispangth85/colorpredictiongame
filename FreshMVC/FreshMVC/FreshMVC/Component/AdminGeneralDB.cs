using System.Data;
using System.Data.SqlClient;
using System;

namespace FreshMVC.Component
{
    public class AdminGeneralDB
    {
        public static DataSet GetAllCharges(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllCharges", sqlConn);
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

        public static DataSet GetAllNotification(int viewPage, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllNotifications", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

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

        public static DataSet GetAllSupportMessageByUsername(string username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllSupportMessageByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pTitle = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 50);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = username;

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

        public static DataSet GetSupportMessageByOrderID(int orderID, int typeID, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetSupportMessageByOrderID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pOrderID = sqlComm.Parameters.Add("@orderID", SqlDbType.Int);
            pOrderID.Direction = ParameterDirection.Input;
            pOrderID.Value = orderID;

            SqlParameter pTypeID = sqlComm.Parameters.Add("@typeID", SqlDbType.Int);
            pTypeID.Direction = ParameterDirection.Input;
            pTypeID.Value = typeID;

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

        public static DataSet GetAllBanner(int viewPage, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllBanner", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

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

        public static DataSet GetAllPricePerKM(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllPricePerKM", sqlConn);
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

        public static DataSet GetAllReward(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllReward", sqlConn);
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

        public static DataSet GetAllOrdersByTypeByReferralID(string type, string referralID, int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrdersByType", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pType = sqlComm.Parameters.Add("@type", SqlDbType.VarChar, 50);
            pType.Direction = ParameterDirection.Input;
            pType.Value = type;

            SqlParameter pID = sqlComm.Parameters.Add("@referralId", SqlDbType.VarChar, 50);
            pID.Direction = ParameterDirection.Input;
            pID.Value = referralID;

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

        public static DataSet GetAllOrdersByType(string type, int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrdersByType", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pType = sqlComm.Parameters.Add("@type", SqlDbType.VarChar, 50);
            pType.Direction = ParameterDirection.Input;
            pType.Value = type;

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

        public static DataSet GetOrderById(int id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetOrderByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@orderId", SqlDbType.Int);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = id;

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

        public static DataSet GetOrderPointById(int id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetOrderPointByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.Int);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = id;

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

        public static DataSet SendMessageBySupport(int orderID, string message, string cs, int typeid, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_SendMessageBySupport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pOrderID = sqlComm.Parameters.Add("@orderID", SqlDbType.Int);
            pOrderID.Direction = ParameterDirection.Input;
            pOrderID.Value = orderID;

            SqlParameter pMsg = sqlComm.Parameters.Add("@message", SqlDbType.NVarChar, 200);
            pMsg.Direction = ParameterDirection.Input;
            pMsg.Value = message;

            SqlParameter pCS = sqlComm.Parameters.Add("@cs", SqlDbType.NVarChar, 50);
            pCS.Direction = ParameterDirection.Input;
            pCS.Value = cs;

            SqlParameter pTypeID = sqlComm.Parameters.Add("@typeID", SqlDbType.Int);
            pTypeID.Direction = ParameterDirection.Input;
            pTypeID.Value = typeid;

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

        public static DataSet DeleteMessageBySupport(int orderID, int typeid, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_DeleteMessageBySupport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pOrderID = sqlComm.Parameters.Add("@orderID", SqlDbType.Int);
            pOrderID.Direction = ParameterDirection.Input;
            pOrderID.Value = orderID;

            SqlParameter pTypeID = sqlComm.Parameters.Add("@typeID", SqlDbType.Int);
            pTypeID.Direction = ParameterDirection.Input;
            pTypeID.Value = typeid;

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

        public static DataSet GetNotificationbyId(string id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetNotificationById", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pId = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pId.Direction = ParameterDirection.Input;
            pId.Value = id;

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

        public static DataSet GetBannerByID(string code, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetBannerById", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = code;

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

        public static DataSet GetVehicleCapacityById(string code, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetVehicleCapacityById", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = code;

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

        public static DataSet GetHomeData(string username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetHomeData", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.VarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

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

        public static DataSet GetRewardById(int id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetRewardById", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pId = sqlComm.Parameters.Add("@id", SqlDbType.Int);
            pId.Direction = ParameterDirection.Input;
            pId.Value = id;

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

        public static void CreateNewReward(string name, string image, int pointNeeded, string expiryDate, string tnc, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_CreateReward", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pName = sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            pName.Direction = ParameterDirection.Input;
            pName.Value = name;

            SqlParameter pImage = sqlComm.Parameters.Add("@Image", SqlDbType.NVarChar, 2000);
            pImage.Direction = ParameterDirection.Input;
            pImage.Value = image;

            SqlParameter pTnC = sqlComm.Parameters.Add("@TnC", SqlDbType.NVarChar, 2000);
            pTnC.Direction = ParameterDirection.Input;
            pTnC.Value = tnc;

            SqlParameter pPointNeeded = sqlComm.Parameters.Add("@PointNeeded", SqlDbType.Int);
            pPointNeeded.Direction = ParameterDirection.Input;
            pPointNeeded.Value = pointNeeded;

            SqlParameter pExpiryDate = sqlComm.Parameters.Add("@ExpiryDate", SqlDbType.NVarChar, 50);
            pExpiryDate.Direction = ParameterDirection.Input;
            pExpiryDate.Value = expiryDate;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void CreateRewardCode(string name, string code, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_CreateRewardCode", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pName = sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            pName.Direction = ParameterDirection.Input;
            pName.Value = name;

            SqlParameter pCode = sqlComm.Parameters.Add("@Code", SqlDbType.NVarChar, 50);
            pCode.Direction = ParameterDirection.Input;
            pCode.Value = code;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void UpdateReward(int id, string name, string image, int pointNeeded, string expiryDate, string tnc, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateReward", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pId = sqlComm.Parameters.Add("@Id", SqlDbType.Int);
            pId.Direction = ParameterDirection.Input;
            pId.Value = id;

            SqlParameter pName = sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
            pName.Direction = ParameterDirection.Input;
            pName.Value = name;

            SqlParameter pImage = sqlComm.Parameters.Add("@Image", SqlDbType.NVarChar, 2000);
            pImage.Direction = ParameterDirection.Input;
            pImage.Value = image;

            SqlParameter pTnC = sqlComm.Parameters.Add("@TnC", SqlDbType.NVarChar, 2000);
            pTnC.Direction = ParameterDirection.Input;
            pTnC.Value = tnc;

            SqlParameter pPointNeeded = sqlComm.Parameters.Add("@PointNeeded", SqlDbType.Int);
            pPointNeeded.Direction = ParameterDirection.Input;
            pPointNeeded.Value = pointNeeded;

            SqlParameter pExpiryDate = sqlComm.Parameters.Add("@ExpiryDate", SqlDbType.NVarChar, 50);
            pExpiryDate.Direction = ParameterDirection.Input;
            pExpiryDate.Value = expiryDate;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void DeleteReward(int id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_DeleteReward", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pId = sqlComm.Parameters.Add("@Id", SqlDbType.Int);
            pId.Direction = ParameterDirection.Input;
            pId.Value = id;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetAllAdmin(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllAdmin", sqlConn);
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

        public static DataSet GetAllAgent(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllAgent", sqlConn);
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

        public static DataSet GetAdminByUsername(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAdminByUsername", sqlConn);
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

        public static DataSet GetAgentByUsername(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAgentByUsername", sqlConn);
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

        public static void CreateNewAdmin(string Username, bool IsActive, string Password, string PIN, string Firstname, string createdBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_CreateAdmin", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;
        
            SqlParameter pIsActive = sqlComm.Parameters.Add("@IsActive", SqlDbType.Bit);
            pIsActive.Direction = ParameterDirection.Input;
            pIsActive.Value = IsActive;
       
            SqlParameter pPassword = sqlComm.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
            pPassword.Direction = ParameterDirection.Input;
            pPassword.Value = Password;

            SqlParameter pPIN = sqlComm.Parameters.Add("@PIN", SqlDbType.NVarChar, 50);
            pPIN.Direction = ParameterDirection.Input;
            pPIN.Value = PIN;

            SqlParameter pSurname = sqlComm.Parameters.Add("@FullName", SqlDbType.NVarChar, 60);
            pSurname.Direction = ParameterDirection.Input;
            pSurname.Value = Firstname;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;       

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void UpdateAdmin(string Username, string Firstname, string createdBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateAdmin", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;        

            SqlParameter pSurname = sqlComm.Parameters.Add("@FullName", SqlDbType.NVarChar, 60);
            pSurname.Direction = ParameterDirection.Input;
            pSurname.Value = Firstname;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetAllUserByUsername(int viewPage, string username, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

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

        public static DataSet GetAllUser(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllUser", sqlConn);
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

        public static DataSet GetAllTransaction(string storedProcedure, int viewPage, string username, DateTime? dateFrom, DateTime? dateTo, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand(storedProcedure, sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

            SqlParameter pPages = sqlComm.Parameters.Add("@pages", SqlDbType.Int);
            pPages.Direction = ParameterDirection.Output;

            var pDateFrom = sqlComm.Parameters.Add("@dateFrom", SqlDbType.DateTime);
            pDateFrom.Direction = ParameterDirection.Input;
            pDateFrom.IsNullable = true;
            pDateFrom.Value = dateFrom;

            var pDateTo = sqlComm.Parameters.Add("@dateTo", SqlDbType.DateTime);
            pDateTo.Direction = ParameterDirection.Input;
            pDateTo.IsNullable = true;
            pDateTo.Value = dateTo;

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

        public static DataSet GetMerchantAllWalletTransactions(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetMerchantAllWalletTransactions", sqlConn);
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

        public static DataSet GetAgentAllWalletTransactions(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAgentAllWalletTransactions", sqlConn);
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

        public static DataSet GetAllTransaction(string storedProcedure, int viewPage, string username, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand(storedProcedure, sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

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

        public static DataSet GetAllUserTopUp(int viewPage, DateTime? dateFrom, DateTime? dateTo, string referalId, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllUserTopUp", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pPages = sqlComm.Parameters.Add("@pages", SqlDbType.Int);
            pPages.Direction = ParameterDirection.Output;

            var pDateFrom = sqlComm.Parameters.Add("@dateFrom", SqlDbType.DateTime);
            pDateFrom.Direction = ParameterDirection.Input;
            pDateFrom.IsNullable = true;
            pDateFrom.Value = dateFrom;

            var pDateTo = sqlComm.Parameters.Add("@dateTo", SqlDbType.DateTime);
            pDateTo.Direction = ParameterDirection.Input;
            pDateTo.IsNullable = true;
            pDateTo.Value = dateTo;


            SqlParameter pUser = sqlComm.Parameters.Add("@referalId", SqlDbType.NVarChar, 100);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = referalId;


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

        public static DataSet GetAllUserReport(string referalId, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllUserReport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@referalId", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = referalId;

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

        public static void UpdateUser(string Username, string Firstname, string referralID, string createdBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pFirstName = sqlComm.Parameters.Add("@FullName", SqlDbType.NVarChar, 60);
            pFirstName.Direction = ParameterDirection.Input;
            pFirstName.Value = Firstname;

            SqlParameter pReferralID = sqlComm.Parameters.Add("@ReferralID", SqlDbType.NVarChar, 60);
            pReferralID.Direction = ParameterDirection.Input;
            pReferralID.Value = referralID;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void Registration(string username, string password, string intro, string firstName, string lastName, string email, string state, int role, int? isPersonal, out int ok, out string message)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_RegisterUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pPassword = sqlComm.Parameters.Add("@password", SqlDbType.NVarChar, 50);
            pPassword.Direction = ParameterDirection.Input;
            pPassword.Value = password;

            SqlParameter pIntro = sqlComm.Parameters.Add("@intro", SqlDbType.NVarChar, 50);
            pIntro.Direction = ParameterDirection.Input;
            pIntro.Value = intro;

            SqlParameter pFirstName = sqlComm.Parameters.Add("@firstName", SqlDbType.NVarChar, 60);
            pFirstName.Direction = ParameterDirection.Input;
            pFirstName.Value = firstName;

            SqlParameter pLastName = sqlComm.Parameters.Add("@lastName", SqlDbType.NVarChar, 60);
            pLastName.Direction = ParameterDirection.Input;
            pLastName.Value = lastName;

            SqlParameter pEmail = sqlComm.Parameters.Add("@email", SqlDbType.NVarChar, 100);
            pEmail.Direction = ParameterDirection.Input;
            pEmail.Value = email;

            SqlParameter pState = sqlComm.Parameters.Add("@state", SqlDbType.NVarChar, 100);
            pState.Direction = ParameterDirection.Input;
            pState.Value = state;

            SqlParameter pRole = sqlComm.Parameters.Add("@roleId", SqlDbType.Int);
            pRole.Direction = ParameterDirection.Input;
            pRole.Value = role;

            SqlParameter pIsPersonal = sqlComm.Parameters.Add("@isPersonal", SqlDbType.Int);
            pIsPersonal.Direction = ParameterDirection.Input;
            pIsPersonal.Value = isPersonal;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            message = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static void CheckReferralValid(string referralCode, out int ok, out string message)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_CheckReferralValid", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@referral", SqlDbType.NVarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = referralCode;

            var pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            var pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            ok = (int)pOk.Value;
            message = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetUserByUsername(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetUserByUsername", sqlConn);
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

        public static void ApprovePayment(out int ok, out string message)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_ApprovePayment", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            message = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetAllRiderWithdrawal(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllRiderWithdrawal", sqlConn);
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

        public static DataSet GetAllWithdrawalLog(string username, string filterFrom, string filterTo, int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllWithdrawalLog", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUser = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = username;

            SqlParameter pFilterFrom = sqlComm.Parameters.Add("@searchFrom", SqlDbType.NVarChar);
            pFilterFrom.Direction = ParameterDirection.Input;
            pFilterFrom.Value = filterFrom;

            SqlParameter pFilterTo = sqlComm.Parameters.Add("@searchTo", SqlDbType.NVarChar);
            pFilterTo.Direction = ParameterDirection.Input;
            pFilterTo.Value = filterTo;

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

        public static DataSet GetAllRider(int viewPage, string filter, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllRider", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pSearch = sqlComm.Parameters.Add("@vehicleType", SqlDbType.NVarChar);
            pSearch.Direction = ParameterDirection.Input;
            pSearch.Value = filter;

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

        public static DataSet GetAllRiderReport(out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllRiderReport", sqlConn);
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

        public static DataSet GetRiderByUsername(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetRiderByUsername", sqlConn);
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

        public static void UpdateRider(string Username, string Firstname, string LastName, string Email, bool accountisBlock, string createdBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateRider", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pFirstName = sqlComm.Parameters.Add("@FullName", SqlDbType.NVarChar, 60);
            pFirstName.Direction = ParameterDirection.Input;
            pFirstName.Value = Firstname;

            SqlParameter pLastName = sqlComm.Parameters.Add("@LastName", SqlDbType.NVarChar, 60);
            pLastName.Direction = ParameterDirection.Input;
            pLastName.Value = LastName;

            SqlParameter pEmail = sqlComm.Parameters.Add("@Email", SqlDbType.NVarChar, 60);
            pEmail.Direction = ParameterDirection.Input;
            pEmail.Value = Email;

            SqlParameter pIsBlock = sqlComm.Parameters.Add("@isBlock", SqlDbType.Bit);
            pIsBlock.Direction = ParameterDirection.Input;
            pIsBlock.Value = accountisBlock;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        public static DataSet GetAllKYC(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllKYC", sqlConn);
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

        public static DataSet GetKYCByUsername(string Username, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetKYCByUsername", sqlConn);
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

        public static DataSet GetMerchantProductSetListByMerchant(int viewPage, string user, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductSetListByMerchant", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pUser = sqlComm.Parameters.Add("@User", SqlDbType.NVarChar, 200);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = user;

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

        public static DataSet GetMerchantProductList(int viewPage, string user, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetMerchantProductList", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pUser = sqlComm.Parameters.Add("@User", SqlDbType.NVarChar, 200);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = user;

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

        public static DataSet GetProductSetCategoryList(int viewPage, string user, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductSetCategoryList", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

            SqlParameter pUser = sqlComm.Parameters.Add("@User", SqlDbType.NVarChar, 200);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = user;

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

        public static DataSet GetProductCategoryList(int viewPage, string user, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductCategoryList", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

            SqlParameter pUser = sqlComm.Parameters.Add("@User", SqlDbType.NVarChar, 200);
            pUser.Direction = ParameterDirection.Input;
            pUser.Value = user;

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
        public static void UpdateKYC(string Username, int accountStatus, string statusComment, string createdBy, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand("SP_UpdateKYC", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Username;

            SqlParameter pAccountStatus = sqlComm.Parameters.Add("@AccountStatus", SqlDbType.Int);
            pAccountStatus.Direction = ParameterDirection.Input;
            pAccountStatus.Value = accountStatus;

            SqlParameter pStatusComment = sqlComm.Parameters.Add("@StatusComment", SqlDbType.NVarChar, 2000);
            pStatusComment.Direction = ParameterDirection.Input;
            pStatusComment.Value = statusComment;

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 50);
            pMessage.Direction = ParameterDirection.Output;

            sqlComm.ExecuteNonQuery();

            ok = (int)pOk.Value;
            msg = pMessage.Value.ToString();
            sqlConn.Close();
        }

        #region PromoCode
        public static DataSet GetAllPromoCode(int viewPage, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllPromoCode", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

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

        public static DataSet GetPromoByID(string code, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetPromoById", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = code;

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
        #endregion

        #region Category
        public static DataSet GetAllCategoryListByParentID(int viewPage, int merchantParentID, string title, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllCategoryListByParentID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pMerchantParentID = sqlComm.Parameters.Add("@merchantParentID", SqlDbType.Int);
            pMerchantParentID.Direction = ParameterDirection.Input;
            pMerchantParentID.Value = merchantParentID;

            SqlParameter pTitle = sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = title;

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

        public static DataSet GetCategoryByID(string idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetCategoryByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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
        #endregion


        #region Product
        public static DataSet GetAllProductListByMerchantFromUser(int viewPage, string merchant, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductListByMerchantFromUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = merchant;

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
        

        public static DataSet GetAllProductListByMerchant(int viewPage, string merchant, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductListByMerchant", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = merchant;

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

        public static DataSet GetProductByID(int idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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

        public static DataSet GetAllProductChoiceListByProductID(int viewPage, int productID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductChoiceListByProductID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = productID;

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

        public static DataSet GetProductChoiceByID(string idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductChoiceByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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

        public static DataSet GetAllProductChoiceItemListByProductChoiceID(int viewPage, int productID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductChoiceItemListByProductChoiceID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = productID;

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

        public static DataSet GetProductChoiceItemByID(string idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductChoiceItemByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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

        public static DataSet GetAllProductSideListByProductID(int viewPage, int productID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductSideListByProductID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = productID;

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

        public static DataSet GetProductSideByID(string idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductSideByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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

        public static DataSet GetAllProductSideItemListByProductSideID(int viewPage, int productID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductSideItemListByProductSideID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = productID;

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

        public static DataSet GetProductSideItemByID(string idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductSideItemByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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
        #endregion

        #region ProductSet
        public static DataSet GetAllProductSetListByMerchant(int viewPage, string merchant, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductSetListByMerchant", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = merchant;

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

        public static DataSet GetProductSetByID(int idz, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetProductSetByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@id", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = idz;

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
        #endregion

        public static DataSet GetOrderByGUID(string guid)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetOrderByGUID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pTitle = sqlComm.Parameters.Add("@guid", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = guid;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }

        #region Orders searchInput, selectedOption
        public static DataSet GetAllOrderedListByMerchant(int viewPage, string merchant, string searchInput, string selectedOption, int status, string date, int viewall, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrderedListByMerchant", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 200);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

            SqlParameter pSearchInput = sqlComm.Parameters.Add("@searchInput", SqlDbType.NVarChar, 200);
            pSearchInput.Direction = ParameterDirection.Input;
            pSearchInput.Value = searchInput;

            SqlParameter pSelectedInput = sqlComm.Parameters.Add("@selectedOption", SqlDbType.NVarChar, 200);
            pSelectedInput.Direction = ParameterDirection.Input;
            pSelectedInput.Value = selectedOption;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            SqlParameter pDate = sqlComm.Parameters.Add("@date", SqlDbType.DateTime);
            pDate.Direction = ParameterDirection.Input;
            pDate.Value = date;

            SqlParameter pViewAll = sqlComm.Parameters.Add("@viewall", SqlDbType.Int);
            pViewAll.Direction = ParameterDirection.Input;
            pViewAll.Value = viewall;

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

        public static DataSet GetAllOrderedListByMerchantExport(string merchant, int status, string date)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrderedListByMerchantExport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 200);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            SqlParameter pDate = sqlComm.Parameters.Add("@date", SqlDbType.DateTime);
            pDate.Direction = ParameterDirection.Input;
            pDate.Value = date;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }

        public static DataSet GetAllOrderedListByMerchantEasyParcelExport(string merchant, int status, string date)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrderedListByMerchantEasyParcelExport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 200);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

            SqlParameter pStatus = sqlComm.Parameters.Add("@status", SqlDbType.Int);
            pStatus.Direction = ParameterDirection.Input;
            pStatus.Value = status;

            SqlParameter pDate = sqlComm.Parameters.Add("@date", SqlDbType.DateTime);
            pDate.Direction = ParameterDirection.Input;
            pDate.Value = date;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region DeleteProductByID
        public static DataSet DeleteProductByID(int productID)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_DeleteProductByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pTitle = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = productID;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion


        #region ProcessProductImport
        public static DataSet ProcessProductImport(string merchant)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_ProcessProductImport", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetAllOrderedListByUser
        public static DataSet GetAllOrderedListByUser(int viewPage, string username, int status)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllOrderedListByUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 200);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = username;

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

        #region CheckMerchantMaxOrderReachedByDate
        public static DataSet CheckMerchantMaxOrderReachedByDate(string merchant, DateTime datetime, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_CheckMerchantMaxOrderReachedByDate", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pTitle = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = merchant;

            SqlParameter dDate = sqlComm.Parameters.Add("@datetime", SqlDbType.DateTime);
            dDate.Direction = ParameterDirection.Input;
            dDate.Value = datetime;

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
        #endregion

        #region GetAllFavShopByUsername
        public static DataSet GetAllFavShopByUsername(string username)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllFavShopByUsername", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 200);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = username;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        public static DataSet GenerateReferralCode(string username, int type)//type 0 : admin/agent/user  1 : merchant
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GenerateReferralCode", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = username;

            SqlParameter pType = sqlComm.Parameters.Add("@type", SqlDbType.Int);
            pType.Direction = ParameterDirection.Input;
            pType.Value = type;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }

        #region DeliverySetup
        public static DataSet GetAllDeliverySetupList(int viewPage, string merchant, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllDeliverySetupList", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

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
        #endregion

        #region PointWalletOperation
        public static void PointWalletOperation(string username, string merchant, string cashName, decimal amount, int orderID, string appOther)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_PointWalletOperation", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pMerchant = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pMerchant.Direction = ParameterDirection.Input;
            pMerchant.Value = merchant;

            SqlParameter pCashName = sqlComm.Parameters.Add("@cashName", SqlDbType.NVarChar, 100);
            pCashName.Direction = ParameterDirection.Input;
            pCashName.Value = cashName;

            SqlParameter pCashNum = sqlComm.Parameters.Add("@cashNum", SqlDbType.Decimal);
            pCashNum.Direction = ParameterDirection.Input;
            pCashNum.Value = amount;

            SqlParameter pOrderID = sqlComm.Parameters.Add("@orderID", SqlDbType.Int);
            pOrderID.Direction = ParameterDirection.Input;
            pOrderID.Value = orderID;

            SqlParameter pAppOther = sqlComm.Parameters.Add("@appother", SqlDbType.NVarChar, 100);
            pAppOther.Direction = ParameterDirection.Input;
            pAppOther.Value = appOther;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();
        }
        #endregion

        #region Inventory
        public static DataSet GetAllProductListByMerchantInventory(int viewPage, string merchant, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProductListByMerchantInventory", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pTitle = sqlComm.Parameters.Add("@merchant", SqlDbType.NVarChar, 100);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = merchant;

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
        #endregion

        #region Inventory
        public static DataSet ProductMovementOperation(int productID, string cashname, int amount, string appOther)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_ProductMovementOperation", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pProductID = sqlComm.Parameters.Add("@proID", SqlDbType.Int);
            pProductID.Direction = ParameterDirection.Input;
            pProductID.Value = productID;

            SqlParameter pCashName = sqlComm.Parameters.Add("@cashName", SqlDbType.NVarChar, 100);
            pCashName.Direction = ParameterDirection.Input;
            pCashName.Value = cashname;

            SqlParameter pAmount = sqlComm.Parameters.Add("@cashNum", SqlDbType.Int);
            pAmount.Direction = ParameterDirection.Input;
            pAmount.Value = amount;

            SqlParameter pAppOther = sqlComm.Parameters.Add("@appother", SqlDbType.NVarChar, 200);
            pAppOther.Direction = ParameterDirection.Input;
            pAppOther.Value = appOther;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }

        public static DataSet GetInventoryTransactions(int viewPage, int productID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetInventoryTransactions", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pProductID = sqlComm.Parameters.Add("@productID", SqlDbType.Int);
            pProductID.Direction = ParameterDirection.Input;
            pProductID.Value = productID;

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

        public static DataSet ConfirmedOrderProductInventory(int idz)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_ConfirmedOrderProductInventory", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pID = sqlComm.Parameters.Add("@placeOrderID", SqlDbType.Int);
            pID.Direction = ParameterDirection.Input;
            pID.Value = idz;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetUserAccessRight
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
        #endregion

        #region GetModulesAccessRight
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
        #endregion

        #region UpdateAdminAccessRight
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

            SqlParameter pCreatedBy = sqlComm.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
            pCreatedBy.Direction = ParameterDirection.Input;
            pCreatedBy.Value = createdBy;

            SqlParameter pCreatedOn = sqlComm.Parameters.Add("@CreatedOn", SqlDbType.DateTime);
            pCreatedOn.Direction = ParameterDirection.Input;
            pCreatedOn.Value = DateTime.Now;

            SqlParameter pUpdatedBy = sqlComm.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
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
        #endregion

        #region GetAllUser
        public static DataSet GetAllUser(int viewPage, string filterType, string filterValue, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllUser", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

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
        #endregion

        #region GetAllRedPacketListing
        public static DataSet GetAllRedPacketListing(int viewPage, string filterType, string filterValue, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllRedPacketListing", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

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
        #endregion

        #region GetAllGame
        public static DataSet GetAllGame(int viewPage, string filterType, string filterValue, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllGame", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

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
        #endregion

        #region GetGameByID
        public static DataSet GetGameByID(int id, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetGameByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@ID", SqlDbType.Int);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = id;

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
        #endregion

        #region GenerateGameSessionByID
        public static DataSet GenerateGameSessionByID(int gameid)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GenerateGameSessionByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pGameID = sqlComm.Parameters.Add("@ID", SqlDbType.Int);
            pGameID.Direction = ParameterDirection.Input;
            pGameID.Value = gameid;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetAllGameSessionActive
        public static DataSet GetAllGameSessionActive()
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllGameSessionActive", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region PlaceBet
        public static DataSet PlaceBet(string username, int number, int amount, int gameid, string period, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_PlaceBet", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pUsername = sqlComm.Parameters.Add("@username", SqlDbType.VarChar, 100);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = username;

            SqlParameter pNumber = sqlComm.Parameters.Add("@number", SqlDbType.Int);
            pNumber.Direction = ParameterDirection.Input;
            pNumber.Value = number;

            SqlParameter pAmount = sqlComm.Parameters.Add("@amount", SqlDbType.Int);
            pAmount.Direction = ParameterDirection.Input;
            pAmount.Value = amount;

            SqlParameter pGameID = sqlComm.Parameters.Add("@gameid", SqlDbType.Int);
            pGameID.Direction = ParameterDirection.Input;
            pGameID.Value = gameid;

            SqlParameter pPeriod = sqlComm.Parameters.Add("@period", SqlDbType.VarChar, 100);
            pPeriod.Direction = ParameterDirection.Input;
            pPeriod.Value = period;

            SqlParameter pOk = sqlComm.Parameters.Add("@ok", SqlDbType.Int);
            pOk.Direction = ParameterDirection.Output;

            SqlParameter pMessage = sqlComm.Parameters.Add("@msg", SqlDbType.VarChar, 2000);
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
        #endregion

        #region GetGameSessionHistory
        public static DataSet GetGameSessionHistory(int gameid)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetGameSessionHistory", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pGameID = sqlComm.Parameters.Add("@gameid", SqlDbType.Int);
            pGameID.Direction = ParameterDirection.Input;
            pGameID.Value = gameid;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetBetHistory
        public static DataSet GetBetHistory(int gameid, string username)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetBetHistory", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pGameID = sqlComm.Parameters.Add("@gameid", SqlDbType.Int);
            pGameID.Direction = ParameterDirection.Input;
            pGameID.Value = gameid;

            SqlParameter pTitle = sqlComm.Parameters.Add("@username", SqlDbType.NVarChar, 200);
            pTitle.Direction = ParameterDirection.Input;
            pTitle.Value = username;

            da.SelectCommand = sqlComm;
            DataSet ds = new DataSet();

            sqlConn.Open();
            da.Fill(ds);

            sqlConn.Close();

            return ds;
        }
        #endregion

        #region GetAllWithdrawalList

        public static DataSet GetAllWithdrawalList(int viewPage, string filterType, string filterValue, string fromDate, string toDate, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllWithdrawal", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

            SqlParameter pFromDate = sqlComm.Parameters.Add("@fromDate", SqlDbType.NVarChar, 200);
            pFromDate.Direction = ParameterDirection.Input;
            pFromDate.Value = fromDate;

            SqlParameter pToDate = sqlComm.Parameters.Add("@toDate", SqlDbType.NVarChar, 200);
            pToDate.Direction = ParameterDirection.Input;
            pToDate.Value = toDate;

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

        #endregion

        #region GetAllRechargeList

        public static DataSet GetAllRechargeList(int viewPage, string filterType, string filterValue, string fromDate, string toDate, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllRecharge", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

            SqlParameter pFromDate = sqlComm.Parameters.Add("@fromDate", SqlDbType.NVarChar, 200);
            pFromDate.Direction = ParameterDirection.Input;
            pFromDate.Value = fromDate;

            SqlParameter pToDate = sqlComm.Parameters.Add("@toDate", SqlDbType.NVarChar, 200);
            pToDate.Direction = ParameterDirection.Input;
            pToDate.Value = toDate;

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
        #endregion

        #region GetAllProduct
        public static DataSet GetAllProducts(int viewPage, string filterType, string filterValue, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllProducts", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

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
        #endregion

        #region GetAllGameSessionListing
        public static DataSet GetAllGameSessionListingByID(int viewPage, int gameID, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllGameSessionListingByID", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pGameID = sqlComm.Parameters.Add("@gameid", SqlDbType.Int);
            pGameID.Direction = ParameterDirection.Input;
            pGameID.Value = gameID;

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
        #endregion
        
        #region GetLotteryBetListing
        public static DataSet GetLotteryBetListing(int viewPage, string filterType, string filterValue, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetLotteryBetListing", sqlConn);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlParameter pViewPage = sqlComm.Parameters.Add("@viewPage", SqlDbType.Int);
            pViewPage.Direction = ParameterDirection.Input;
            pViewPage.Value = viewPage;

            SqlParameter pFilterType = sqlComm.Parameters.Add("@filterType", SqlDbType.VarChar, 200);
            pFilterType.Direction = ParameterDirection.Input;
            pFilterType.Value = filterType;

            SqlParameter pUsername = sqlComm.Parameters.Add("@keyword", SqlDbType.VarChar, 200);
            pUsername.Direction = ParameterDirection.Input;
            pUsername.Value = Helper.NVL(filterValue);

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
        #endregion

        #region GetAllBanner
        public static DataSet GetAllBanners(int viewPage, out int pages, out int ok, out string msg)
        {
            SqlConnection sqlConn = DBConn.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand sqlComm = new SqlCommand("SP_GetAllBanners", sqlConn);
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
        #endregion
    }
}
