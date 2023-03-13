using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace WebApi.Dtos
{
    public class UserDto
    {
        public string vehicle { get; set; }
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string CompanyName { get; set; }
        public string NickName { get; set; }
        public string Language { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string SelectedBank { get; set; }
        public string BankNumber { get; set; }
        public string Pin { get; set; }
        public string PhotoPath { get; set; }
        public string Contact { get; set; }
        public string Otp { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Country { get; set; }  
        public string ReferralCode { get; set; }  
        public bool IsLogin { get; set; }
        public string FirstName { get; set; }
        public int IsPersonal { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }

        public string PlateNumber { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal ReloadAmount { get; set; }
        public int Rating { get; set; }
        public int AccountStatus { get; set; }
        public string StatusComment { get; set; }
        public string ICFront { get; set; }
        public int WorkStatus { get; set; }
        public int? RewardPoint { get; set; }

        public string Token { get; set; }
        public string ICBack { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int TotalActiveJob { get; set; }

        public string LicensePath { get; set; }

        public string roadTax { get; set; }
    }

    public class QueueOrderDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Status { get; set; }
    }

    public class Merchant
    {
        public string Username { get; set; }

        public string MerchantImage { get; set; }
        public string MerchantAdd { get; set; }
        public string MerchantRating { get; set; }

        public string MerchantLat { get; set; }
        public string MerchantLng { get; set; }

        public string MerchantName { get; set; }
    }

    public class MerchantCategoryAndFoods
    {
        public string categoryName { get; set; }
        public string categoryId { get; set; }

        public List<MerchantProduct> products;

        public MerchantCategoryAndFoods()
        {
            products = new List<MerchantProduct>();
        }
    }

    public class MerchantProduct
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }
    }

    public class MerchantFoodCategory
    {
        public string categoryId { get; set; }

        public string categoryName { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
        public string Description { get;set; }
        public decimal ParcelValue { get; set; }
        public string PromoCode { get; set; }
        public int PaymentType { get; set; }
        public int Status { get; set; }
        public int CODPoint { get; set; }

        public string CarPlateNo { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime CompletedDate { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountBase { get; set; }
        public decimal AmountMileageOrExtraHours { get; set; }
        public decimal ExtraMileageKM { get; set; }
        public decimal AmountAddStop { get; set; }
        public decimal AmountAdditionalServiceOrCharge { get; set; }
        public decimal AmountChauffeurTwoWay { get; set; }
        public decimal Discount { get; set; }
        public decimal CompanyCommission { get; set; }
        public decimal RiderOrderAmount { get; set; }
        public decimal Weight { get; set; }
        public string CustomerSupport { get; set; }

        public string CustomerSupportPhone { get; set; }
        public int IsCustomerSupport { get; set; }
        public string RiderUsername { get; set; }
        public string RiderName { get; set; }
        public string RiderImage { get; set; }
        public Decimal RiderRating { get; set; }

        public String VehicleType { get; set; }

        public List<OrderPointDto> Points { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }
        public int PaymentMethodPoint { get; set; }
        public string ChauffeurPhone { get; set; }
        public string ChauffeurName { get; set; }
        public string ChauffeurVehiclePlate { get; set; }
        public int ChauffeurTripType { get; set; }
        public string ChauffeurComments { get; set; }

        public OrderDto()
        {
            Points = new List<OrderPointDto>();
        }
    }

    public class OrderPointDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Arrival { get; set; }
        public bool ClearArrival { get; set; }
        public string Comment { get; set; }
        public string PlaceId { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public double dLat { get; set; }
        public double dLng { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
        public DateTime UpdatedOn { get; set; }
        public double DistanceFromFirstPoint { get; set; }
        public bool OptimizedFirstPoint { get; set; }
        public int Index { get; set; }
        public int OptimizedIndex { get; set; }
        public string ImagePath { get; set; }
    }

    public class OrderMessageRetrieveDto
    {
        public List<OrderMessageDto> List { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }
        public string ResponderName { get; set; }

        public OrderMessageRetrieveDto()
        {
            List = new List<OrderMessageDto>();
        }
    }

    public class EmailObject
    {
        public string email { get; set; }
        public string id { get; set; }
        public DataObject data { get; set; }

        public EmailObject(string zemail, string zid, DataObject zdata)
        {
            email = zemail;
            id = zid;
            data = zdata;
        }
    }

    public class DataObject
    {
        public string name { get; set; }
        public string Total { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string OrderID { get; set; }
        public string Courier { get; set; }
        public string Issued { get; set; }
        public string Vehicle { get; set; }
        public string Address { get; set; }
        public string DropOff { get; set; }
        public string PaymentMethod { get; set; }
        public string StartingPrice { get; set; }
        public string Mileage { get; set; }
        public string MileagePrice { get; set; }
        public string AdditionalStopPrice { get; set; }
        public string link { get; set; }

        public DataObject(string zname, string ztotal, string zdate, string ztime, string zorderid, string zcourier, string zissued, string zvehicle, string zaddress, string zdropoff, string zpaymentmethod, string zstartingprice, string zmileage, string zmileageprice, string zadditionalstopprice, string zlink)
        {
            name = zname;
            Total = ztotal;
            Date = zdate;
            Time = ztime;
            OrderID = zorderid;
            Courier = zcourier;
            Issued = zissued;
            Vehicle = zvehicle;
            Address = zaddress;
            DropOff = zdropoff;
            PaymentMethod = zpaymentmethod;
            StartingPrice = zstartingprice;
            Mileage = zmileage;
            MileagePrice = zmileageprice;
            AdditionalStopPrice = zadditionalstopprice;
            link = zlink;
        }
    }

    public class OrderPointArrivedDto
    {
        public int OrderID { get; set; }
        public int Points { get; set; }
    }

    public class OrderMessageDto
    {
        public int OrderId { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Rider { get; set; }
        public string Message { get; set; }
        public string Read { get; set; }
        public string OrderType { get; set; }
        public string OrderDescr { get; set; }
        public decimal OrderValue { get; set; }
        public string PromoCode { get; set; }
        public int PaymentType { get; set; }
        public int OrderStatus { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal RiderOrderAmount { get; set; }
        public decimal OrderDiscount { get; set; }
        public int CODPoint { get; set; }
        public decimal CompanyCommission { get; set; }
        public string VehicleType { get; set; }
        public string StartPoint { get; set; }
        public DateTime StartArrival { get; set; }
        public string EndPoint { get; set; }
        public DateTime EndArrival { get; set; }
        public DateTime CreatedOn { get; set; }
        public int MessageType { get; set; }
        public int Points { get; set; }
        public string RiderName { get; set; }
        public string RiderImage { get; set; }
        public List<OrderPointDto> OrderPoints { get; set; }
        public int UserRating { get; set; }
    }

    public class SendEmailDto
    {
        public string To { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
    }

    public class UploadDto
    {
        public IFormFile file { get; set; }
        public string Username { get; set; }
        public string Folder { get; set; }
        public string FolderTwo { get; set; }
    }

    public class SmsDto
    {
        public string SelectedCountry { get; set; }
        public string Phone { get; set; }
        public string SMSContent { get; set; }
        public string LanguageCode { get; set; }
    }

    public class LanguageDto
    {
        public string Language { get; set; }

        public int Id { get; set; }
    }

    public class VehicleCapacityModel
    {
        public int ID { get; set; }
        public string VehicleType { get; set; }
        public Decimal Max { get; set; }
        public Decimal PerKMCharge { get; set; }
        public Decimal PerKMChargeBusiness { get; set; }
        public Decimal BaseCharge { get; set; }
        public Decimal AdditionalCharge { get; set; }
    }

    public class PromoDto
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public int OK { get; set; }
    }

    public class ProductRet
    {
        public int ProductId { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncAddOn { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<ProductChoicesSides> ProductChoicesSides { get; set; }
        public string MerchantName { get; set; }
        public string MerchantImage { get; set; }
        public string MerchantAddress { get; set; }
        public decimal MerchantLat { get; set; }
        public decimal MerchantLng { get; set; }
        public int MerchantId { get; set; }

        public ProductRet()
        {
            ProductChoicesSides = new List<ProductChoicesSides>();
        }
    }

    public class ProductChoicesSides
    { 
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int MinimumPick { get; set; }
        public List<Items> Items { get; set; }
        public int Type { get; set; }//Choice = 0 Side = 1
        public int Id { get; set; }
        public int SelectedChoiceIndex { get; set; }

        public ProductChoicesSides()
        {
            Items = new List<Items>();
        }
    }

    public class Items
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ID { get; set; }
        public bool IsCheck { get; set; }
    }

    public class MyCartModel
    {
        public int PlaceOrderId { get; set; }
        public int MerchantId { get; set; }
        public string MerchantUsername { get; set; }
        public string Username { get; set; }
        public decimal SubPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Delivery { get; set; }
        public List<ProductRet> productList { get; set; }
    }

    public class MerchantNewOrderList
    {
        public List<MyCartModel> orderList { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }

        public MerchantNewOrderList()
        {
            orderList = new List<MyCartModel>();
        }
    }

}