using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Model.RequestModels
{
    public class GetTotalAmountByUserId
    {
        public string? UserId { get; set; }
        public string? expdId { get; set; }
        public string? expId { get; set;}
    }
}
