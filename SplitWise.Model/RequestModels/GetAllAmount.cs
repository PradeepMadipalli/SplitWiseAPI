using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class GetUserAmount
    {

        public string? expid { get; set; }
        public string? Amount { get; set; }
        public string? PaidBy { get; set; }

    }
    public class GetAllAmount
    {
        public decimal? TotalAmount { get; set; }
        public decimal? OweAmount { get; set; }
        public decimal? OwedAmount { get; set; }

    }
    public class GetAllAmountDetails
    {
        public string? expId { get; set; }
        public string? PaidBy { get; set; }
        public string? Amount { get; set; }
        public string? participantId { get; set; }
        public string? ParticipantAmount { get; set; }
        public string? share { get; set; }

    }
    public class GetGridandExid
    {
        public string? expId { get; set; }
        public string? Groupid { get; set; }
        public string? expName { get; set; }
    }
    public class GetAllPaidDetails
    {
        public string? expId { get; set; }
        public string? expName { get; set; } 
        public string? Groupid { get; set; }
        public string? PaidBy { get; set; }
        public string? Amount { get; set; }
        public string? participantId { get; set; }
        public string? ParticipantAmount { get; set; }
        public string? share { get; set; }
    }


}
