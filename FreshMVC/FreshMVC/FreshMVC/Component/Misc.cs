using FreshMVC.Models;
using FreshMVC.ThirdParties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FreshMVC.Component
{
    public static class Misc
    {
        public const string _baseUrl = "https://receiptracker.commitzsolution.com";
        //public const string _baseUrl = "https://localhost:44354";
        private static string _numbers = "0123456789";

        public static IEnumerable<SelectListItem> ConstructsRiderAccountStatus()
        {
            List<SelectListItem> filteringCriteriaList = new List<SelectListItem>();

            SelectListItem fcAll = new SelectListItem();
            fcAll.Text = Resources.PackBuddyShared.lblActive;
            fcAll.Value = "0";
            filteringCriteriaList.Add(fcAll);

            SelectListItem fcMoto = new SelectListItem();
            fcMoto.Text = Resources.PackBuddyShared.lblBlock;
            fcMoto.Value = "1";
            filteringCriteriaList.Add(fcMoto);

            return filteringCriteriaList;
        }

        public static string GenerateRandomDigit(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            Random random = new Random();

            for (var i = 0; i < 6; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }

            return builder.ToString();
        }

        public static int ConstructPageList(int selectedPage, int pages, PaginationBase model)
        {
            //if pages is empty (meaning there is no data, we default it to 1)
            //solved the email : member listing searching bug
            if (pages == 0)
            {
                pages = 1;
            }

            List<int> pageList = new List<int>();
            for (int z = 1; z <= pages; z++)
            {
                pageList.Add(z);
            }

            model.Pages = from c in pageList
                          select new SelectListItem
                          {
                              Selected = (c.ToString() == selectedPage.ToString()),
                              Text = c.ToString(),
                              Value = c.ToString()
                          };

            return selectedPage;
        }

        #region ConstructNewPageList
        public static int ConstructNewPageList(int selectedPage, int pages, PaginationBase model)
        {
            //if pages is empty (meaning there is no data, we default it to 1)
            //solved the email : member listing searching bug
            if (pages == 0)
            {
                pages = 1;
            }

            int[] pageList = new int[pages];
            for (int z = 1; z <= pages; z++)
            {
                pageList[z - 1] = z;
            }

            model.NewPages = pageList;

            return selectedPage;
        }
        #endregion

        public static List<SelectListItem> GetAccountType()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "Business";
            firstAcc.Value = "0";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "Personal";
            secondAcc.Value = "1";
            accounts.Add(secondAcc);

            return accounts;
        }

        public static List<SelectListItem> GetState()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "JOHOR";
            firstAcc.Value = "JOHOR";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "KEDAH";
            secondAcc.Value = "KEDAH";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "KELANTAN";
            thirdAcc.Value = "KELANTAN";
            accounts.Add(thirdAcc);

            var FourthAcc = new SelectListItem();
            FourthAcc.Text = "KUALA LUMPUR";
            FourthAcc.Value = "KUALA LUMPUR30";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "MELAKA";
            FourthAcc.Value = "MELAKA";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "NEGERI SEMBILAN";
            FourthAcc.Value = "NEGERI SEMBILAN";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "PAHANG";
            FourthAcc.Value = "PAHANG";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "PERAK";
            FourthAcc.Value = "PERAK";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "PERLIS";
            FourthAcc.Value = "PERLIS";

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "PULAU PINANG";
            FourthAcc.Value = "PULAU PINANG";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "SABAH";
            FourthAcc.Value = "SABAH";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "SARAWAK";
            FourthAcc.Value = "SARAWAK";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "SELANGOR";
            FourthAcc.Value = "SELANGOR";
            accounts.Add(FourthAcc);

            FourthAcc = new SelectListItem();
            FourthAcc.Text = "TERENGGANU";
            FourthAcc.Value = "TERENGGANU";
            accounts.Add(FourthAcc);

            return accounts;
        }

        public static List<SelectListItem> GetMotorcycleWeightList()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "0 kg - 1 kg";
            firstAcc.Value = "1";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "1 kg - 5 kg";
            secondAcc.Value = "5";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "5 kg - 8 kg";
            thirdAcc.Value = "8";
            accounts.Add(thirdAcc);

            var fourthAcc = new SelectListItem();
            fourthAcc.Text = "8 kg - 10 kg";
            fourthAcc.Value = "10";
            accounts.Add(fourthAcc);

            return accounts;
        }

        public static List<SelectListItem> GetVehiclesList()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "Motorcycle";
            firstAcc.Value = "Motorcycle";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "Car";
            secondAcc.Value = "Car";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "4by4";
            thirdAcc.Value = "4by4";
            accounts.Add(thirdAcc);

            var fourthAcc = new SelectListItem();
            fourthAcc.Text = "Van";
            fourthAcc.Value = "Van";
            accounts.Add(fourthAcc);

            return accounts;
        }

        public static List<SelectListItem> GetCarWeightList()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "0 kg - 10 kg";
            firstAcc.Value = "10";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "10 kg - 20 kg";
            secondAcc.Value = "20";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "0 kg - 30 kg";
            thirdAcc.Value = "30";
            accounts.Add(thirdAcc);

            var fourthAcc = new SelectListItem();
            fourthAcc.Text = "30 kg - 50 kg";
            fourthAcc.Value = "50";
            accounts.Add(fourthAcc);

            return accounts;
        }

        public static List<SelectListItem> Get4By4WeightList()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "0 kg - 20 kg";
            firstAcc.Value = "20";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "20 kg - 30 kg";
            secondAcc.Value = "30";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "30 kg - 60 kg";
            thirdAcc.Value = "60";
            accounts.Add(thirdAcc);

            var fourthAcc = new SelectListItem();
            fourthAcc.Text = "60 kg - 80 kg";
            fourthAcc.Value = "80";
            accounts.Add(fourthAcc);

            return accounts;
        }

        public static List<SelectListItem> GetVanWeightList()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "0 kg - 50 kg";
            firstAcc.Value = "50";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "50 kg - 70 kg";
            secondAcc.Value = "70";
            accounts.Add(secondAcc);

            var thirdAcc = new SelectListItem();
            thirdAcc.Text = "70 kg - 80 kg";
            thirdAcc.Value = "80";
            accounts.Add(thirdAcc);

            var fourthAcc = new SelectListItem();
            fourthAcc.Text = "80 kg - 100 kg";
            fourthAcc.Value = "100";
            accounts.Add(fourthAcc);

            return accounts;
        }

        public static List<SelectListItem> GetTransactionType()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "TOPUP";
            firstAcc.Value = "TOPUP";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "DEDUCT";
            secondAcc.Value = "DEDUCT";
            accounts.Add(secondAcc);

            return accounts;
        }

        public static List<SelectListItem> GetInventoryTransactionType()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "ADD";
            firstAcc.Value = "ADD";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = "DEDUCT";
            secondAcc.Value = "DEDUCT";
            accounts.Add(secondAcc);

            return accounts;
        }

        public static List<SelectListItem> GetPaymentGatewayType()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = "Stripe";
            firstAcc.Value = "Stripe";
            accounts.Add(firstAcc);

            return accounts;
        }

        public static double CoordinateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var p = 0.017453292519943295;
            var a = 0.5 -
                Math.Cos((lat2 - lat1) * p) / 2 +
                Math.Cos(lat1 * p) * Math.Cos(lat2 * p) * (1 - Math.Cos((lon2 - lon1) * p)) / 2;
            return 12742 * Math.Asin(Math.Sqrt(a));
        }

        public static void RecomputeDistance(ref List<PointModel> _list, PointModel first)
        {
            //recompute the distance with the first point
            for (int z = 1; z < _list.Count; z++)
            {
                if (_list[z].OptimizedIndex == -1)
                {
                    double distance = Misc.CoordinateDistance(first.dLat, first.dLng, _list[z].dLat, _list[z].dLng);
                    _list[z].DistanceFromFirstPoint = distance;
                }
            }
        }

        public static double GetDistance(String oriLat, String oriLng, String destLat2, String destLng2)
        {
            double currentDistance = 0;
            var url = string.Format("https://maps.googleapis.com/maps/api/distancematrix/json?key=AIzaSyDtd6FB22M2tWCd81aLU1MkajInD2TZG3Q&origins={0},{1}&destinations={2},{3}&mode=driving&language=en-EN&sensor=false", oriLat, oriLng, destLat2, destLng2);
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            var jsonResponseString = reader.ReadToEnd();

            dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponseString);

            if (result != null && result.rows[0].elements[0].distance != null)
            {
                String strDistance = result.rows[0].elements[0].distance.value;
                strDistance = strDistance.Replace("{", "").Replace("}", "");
                double distanceMeter = double.Parse(strDistance);
                currentDistance = distanceMeter / 1000;//change it from Meter to KiloMeter
            }

            return currentDistance;
        }

        public static void CalculateAmountBasedOnDistanceBikeCarPickup(double distance, int counter, double pricePerKM, double basePrice, double nextAddStop, out double outBasePrice, out double outExtraMileageKM, out double outExtraMileage, out double outAdditionalStop)
        {
            double extraKM = distance - 5;
            outBasePrice = 0.0;
            outExtraMileageKM = 0.0;
            outAdditionalStop = 0.0;
            outExtraMileage = 0.0;

            if (counter == 0)
            {
                if (extraKM > 0)
                {
                    var modz = extraKM % 1;
                    var numz = (int)extraKM;

                    //add the mileage before doing the mod
                    outExtraMileageKM = outExtraMileageKM + extraKM;

                    if (modz > 0)
                    {
                        numz = numz + 1;
                    }

                    outBasePrice = basePrice;
                    outExtraMileage = outExtraMileage + (numz * pricePerKM);
                }
                else
                {
                    outBasePrice = basePrice;
                }
            }
            else
            {
                var modz = distance % 1;
                var numz = (int)distance;

                //add the mileage before doing the mod
                outExtraMileageKM = outExtraMileageKM + distance;

                if (modz > 0)
                {
                    numz = numz + 1;
                }

                //      amount = nextAddStop + (num * pricePerKM); //previous calculation
                outAdditionalStop = outAdditionalStop + nextAddStop;
                outExtraMileage = outExtraMileage + (numz * pricePerKM);
            }
        }
        public static void CalculateAmountBasedOnDistanceVanLorry(double distance, int counter, double pricePerKM, double basePrice, double nextAddStop, out double outBasePrice, out double outExtraMileageKM, out double outExtraMileage, out double outAdditionalStop)
        {
            double extraKM = distance - 10;
            outBasePrice = 0.0;
            outExtraMileageKM = 0.0;
            outAdditionalStop = 0.0;
            outExtraMileage = 0.0;

            if (counter == 0)
            {
                if (extraKM > 0)
                {
                    var modz = extraKM % 50;
                    var numz = (int)extraKM;

                    //add the mileage before doing the mod
                    outExtraMileageKM = outExtraMileageKM + extraKM;

                    if (modz > 0)
                    {
                        numz = numz + 1;
                    }

                    outBasePrice = basePrice;
                    outExtraMileage = outExtraMileage + (numz * pricePerKM);
                }
                else
                {
                    outBasePrice = basePrice;
                }
            }
            else
            {
                var modz = distance % 50;
                var numz = (int)distance;

                //add the mileage before doing the mod
                outExtraMileageKM = outExtraMileageKM + distance;

                if (modz > 0)
                {
                    numz = numz + 1;
                }

                //      amount = nextAddStop + (num * pricePerKM); //previous calculation
                outAdditionalStop = outAdditionalStop + nextAddStop;
                outExtraMileage = outExtraMileage + (numz * pricePerKM);
            }
        }

        #region SendSMS
        public static void SendSMS(string selectedCountry, string cellPhone, string smsContent, string languageCode)
        {
            //do not send sms for this project
            return;

            ISms client = new SmsClient();

            int type = 1;//for english, we'll set the type as 1      

            //For unicode (chinese / japanese, we need to set to type 3 as well as encode it - Only for BulkSMS2u - SMS123 doesn't require getbyte
            //BJ tested send to china and didn't receive.. thus go back to SMS123..
            if (selectedCountry.ToUpper() == "MY"/* || selectedCountry.ToUpper() == "CN"*/)
            {
                smsContent = string.Format("RM0.00 {0}", smsContent);

                if (languageCode == "zh-CN" || languageCode == "zh-TW" || languageCode == "ja-JP")
                {
                    type = 3; //For chinese letter, when sending out sms, we need to set the type as = 3
                    smsContent = System.Text.Encoding.BigEndianUnicode.GetBytes(smsContent).Aggregate("", (agg, val) => agg + val.ToString("X2"));
                }
            }

            client.SendMessage(cellPhone, smsContent, selectedCountry, type);
        }
        #endregion

        #region OptimizeOrders
        public static void OptimizeOrders(List<PlaceOrderModel> dataList, AddressOptimization merchantPoint)
        {
            try
            {
                List<AddressOptimization> list = new List<AddressOptimization>();

                //add in merchantPoint
                list.Add(merchantPoint);

                //add in all other points
                foreach (PlaceOrderModel p in dataList)
                {
                    AddressOptimization pt = new AddressOptimization();
                    pt.ID = p.DeliIDOpt;
                    pt.UserLinkedID = p.DeliLinkedIDOpt;
                    pt.Lat = p.DeliLatOpt;
                    pt.Lng = p.DeliLngOpt;

                    list.Add(pt);
                }

                //now we have all the points that needs to be optimized - construct all possible combination
                List<LocationMapOptimization> combis = new List<LocationMapOptimization>();

                for (int z = 0; z < list.Count; z++)
                {
                    for (int x = z + 1; x < list.Count; x++)
                    {
                        LocationMapOptimization child = new LocationMapOptimization();
                        child.From = list[z].ID;
                        child.FromLat = double.Parse(list[z].Lat.ToString());
                        child.FromLng = double.Parse(list[z].Lng.ToString());
                        child.To = list[x].ID;
                        child.ToLat = double.Parse(list[x].Lat.ToString());
                        child.ToLng = double.Parse(list[x].Lng.ToString());

                        combis.Add(child);
                    }
                }

                //now we have all  the combination. Recompute the distance [by straight line, as getting the real distance require call to google distance API which will need $] Future if client pay then yes we change it
                foreach (LocationMapOptimization child in combis)
                {
                    child.Distance = Misc.CoordinateDistance(child.FromLat, child.FromLng, child.ToLat, child.ToLng);
                }

                #region Djiksra
                //Start djikstra magic https://www.youtube.com/watch?v=pVfj6mxhdMw
                List<DjikstraOptimization> djikList = new List<DjikstraOptimization>();
                //insert all the points
                foreach (AddressOptimization p in list)
                {
                    DjikstraOptimization d = new DjikstraOptimization();
                    d.Vertex = p.ID;
                    d.UserLinkedVertex = p.UserLinkedID;
                    d.ShortestDistance = double.MaxValue;
                    d.PreviousVertex = -1;
                    d.VisitedVertex = false;

                    if (p.ID == 0)//starting point
                    {
                        d.ShortestDistance = 0;//Initialize starting point shortest distance 0. Because we always go from merchant house address
                    }

                    djikList.Add(d);
                }

                bool completed = false;
                int index = 0;

                while (!completed)
                {
                    //first find the smallest distance of unvisited vertex
                    var unvisitedList = djikList.Where(v => !v.VisitedVertex).ToList();

                    if (unvisitedList.Count <= 1)//last point doesn't require any computation
                    {
                        completed = true;
                    }
                    else
                    {
                        var smallestUnvisitedVertex = unvisitedList.Where(v => v.ShortestDistance == unvisitedList.Min(w => w.ShortestDistance)).First();

                        if (smallestUnvisitedVertex != null)
                        {
                            //smallestUnvisitedVertex.Vertex
                            //get the shortest distance from Vertex (Based on the combis)
                            var allCombisBasedOnVertex = combis.Where(v => v.From == smallestUnvisitedVertex.Vertex || v.To == smallestUnvisitedVertex.Vertex).ToList();

                            //filter out vertex that already visited
                            for (int i = allCombisBasedOnVertex.Count - 1; i >= 0; i--)
                            {
                                var from = allCombisBasedOnVertex[i].From;
                                var to = allCombisBasedOnVertex[i].To;

                                //check if its already visited
                                var existsFrom = djikList.Exists(m => m.Vertex == from && m.VisitedVertex);
                                var existsTo = djikList.Exists(m => m.Vertex == to && m.VisitedVertex);

                                if (existsFrom || existsTo)
                                {
                                    allCombisBasedOnVertex.RemoveAt(i);
                                }
                            }

                            //from the combis, get the shortest path OR if user wanted to route it to their preferred destination
                            var shortestCombi = allCombisBasedOnVertex.Where(v => v.Distance == allCombisBasedOnVertex.Min(w => w.Distance)).FirstOrDefault();

                            if (smallestUnvisitedVertex.UserLinkedVertex != 0)
                            {
                                shortestCombi = allCombisBasedOnVertex.Where(v => v.From == smallestUnvisitedVertex.UserLinkedVertex || v.To == smallestUnvisitedVertex.UserLinkedVertex).FirstOrDefault();
                            }

                            var opponentVertex = shortestCombi.From;

                            if (smallestUnvisitedVertex.Vertex == opponentVertex)
                            {
                                opponentVertex = shortestCombi.To;
                            }

                            var tempVertex = djikList.Where(z => z.Vertex == opponentVertex).Single();
                            tempVertex.ShortestDistance = shortestCombi.Distance;
                            tempVertex.PreviousVertex = smallestUnvisitedVertex.Vertex;
                            tempVertex.Sequence = index;

                            smallestUnvisitedVertex.VisitedVertex = true;
                            index++;
                        }
                        else
                        {
                            //message = "Problem : couldn't find instance on unvisitedList. Need investigate";
                        }
                    }
                }
                #endregion

                var sortedDjikstra = djikList.OrderBy(x => x.Sequence).ToList();

                //now copy the sequence to OptimizationSequence
                foreach (PlaceOrderModel p in dataList)
                {
                    //p.DeliIDOpt
                    p.OptimizationSequence = djikList.Where(d => d.Vertex == p.DeliIDOpt).FirstOrDefault().Sequence + 1;
                }
            }
            catch (Exception e)
            {

            }

        }
        #endregion

        #region MassagePhoneNumber
        public static string MassagePhoneNumber(string phone, string countryCode)
        {
            //this is to remove the leading 0 (if any) + dash
            phone = phone.Replace("-", "");
            phone = phone.Replace("+", "");
            phone = phone.Replace(" ", "");
            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");

            if (phone.StartsWith(countryCode))
            {
                phone = phone.Substring(countryCode.Length, phone.Length - countryCode.Length);
            }

            //phone = phone.TrimStart(new Char[] { '0' });

            return phone;
        }
        #endregion

        #region GenerateRandomAlphNumeric
        public static string GenerateRandomAlphNumeric(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region ConstructGameType
        public static List<SelectListItem> ConstructGameType()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = Resources.PackBuddyShared.lblBeauty;
            firstAcc.Value = "0";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = Resources.PackBuddyShared.lblBeast;
            secondAcc.Value = "1";
            accounts.Add(secondAcc);

            return accounts;
        }
        #endregion

        #region ConstructStatus
        public static List<SelectListItem> ConstructStatus()
        {
            var accounts = new List<SelectListItem>();

            var firstAcc = new SelectListItem();
            firstAcc.Text = Resources.PackBuddyShared.lblActive;
            firstAcc.Value = "1";
            accounts.Add(firstAcc);

            var secondAcc = new SelectListItem();
            secondAcc.Text = Resources.PackBuddyShared.lblInActive;
            secondAcc.Value = "0";
            accounts.Add(secondAcc);

            return accounts;
        }
        #endregion

    }

    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static MerchantModel GetObjectMerchantModel(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new MerchantModel() : JsonConvert.DeserializeObject<MerchantModel>(value);
        }

        public static CartCookie GetObjectCartCookie(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new CartCookie() : JsonConvert.DeserializeObject<CartCookie>(value);
        }

        public static ClientModel GetObjectClientCookie(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new ClientModel() : JsonConvert.DeserializeObject<ClientModel>(value);
        }

        public static PlaceOrderListModel GetObjectPlaceOrderList(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new PlaceOrderListModel() : JsonConvert.DeserializeObject<PlaceOrderListModel>(value);
        }

        public static StripeCookie GetObjectStripeCookie(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new StripeCookie() : JsonConvert.DeserializeObject<StripeCookie>(value);
        }
    }
}
