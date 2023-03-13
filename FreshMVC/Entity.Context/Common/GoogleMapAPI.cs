using System;
using System.IO;
using System.Net;
using System.Text;
using WebApi.Dtos;

namespace Entity.Context
{
    public class GoogleMapAPI
    {
        private static string GoogleMapKey = "AIzaSyAoteKL3ZwTp-jL50mLk5AXsg0WsC-Dc04";

        public static double CalculateDistance(DistanceDto dataDto)
        {
            var origin = dataDto.Origin;
            double accumulateDistance = 0;

            var count = 0;
            foreach (var e in dataDto.Destination)
            {
                var destination = "place_id:" + e + "|";

                if (count > 0)
                    origin = dataDto.Destination[count - 1];

                var myUri = new Uri(string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?origins=place_id:{0}&destinations={1}&departure_time=now&key={2}", origin, destination, GoogleMapKey));
                var myWebRequest = WebRequest.Create(myUri);
                var myHttpWebRequest = (HttpWebRequest)myWebRequest;
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.Method = "GET";

                var myWebResponse = myWebRequest.GetResponse();
                var responseStream = myWebResponse.GetResponseStream();
                if (responseStream != null)
                {
                    var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                    var json = myStreamReader.ReadToEnd();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    responseStream.Close();
                    myWebResponse.Close();

                    if (result != null)
                    {
                        var currentDistance = result.rows[0].elements[0].distance.text;
                        var Distance = double.Parse(currentDistance.Value.Replace("km", "").Trim());
                        accumulateDistance += Distance;
                    }
                }

                count += 1;
            }

            return accumulateDistance;
        }
    }
}
