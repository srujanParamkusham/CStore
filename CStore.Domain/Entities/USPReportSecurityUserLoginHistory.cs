using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Catalyst.MVC.Infrastructure.Attributes.Entity;
using CStore.Domain.Entities;

namespace CStore.Domain.Entities
{
    [SQLRequestType(typeof(CStore.Domain.SQLRequests.USPReportSecurityUserLoginHistorySQLRequest))]
    public partial class USPReportSecurityUserLoginHistory : DomainEntity
    {
        public Int32 SecurityUserLoginHistoryId { get; set; }
        public Int32? SecurityUserId { get; set; }
        public String UserName { get; set; }
        public String MachineName { get; set; }
        public String ApplicationCode { get; set; }
        public Boolean SuccessfulLogin { get; set; }
        public Boolean AccountWasLocked { get; set; }
        public String IPAddress { get; set; }
        public String Browser { get; set; }
        public String ScreenResolution { get; set; }
        public String Message { get; set; }
        public String SessionId { get; set; }
        public DateTime? SessionEndDate { get; set; }
        public DateTime? LastRequestDate { get; set; }
        public DateTime? SessionTimeoutDate { get; set; }
        public String LastRequestUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public String CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public String ModifyUser { get; set; }
    }
}
