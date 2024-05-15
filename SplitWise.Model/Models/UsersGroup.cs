using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SplitWise.Model.Models
{
    public class UsersGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public virtual SplitUser? User { get; set; }

        public Guid? GroupId { get; set; }

        public virtual Group? Group { get; set; }
        public int Status { get; set; }
    }
}
