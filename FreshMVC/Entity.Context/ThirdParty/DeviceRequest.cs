namespace BMS.Entity.Context.ThirdParty
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class DeviceRequest
    {
        public static bool AddUser(string url, string accessToken, string surname, string givenName, string card1, string card2, string startTime, string endTime, string pin, string duressPin, string parentPhone, out long accountId, out string exceptionMsg)
        {
            exceptionMsg = string.Empty;
            accountId = 0;

            try
            {
                exceptionMsg = string.Empty;
                var myUri = new Uri(url + "/api/user/add");
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    //string data = "{ \"surname\" : \"Alain1\", \"givenName\" : \"Bin1\", \"card1\" : \"123123123\", \"card2\" : \"12312313\", \"startTime\" : \"2019-12-14 00:00:00\", \"endTime\" : \"2019-12-15 00:00:00\", \"pin\" : \"\", \"duressPin\" : \"\" , \"parentPhone\" : \"\" }";
                    var data = new RegisterUser();
                    data.surname = surname;
                    data.givenName = givenName;
                    data.card1 = card1;
                    data.card2 = card2;
                    data.startTime = startTime;
                    data.endTime = endTime;
                    data.pin = pin;
                    data.duressPin = duressPin;
                    data.parentPhone = parentPhone;
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                }

                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();
                if (responseStream == null)
                    return false;

                var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                var json = myStreamReader.ReadToEnd();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                responseStream.Close();
                myWebResponse.Close();

                exceptionMsg = result.msg.ToString();
                accountId = result.data == null ? 0 : result.data.Value;
                return result.code.ToString() == "0";
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.InnerException.ToString();
                return false;
            }
        }

        public static bool EditUser(string url, string accessToken, string id, string surname, string givenName, string card1, string card2, string startTime, string endTime, string pin, string duressPin, string parentPhone, out string exceptionMsg)
        {
            exceptionMsg = string.Empty;

            try
            {
                exceptionMsg = string.Empty;
                var myUri = new Uri(url + "/api/user/edit");
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    //string data = "{ \"surname\" : \"Alain1\", \"givenName\" : \"Bin1\", \"card1\" : \"123123123\", \"card2\" : \"12312313\", \"startTime\" : \"2019-12-14 00:00:00\", \"endTime\" : \"2019-12-15 00:00:00\", \"pin\" : \"\", \"duressPin\" : \"\" , \"parentPhone\" : \"\" }";
                    var data = new EditUser();
                    data.userId = id;
                    data.surname = surname;
                    data.givenName = givenName;
                    data.card1 = card1;
                    data.card2 = card2;
                    data.startTime = startTime;
                    data.endTime = endTime;
                    data.pin = pin;
                    data.duressPin = duressPin;
                    data.parentPhone = parentPhone;
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                }

                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();
                if (responseStream == null)
                    return false;

                var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                var json = myStreamReader.ReadToEnd();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                responseStream.Close();
                myWebResponse.Close();

                exceptionMsg = result.msg.ToString();
                return result.code.ToString() == "0";
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.InnerException.ToString();
                return false;
            }
        }

        public static bool CheckTaskStatus(string url, string accessToken, out string exceptionMsg)
        {
            exceptionMsg = string.Empty;

            try
            {
                exceptionMsg = string.Empty;
                var myUri = new Uri(url + "/api/getDownloadStatus");
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.ContentType = "application/json";
                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();
                if (responseStream == null)
                    return false;

                var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                var json = myStreamReader.ReadToEnd();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                responseStream.Close();
                myWebResponse.Close();

                exceptionMsg = result.msg.ToString();
                return result.code.ToString() == "0";
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.InnerException.ToString();
                return false;
            }
        }

        public static bool DownloadusertoGroup(string url, string accessToken, string[] UserList, string[] DeviceList, string[] ElevatorGroupList, string commandType, out string exceptionMsg)
        {
            exceptionMsg = string.Empty;

            try
            {
                exceptionMsg = string.Empty;
                var myUri = new Uri(url + "/api/user/downloadUsers");
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.PreAuthenticate = true;
                myHttpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    var data = new DownloadUsers();
                    data.UserList = UserList.ToList();
                    data.DeviceList = DeviceList.ToList();
                    data.ElevatorGroupList = ElevatorGroupList.ToList();
                    data.cmd = commandType;
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                }

                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();
                if (responseStream == null)
                    return false;

                var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                var json = myStreamReader.ReadToEnd();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                responseStream.Close();
                myWebResponse.Close();

                exceptionMsg = result.msg.ToString();
                return result.code.ToString() == "0";
            }
            catch (Exception ex)
            {
                exceptionMsg = ex.InnerException.ToString();
                return false;
            }
        }
    }
}
