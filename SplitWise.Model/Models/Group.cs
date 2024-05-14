using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class Group
    {
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }

        public virtual ICollection<UsersGroup>? Groups { get; set; }
    }
}
