using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Context.Dtos
{
    public class TransactionDto
    {
        public string Username { get; set; }
        public decimal CashWallet { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }

        public List<TransactionLog> TransLog { get; set; }

        public TransactionDto()
        {
            TransLog = new List<TransactionLog>();
        }
    }

    public class TransactionLog
    {
        public string TransactionType { get; set; }

        public string TransactionDate { get; set; }

        public decimal TransactionCredit { get; set; }

        public string PaymentType { get; set; }

        public string Status { get; set; }

        public string TransactionNo { get; set; }

        public int OrderId { get; set; }
    }

    public class TransactionDetails
    {
        public string orderId { get; set; }
        public List<TransactionPickUpAndDropOffPoints> TransPoint { get; set; }
        public int OK { get; set; }
        public string Message { get; set; }

        public TransactionDetails()
        {
            TransPoint = new List<TransactionPickUpAndDropOffPoints>();
        }
    }

    public class TransactionPickUpAndDropOffPoints
    {
        public string Location { get; set; }
    }

    public class EarningLog
    {
        public int OK { get; set; }
        public string Message { get; set; }

        public string Year { get; set; }

        public string Balance { get; set; }
        public string TotalTripsCurrentMonth { get; set; }
        public List<DailyEarningLog> DailyLogs { get; set; }
        public List<MonthlyEarningLog> Logs { get; set; }
        public EarningLog()
        {
            Logs = new List<MonthlyEarningLog>();
            DailyLogs = new List<DailyEarningLog>();
        }
    }

    public class MonthlyEarningLog
    {
        public string TransDate { get; set; }
        public string Earning { get; set; }
        public int Trips { get; set; }
    }

    public class DailyEarningLog
    {
        public string TransDay { get; set; }
        public string TransMonth { get; set; }
        public decimal Earning { get; set; }
    }
}
