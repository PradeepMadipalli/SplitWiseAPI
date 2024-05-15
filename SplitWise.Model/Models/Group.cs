using SplitWise.Model.RequestModels;
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

        public int Status { get; set; }
        public string? UserId { get; set; }
        public string? Category { get; set; }
        public string? SimplifyDebts { get; set; }
        public string? Comments { get; set; }

        public virtual ICollection<UsersGroup>? Groups { get; set; }
    }
    public class RealGroup
    {
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }
    }

    public class getEditGroup
    {
        public Guid? GroupId { get; set; }
        public string? GroupName { get; set; }

        public List<GetUsers> usersGroups { get; set; }

    }
    public class RequesteditGroup
    {
        public string? groupid { get; set; }
    }
    public class getuserGroup
    {
        public int Id { get; set; }
        public string? groupId { get; set; }
        public string? userId { get; set; }
    }
}
