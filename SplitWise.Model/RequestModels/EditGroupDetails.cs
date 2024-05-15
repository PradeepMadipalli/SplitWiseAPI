using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class EditGroupDetails
    {
        public string? groupId { get; set; }
        public string? groupName { get; set; }
        public string? Category { get; set; }
        public string? SimplifyDebts { get; set; }
        public string? Comments { get; set; }
    }
}
