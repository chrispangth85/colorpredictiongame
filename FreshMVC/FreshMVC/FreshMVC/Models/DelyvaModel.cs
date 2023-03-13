using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FreshMVC.Models
{
    #region DelyvaQuoteResultParentModel
    public class Price
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Weight
    {
        public double value { get; set; }
        public string unit { get; set; }
    }

    public class Distance
    {
        public double value { get; set; }
        public string unit { get; set; }
    }

    public class Commission
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public double S { get; set; }
    }

    public class AgentCommission
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public double S { get; set; }
    }

    public class Insurance
    {
    }

    public class ServiceCompany
    {
        public int id { get; set; }
        public string companyCode { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class Required
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Addon
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public List<Required> required { get; set; }

        public Addon()
        {
            required = new List<Required>();
        }
    }

    public class Service2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string custDescription { get; set; }
        public string driverInstruction { get; set; }
        public List<object> timeSlot { get; set; }
        public int collectCash { get; set; }
        public string minOrderValue { get; set; }
        public int distanceType { get; set; }
        public int instantQuoteWeight { get; set; }
        public Insurance insurance { get; set; }
        public ServiceCompany serviceCompany { get; set; }
        public List<Addon> addon { get; set; }

        public Service2()
        {
            timeSlot = new List<object>();
            addon = new List<Addon>();
        }
    }

    public class Detail
    {
        public string name { get; set; }
        public string type { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double commission { get; set; }
        public string unit { get; set; }
        public double? agentCommission { get; set; }
    }

    public class Service
    {
        public Price price { get; set; }
        public Weight weight { get; set; }
        public Distance distance { get; set; }
        public Commission commission { get; set; }
        public AgentCommission agentCommission { get; set; }
        public Service2 service { get; set; }
        public List<string> itemType { get; set; }
        public List<Detail> details { get; set; }
        public string name { get; set; }
        public string code { get; set; }

        public Service()
        {
            itemType = new List<string>();
            details = new List<Detail>();
        }
    }

    public class Coord
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }

    public class Waypoint
    {
        public string type { get; set; }
        public DateTime scheduledAt { get; set; }
        public List<Inventory> inventory { get; set; }
        public Contact contact { get; set; }

        public Waypoint()
        {
            inventory = new List<Inventory>();
        }
    }

    public class Mileage
    {
        public string unit { get; set; }
        public double value { get; set; }
        public int accuracy { get; set; }
    }

    public class DelyvaQuoteResultModel
    {
        public List<Service> services { get; set; }
        public List<Waypoint> waypoints { get; set; }
        public string perf { get; set; }
        public Distance distance { get; set; }
        public Mileage mileage { get; set; }

        public DelyvaQuoteResultModel()
        {
            services = new List<Service>();
            waypoints = new List<Waypoint>();
        }
    }

    public class DelyvaQuoteResultParentModel
    {
        public List<object> errors { get; set; }
        public DelyvaQuoteResultModel data { get; set; }

        public DelyvaQuoteResultParentModel()
        {
            errors = new List<object>();
        }
    }
    #endregion

    #region DelyvaQuoteModel
    public class Origin
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }

    public class Destination
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }

    public class DelyvaQuoteModel
    {
        public string customerId { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public Weight weight { get; set; }
    }


    #endregion

    #region DelyvaOrderModel
    public class Inventory
    {
        public string name { get; set; }
        public string type { get; set; }
        public Price price { get; set; }
        public Weight weight { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }
    }
    public class Contact
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string unitNo { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }

    public class DelyvaOrderModel
    {
        public string customerId { get; set; }
        public bool process { get; set; }
        public string serviceCode { get; set; }
        public int paymentMethodId { get; set; }
        public List<Waypoint> waypoint { get; set; }

        public DelyvaOrderModel()
        {
            waypoint = new List<Waypoint>();
        }
    }

    public class DelyvaOrderResult
    {
        public string orderId { get; set; }
        public string status { get; set; }
        public int statusCode { get; set; }
        public object invoiceId { get; set; }
    }

    public class DelyvaOrderResultRoot
    {
        public DelyvaOrderResult data { get; set; }
    }

    #endregion
}