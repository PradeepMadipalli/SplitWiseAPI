using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class ExpenseDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid expdId {  get; set; }
        public Guid? expId { get; set; }
        public virtual Expense? Expenses { get; set; }
        public string? Amount { get; set; }
        public string? Paidby { get; set; }
        public string? ParticipantId { get; set; }
        public string? ParticipantAmount { get; set; }
        public string? SplitBy { get; set; }
        public string? Share { get; set; }

    }
}
