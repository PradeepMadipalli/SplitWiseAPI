using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class GroupUserRequest
    {
        public string groupId { get; set; }
    }
    public class GroupUserIdRequest
    {
        public string? UserId { get; set; }
    }

}
