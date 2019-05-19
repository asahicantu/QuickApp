using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
        public class Svc : AuditableEntity
        {
            public int Id { get; set; }
            public int? DomainId { get; set; }
            public int? AccountId { get; set; }
            public string Country { get; set; }
            public string ServiceDeliveryManager { get; set; }
            public string AccountManager { get; set; }
            public DateTime? Date { get; set; }
            public string QuoteFTL { get; set; }
            public string PO { get; set; }
            public string Client { get; set; }
            public string Field { get; set; }
            public string Well { get; set; }
            public string AU { get; set; }
            public string AC { get; set; }
            public string Portfolio { get; set; }
            public string SubPortfolio { get; set; }
            public string MasterCode { get; set; }
            public string Currency { get; set; }
            public decimal? FXRate { get; set; }
            public string Comment { get; set; }
            public string TechnicalLead { get; set; }
            public string ChangePointTask { get; set; }
            public decimal? ROFO { get; set; }
            public decimal? iMF { get; set; }
            public decimal? MMF { get; set; }
            public decimal? SentToInvoice { get; set; }
            public decimal? Revenue { get; set; }
            public string InvocieNumber { get; set; }
            public decimal? Cost { get; set; }
            public decimal? CostReceived { get; set; }
            public string CostType { get; set; }
            public string GLAccount { get; set; }
            public string CostDescription { get; set; }
            
        }
    }


