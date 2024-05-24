using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class SettleUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SettleId { get; set; }
        public string? PayerId { get; set; }
        public string? PayeeId { get; set; }
        public string? GroupId { get; set; }
        public string? Amount { get; set; }
        public string? CreaqtedBy { get; set; }
        public string? CreaqtedDate { get; set;}

        
    }
}
