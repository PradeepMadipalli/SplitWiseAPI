using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? ReceiverId { get; set; }
        public string? CreatedId { get; set; }
        public string? CreatedBy { get; set; }
        public string? GroupId { get; set; }
        public string? Message { get; set; }
    }

    public class getActivity
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? ReceiverId { get; set; }
        public string? CreatedId { get; set; }
        public string? CreatedBy { get; set; }
        public string? GroupId { get; set; }
        public string? Message { get; set; }

    }
}
