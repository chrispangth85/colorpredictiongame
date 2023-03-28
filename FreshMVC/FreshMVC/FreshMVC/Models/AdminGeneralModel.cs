using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FreshMVC.Models
{
    public class PaginationBase
    {
        public IEnumerable<SelectListItem> Pages { get; set; }
        public int[] NewPages { get; set; }
    }

    public class PaginationAdminModel : PaginationBase
    {
        public List<AdminModel> AdminList
        {
            get; set;
        }

        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public int ID { get; set; }
        public string FilterValue { get; set; }

        public PaginationAdminModel()
        {
            AdminList = new List<AdminModel>();
            FilteringCriteria = new List<SelectListItem>();
        }
    }

    public class ChargeSummaryModel
    {
        public int Count { get; set; }
        public string Item { get; set; }
        public string Price { get; set; }
    }

    public class SendItemModel
    {
        public IEnumerable<SelectListItem> VehicleList { get; set; }
        public string VehicleType { get; set; }
        public string Item { get; set; }
        public string ParcelValue { get; set; }

        public decimal TotalPrice { get; set; }

        public List<ChargeSummaryModel> ChargeList { get; set; }
        public IEnumerable<SelectListItem> WeightList { get; set; }
        public string SelectedWeight { get; set; }

        public string PickupAddress { get; set; }
        public string PickupPhone { get; set; }
        public string PickupContactName { get; set; }
        public string CurrentDT { get; set; }
        public string Username { get; set; }
        public string UserBalance { get; set; }
        public string UserFirstName { get; set; }
        public string Extension { get; set; }
        public List<PointModel> Points { get; set; }
        public decimal Total { get; set; }

        public SendItemModel()
        {
            Points = new List<PointModel>();
            ChargeList = new List<ChargeSummaryModel>();
        }
    }

    public class PointModel
    {
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string PickUpTime { get; set; }
        public string SpecialInstruction { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }
        public int OptimizedIndex { get; set; }
        public bool OptimizedFirstPoint { get; set; }
        public double DistanceFromFirstPoint { get; set; }
        public double dLat { get; set; }
        public double dLng { get; set; }
    }

    public class TemporaryAddressModel
    {
        public string TemporaryClientAddress { get; set; }
        public decimal TemporaryLat { get; set; }
        public decimal TemporaryLng { get; set; }
        public string TemporaryPostalCode { get; set; }
        public string TemporaryCity { get; set; }
        public string Wallet { get; set; }
        public int TotalVisit { get; set; }
        public List<BarChartModel> BarList { get; set; }

        public TemporaryAddressModel()
        {
            BarList = new List<BarChartModel>();
        }
    }

    public class BarChartModel
    {
        public string DateString { get; set; }
        public DateTime Date { get; set; }
        public string TotalOrdered { get; set; }
        public string TotalConfirmed { get; set; }

        public BarChartModel()
        {
            TotalOrdered = "0";
            TotalConfirmed = "0";
        }
    }

    public class AdminModel
    {
        public IEnumerable<SelectListItem> AccountTypeList { get; set; }
        public string AccountType { get; set; }
        public bool IsAgent { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public string SelectedState { get; set; }
        public string Extension { get; set; }
        public string Number { get; set; }
        public string WalletBalance { get; set; }
        public string CashWallet { get; set; }
        public string RechargeWallet { get; set; }
        public string WeeklyWithdrawal { get; set; }
        public string NetBalance { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        
        public string JoinedOn { get; set; }

        public string Status { get; set; }
        public int UserID { get; set; }

        public string ParentID { get; set; }
        public string Address { get; set; }
        public string Path { get; set; }
        public string ProtraitPhotoPath { get; set; }
        public decimal MinimumOrder { get; set; }
        public IFormFile ProtraitPhoto { get; set; }
        public string Path1 { get; set; }
        public string ProtraitPhotoPath1 { get; set; }
        public IFormFile ProtraitPhoto1 { get; set; }
        public string Path2 { get; set; }
        public string ProtraitPhotoPath2 { get; set; }
        public IFormFile ProtraitPhoto2 { get; set; }

        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string StripeAPI { get; set; }
        public string DelyvaAPI { get; set; }
        public string DelyvaCustomerID { get; set; }
        public IEnumerable<SelectListItem> PaymentGatewayList { get; set; }
        public string SelectedPaymentGateway { get; set; }
        public bool isMondayWorking { get; set; }
        public bool isTuesWorking { get; set; }
        public bool isWedWorking { get; set; }
        public bool isThursWorking { get; set; }
        public bool isFridayWorking { get; set; }
        public bool isSatWorking { get; set; }
        public bool isSunWorking { get; set; }
        public string Phone { get; set; }
        public string Bank { get; set; }
        public string BankName { get; set; }
        public string BankAccNo { get; set; }
        public string TnGImage { get; set; }
        public string Website { get; set; }
        public string TnGImagePhotoPath { get; set; }
        public int AdvanceHourOrder { get; set; }
        public string SubscriptionDate { get; set; }
        public IFormFile TnGImagePhoto { get; set; }

        public string Description { get; set; }
        public string LastName { get; set; }

        public string OperationHours { get; set; }
        public string ClosingHours { get; set; }
        public int MaximumOrder { get; set; }
        public string IntroducerCode { get; set; }
        public bool IsApplyPointSystem { get; set; }
        public bool ProductSizeRectangle { get; set; }

        public string Rating { get; set; }

        public string Vehicle { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string ReferralCode { get; set; }

        public string ReferralID { get; set; }
        public string ImagePath { get; set; }
        public string Remark { get; set; }
        public string AppUser { get; set; }
        public string TransactionDate { get; set; }
        public string CashIn { get; set; }
        public string CashOut { get; set; }

        public string Password { get; set; }
        public string CurrentPassword { get; set; }
        public bool IsChangePassword { get; set; }

        public bool AdminChangePassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Pin { get; set; }

        public bool IsActive { get; set; }
        public string Intro { get; set; }

        public string FrontIcPath { get; set; }
        public string BackIcPath { get; set; }
        public string LicensePath { get; set; }
        public string StatusComment { get; set; }

        public string RoadTaxPath { get; set; }

        public List<AdminAccessRightModel> AdminAccessRight { get; set; }

        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
    }

    public class PaginationRewardModel : PaginationBase
    {
        public List<RewardModel> RewardList { get; set; }

        public PaginationRewardModel()
        {
            RewardList = new List<RewardModel>();
        }
    }

    public class RewardModel
    {
        public int RewardID { get; set; }
        public string Name { get; set; }
        public string TnC { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int PointNeeded { get; set; }
        public string ExpiryDate { get; set; }
        public string CodeList { get; set; }
        public List<RewardCodeModel> RewardCodeList { get; set; }

        public RewardModel()
        {
            RewardCodeList = new List<RewardCodeModel>();
        }
    }

    public class RewardCodeModel
    {
        public int RewardCodeID { get; set; }
        public int RewardID { get; set; }
        public string RewardCode { get; set; }
        public string RedeemedBy { get; set; }
        public string RedeemedOn { get; set; }
    }

    public class PaginationOrderModel : PaginationBase
    {
        public string SelectedFilteringCriteria { get; set; }
        public List<OrderModel> OrderList
        {
            get; set;
        }

        public PaginationOrderModel()
        {
            OrderList = new List<OrderModel>();
        }
    }

    public class OrderModel
    {
        public int Number { get; set; }
        public int OrderID { get; set; }
        public string OrderIDDisplay { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public decimal ParcelValue { get; set; }
        public string PromoCode { get; set; }
        public decimal Amount { get; set; }
        public string VehicleType { get; set; }
        public string Rider { get; set; }

        public string CompletedDate { get; set; }
        public decimal Weight { get; set; }
        public List<OrderPointModel> OrderPointList { get; set; }

        public OrderModel()
        {
            OrderPointList = new List<OrderPointModel>();
        }
    }

    public class OrderPointModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Arrival { get; set; }
        public string Comment { get; set; }
        public string ImagePath { get; set; }

        public string RiderArrivalTime { get; set; }
    }

    public class PaginationNotificationModel : PaginationBase
    {
        public List<NotificationModel> NotificationList
        {
            get; set;
        }

        public PaginationNotificationModel()
        {
            NotificationList = new List<NotificationModel>();
        }
    }

    public class NotificationModel
    {
        public int Number
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }

        public string MobileTitle { get; set; }
        public string MobileContent { get; set; }
        public string Description
        {
            get; set;
        }

        public string ProtraitPhotoName
        {
            get; set;
        }
        public string ProtraitPhotoPath
        {
            get; set;
        }
        public IFormFile ProtraitPhoto
        {
            get; set;
        }

        public string RedirectURL
        {
            get; set;
        }
        public string Path
        {
            get; set;
        }

        public string SelectedTarget
        {
            get; set;
        }

        public string FromTime { get; set; }

        public string From
        {
            get; set;
        }

        public string ToTime { get; set; }

        public string To
        {
            get; set;
        }

        public string CreatedOn
        {
            get; set;
        }

        public string Image
        {
            get; set;
        }
    }

    public class PaginationBannerModel : PaginationBase
    {
        public List<BannerModel> BannerList
        {
            get; set;
        }

        public PaginationBannerModel()
        {
            BannerList = new List<BannerModel>();
        }
    }

    public class BannerModel
    {
        public int Number
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }

        public string ProtraitPhotoName
        {
            get; set;
        }
        public string ProtraitPhotoPath
        {
            get; set;
        }
        public IFormFile ProtraitPhoto
        {
            get; set;
        }

        public string RedirectURL
        {
            get; set;
        }
        public string Path
        {
            get; set;
        }

        public string From
        {
            get; set;
        }

        public string To
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string CreatedOn
        {
            get; set;
        }

        public string Image
        {
            get; set;
        }

        public string ImageURL
        {
            get; set;
        }

        public Decimal Price { get; set; }
        public Decimal PriceBusiness { get; set; }
        public string VehicleType { get; set; }
    }

    public class PaginationPromoModel : PaginationBase
    {
        public List<PromoModel> List
        {
            get; set;
        }

        public PaginationPromoModel()
        {
            List = new List<PromoModel>();
        }
    }

    public class PromoModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class PaginationCategoryModel : PaginationBase
    {
        public List<CategoryModel> List
        {
            get; set;
        }

        public PaginationCategoryModel()
        {
            List = new List<CategoryModel>();
        }
    }

    public class CategoryModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public decimal Price { get; set; }
    }

    public class PaginationModel<T> : PaginationBase
    {
        public List<T> ModelList
        {
            get; set;
        }

        public PaginationModel()
        {
            ModelList = new List<T>();
        }
    }

    public class ChargeModel
    {
        public int Number
        {
            get; set;
        }

        public string Distance
        {
            get; set;
        }
        public string Rate
        {
            get; set;
        }

        public string CreatedOn
        {
            get; set;
        }

        public string CreatedBy
        {
            get; set;
        }
    }

    public class PaginationCustomerSupportMessageModel : PaginationBase
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int OrderID { get; set; }

        public List<CustomerSupportMessageModel> list
        {
            get; set;
        }

        public PaginationCustomerSupportMessageModel()
        {
            list = new List<CustomerSupportMessageModel>();
        }
    }

    public class CustomerSupportMessageModel
    {
        public DateTime LastMessageDT { get; set; }
        public string CreatedOn { get; set; }
        public string Username { get; set; }
        public string Support { get; set; }
        public string Firstname { get; set; }
        public int OrderID { get; set; }
        public int UnreadTotal { get; set; }
        public string Message { get; set; }
        public string UserSender { get; set; }
        public int MessageType { get; set; }
        public string Image { get; set; }
        public string SupportImage { get; set; }
        public bool IsUser { get; set; }

    }

    public class PaginationPaymentModel : PaginationBase
    {
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public string FilterValue { get; set; }
        public IFormFile ExcelFile { get; set; }

        public string TemporaryOrderID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<PaymentModel> List
        {
            get; set;
        }

        public PaginationPaymentModel()
        {
            List = new List<PaymentModel>();
        }
    }

    public class PaginationBankModelModel : PaginationBase
    {
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public string FilterValue { get; set; }

        public List<BankModel> List
        {
            get; set;
        }

        public PaginationBankModelModel()
        {
            List = new List<BankModel>();
        }
    }

    public class BankModel
    {        
        public int Id { get; set; }
        public string ActualName { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }        
    }


    public class SystemConfigurationModel
    {
        public decimal WithdrawalMinAmount { get; set; }
        public decimal WithdrawalMaxAmount { get; set; }
        public decimal WithdrawalServiceFee { get; set; }
        public decimal FirstChargeLevel1 { get; set; }
        public decimal FirstChargeLevel2 { get; set; }
        public decimal RechargeMinAmount { get; set; }
        public decimal RechargeMaxAmount { get; set; }
        public decimal WinningLevel1 { get; set; }
        public decimal WinningLevel2 { get; set; }
        public decimal GameServiceFee { get; set; }
        public decimal GameHandlingFee { get; set; }
        public decimal SponsorBonusLevel1 { get; set; }
        public decimal SponsorBonusLevel2 { get; set; }
        public decimal SponsorBonusLevel3 { get; set; }
        public decimal CoinWithdrawalCharges { get; set; }
        public decimal CoinMinWithdrawal { get; set; }
        public decimal CoinMaxWithdrawal { get; set; }
        public string GatewayPaymentHost { get; set; }
        public string GatewayWithdrawalHost { get; set; }
        public string GatewayMemberID { get; set; }
        public string GatewayPaymentKey { get; set; }
        public string SupportPhoneNumber { get; set; }
        public string SupportApkUrl { get; set; }
    }

    public class PaymentModel
    {
        public int Number { get; set; }

        public string BalanceAfterWdr { get; set; }
        public string BalanceBeforeWdr { get; set; }
        public string ServiceFee { get; set; }
        public string PhoneNumber { get; set; }
        public string MerchantCode { get; set; }
        public string id { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string Amount { get; set; }
        public string FinalAmount { get; set; }
        public string Currency { get; set; }
        public string ProductDesc { get; set; }
        public string Username { get; set; }
        public string CashName { get; set; }
        public string CashIn { get; set; }
        public string CashOut { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Signature { get; set; }
        public string BankName { get; set; }
        public string Bankcode { get; set; }
        public string RefNo { get; set; }
        public string Created { get; set; }
        public string AccountName { get; set; }
        public string CardNo { get; set; }
        public string SubBranch { get; set; }
        public string NotifyUrl { get; set; }
        public string CallbackUrl { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PayMd5 { get; set; }
        public string PaymentUrl { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }        
        public IEnumerable<SelectListItem> BankList { get; set; }
        public string SelectedBank { get; set; }
    }

    public class AdminAccessRightModel
    {
        public string MainModule { get; set; }
        public string Module { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionName { get; set; }
        public bool Function { get; set; }
        public bool IsSelectAll { get; set; }
    }

    public class PaginationProductModel : PaginationBase
    {
        public IFormFile ExcelFile { get; set; }
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public string FilterValue { get; set; }

        public List<ProductModel> List
        {
            get; set;
        }

        public PaginationProductModel()
        {
            List = new List<ProductModel>();
        }
    }

    public class ProductModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string AdditionalDesc { get; set; }
        public string Other { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImagePhoto { get; set; }
        public string ChoiceSideSelectedDetail { get; set; }
        public string Path { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal ParcelValue { get; set; }
        public decimal TotalPrice { get; set; }
        public int SetDay { get; set; }
        public int SetDayExpiry { get; set; }
        public bool IsManageInventory { get; set; }
        public bool IsPublish { get; set; }
        public string SetDetail { get; set; }
        public string PriceInString { get; set; }
        public bool IsSideItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int MerchantParentID { get; set; }
        public string MerchantUsername { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAddress { get; set; }

        public List<ProductChoiceModel> ProductChoiceList { get; set; }
        public List<ProductSideModel> ProductSideList { get; set; }

        public List<SingleProduct> selectedProducts { get; set; }
        public int Quantity { get; set; }
        public int StockAmount { get; set; }


        public IEnumerable<SelectListItem> CategoryProductList { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedProduct { get; set; }

        public int MerchantProductId { get; set; }
        public List<ProductChoicesSides> ProductChoicesSides { get; set; }
        public IEnumerable<SelectListItem> TransactionTypeList { get; set; }
        public string SelectedTransactionType { get; set; }

        public ProductModel()
        {
            ProductChoiceList = new List<ProductChoiceModel>();
            ProductSideList = new List<ProductSideModel>();
            selectedProducts = new List<SingleProduct>();
            ProductChoicesSides = new List<ProductChoicesSides>();
            SetDay = 1;//default to have set day 1
        }
    }

    //Mainly we need to duplicate this object, because original this object has IFormFile which somehow causes the serialize doesn't work [we do not need this on Merchant Home Page. We need it on merchant backend to upload product]
    public class ProductModelDup
    {
        public int id { get; set; }
        public int UniqueID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImagePath { get; set; }
        public string ChoiceSideSelectedDetail { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal TotalPrice { get; set; }
        public int SetDay { get; set; }
        public int SetDayExpiry { get; set; }
        public string SetDetail { get; set; }
        public string PriceInString { get; set; }
        public string CategoryName { get; set; }
        public string MerchantUsername { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAddress { get; set; }
        public bool IsManageInventory { get; set; }
        public int StockQuantity { get; set; }

        public List<SingleProduct> selectedProducts { get; set; }
        public int Quantity { get; set; }

        public List<ProductChoicesSides> ProductChoicesSides { get; set; }
        public int ProductID { get; set; }

        public ProductModelDup()
        {
            selectedProducts = new List<SingleProduct>();
            ProductChoicesSides = new List<ProductChoicesSides>();
            SetDay = 1;//default to have set day 1
        }
    }

    public class SingleProduct
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }

    public class PaginationProductChoiceModel : PaginationBase
    {
        public int ProductID { get; set; }
        public List<ProductChoiceModel> List
        {
            get; set;
        }

        public PaginationProductChoiceModel()
        {
            List = new List<ProductChoiceModel>();
        }
    }

    public class ProductChoiceModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public int Sequence { get; set; }
        public int ProductID { get; set; }
        public int MinimumPick { get; set; }
    }

    public class PaginationProductChoiceItemModel : PaginationBase
    {
        public int ProductChoiceID { get; set; }
        public int ProductID { get; set; }
        public List<ProductChoiceItemModel> List
        {
            get; set;
        }

        public PaginationProductChoiceItemModel()
        {
            List = new List<ProductChoiceItemModel>();
        }
    }

    public class ProductChoiceItemModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public int ProductChoiceID { get; set; }
        public int ProductID { get; set; }
    }

    public class PaginationProductSideModel : PaginationBase
    {
        public int ProductID { get; set; }
        public List<ProductSideModel> List
        {
            get; set;
        }

        public PaginationProductSideModel()
        {
            List = new List<ProductSideModel>();
        }
    }

    public class ProductSideModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public int Sequence { get; set; }
        public int ProductID { get; set; }
        public int MinimumPick { get; set; }
    }

    public class PaginationProductSideItemModel : PaginationBase
    {
        public int ProductSideID { get; set; }
        public int ProductID { get; set; }
        public List<ProductSideItemModel> List
        {
            get; set;
        }

        public PaginationProductSideItemModel()
        {
            List = new List<ProductSideItemModel>();
        }
    }

    public class ProductSideItemModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public int ProductSideID { get; set; }
        public int ProductID { get; set; }
    }

    public class PaginationProductSetModel : PaginationBase
    {
        public List<ProductSetModel> List
        {
            get; set;
        }

        public PaginationProductSetModel()
        {
            List = new List<ProductSetModel>();
        }
    }

    public class ProductSetModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImagePhoto { get; set; }
        public string Path { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int CategoryID { get; set; }
        public string Merchant { get; set; }

        public List<ProductSetMainModel> ProductSetMainList { get; set; }


        public ProductSetModel()
        {
            ProductSetMainList = new List<ProductSetMainModel>();
        }
    }

    public class ProductSetMainModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Desc { get; set; }
        public int Sequence { get; set; }
        public int ProductSetID { get; set; }
    }

    public class PaginationReceiptModel : PaginationBase
    {
        public List<ReceiptModel> List { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public PaginationReceiptModel()
        {
            List = new List<ReceiptModel>();
            Categories = new List<SelectListItem>();
        }
    }

    public class ReceiptModel
    {
        public string ProtraitPhotoPath { get; set; }
        public IFormFile ProtraitPhoto { get; set; }
        public string Date { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public string Path { get; set; }

        public ReceiptModel()
        {
            Categories = new List<SelectListItem>();
        }
    }

    public class MemberHomeModel
    {
        public string ThisYearReceiptCount { get; set; }
        public string Username { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public List<ProductModel> TopProductList { get; set; }
        public List<ProductModel> ActiveProductList { get; set; }

        public List<BannerModel> BannerList { get; set; }

        public MemberHomeModel()
        {
            TopProductList = new List<ProductModel>();
            ActiveProductList = new List<ProductModel>();
            BannerList = new List<BannerModel>();
        }
    }

    public class ProductListModel
    {
        public List<ProductModel> ProductList { get; set; }
        public ProductListModel()
        {
            ProductList = new List<ProductModel>();
        }
    }

    public class CategoryProduct
    {
        public string CategoryName { get; set; }
        public List<ProductModelDup> ProductList { get; set; }
        public CategoryProduct()
        {
            ProductList = new List<ProductModelDup>();
        }
    }

    public class MerchantModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public decimal Wallet { get; set; }
        public decimal Amount { get; set; }
        public string Address { get; set; }
        public int ID { get; set; }
        public bool DelyvaAllOrderGenerated { get; set; }
        public string Banner { get; set; }
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
        public bool HasBanner { get; set; }
        public bool HasBanner1 { get; set; }
        public bool HasBanner2 { get; set; }
        public int MaximumOrder { get; set; }
        public decimal MinimumOrder { get; set; }
        public string Phone { get; set; }
        public string Bank { get; set; }
        public string BankBeneName { get; set; }
        public string BankAccNo { get; set; }
        public string TouchNGoQR { get; set; }
        public string OperatingDay { get; set; }
        public string ClosingDay { get; set; }
        public string OperatingOpenHour { get; set; }
        public string OperatingClosingHour { get; set; }
        public List<ProductModelDup> ProductList { get; set; }
        public List<PostalChargesModel> PostalCharges { get; set; }
        public List<CategoryProduct> List { get; set; }
        public IEnumerable<SelectListItem> TransactionTypeList { get; set; }
        public string SelectedTransactionType { get; set; }
        public int ShowPendingBtn { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string Website { get; set; }
        public bool ProductSizeRectangle { get; set; }
        public string MerchantAddress { get; set; }
        public string MerchantDeliveryDateTimeString { get; set; }
        public DateTime MerchantDeliveryDateTime { get; set; }
        public string ClientOrMerchantDateTime { get; set; }
        public string ClientOrMerchantAddress { get; set; }
        public int ClientSelectedDelivery { get; set; }//0: Pickup 1: Delivery
        public string ClientSelectedDeliveryString { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalWeight { get; set; }
        public Decimal TotalDiscount { get; set; }
        public Decimal TotalCoinRedeem { get; set; }
        public Decimal TotalShipping { get; set; }
        public Decimal TotalTax { get; set; }
        public Decimal GrandTotal { get; set; }
        public int TotalItemInCart { get; set; }
        public int ClientSelectedPaymentMethod { get; set; }//0:Manual Bank Transfer 1:TouchNGoEWallet
        public string AdditionalInfo { get; set; }
        public decimal Points { get; set; }
        public int OrderID { get; set; }
        public string OrderGUID { get;set; }
        public string OrderLink { get; set; }
        public string WhatsappLink { get; set; }
        public int Status { get; set; }//0:Ordered 1:Confirmed 2:Completed -1:Cancelled
        public string CurrentDT { get; set; }
        public List<AddressSetModel> AddressList { get; set; }
        public string TemporaryClientAddress { get; set; }
        public int TemporaryIndex { get; set; }
        public int TemporaryIndexAddress { get; set; }
        public decimal TemporaryLat { get; set; }
        public decimal TemporaryLng { get; set; }
        public string TemporaryPostalCode { get; set; }
        public string TemporaryCity { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string TelegramChatID { get; set; }
        public string OrderedOnString { get; set; }
        public int IsConfirmed { get; set; }
        public string ConfirmedOnString { get; set; }
        public int IsCompleted { get; set; }
        public string CompletedOnString { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string LoggedInUsername { get; set; }
        public bool IsFavMerchantShop { get; set; }
        public decimal UserPointsForCurrentMerchant { get; set; }
        public string LoggedInMessage { get; set; }
        public List<AddressModel> UserAddressList { get; set; }
        public int AdvanceHourOrder { get; set; }
        public string Type { get; set; }
        public bool IsApplyPointSystem { get; set; }
        public string StripeAPI { get; set; }
        public string SelectedPaymentMerchant { get; set; }
        public bool IsSameSessionVersionMerchant { get; set; }
        public List<ServiceCompanyModel> serviceList { get; set; }
        public string SelectedServiceCompany { get; set; }
        public string MerchantDelyvaAPI { get; set; }
        public string MerchantDelyvaCustomerID { get; set; }

        public MerchantModel()
        {
            ProductList = new List<ProductModelDup>();
            List = new List<CategoryProduct>();
            AddressList = new List<AddressSetModel>();
            UserAddressList = new List<AddressModel>();
            PostalCharges = new List<PostalChargesModel>();
            serviceList = new List<ServiceCompanyModel>();
        }
    }
    public class AddressSetModel 
    {
        public string Address { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryDateTimeString { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string PickupDateTimeString { get; set; }
        public string DelyvaOrderID { get; set; }
        public string FloorHouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Remark { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int FlaggedMaxOrderReached { get; set; }
    }

    public class ProductChoicesSides
    {
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int MinimumPick { get; set; }
        public string MinimumPickDesc { get; set; }
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

    public class CartCookie
    {
        public List<MerchantModel> Merchant { get; set; }
        public CartCookie()
        {
            Merchant = new List<MerchantModel>();
        }
    }

    public class StripeCookie
    {
        public string Merchant { get; set; }
        public string Guid { get; set; }
        public decimal Amount { get; set; }
        public string SessionID { get; set; }
        public string PaymentIntentID { get; set; }
        public string StripeAPI { get; set; }
    }

    public class PaginationPlaceOrderModel : PaginationBase
    {
        public int OrderStatus { get; set; }
        public string SearchInput { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }
        public string SelectedOption { get; set; }
        public string Date { get; set; }
        public int OrderType { get; set; }
        public bool IsViewAll { get; set; }
        public List<PlaceOrderModel> List
        {
            get; set;
        }

        public PaginationPlaceOrderModel()
        {
            List = new List<PlaceOrderModel>();
        }
    }

    public class PlaceOrderModel
    {
        public int Number { get; set; }
        public int OptimizationSequence { get; set; }
        public string OrderID { get; set; }
        public string OrderGUID { get; set; }
        public string MerchantUsername { get; set; }
        public string MerchantPhone { get; set; }
        public string MerchantTnGQR { get; set; }
        public string MerchantBankBeneficial { get; set; }
        public string MerchantBank { get; set; }
        public string MerchantAccountNumber { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalWeight { get; set; }
        public string CreatedOnString { get; set; }
        public bool PointsRedeem { get; set; }
        public string MerchantApplyPointSystem { get; set; }
        public string DeliveryOnString { get; set; }
        public int DeliIDOpt { get; set; }
        public int DeliLinkedIDOpt { get; set; }
        public decimal DeliLatOpt { get; set; }
        public decimal DeliLngOpt { get; set; }
        public int ClientSelectedPaymentMethod { get; set; }//0:Manual Bank Transfer 1:COD 2:TouchNGoEWallet
        public string OrderLink { get; set; }
        public string OrderLinkOri { get; set; }
        public string WhatsappLink { get; set; }
        public MerchantModel MerchantModel { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string DeliveryType { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string ViewOrderLink { get; set; }
        public string ViewDelyvaOrderLink { get; set; }
        public string DelyvaOrderID { get; set; }
        public string Quantity { get; set; }
        public string FirstProductImage { get; set; }
        public string FirstProductTitle { get; set; }
        public string ConfirmedOnString { get; set; }
        public string CompletedOnString { get; set; }
    }

    public class ClientModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IntroCode { get; set; }
    }

    public class PaginationAddressModel : PaginationBase
    {
        public List<AddressModel> List { get; set; }

        public PaginationAddressModel()
        {
            List = new List<AddressModel>();
        }
    }

    public class AddressModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string FloorHouse { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string TemporaryClientAddress { get; set; }
        public decimal TemporaryLat { get; set; }
        public decimal TemporaryLng { get; set; }

    }

    public class PaginationFavShopModel : PaginationBase
    {
        public List<FavShopModel> List { get; set; }

        public PaginationFavShopModel()
        {
            List = new List<FavShopModel>();
        }
    }

    public class FavShopModel
    {
        public int Id { get; set; }
        public string MerchantUsername { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAddress { get; set; }
        public string MerchantBanner { get; set; }

    }

    public class AddressOptimization
    {
        public int ID { get; set; }
        public int UserLinkedID { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }

    public class LocationMapOptimization
    {
        public int From { get; set; }
        public double FromLat { get; set; }
        public double FromLng { get; set; }
        public int To { get; set; }
        public double ToLat { get; set; }
        public double ToLng { get; set; }
        public double Distance { get; set; }
    }

    public class DjikstraOptimization
    {
        public int Vertex { get; set; }
        public int UserLinkedVertex { get; set; }
        public double ShortestDistance { get; set; }
        public int PreviousVertex { get; set; }
        public bool VisitedVertex { get; set; }
        public int Sequence { get; set; }
        public string Address { get; set; }
    }

    public class PlaceOrderListModel
    {
        public List<PlaceOrderModel> List { get; set; }
        public string MerchantAddress { get; set; }
        public decimal MerchantLat { get; set; }
        public decimal MerchantLng { get; set; }
        public string TemporaryClientAddress { get; set; }
        public decimal TemporaryLat { get; set; }
        public decimal TemporaryLng { get; set; }
        public int TemporaryIndex { get; set; }
        public int PositionToRemove { get; set; }
        public int LinkedIDOpt { get; set; }

        public PlaceOrderListModel()
        {
            List = new List<PlaceOrderModel>();
        }
    }

    public class PostalChargesModel
    {
        public string Postal { get; set; }
        public decimal Charges { get; set; }
    }

    public class GooglePlusAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
    }

    public class GoogleUserOutputData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
    }

    public class ServiceCompanyModel
    {
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ServiceCompanyCode { get; set; }
    }

    public class DelyvaServiceCompanyQuoteModel
    {
        public List<ServiceCompanyModel> list { get; set; }

        public DelyvaServiceCompanyQuoteModel()
        {
            list = new List<ServiceCompanyModel>();
        }
    }

    public class PaginationRedPacketModel : PaginationBase
    {
        public List<RedPacketModel> List { get; set; }
        public decimal RedPacketBalance { get; set; }
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public string FilterValue { get; set; }
        public PaginationRedPacketModel()
        {
            List = new List<RedPacketModel>();
        }
    }

    public class RedPacketModel
    {
        public int Number { get; set; }
        public int ID { get; set; }
        public string IDEncrypted { get; set; }
        public string ReferenceID { get; set; }
        public string Agent { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string CreatedOn { get; set; }
        public string ClaimedOn { get; set; }
        public string Status { get; set; }
        public int StatusInt { get; set; }
        public decimal RedPacketBalance { get; set; }
        public RedPacketModel()
        {
        }
    }

    public class SponsorChartModel
    {
        public List<SponsorModel> FirstLevel { get; set; }
        public List<SponsorModel> SecondLevel { get; set; }
        public List<SponsorModel> ThirdLevel { get; set; }
        public List<SponsorModel> FourthLevel { get; set; }
        public List<SponsorModel> FifthLevel { get; set; }
        public decimal RedPacketBalance { get; set; }
        public string Upline { get; set; }
        public string UpIDEnc { get; set; }
        public SponsorChartModel()
        {
            FirstLevel = new List<SponsorModel>();
            SecondLevel = new List<SponsorModel>();
            ThirdLevel = new List<SponsorModel>();
            FourthLevel = new List<SponsorModel>();
            FifthLevel = new List<SponsorModel>();
        }
    }

    public class SponsorModel
    {
        public string Username { get; set; }
        public string EncryptedUsername { get; set; }
    }

    public class PaginationGameModel : PaginationBase
    {
        public List<GameModel> List { get; set; }
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public int ID { get; set; }
        public string FilterValue { get; set; }

        public PaginationGameModel()
        {
            List = new List<GameModel>();
            FilteringCriteria = new List<SelectListItem>();
        }
    }
    public class GameModel
    {
        public int Number { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }
        public decimal No9Return { get; set; }
        public decimal No8Return { get; set; }
        public decimal No7Return { get; set; }
        public decimal No6Return { get; set; }
        public decimal No5Return { get; set; }
        public decimal No4Return { get; set; }
        public decimal No3Return { get; set; }
        public decimal No2Return { get; set; }
        public decimal No1Return { get; set; }
        public decimal No0Return { get; set; }
        public decimal RedReturn { get; set; }
        public decimal VioletReturn { get; set; }
        public decimal Red0Return { get; set; }
        public decimal GreenReturn { get; set; }
        public decimal Green5Return { get; set; }
        public IEnumerable<SelectListItem> GameTypeList { get; set; }
        public string SelectedGameType { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public string SelectedStatus { get; set; }
        public IFormFile ProductImage { get; set; }
        public string Username { get; set; }
        public string Period { get; set; }
        public decimal BetAmount { get; set; }
        public decimal WinAmount { get; set; }
        public string NumberString { get; set; }
        public GameModel()
        {
        }
    }

    public class MemberGamePageModel
    {
        public List<MemberGameModel> GameList { get; set; }
        public DateTime ServerCurrentTime { get; set; }
        public int TimeLeftBasedEndDateTotalSeconds { get; set; }
        public int MinuteLeft { get; set; }
        public int SecondLeft { get; set; }
        public Decimal Balance { get; set; }
        public MemberGamePageModel()
        {
            GameList = new List<MemberGameModel>();
        }
    }

    public class MemberGameModel
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public string CurrentPeriod { get; set; }
        public int DurationSeconds { get; set; }
        public List<GameHistoryRecord> HistoryList { get; set; }
        public List<GameHistoryRecord> MyRecordHistoryList { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public MemberGameModel()
        {
            HistoryList = new List<GameHistoryRecord>();
            MyRecordHistoryList = new List<GameHistoryRecord>();
        }
    }

    public class GameHistoryRecord
    {
        public string Period { get; set; }
        public decimal TotalBet { get; set; }
        public decimal Price { get; set; }
        public int ResultNumber { get; set; }
        public string ResultNumberString { get; set; }
        public decimal Won { get; set; }
        public string ResultColor { get; set; }
    }

    public class ReverseStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return y.CompareTo(x);
        }
    }

    public class AdminGameMainModel
    {
        public List<AdminGameModel> GameList { get; set; }
        public AdminGameMainModel()
        {
            GameList = new List<AdminGameModel>();
        }
    }

    public class AdminGameModel
    {
        public List<SessionGameModel> SessionList { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public int GameID { get; set; }
        public AdminGameModel()
        {
            SessionList = new List<SessionGameModel>();
        }
    }
    public class SessionGameModel
    {
        public int Number { get; set; }
        public string Period { get; set; }
        public string Result { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int GameState { get; set; }
        public SessionGameModel()
        {

        }
    }

    public class PaginationDailyReportModel : PaginationBase
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<DailyReportModel> DailyReportList
        {
            get; set;
        }

        public PaginationDailyReportModel()
        {
            DailyReportList = new List<DailyReportModel>();
        }
    }

    public class DailyReportModel
    {
        public string Number { get; set; }
        public string id { get; set; }
        public string Created { get; set; }
        public decimal RechargeAmount { get; set; }
        public decimal BetAmount { get; set; }
        public decimal WinAmount { get; set; }
        public decimal WithdrawAmount { get; set; }


    }

    public class TopupModel
    {
        public string Username { get; set; }
        public string TopupTitle { get; set; }
        public string TopupType { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Balance { get; set; }
        public string Amount { get; set; }
        public IEnumerable<SelectListItem> Options { get; set; }
        public string SelectedOption { get; set; }
        public string SelectedOptionText { get; set; }
    }

    public class PaginationCompanyWalletModel : PaginationBase
    {
        public IFormFile ExcelFile { get; set; }
        public IEnumerable<SelectListItem> FilteringCriteria { get; set; }
        public string SelectedFilteringCriteria { get; set; }
        public string FilterValue { get; set; }

        public List<CompanyWalletModel> List
        {
            get; set;
        }

        public PaginationCompanyWalletModel()
        {
            List = new List<CompanyWalletModel>();
        }
    }

    public class CompanyWalletModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> NetworkTypeOptions { get; set; }
        public string NetworkType { get; set; }
        public string WalletAddress { get; set; }
        public Boolean IsActive { get; set; }
    }


    public class PaginationCountryModel : PaginationBase
    {
        public List<CountryModel> List
        {
            get; set;
        }

        public PaginationCountryModel()
        {
            List = new List<CountryModel>();
        }
    }

    public class CountryModel
    {
        public int Number { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Sell { get; set; }
        public decimal Buy { get; set; }

    }


}