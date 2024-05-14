using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.Models
{
    public class SplitUser : IdentityUser
    {
        public SplitUser()
        {
            Groups = new HashSet<UsersGroup>();
        }
        public ICollection<UsersGroup> Groups { get; set; }
    }
}
