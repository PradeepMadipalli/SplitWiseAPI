using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class GetTransactionResponse
    {
        public Guid TransID { get; set; }
        public string? PaidId { get; set; }
        public string? Groupid { get; set; }
        public string? ReceiverId { get; set; }
        public string? Amount { get; set; }
        public string? TransactionMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdateDBy { get; set; }
    }
}
