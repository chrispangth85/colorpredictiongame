﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Entity.Context.Models
{
    public partial class CvdUser
    {
        public int CusrId { get; set; }
        public string CusrUsername { get; set; }
        public string CusrPassword { get; set; }
        public string CusrPin { get; set; }
        public int CcountryId { get; set; }
        public int CroleId { get; set; }
        public string CusrFirstname { get; set; }
        public string CusrCreatedby { get; set; }
        public DateTime CusrCreatedon { get; set; }
        public string CusrUpdatedby { get; set; }
        public DateTime CusrUpdatedon { get; set; }
        public bool CusrDeletionstate { get; set; }
        public string CusrEmail { get; set; }
        public string CusrLastname { get; set; }
        public string CusrReferralCode { get; set; }
        public string CusrImagepath { get; set; }
        public string CusrReferralid { get; set; }
        public decimal? CusrCashwlt { get; set; }
        public decimal? CusrRechargewlt { get; set; }
        public decimal? CusrRedpacketwlt { get; set; }
        public string MemberLevel2Intro { get; set; }
        public string MemberLevel3Intro { get; set; }
        public string MemberLevel4Intro { get; set; }
        public string MemberLevel5Intro { get; set; }
    }
}