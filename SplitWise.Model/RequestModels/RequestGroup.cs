using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class RequestGroup
    {

        public string? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Userlist { get; set; }
        public string? UserId { get; set; }
        public string? Category { get; set; }
        public bool SimplifyDebts { get; set; }
        public string? Comments { get; set; }
    }
}

