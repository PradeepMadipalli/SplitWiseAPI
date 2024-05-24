using SplitWise.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SplitWise.Model.RequestModels
{
    public class Expenseresponse
    {
        public string? expId { get; set; }
        public string? groupId { get; set; }
        public string? Name { get; set; }
        public string? Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public string? GroupSelection { get; set; }
        public List<ExpenseDetailsresponse> expenseDetailsresponses { get; set; }
    }
    public class ExpenseDetailsresponse
    {
        public string expdId { get; set; }
        public string? expId { get; set; }
        public string? Amount { get; set; }
        public string? Paidby { get; set; }
        public string? ParticipantId { get; set; }
        public string? ParticipantAmount { get; set; }
        public string? SplitBy { get; set; }
        public string? Share { get; set; }
    }
    public class ExpenseShareResponse
    {
        public string? expId { get; set; }
        public string? Name { get; set; }

        public string? Amount { get; set; }
        public string? Paidby { get; set; }
        public string? ParticipantId { get; set; }

        public string? ParticipantAmount { get; set; }
        public string? SplitBy { get; set; }
        public string? Share { get; set; }
    }

}
