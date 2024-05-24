using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid expId { get; set; }
        public string? groupId { get; set; }
        public string? Name { get; set; }
        public string? Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public string? GroupSelection { get; set; }
        public ICollection<ExpenseDetails>? Expensess { get; set; }
    
    }
}
