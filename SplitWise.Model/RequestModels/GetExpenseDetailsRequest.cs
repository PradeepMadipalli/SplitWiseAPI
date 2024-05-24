using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class GetExpenseDetailsRequest
    {
        public string? groupId {  get; set; }
        public string? expId { get; set;}
    }
}
